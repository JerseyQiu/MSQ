using System;
using System.Activities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Variables
{
    /// <summary>
    /// This is a custom sequence activity which can only contain child activities which implement the IIQVarGroupActivity
    /// interface.  It is used to provide metadata used for placing IQ Variable Group activities onto tabs.
    /// </summary>
    [IQVarTabGroupActivity_DisplayName]
    [Utilities_Category]
    [Designer(typeof(IQVarTabGroupActivityDesigner))]
    [ContentProperty("Activities")]
    [MosaiqActivity]
    [Variables_ActivityGroup]
    public sealed class IQVarTabGroupActivity : MosaiqNativeActivity, IIQVarTabGroupActivity
    {
        #region Constructor

        /// <summary> Default Ctor</summary>
        public IQVarTabGroupActivity()
        {
            Activities = new Collection<Activity>();
            _onChildComplete = new CompletionCallback(InternalExecute);
        }

        #endregion

        #region Private Fields

        private readonly Variable<int> _activityIndex = new Variable<int>("ActivityIndex", 0);
        private readonly CompletionCallback _onChildComplete;

        #endregion

        #region Public Properties

        /// <summary> The child activities which this activity will execute in sequence </summary>
        [Browsable(false)]
        public Collection<Activity> Activities { get; set; }

        /// <summary>
        /// The order in which activities will be displayed in the IQ Script Arguments UI
        /// </summary>
        [RequiredArgument]
        [ConfigurationCategory]
        [IQVarGroupActivity_DisplayOrder_Description]
        [IQVarGroupActivity_DisplayOrder_DisplayName]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// New implementation just so we can add a description that matches the usage of Display Name for this 
        /// activity class.
        /// </summary>
        [IQVarTabGroupActivity_DisplayName_Description]
        public new string DisplayName
        {
            get { return base.DisplayName; }
            set { base.DisplayName = value; }
        }

        /// <summary> Returns all activities as group activities. </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<IIQVarGroupActivity> GroupActivities
        {
            get
            {
                return Activities
                    .OfType<IIQVarGroupActivity>()
                    .OrderBy(e => e.DisplayOrder)
                    .ThenBy(e => e.DisplayName)
                    .ToList();
            }
        }


        #endregion

        #region Overriden Methods
        /// <summary>
        /// Validation and implementation support.
        /// </summary>
        /// <param name="metadata"></param>
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

            metadata.SetChildrenCollection(Activities);
            metadata.AddImplementationVariable(_activityIndex);

            if (Activities.Count <= 0)
                metadata.AddValidationError(Strings.IQVarTabGroupActivity_MustHaveActivity);

            if (Activities.Any(act => !(act is IIQVarGroupActivity)))
                metadata.AddValidationError(Strings.IQVarTabGroupActivity_OnlyIQTabVariableActivitiesAllowed);
        }

        /// <summary>
        /// Execute implementation
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(NativeActivityContext context)
        {
            if ((Activities != null) && (Activities.Count > 0))
            {
                Activity activity = Activities[0];
                context.ScheduleActivity(activity, _onChildComplete);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Defines the callback method for invoking the next child activity when the current one completes.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="completedInstance"></param>
        private void InternalExecute(NativeActivityContext context, ActivityInstance completedInstance)
        {
            int index = _activityIndex.Get(context);
            if ((index >= Activities.Count) || (Activities[index] != completedInstance.Activity))
            {
                index = Activities.IndexOf(completedInstance.Activity);
            }
            int num2 = index + 1;
            if (num2 != Activities.Count)
            {
                Activity activity = Activities[num2];
                context.ScheduleActivity(activity, _onChildComplete);
                _activityIndex.Set(context, num2);
            }
        }

        #endregion

        #region IXlateObject Implementation
        /// <summary>
        /// Method to extract all translatable strings from an object.
        /// </summary>
        /// <returns></returns>
        public List<string> GetXlateStrings()
        {
            return new List<String> {DisplayName};
        }

        /// <summary>
        /// Method to set all translatable strings on an object.  The "key" is the original string
        /// value and the "value" is the translated string value.
        /// </summary>
        /// <param name="xlateStrings"></param>
        public void SetXlateStrings(IDictionary<string, string> xlateStrings)
        {
            string xlateDisplayName;
            if (xlateStrings.TryGetValue(DisplayName, out xlateDisplayName))
            {
                if (!String.IsNullOrEmpty(xlateDisplayName))
                    DisplayName = xlateDisplayName;
            }
        }

        #endregion
    }
}
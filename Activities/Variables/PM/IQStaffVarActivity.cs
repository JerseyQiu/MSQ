using System;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Core.Globals;
using Impac.Mosaiq.Core.Security.SecurityLib;
using Impac.Mosaiq.IQ.Activities.Variables.PM.Details;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;
using Impac.Mosaiq.UI.Framework;
using Impac.Mosaiq.UI.InputTemplates.DataModelClasses;
using Impac.Mosaiq.UI.InputTemplates.Enumerations;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM
{
    #region Activity Class
    /// <summary>
    /// This activity allows the user to select a staff in the UI and outputs the primary key to
    /// the output parameter.
    /// </summary>
    [IQStaffVarActivity_DisplayName]
    [PracticeManagement_Category]
    [Variables_ActivityGroup]
    public sealed class IQStaffVarActivity : IQVariableActivityEntity<int, Staff, IQIntegerVar, IQStaffVarDetail, IQStaffVarConfig>
    {
        
    }
    #endregion

    #region Configuration Class
    /// <summary> Configuration class for the IQVarStaffActivity </summary>
    [Serializable]
    public class IQStaffVarConfig : IQVariableConfigEntity<int, Staff, IQIntegerVar, IQStaffVarDetail>
    {
        /// <summary>
        /// Displays the staff browse
        /// </summary>
        /// <returns></returns>
        protected override IQOpResult<int> OpenEditor(int defaultValue, IQVarElementTarget target)
        {
            var wc = new WidgetContract(NTStrings.IQStaffVarActivity_StaffBrowseWidget_ClassName,
                                        NTStrings.IQStaffVarActivity_StaffBrowseWidget_InstanceName,
                                        WidgetContract.RequestOptions.DialogResult);

            // If the current value is 0 do not pass it into the control since we internalize 0 as not valid
            if (defaultValue != 0)
            {
                wc.CustomData.Add(Mosaiq.Core.Defs.Constants.StringConstants.KEY_SELECTEDID, defaultValue);
            }

            var staffProperties = new StaffProperties
                                      {
                                          HideFavorites = true,
                                          HideInactive = true,
                                          ShowLocations = StaffLocationBrowseMode.Staff,
                                          IncludeAllDepartments = true,
                                          NoEdit = true
                                      };

            wc.CustomData.Add(NTStrings.IQStaffVarActivity_StaffBrowseWidget_Properties, staffProperties);

            wc.SecEnums = SecurityEnums.EC_GEN;
            wc.WidgetTitle = Strings.IQStaffVarActivity_WidgetTitle;

            WidgetManager.AddWidgetModule(NTStrings.IQStaffVarActivity_StaffBrowseWidget_AssemblyName);
            WidgetManager.ActivateWidget(null, wc);

            // Open the browse as a modal window
            wc.ActiveWidget.ShowDialog();

            //If the user hit "close", don't update the primaryKeyValue
            return wc.Result == WidgetContract.ResultOptions.Completed
                       ? new IQOpResult<int>
                             {
                                 Result = OpResultEnum.Completed,
                                 Value = (int) wc.CustomData[Mosaiq.Core.Defs.Constants.StringConstants.KEY_SELECTEDID]
                             }
                       : new IQOpResult<int> {Result = OpResultEnum.Cancelled};
        }

        /// <summary> Overriden to enable the Current state. </summary>
        public override bool SupportsCurrent
        {
            get { return true; }
        }

        /// <summary> 
        /// Override of the CurrentValueText property, so that the user selected "Active User" instead of "Current"
        /// </summary>
        public override string CurrentValueText { get { return Strings.IQStaffVarActivity_ActiveUser; } }

        /// <summary>
        /// Returns the "current" value of the object at script execution time.  Only used for activities that support
        /// the "current" state. 
        /// </summary>
        /// <returns></returns>
        protected override int GetCurrentValue()
        {
            return Global.StaffId;
        }
    }
    #endregion
}
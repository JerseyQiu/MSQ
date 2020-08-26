using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Core.Toolbox.Ropes;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Details;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Formatter;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
    #region Activity Classs
    /// <summary>
    /// This activity allows the user to select an observation item (data item or table item) in the UI and 
    /// outputs the GUID key to the output parameter.
    /// </summary>
    [IQDataItemVarActivity_DisplayName]
    [GeneralCharting_Category]
    [Variables_ActivityGroup]
    public sealed class IQDataItemVarActivity : IQVariableActivityEntity<Guid, ObsDef, IQGuidVar, IQObsDefLabelVarDetail, IQDataItemVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> Configuration class for the IQVarDataItemActivity class</summary>
    [Serializable]
    public class IQDataItemVarConfig : IQVariableConfigEntity<Guid, ObsDef, IQGuidVar, IQObsDefLabelVarDetail>
    {
        #region Constructors
        /// <summary> Default Contructor </summary>
        public IQDataItemVarConfig()
        {
            AllowedDataItemTypes = new List<ObsDefDataFormat>();
            AllowedDataItemTypes.AddRange(GetDefaultDataItemTypes());
            AllowTableTitles = true;
        }
        #endregion

        #region Public Properties (Serializable)
        /// <summary> Gets and sets the data item types allowed for this activity. </summary>
        [RestoreInclude]
        [ConfigurationActivity_Category]
        [IQDataItemVarActivity_AllowedDataItemTypes_DisplayName]
        public List<ObsDefDataFormat> AllowedDataItemTypes { get; set; }

        /// <summary> Gets and sets whether table titles are allowed. </summary>
        [RestoreInclude]
        [ConfigurationActivity_Category]
        [IQDataItemVarActivity_AllowTableTitles_DisplayName]
        public bool AllowTableTitles { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Opens the data item browse
        /// </summary>
        /// <returns></returns>
        protected override IQOpResult<Guid> OpenEditor(Guid defaultValue, IQVarElementTarget target)
        {
            OnBeforeClarionEditorInvoke(this);
            int obdId = CallClarion.GetObsDefDataItem(0);
            OnAfterClarionEditorInvoke(this);

            //If the user hit "close", don't update the primaryKeyValue
            return (obdId != 0)
                       ? new IQOpResult<Guid> {Result = OpResultEnum.Completed, Value = ObsDef.GetEntityById(obdId).OBD_GUID}
                       : new IQOpResult<Guid> {Result = OpResultEnum.Cancelled};
        }

        /// <summary>
        ///  Overriden method for looking up details by ID (since we're not using the primary key of obsdef)
        ///  </summary>
        /// <param name="ids"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        protected override IDictionary<Guid, ObsDef> GetEntitiesDictFromIds(IEnumerable<Guid> ids, ImpacPersistenceManager pm)
        {
            var query = new ImpacRdbQuery(typeof(ObsDef));
            query.AddClause(ObsDefDataRow.OBD_GUIDEntityColumn, EntityQueryOp.In, ids.ToList());
            return pm.GetEntities<ObsDef>(query, QueryStrategy.Normal).ToDictionary(e => e.OBD_GUID, e => e);
        }

        /// <summary> Adds the property editor for selecting the AvailableItemTypes </summary>
        public override RepositoryItem GetPropertyEditor(string propertyName)
        {
            if (propertyName == "AllowedDataItemTypes")
            {
                var formatter = new FlowsheetItemTypeFormatter { Config = this };
                var flowsheetItemTypeSelector = new RepositoryItemButtonEdit();
                flowsheetItemTypeSelector.ButtonClick += flowsheetItemTypeSelector_ButtonClick;
                flowsheetItemTypeSelector.DoubleClick += flowsheetItemTypeSelector_DoubleClick;
                flowsheetItemTypeSelector.DisplayFormat.Format = formatter;
                flowsheetItemTypeSelector.DisplayFormat.FormatType = FormatType.Custom;
                flowsheetItemTypeSelector.EditFormat.Format = formatter;
                flowsheetItemTypeSelector.EditFormat.FormatType = FormatType.Custom;
                flowsheetItemTypeSelector.TextEditStyle = TextEditStyles.DisableTextEditor;
                return flowsheetItemTypeSelector;
            }

            return base.GetPropertyEditor(propertyName);
        }

        /// <summary> Adding validation logic to ensure selected item is of one of the supported types. </summary>
        /// <param name="errors"></param>
        /// <param name="value"></param>
        /// <param name="valueType"></param>
        protected override void ValidateValue(IList<ValidationError> errors, Guid value, IQVarElementTarget valueType)
        {
            ValidateDataItems(value, errors);
            ValidateTableTitle(value, errors);
            base.ValidateValue(errors, value, valueType);
        }
        #endregion

        #region Event Handlers
        private void flowsheetItemTypeSelector_DoubleClick(object sender, EventArgs e)
        {
            InvokeFlowsheetItemTypeSelectorForm();
        }

        private void flowsheetItemTypeSelector_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            InvokeFlowsheetItemTypeSelectorForm();
        }
        #endregion

        #region Private Methods
        private void ValidateTableTitle(Guid value, IList<ValidationError> errors)
        {
            ObsDef def = ObsDef.GetEntityByObdGuid(value);

            //No validation necessary if it's not a table title or if it is a table title and table titles are allowed.
            if (def.ObsDefDataFormat != ObsDefDataFormat.Choice || AllowTableTitles)
                return;

            //Otherwise, add error.
            string errorMsg = Rope.Format(Strings.IQDataItemVarActivity_TableTitlesNotAllowed, def.Label);
            errors.Add(new ValidationError(errorMsg, false, "Config"));
        }

        private void ValidateDataItems(Guid value, IList<ValidationError> errors)
        {
            ObsDef def = ObsDef.GetEntityByObdGuid(value);

            //Only performs validation if it's a data item.
            if (def.ObsDefDataFormat == ObsDefDataFormat.Choice)
                return;

            //If the data item type is in the list of allowed data item types, return.
            if (AllowedDataItemTypes.Contains(def.ObsDefDataFormat))
                return;

            //Otherwise, add error.
            IEnumerable<string> supportedTypes = AllowedDataItemTypes.Select(ConvertToString);
            string supportedTypeString = String.Join(", ", supportedTypes);

            string errorMsg = AllowedDataItemTypes.Count > 0
                                  ? Rope.Format(Strings.IQDataItemVarActivity_DataItemTypeNotAllowed, def.Label,
                                                supportedTypeString)
                                  : Rope.Format(Strings.IQDataItemVarActivity_DataItemsNotAllowed,
                                                def.Label);

            errors.Add(new ValidationError(errorMsg, false, "Config"));
        }

        private void InvokeFlowsheetItemTypeSelectorForm()
        {
            var dlg = new FlowsheetItemTypeSelector { SelectedValues = AllowedDataItemTypes };
            Backup();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Commit();
                OnRefreshRequested();
            }
            else
            {
                Rollback();
            }
        }
        #endregion

        #region Static Methods
        internal static string ConvertToString(ObsDefDataFormat format)
        {
            switch (format)
            {
                case ObsDefDataFormat.CheckBox:
                    return Strings.ObsDefType_CheckBox;

                case ObsDefDataFormat.Choice:
                    return Strings.ObsDefType_Choice;

                case ObsDefDataFormat.Date:
                    return Strings.ObsDefType_Date;

                case ObsDefDataFormat.Numeric:
                    return Strings.ObsDefType_Numeric;

                case ObsDefDataFormat.String:
                    return Strings.ObsDefType_String;

                case ObsDefDataFormat.Time:
                    return Strings.ObsDefType_Time;

                default:
                    throw new InvalidOperationException(format + "is not supported");
            }
        }

        private static IEnumerable<ObsDefDataFormat> GetDefaultDataItemTypes()
        {
            yield return ObsDefDataFormat.CheckBox;
            yield return ObsDefDataFormat.Date;
            yield return ObsDefDataFormat.Numeric;
            yield return ObsDefDataFormat.String;
            yield return ObsDefDataFormat.Time;
        }
        #endregion
    }
    #endregion
}
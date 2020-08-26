using System;
using System.Collections.Generic;
using System.Linq;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Core.Security.SecurityLib;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Details;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;
using Impac.Mosaiq.UI.Framework;
using Impac.Mosaiq.Widgets.General.CarePlanWidgets;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
    #region Activity Classs
    /// <summary>
    /// This activity allows the user to select an observation item (data item or table item) in the UI and 
    /// outputs the GUID key to the output parameter.
    /// </summary>
    [IQCareplanVarActivity_DisplayName]
    [GeneralCharting_Category]
    [Variables_ActivityGroup]
    public sealed class IQCareplanVarActivity : IQVariableActivityEntity<int, CPlan, IQIntegerVar, IQCareplanVarDetail, IQCareplanVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> Configuration class for the IQVarDataItemActivity class</summary>
    [Serializable]
    public class IQCareplanVarConfig : IQVariableConfigEntity<int, CPlan, IQIntegerVar, IQCareplanVarDetail>
    {
        /// <summary>
        /// Opens the data item browse
        /// </summary>
        /// <returns></returns>
        protected override IQOpResult<int> OpenEditor(int currentValue, IQVarElementTarget target)
        {
            WidgetContract wc = new WidgetContract(NTStrings.IQCareplanVarActivity_CarePlanBrowseWidget_ClassName,
                                                   NTStrings.IQCareplanVarActivity_CarePlanBrowseWidget_InstanceName,
                                                   WidgetContract.RequestOptions.DialogResult);

            // If the current value is 0 do not pass it into the control since we internalize 0 as not valid
            if (currentValue != 0)
            {
                wc.CustomData.Add(CarePlanBrowseWidget.CustomDataKeys.SELECTED_SET_ID, currentValue.ToString());
            }

            wc.SecEnums = SecurityEnums.EC_GEN;
            wc.WidgetTitle = Strings.IQCareplanVarActivity_WidgetTitle;

            WidgetManager.AddWidgetModule(NTStrings.IQCareplanVarActivity_CarePlanBroweseWidget_AssemblyName);
            WidgetManager.ActivateWidget(null, wc);

            // Open the browse as a modal window
            wc.ActiveWidget.ShowDialog();

            //If the user hit "close", don't update the primaryKeyValue
            return wc.Result == WidgetContract.ResultOptions.Completed
                       ? new IQOpResult<int>
                             {
                                 Result = OpResultEnum.Completed,
                                 Value = (int) wc.CustomData[CarePlanBrowseWidget.CustomDataKeys.SELECTED_SET_ID]
                             }
                       : new IQOpResult<int> {Result = OpResultEnum.Cancelled};
        }

        /// <summary>
        ///  Overriden method for looking up details by ID (since we're not using the primary key of obsdef)
        ///  </summary>
        /// <param name="ids"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        protected override IDictionary<int, CPlan> GetEntitiesDictFromIds(IEnumerable<int> ids, ImpacPersistenceManager pm)
        {
            var query = new ImpacRdbQuery(typeof(CPlan));
            //Defect 12161, Change logic to use CPL_SET_ID rather than CPL_ID to trigger IQ
            query.AddClause(CPlanDataRow.CPL_Set_IDEntityColumn, EntityQueryOp.In, ids.ToList());
            query.AddClause(CPlanDataRow.VersionEntityColumn, EntityQueryOp.EQ, 0);
            return pm.GetEntities<CPlan>(query, QueryStrategy.Normal).ToDictionary(e =>(int) e.CPL_Set_ID, e => e);
        }

        /// <summary> Display a careplan's category </summary>
        public override bool ShowCategories
        {
            get { return true; }
        }
    }
    #endregion
}
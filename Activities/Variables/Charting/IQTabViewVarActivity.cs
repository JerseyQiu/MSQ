using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Core.Defs.Enumerations;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Details;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;
using Impac.Mosaiq.Widgets.General.ObsDefConfig;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
    #region Activity Class
    /// <summary>
    /// This activity allows the user to select an observation tab view in the UI and outputs the 
    /// GUID key to the output parameter.
    /// </summary>
    [IQVarTabViewActivity_DisplayName]
    [GeneralCharting_Category]
    [Variables_ActivityGroup]
    public sealed class IQTabViewVarActivity : IQVariableActivityEntity<Guid, ObsDef, IQGuidVar, IQObsDefLabelVarDetail, IQTabViewVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> Configuration class for the IQVarTabViewActivity </summary>
    [Serializable]
    public class IQTabViewVarConfig : IQVariableConfigEntity<Guid, ObsDef, IQGuidVar, IQObsDefLabelVarDetail>
    {
        /// <summary> Displays the tab view selection dialog </summary>
        /// <returns></returns>
        protected override IQOpResult<Guid> OpenEditor(Guid defaultValue, IQVarElementTarget target)
        {
            int obdId = GetObsDefView();

            //If the user hit "close", don't update the primaryKeyValue
            return (obdId != 0)
                       ? new IQOpResult<Guid> {Result = OpResultEnum.Completed, Value = ObsDef.GetEntityById(obdId).OBD_GUID}
                       : new IQOpResult<Guid> {Result = OpResultEnum.Cancelled};
        }

        /// <summary> Overriden details lookup since we're not using the primary key of ObsDef. </summary>
        /// <param name="ids"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        protected override IDictionary<Guid, ObsDef> GetEntitiesDictFromIds(IEnumerable<Guid> ids, ImpacPersistenceManager pm)
        {
            var query = new ImpacRdbQuery(typeof(ObsDef));
            query.AddClause(ObsDefDataRow.OBD_GUIDEntityColumn, EntityQueryOp.In, ids.ToList());
            return pm.GetEntities<ObsDef>(query, QueryStrategy.Normal).ToDictionary(e => e.OBD_GUID, e => e);
        }

        private int GetObsDefView()
        {
            int result = 0;
            ObsDefSelect dialog = new ObsDefSelect();
            dialog.SelectionMode(MedDefs.ObdType.View);

            if (dialog.ShowDialog() == DialogResult.OK)
                result = dialog.SelectedObdId;

            return result;
        }
    }
    #endregion
}
using System;
using System.Collections.Generic;
using System.Linq;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Details;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
    #region Activity Classs
    /// <summary>
    /// This activity allows the user to select a table item in the UI and outputs the GUID key to the output 
    /// parameter.
    /// </summary>
    [IQTableItemVarActivity_DisplayName]
    [GeneralCharting_Category]
    [Variables_ActivityGroup]
    public sealed class IQTableItemVarActivity : IQVariableActivityEntity<Guid, ObsDef, IQGuidVar, IQObsDefTableItemVarDetail, IQTableItemVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> Configuration class for the IQTableItemVarActivity class</summary>
    [Serializable]
    public class IQTableItemVarConfig : IQVariableConfigEntity<Guid, ObsDef, IQGuidVar, IQObsDefTableItemVarDetail>
    {
        #region Overriden Methods
        /// <summary>
        /// Opens the data item browse
        /// </summary>
        /// <returns></returns>
        protected override IQOpResult<Guid> OpenEditor(Guid defaultValue, IQVarElementTarget target)
        {
            OnBeforeClarionEditorInvoke(this);
            int obdId = CallClarion.GetObsDefTableItem(0);
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
        #endregion
    }
    #endregion
}
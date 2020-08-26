using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Core.Toolbox.Ropes;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Details
{
    /// <summary>
    /// IQProcedureVarDetail implementation for procedures and supplies objects.
    /// </summary>
    public class IQDrugVarDetail : IQVariableDetail<Drug, IQVarElement<int>>
    {
        #region IIQVariableDetail Implementation
        /// <summary>
        /// Displaying the observation order short string as the selected item.
        /// </summary>
        public override string DesignTimeDisplayString
        {
            get
            {
                if (Data.ExternalDrugInd)
                    return Data.Description;
                else
                    return Rope.Format("[{0:GenericName}] {1:Description} {2:Route} {3:Form} {4:Strength}", Data.Generic_Name.Trim(), Data.Drug_Label.Trim(), Data.RouteDesc, Data.DoseFormDesc, Data.StrengthDesc);
            }
        }

        /// <summary>
        /// The primary key of the CPT table
        /// </summary>
        public override string DesignTimeKeyString
        {
            get { return Data.DRG_ID.ToString(); }
        }

        /// <summary> Category Value </summary>
        public override string Category
        {
            get { return Data.ObsDefEntityOnDrugType.Label; }
        }
        #endregion
    }
}

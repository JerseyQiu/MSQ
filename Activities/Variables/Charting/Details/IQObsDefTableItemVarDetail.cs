using System;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Core.Toolbox.Ropes;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting.Details
{
    /// <summary>
    /// IQVarDetail implementation for TableItems.  This class has special display requirements as TableItems must display both the
    /// label and the description of the Table Item, as well as the label of the related table item (otherwise, how would one know the
    /// difference between the "No" answer used for so many assessments).
    /// </summary>
    public class IQObsDefTableItemVarDetail : IQVariableDetail<ObsDef, IQVarElement<Guid>>
    {
        #region IIQVariableDetail Implementation
        /// <summary>
        /// The human readable display string for a design time value (ie. something looked up from the database from
        /// a stored primary key or a value entered directly from the user.
        /// </summary>
        public override string DesignTimeDisplayString
        {
            get
            {
                return Rope.Format("{0:FullDesc} ({1:TableTitle})", Data.FullDescription.Trim(), Data.GetTableTitleEntity().LabelInactiveIndicator);
            }
        }

        /// <summary>
        /// The a string displaying the key for a design time value.  This could be the primary key of a selected value
        /// something typed in by the user, such as a number or string.
        /// </summary>
        public override string DesignTimeKeyString
        {
            get { return Data.OBD_GUID.ToString(); }
        }
        #endregion
    }
}

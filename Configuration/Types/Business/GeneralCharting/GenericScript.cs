using System;
using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Types.DecisionSupport
{
    /// <summary>
    ///  Defines a script type with no arguments which can be launched from any decision support feature.
    /// </summary>
    public class GenericScript : ScriptTypeBase
    {
        #region Overrides of ScriptConfigBase

        /// <summary> The unique identifier for this script type. </summary>
        public override Guid Guid
        {
            get { return ScriptTypeGuids.GenericScript; }
        }

        /// <summary> The display name of this script type </summary>
        public override string Name
        {
            get { return Strings.GenericScript_Name; }
        }

        /// <summary> The display description of this script type </summary>
        public override string Description
        {
            get { return Strings.GenericScript_Description; }
        }

        /// <summary> The technical usage usage of this script type</summary>
        public override string Usage
        {
            get { return Strings.Usage_DecisionSupport; }
        }

        /// <summary> The functional area of MOSAIQ which this script type applies to </summary>
        public override string Category
        {
            get { return Strings.Category_Common; }
        }

        /// <summary> The functional sub are of MOSAIQ which this script type applies to </summary>
        public override string SubCategory
        {
            get { return String.Empty; }
        }
        #endregion
    }
}

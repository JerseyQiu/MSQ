﻿using System;
using System.Collections.Generic;
using Impac.Mosaiq.IQ.Common.Configuration;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration;
using Impac.Mosaiq.IQ.Core.Framework.Configuration;

namespace Impac.Mosaiq.IQ.Configuration.Features.Business.GeneralCharting.Charge
{
    class ChgCodeCaptured : BusinessScriptFeature
    {
        #region Overriden Properties

        /// <summary> The unique identifier for this feature. </summary>
        public override Guid Guid
        {
            get { return ScriptFeatureGuids.ChgCodeCaptured; }
        }

        /// <summary> The display name of this feature </summary>
        public override string Name
        {
            get { return Strings.ChgCodeCaptured_Name; }
        }

        /// <summary> The display description of this feature </summary>
        public override string Description
        {
            get { return Strings.ChgCodeCaptured_Decription; }
        }

        /// <summary> The functional area of MOSAIQ which this feature applies to </summary>
        public override string Domain
        {
            get { return Strings.Category_GeneralCharting; }
        }

        /// <summary> The functional sub area of MOSAIQ which this feature applies to </summary>
        public override string FunctionalArea
        {
            get { return Strings.SubCategory_Charge; }
        }

        /// <summary> The script assignment mode which this feature uses </summary>
        public override ScriptAssignMode ScriptAssignMode
        {
            get { return ScriptAssignMode.Sequential; }
        }

        /// <summary>
        /// Returns the GUID's of the script types which are supported by this Feature.
        /// </summary>
        /// <returns></returns>
        protected override void GetSupportedScriptTypeGuids(IList<Guid> scriptTypeGuids)
        {
            base.GetSupportedScriptTypeGuids(scriptTypeGuids);
            scriptTypeGuids.Add(ScriptTypeGuids.ChargeScript);
        }
        #endregion
    }
}



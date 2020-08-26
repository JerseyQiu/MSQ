using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Impac.Mosaiq.IQ.Configuration.Providers.Base;
using Impac.Mosaiq.IQ.Definitions.Interfaces;

namespace Impac.Mosaiq.IQ.Configuration.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class DemoProvider : ScriptProviderBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IScriptType> GetScriptTypes()
        {
            var types = new List<IScriptType>();

            types.Add(new Impac.Mosaiq.IQ.Configuration.Types.Application.EscribeScriptType());
            types.Add(new Impac.Mosaiq.IQ.Configuration.Types.Application.SmartFormScriptType());
            types.Add(new Impac.Mosaiq.IQ.Configuration.Types.Rule.PharmOrderApproveScript());
            types.Add(new Impac.Mosaiq.IQ.Configuration.Types.Rule.QuickOrderGenerateScript());

            return types;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IScriptFeature> GetScriptFeatures()
        {
            var features = new List<IScriptFeature>();

            features.Add(new Impac.Mosaiq.IQ.Configuration.Features.PatientCarePlanForm());
            features.Add(new Impac.Mosaiq.IQ.Configuration.Features.PharmacyOrderApproval());
            features.Add(new Impac.Mosaiq.IQ.Configuration.Features.QuickOrderGenerate());

            return features;
        }
    }
}

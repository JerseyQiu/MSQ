using System.Activities;
using Impac.Mosaiq.BOM.Entities.Defs;
using Impac.Mosaiq.Charting.CommonUtils;
using Impac.Mosaiq.Core.Defs.Enumerations;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.General.Flowsheet
{
    /// <summary> Gets the patient's current BSA. </summary>
    [GetCurrentBSAActivity_DisplayName]
    [Flowsheet_Category]
    [GeneralCharting_ActivityGroup]
    public class GetCurrentBSAActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The primary key of the patient who's BSA we're looking up.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [GetCurrentBSAActivity_PatId1_DisplayName]
        [GetCurrentBSAActivity_PatId1_Description]
        public InArgument<int> PatId1 { get; set; }

        /// <summary>
        /// Determines whether to look at pharmacy orders when determining the "current" BSA.
        /// </summary>
        [InputParameterCategory]
        [GetCurrentBSAActivity_IncludeOrders_DisplayName]
        [GetCurrentBSAActivity_IncludeOrders_Description]
        public InArgument<bool> IncludeOrders { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary> The BSA Value returned </summary>
        [OutputParameterCategory]
        [GetCurrentBSAActivity_BSA_DisplayName]
        [GetCurrentBSAActivity_BSA_Description]
        public OutArgument<double> BSA { get; set; }
        #endregion

        #region Overrides of MosaiqCodeActivity
        /// <summary>
        /// Get the current BSA for the patient passed in.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            int obxId = 0;
            int patId1 = PatId1.Get(context);
            bool includeOrders = IncludeOrders != null ? IncludeOrders.Get(context): false;

            DoseCalcSourceTypeEnum bsaSourceType;
            int bsaSource;

            BSA.Set(context,
                    includeOrders
                        ? BSAUtils.FindExistingBSAForDoseCalc(patId1, MedDefs.OrderingDoseCalcFormulas.BsaFormulasAndManual, out bsaSourceType, out bsaSource)
                        : BSAUtils.GetMostRecentBSA(patId1, MedDefs.OrderingDoseCalcFormulas.BsaFormulasAndManual, ref obxId));
        }
        #endregion
    }
}

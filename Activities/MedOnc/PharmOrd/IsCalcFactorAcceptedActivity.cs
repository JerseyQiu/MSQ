using System;
using System.Linq;
using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.MedOnc.PharmOrd
{
    /// <summary>
    /// Checks whether a calculation factor for this drug has previously been accepted.
    /// </summary>
    [IsCalcFactorAcceptedActivity_DisplayName]
    [PharmOrd_Category]
    [MedOnc_ActivityGroup]
    public class IsCalcFactorAcceptedActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary> The Rxo_Set_Id of the pharmacy order to check. </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [IsCalcFactorAcceptedActivity_RxoSetId_DisplayName]
        [IsCalcFactorAcceptedActivity_RxoSetId_Description]
        public InArgument<int> RxoSetId { get; set; }

        /// <summary> The accepted calculation factor. </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [IsCalcFactorAcceptedActivity_CalcFactor_DisplayName]
        [IsCalcFactorAcceptedActivity_CalcFactor_Description]
        public InArgument<Double> CalcFactor { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary> Returns whether the calculation factor has been previously accepted </summary>
        [OutputParameterCategory]
        [IsCalcFactorAcceptedActivity_Result_Description]
        [IsCalcFactorAcceptedActivity_Result_DisplayName]
        public OutArgument<bool> Result { get; set; }
        #endregion

        #region Overrides of MosaiqCodeActivity
        /// <summary>
        /// Checks the database to see if the PharmCalcAccept table contains a record indicating that the
        /// calculation factor passed has been accepted..
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            ImpacPersistenceManager pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
            int rxoSetId = RxoSetId.Get(context);
            double calcFactor = CalcFactor.Get(context);

            //Lookup all of the PharmCalcAccept records to get the minimum and maximum accepted values.
            EntityList<PharmCalcAccept> acceptRecords = PharmCalcAccept.FindByRxoSetId(pm, QueryStrategy.Normal, rxoSetId);
            double maxAccepted = acceptRecords.Where(e => e.Accepted_High.HasValue).Max(e => e.Accepted_High.Value);
            double minAccepted = acceptRecords.Where(e => e.Accepted_Low.HasValue).Min(e => e.Accepted_Low.Value);

            Result.Set(context, calcFactor >= minAccepted && calcFactor <= maxAccepted);
        }
        #endregion
    }
}

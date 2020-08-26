using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.MedOnc.PharmOrd
{
    /// <summary>Records an accepted calculation factor value in the PharmCalcAccept table </summary>
    [AcceptCalcFactorActivity_DisplayName]
    [PharmOrd_Category]
    [MedOnc_ActivityGroup]
    public class AcceptCalcFactorActivity : MosaiqCodeActivity
    {
        #region Properties (Input Parameters)
        /// <summary>
        /// The Rxo_Set_Id that points to the pharmacy order which the accepted value belongs to.
        /// </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [AcceptCalcFactorActivity_RxoSetId_DisplayName]
        [AcceptCalcFactorActivity_RxoSetId_Description]
        public InArgument<int> RxoSetId { get; set; }

        /// <summary> The accepted calculation factor. </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [AcceptCalcFactorActivity_AcceptedCalcFactor_DisplayName]
        [AcceptCalcFactorActivity_AcceptedCalcFactor_Description]
        public InArgument<Double> AcceptedCalcFactor { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Writes to the PharmCalcAccept table indicating that the calculation factor passed in has
        /// been accepted.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            ImpacPersistenceManager pm = ImpacPersistenceManagerFactory.CreatePersistenceManager();
            int rxoSetId = RxoSetId.Get(context);
            double calcFactor = AcceptedCalcFactor.Get(context);
            
            //Get PharmOrd Object
            BOM.Entities.PharmOrd pharmOrd = BOM.Entities.PharmOrd.GetEntityByRxoSetIdAndVersion0(rxoSetId);

            if (pharmOrd.IsNullEntity)
                throw new InvalidOperationException("Cannot find PharmOrd record with Version = 0 and Rxo_Set_Id = " + rxoSetId);

            //Only write to the table if a BSA is found.
            if (pharmOrd.BSA != 0)
            {
                PharmCalcAccept obj = PharmCalcAccept.Create(pm);
                obj.RXO_Set_ID = rxoSetId;

                //If the BSA on the order is equal to the value passed in, reject the change since there's no reason to save the record.
                if(calcFactor == pharmOrd.BSA)
                {
                    pm.RejectChanges();
                    return;
                }

                //Otherwise, if the acepted calc factor is greater than the order BSA, set the high value.  If it's less than the order
                //BSA, set the low value.  
                if (calcFactor > pharmOrd.BSA)
                    obj.Accepted_High = calcFactor;
                else 
                    obj.Accepted_Low = calcFactor;

                pm.SaveChanges();
            }
        }
        #endregion
    }
}
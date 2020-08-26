using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.Entities.Revisioning;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using IdeaBlade.Persistence;

// 
namespace Impac.Mosaiq.IQ.Activities.RadOnc.TreatmentPrescription
{
    /// <summary> Activity used to display an information message </summary>
    [ChangePrescribedFractionsActivity_DisplayName]
    [TreatmentPrescription_Category]
    [RadOnc_ActivityGroup]
    public class ChangePrescribedFractionsActivity : MosaiqCodeActivity
    {

        #region Properties (Input Parameters)
        /// <summary> Patient ID of current open patient as input </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [ChangePrescribedFractionsActivity_PatId_DisplayName]
        [ChangePrescribedFractionsActivity_PatId_Description]
        public InArgument<int> PatId { get; set; }

        /// <summary> SIT_ID of the Rad Rx to adjust as input </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [ChangePrescribedFractionsActivity_SIT_ID_DisplayName]
        [ChangePrescribedFractionsActivity_SIT_ID_Description]
        public InArgument<int> Sitid { get; set; }

        /// <summary> The amount of dose to add/subtract from given Rad Rx as input </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [ChangePrescribedFractionsActivity_DeltaFraction_DisplayName]
        [ChangePrescribedFractionsActivity_DeltaFraction_Description]
        public InArgument<int> DeltaFraction { get; set; }

        /// <summary>
        /// The persistence manager to use with the query. If not provided, a new persistence manager will be created.
        /// </summary>
        [InputParameterCategory]
        [ChangePrescribedFractionsActivity_PersistenceManager_DisplayName]
        [ChangePrescribedFractionsActivity_PersistenceManager_Description]
        public InArgument<ImpacPersistenceManager> PersistenceManager { get; set; }

        #endregion

        #region Properties (Output Parameters)
        /// <summary> The result of the activity </summary>
        [OutputParameterCategory]
        [ChangePrescribedFractionsActivity_FractionsChanged_DisplayName]
        [ChangePrescribedFractionsActivity_FractionsChanged_Description]
        public OutArgument<bool> FractionsChanged { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Activity heavy lifting.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {            
            // This processing is intended for the Plan-of-the-Day feature and does not necessarily support any other usage
            // The passed Site.SIT_ID must be for a version zero prescription record (aka the 'tip' or 'current' version)
            // Prescribed fraction dose must not be zero and must be uniform 
            // If fractionation details records exist for the prescription (Site_FractionDetails) then the passed deltaFraction must be negative
            // For Plan-of-the-Day the deltaFraction value is -1
            // The valid range of prescribed fractions after the adjustment is 0 to 100 inclusive

            // Activity inputs
            int patId = PatId.Get(context);
            int sitId = Sitid.Get(context);
            int deltaFraction = DeltaFraction.Get(context);
            ImpacPersistenceManager pm = PersistenceManager.Expression != null
                                             ? PersistenceManager.Get(context)
                                             : PM;

            // Activity output
            FractionsChanged.Set(context, false); // Initialize the Activity output to False

            // Don't do anything if deltaFraction is zero i.e., no adjustment requested
            if (deltaFraction == 0)
            {
                return;
            }

            // Get the Rad Rx to be updated
            PrescriptionSite site = PrescriptionSite.GetEntityByID(sitId, pm);

            // Determine whether the request is valid for this prescription

            // If the Site record no longer exists or the record's version is no longer zero (because a version roll has occurred) 
            // then do not proceed with updates to this prescription
            if (site.IsNullEntity || site.Version != 0)
            {
                return;
            }

            // If the prescribed fraction dose is zero or non-uniform then do not proceed with updates to this prescription
            // (Dose_Tx is zero when fraction dose is non-uniform; check for negative dose is excessive but doesn't hurt)
            if (site.Dose_Tx <= 0)
            {
                return;
            }

            // If the adjusted number of fractions won't be valid (must be between 0 and 100 inclusive)
            // then do not proceed with updates to this prescription
            int newFractions = site.Fractions + deltaFraction;
            if ((newFractions < 0) || (newFractions > 100))
            {
                return;
            }

            // Get the fraction and wave detail records (if any) for this prescription
            if (site.FxType == 2)
            {
                // When there are fraction detail records, FxType is 2                              
                EntityList<SiteFractionDetails> siteFractionDetails = SiteFractionDetails.GetSiteFractionDetailsEntitiesBySitId(sitId, pm, QueryStrategy.DataSourceOnly);

                // There can be wave detail records only when there are fraction detail records
                if (siteFractionDetails.Count > 0)
                {
                    EntityList<SiteWaveDetails> siteWaveDetails = SiteWaveDetails.GetSiteWaveDetailsEntitiesBySitId(sitId, pm, QueryStrategy.DataSourceOnly);
                }

                // If there are fraction and/or wave detail records and deltaFraction > 0
                // then do NOT proceed because inserting fraction and/or wave detail records is NOT supported here
                if (deltaFraction > 0 && siteFractionDetails.Count > 0)
                {
                    return;
                }
            }

            // The request is valid for this prescription so make the adjustments

            // Prepare for edit, including version roll if necessary
            var revisionedSite = (PrescriptionSite)site.PrepareEntityForEdit("PlanOfTheDay", true);            

            // Adjust prescribed total dose and prescribed number of fractions 
            revisionedSite.Dose_Ttl = revisionedSite.Dose_Tx * newFractions;
            revisionedSite.Fractions = (short)newFractions;

            // If the prescription is approved, maintain the approval status and approving staff 
            // but set the approval date/time stamp to now
            if (revisionedSite.Sanct_Id.HasValue)
            {
                revisionedSite.Sanct_DtTm = DateTime.Now;
            }

            if (revisionedSite.FxType == 2)
            {
                // Manage the fraction details, fraction specific Notes and wave details records (if any)
                RemoveExcessSiteFractionDetailsAndNotes(pm, revisionedSite);
                RemoveUnreferencedSiteWaveDetails(pm, revisionedSite);

                // If fractions were reduced to zero 
                // then there will be no details records when the update is complete so change FxType to 0 (no details records)
                if (revisionedSite.Fractions == 0)
                {
                    revisionedSite.FxType = 0;
                }
            }

            try
            {
                pm.SaveChanges();
            }
            catch 
            {
                return;
            }

            FractionsChanged.Set(context, true);
        }

        #region private methods

        /// <summary>
        /// Remove from the persistence manager or mark for deletion the excess SiteFractionDetails records, if there are any.
        /// (There aren't necessarily any SiteFractionDetails records for the Site.)
        /// If a SiteFractionDetails record is to be removed/deleted and it contains a link to a Notes record
        /// which is referenced by no other SiteFractionDetails record then mark it for deletion.
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="revisionedSite"></param>
        private static void RemoveExcessSiteFractionDetailsAndNotes(ImpacPersistenceManager pm, PrescriptionSite revisionedSite)
        {
            List<SiteFractionDetails> sfdsToRemove = 
                pm.GetAllEntitiesInCache().OfType<SiteFractionDetails>()
                .Where(e => e.SIT_ID == revisionedSite.SIT_ID && e.Seq > revisionedSite.Fractions)
                .ToList();
           
            if (sfdsToRemove.Count > 0)
            {               
                if (revisionedSite.SIT_ID > 0)
                {
                    // A version roll will NOT occur 
                    // Remove fraction-specific notes that will be orphaned (if any) when the excess fractions are removed
                    // (No fraction-specific notes will be orphaned when there is a version roll 
                    // - they'll still be referenced from the about-to-be historic rx fx detail recs)
                    RemoveFxNotesThatWillBeOrphaned(pm, sfdsToRemove, revisionedSite);
                }
                    
                // Mark for deletion the excess fraction detail records
                sfdsToRemove.Delete();                                
            }
        }

        /// <summary>
        /// Remove fraction-specific notes that will be orphaned when the excess fractions are removed
        /// (The same Notes record can be referenced from the same fraction# SiteFractionDetails  
        ///  record in more than one version of a Site.)
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="sfdsToRemove"></param>
        /// <param name="revisionedSite"></param>
        private static void RemoveFxNotesThatWillBeOrphaned(ImpacPersistenceManager pm, IEnumerable<SiteFractionDetails> sfdsToRemove, PrescriptionSite revisionedSite)
        {
            // Normal usage is for Plan-of-the-Day i.e., a single Site_FractionDetails record will be removed
            // and if there is a fraction-specific note for that Site_FractionDetails recore then it will be removed.
            // Usage of fraction-specific notes in general is probably uncommon.
            // Usage with Plan-of-the-Day should be even less common given that the feature removes fractions.
            // Considering the above, performance tuning of the functionality below offers little if any benefit.

            List<int?> fxNoteIdsInsfdsToRemove = sfdsToRemove.Where(d => d.FxNote_ID != null).Select(d => d.FxNote_ID).ToList();

            // Notes records are not in the cache (yet)
            
            if (fxNoteIdsInsfdsToRemove.Count > 0)
            {
                // Determine whether any of the fx Notes records will be orphaned when the fraction details record is removed               

                // Get the SIT_IDs for the historic versions of the Site
                var historicSitesInSetQuery = new ImpacRdbQuery(typeof(PrescriptionSite));
                historicSitesInSetQuery.AddClause(PrescriptionSite.SIT_SET_IDEntityColumn, EntityQueryOp.EQ, revisionedSite.SIT_SET_ID);
                historicSitesInSetQuery.AddClause(PrescriptionSite.VersionEntityColumn, EntityQueryOp.GT, 0);
                var historicSites = pm.GetEntities<PrescriptionSite>(historicSitesInSetQuery);
                var historicSIT_IDs = historicSites.Select(s => s.SIT_ID).ToList();

                // Get the FxNote_IDs in the fraction details for the historic versions of the Site
                var historicSFDsQuery = new ImpacRdbQuery(typeof(SiteFractionDetails));
                historicSFDsQuery.AddClause(SiteFractionDetails.SIT_IDEntityColumn, EntityQueryOp.In, historicSIT_IDs);
                var historicSFDs = pm.GetEntities<SiteFractionDetails>(historicSFDsQuery);
                List<int?> historicSFDFx_NoteIDs = historicSFDs.Where(n => n.FxNote_ID != null).Select(n => n.FxNote_ID).ToList();
               
                for (int i = 0; i < fxNoteIdsInsfdsToRemove.Count; i++)
                {
                    int fxNoteId = fxNoteIdsInsfdsToRemove[i].GetValueOrDefault();

                    // Until we find evidence otherwise this fxNoteId will be orphaned
                    bool isGoingToBeOrphaned = true;

                    if (historicSFDFx_NoteIDs.Count > 0)
                    {                        
                        // If this fxNoteId is an fx note for an historic version of the Site then it will NOT be orphaned
                        if (historicSFDFx_NoteIDs.Exists(h => h == fxNoteId))
                        {
                            isGoingToBeOrphaned = false;
                        }
                    }
                    
                    if (isGoingToBeOrphaned)
                    {
                        // Either there are no fx notes for historic versions of the Site
                        // or there are but this fxNoteId isn't an fx note for any of them                        
                        Notes note = Notes.GetEntityByID(fxNoteId, pm);
                        note.Delete();
                    }
                }                                
            }
        }

        /// <summary>
        /// Excess SiteFractionDetails record (if any) must have already been removed from the persistence manager or marked for delete.
        /// If the SiteFractionDetails record to be removed/deleted was the only one that referenced a particular SiteWaveDetails 
        /// then remove/delete that now unreferenced SiteWaveDetails record.
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="revisionedSite"></param>
        private static void RemoveUnreferencedSiteWaveDetails(ImpacPersistenceManager pm, PrescriptionSite revisionedSite)
        {
            // Assemble the list of SiteWaveDetails that are not referenced by any SiteFractionDetails that isn't marked for deletion
            var swdsToRemove = new List<SiteWaveDetails>(GetUnreferencedSiteWaveDetails(pm, revisionedSite));

            if (swdsToRemove.Count > 0)
            {
                // Mark for deletion the excess wave detail records
                swdsToRemove.Delete();                
            }
        }

        /// <summary>
        /// Assembles and returns the list of SiteWaveDetails that are not referenced by any SiteFractionDetails
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="revisionedSite"></param>
        /// <returns></returns>
        private static IEnumerable<SiteWaveDetails> GetUnreferencedSiteWaveDetails(ImpacPersistenceManager pm, PrescriptionSite revisionedSite)
        {
            var swdEntitiesToRemove = new List<SiteWaveDetails>();

            List<SiteFractionDetails> sfdEntitiesSurviving =
                pm.GetAllEntitiesInCache().OfType<SiteFractionDetails>()
                .Where(e => e.SIT_ID == revisionedSite.SIT_ID && e.Seq <= revisionedSite.Fractions)
                .ToList();

            List<SiteWaveDetails> swdEntities =
                pm.GetAllEntitiesInCache().OfType<SiteWaveDetails>()
                .Where(e => e.SIT_ID == revisionedSite.SIT_ID)
                .ToList();

            // Assemble the list of SiteWaveDetails that are not referenced by any SiteFractionDetails records that will survive this update
            foreach (SiteWaveDetails swdEntity in swdEntities)
            {
                var swdId = swdEntity.SWD_ID;

                if (sfdEntitiesSurviving.Count(e => e.SWD_ID == swdId) == 0)                   
                {
                    swdEntitiesToRemove.Add(swdEntity);
                }
            }
            return swdEntitiesToRemove;
        }

        #endregion private methods

        #endregion
    }
}
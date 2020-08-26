using System.Activities;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using IdeaBlade.Persistence;

// 
namespace Impac.Mosaiq.IQ.Activities.RadOnc.TreatmentCalendar
{
    /// <summary> Activity used to display an information message </summary>
    [DeleteFieldsFromSessionActivity_DisplayName]
    [TreatmentCalendar_Category]
    [RadOnc_ActivityGroup]
    public class DeleteFieldsFromSessionActivity : MosaiqCodeActivity
    {

        #region Properties (Input Parameters)
        /// <summary> Patient ID of current open patient as input </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [DeleteFieldsFromSessionActivity_PatId_DisplayName]
        [DeleteFieldsFromSessionActivity_PatId_Description]
        public InArgument<int> PatId { get; set; }

        /// <summary> Unique PCI_ID of th session to delete fields of </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [DeleteFieldsFromSessionActivity_PCI_ID_DisplayName]
        [DeleteFieldsFromSessionActivity_PCI_ID_Description]
        public InArgument<int> PciId { get; set; }
        #endregion

        #region Properties (Output Parameters)
        /// <summary> The result of the activity </summary>
        [OutputParameterCategory]
        [DeleteFieldsFromSessionActivity_FieldsDeleted_DisplayName]
        [DeleteFieldsFromSessionActivity_FieldsDeleted_Description]
        public OutArgument<int> FieldsDeleted { get; set; }

        /// <summary>
        /// The persistence manager to use with the query. If not provided, a new persistence manager will be created.
        /// </summary>
        [InputParameterCategory]
        [DeleteFieldsFromSessionActivity_PersistenceManager_DisplayName]
        [DeleteFieldsFromSessionActivity_PersistenceManager_Description]
        public InArgument<ImpacPersistenceManager> PersistenceManager { get; set; }

        #endregion

        #region Overriden Methods
        /// <summary>
        /// Activity heavy lifting.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            // Activity inputs
            int patId = PatId.Get(context);
            int pciId = PciId.Get(context);
            ImpacPersistenceManager pm = PersistenceManager.Expression != null
                                             ? PersistenceManager.Get(context)
                                             : PM;

            // Activity output
            FieldsDeleted.Set(context, 0); // Initialize the Activity output to 0

            // Get the available session fields for given PCI_ID
            var query = new ImpacRdbQuery(typeof(PatTxCal));
            query.AddClause(PatTxCal.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId);
            query.AddClause(PatTxCal.PCI_IDEntityColumn, EntityQueryOp.EQ, pciId);
            var sessionItems = pm.GetEntities<PatTxCal>(query);

            // Delete fields in the given session
            int fieldsDeleted = 0;
            try
            {
                // Store PatCItem record
                PatCItem patCItem = null;
                byte patCItemStatus = 0; 

                if (sessionItems.Count > 0)
                {
                    // The Delete() member of PatTxField sets PatCItem.Status to "Pending" if the Status is "Approved"
                    // Save state of PatCItem record
                    patCItem = sessionItems[0].PatCItemEntity;
                    patCItemStatus = patCItem.Status_Enum;
                }
                while (sessionItems.Count > 0)
                {
                    sessionItems[sessionItems.Count - 1].Delete();
                    pm.SaveChanges();
                    fieldsDeleted++;
                }
                if (patCItem != null)
                {
                    patCItem.Status_Enum = patCItemStatus;
                    pm.SaveChanges();
                }
            }
            catch
            {
                fieldsDeleted = -1;
            }
            finally
            {
                // Update Activity output
                FieldsDeleted.Set(context, fieldsDeleted);
            }
        }
        #endregion
    }
}
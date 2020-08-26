using System;
using System.Activities;
using System.Activities.Expressions;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using IdeaBlade.Persistence;

// 
namespace Impac.Mosaiq.IQ.Activities.RadOnc.TreatmentCalendar
{
    /// <summary> Activity used to display an information message </summary>
    [InsertFieldsIntoSessionActivity_DisplayName]
    [TreatmentCalendar_Category]
    [RadOnc_ActivityGroup]
    public class InsertFieldsIntoSessionActivity : MosaiqCodeActivity
    {

        #region Properties (Input Parameters)
        /// <summary> Patient ID of current open patient as input </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [InsertFieldsIntoSessionActivity_PatId_DisplayName]
        [InsertFieldsIntoSessionActivity_PatId_Description]
        public InArgument<int> PatId { get; set; }

        /// <summary> Unique PCI_ID of the session to insert fields for </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [InsertFieldsIntoSessionActivity_PCI_ID_DisplayName]
        [InsertFieldsIntoSessionActivity_PCI_ID_Description]
        public InArgument<int> PciId { get; set; }

        /// <summary> Unique SIT_ID of Rad Rx to insert fields for as input </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [InsertFieldsIntoSessionActivity_SIT_ID_DisplayName]
        [InsertFieldsIntoSessionActivity_SIT_ID_Description]
        public InArgument<int> SitId { get; set; }

        /// <summary> The status for the inserted fields as input (default = 5 (Approved))</summary>
        [InputParameterCategory]
        [InsertFieldsIntoSessionActivity_StatusEnum_DisplayName]
        [InsertFieldsIntoSessionActivity_StatusEnum_Description]
        public InArgument<int> StatusEnum { get; set; }

        /// <summary> Use AFS (Automatic Field Sequencing) to deliver Tx Fields </summary>
        [InputParameterCategory]
        [InsertFieldsIntoSessionActivity_UseAFS_DisplayName]
        [InsertFieldsIntoSessionActivity_UseAFS_Description]
        public InArgument<bool> UseAfs { get; set; }

        /// <summary> Use MFS (Manual Field Sequencing) to deliver Tx Fields </summary>
        [InputParameterCategory]
        [InsertFieldsIntoSessionActivity_UseMFS_DisplayName]
        [InsertFieldsIntoSessionActivity_UseMFS_Description]
        public InArgument<bool> UseMfs { get; set; }

        /// <summary>
        /// The persistence manager to use with the query. If not provided, a new persistence manager will be created.
        /// </summary>
        [InputParameterCategory]
        [InsertFieldsIntoSessionActivity_PersistenceManager_DisplayName]
        [InsertFieldsIntoSessionActivity_PersistenceManager_Description]
        public InArgument<ImpacPersistenceManager> PersistenceManager { get; set; }

        #endregion

        #region Properties (Output Parameters)
        /// <summary> The result of the activity </summary>
        [OutputParameterCategory]
        [InsertFieldsIntoSessionActivity_FieldsInserted_DisplayName]
        [InsertFieldsIntoSessionActivity_FieldsInserted_Description]
        public OutArgument<int> FieldsInserted { get; set; }
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
            int sitId = SitId.Get(context);
            int pciId = PciId.Get(context);
            int statusEnum = StatusEnum.Expression != null ? StatusEnum.Get(context) : 5;
            if ((statusEnum != 5) && (statusEnum != 7))
            {
                statusEnum = 7;
            }
            bool useAfs = UseAfs.Expression != null ? UseAfs.Get(context) : false;
            bool useMfs = UseMfs.Expression != null ? UseMfs.Get(context) : false;
            ImpacPersistenceManager pm = PersistenceManager.Expression != null
                                             ? PersistenceManager.Get(context)
                                             : PM;

            // Activity output
            FieldsInserted.Set(context, -1); // Initialize the Activity output to -1 (indication of error)

            // Do not continue if both AFS and MFS are set to true (there is no way to resolve this at run time)
            if (useAfs && useMfs)
            {
                return;
            }

            // Get the Sit_Set_Id of the Site (Sit_Id) given
            var query = new ImpacRdbQuery(typeof(PrescriptionSite));
            query.AddClause(PrescriptionSiteDataRow.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId);
            query.AddClause(PrescriptionSiteDataRow.SIT_IDEntityColumn, EntityQueryOp.EQ, sitId);
            var site = pm.GetEntities<PrescriptionSite>(query);
            if (site.Count != 1)
            {
                return;
            }

            int sitSetId = (int)site[0].SIT_SET_ID.GetValueOrDefault(0);
            
            // Get the list of treatment fields from the given Rad Rx to insert
            query = new ImpacRdbQuery(typeof(Field));
            query.AddClause(FieldDataRow.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId);
            query.AddClause(FieldDataRow.SIT_Set_IDEntityColumn, EntityQueryOp.EQ, sitSetId);
            query.AddClause(FieldDataRow.VersionEntityColumn, EntityQueryOp.EQ, 0);
            query.AddOrderBy(FieldDataRow.DisplaySequenceEntityColumn);
            var txField = pm.GetEntities<Field>(query);
            if (txField.Count == 0)
            {
                return;
            }

            // Make sure we have a (1) session to insert the fields in to
            query = new ImpacRdbQuery(typeof(PatCItem));
            query.AddClause(PatCItemDataRow.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId);
            query.AddClause(PatCItemDataRow.PCI_IDEntityColumn, EntityQueryOp.EQ, pciId);
            var calendarSession = pm.GetEntities<PatCItem>(query);
            if (calendarSession.Count != 1)
            {
                return;
            }

            // Add new entry to PatTxCal for each treatment field
            int fieldsInserted = 0;
            try
            {
                for (int f = 0; f < txField.Count; f++)
                {
                    PatTxCal sessionItem = PatTxCal.Create(pm);
                    sessionItem.PCI_ID = calendarSession[0].PCI_ID;
                    sessionItem.Status_enum = (byte)statusEnum;
                    sessionItem.Pat_ID1 = patId;
                    sessionItem.FLD_Set_ID = txField[f].FLD_SET_ID;
                    sessionItem.TxSequence = (short)(f + 1);
                    sessionItem.ProFormaPF = 0;
                    if (useAfs)
                    {
                        if (f == 0)
                        {
                            sessionItem.AFS_Begin = true;
                        }
                        else
                        {
                            sessionItem.AFS = true;
                        }
                    }
                    if (useMfs)
                    {
                        if (f == 0)
                        {
                            sessionItem.MFS_Begin = true;
                        }
                        else
                        {
                            sessionItem.MFS = true;
                        }
                    }
                    sessionItem.PF_Only = txField[f].Type_Enum == 3 || txField[f].Type_Enum == 4 || 
                        txField[f].Type_Enum == 5 || txField[f].Type_Enum == 9; //FLD.Type_Enum: 3=Setup, 4=kV Setup, 5=CT, 9=MVCT
                    
                    pm.SaveChanges();
                    fieldsInserted++;
                }
            }
            catch
            {
                fieldsInserted = -1;
            }
            finally
            {
                FieldsInserted.Set(context, fieldsInserted);
            }
        }
        #endregion
    }
}
using System;
using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.Charting.Documents.DocumentFunctions;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.General.Escribe
{
	/// <summary>
	/// </summary>
	[GeneralCharting_ActivityGroup]
	[Escribe_Category]
	[CreateDocumentActivity_DisplayName]
	[CreateDocumentActivity_Description]
	public class CreateDocumentActivity : MosaiqCodeActivity
	{
		enum CreateIfDuplicateExistsOptions { No = 1, Yes = 2}
		enum OpenAfterCreationOptions { No = 1, Yes = 2 }

		#region Properties (Input Parameters)
		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_PatId1_DisplayName]
		[CreateDocumentActivity_PatId1_Description]
		public InArgument<int> PatId { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_DocType_DisplayName]
		[CreateDocumentActivity_DocType_Description]
		public InArgument<int?> DocType { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_DictatedBy_DisplayName]
		[CreateDocumentActivity_DictatedBy_Description]
		public InArgument<int> DictatedBy { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[CreateDocumentActivity_ReviewRequiredBy_DisplayName]
		[CreateDocumentActivity_ReviewRequiredBy_Description]
		public InArgument<int> ReviewRequiredBy { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[CreateDocumentActivity_CosignRequiredBy_DisplayName]
		[CreateDocumentActivity_CosignRequiredBy_Description]
		public InArgument<int> CoSignRequiredBy { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_TranscribedBy_DisplayName]
		[CreateDocumentActivity_TranscribedBy_Description]
		public InArgument<int> TranscribedBy { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_EncounterDate_DisplayName]
		[CreateDocumentActivity_EncounterDate_Description]
		public InArgument<DateTime?> EncounterDate { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_TranscribedDate_DisplayName]
		[CreateDocumentActivity_TranscribedDate_Description]
		public InArgument<DateTime?> TranscribedDate { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_Department_DisplayName]
		[CreateDocumentActivity_Department_Description]
		public InArgument<int?> Department { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_Account_DisplayName]
		[CreateDocumentActivity_Account_Description]
		public InArgument<int?> Account { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_Template_DisplayName]
		[CreateDocumentActivity_Template_Description]
		public InArgument<string> Template { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_Status_DisplayName]
		[CreateDocumentActivity_Status_Description]
		public InArgument<int> Status { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_CreateIfDuplicateExists_DisplayName]
		[CreateDocumentActivity_CreateIfDuplicateExists_Description]
		public InArgument<IQEnum> CreateIfDuplicateExists { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[CreateDocumentActivity_OpenAfterCreation_DisplayName]
		[CreateDocumentActivity_OpenAfterCreation_Description]
		public InArgument<IQEnum> OpenAfterCreation { get; set; }

		#endregion

		#region Overrides of MosaiqCodeActivity
		/// <summary>
		/// 
		/// </summary>
		protected override void DoWork(CodeActivityContext context)
		{
			var patId1 = PatId.Get(context);
			var patient = Patient.GetEntityByID(patId1, PM);
			if (patient.IsNullEntity)
				return;

			short? docType = null;
			var docTypeTemp = DocType.Get(context);
			if (docTypeTemp.HasValue)
				docType = (short)docTypeTemp.Value;

			var dictId = DictatedBy.Get(context);
			if (dictId == 0)
				return;

			int? reviewId = null;
			int temp = ReviewRequiredBy.Get(context);
			if (temp > 0)
				reviewId = temp;

			int? cosignId = null;
			temp = CoSignRequiredBy.Get(context);
			if (temp > 0)
				cosignId = temp;

			int? transId = null;
			temp = TranscribedBy.Get(context);
			if (temp > 0)
				transId = temp;

			var encounterTemp = EncounterDate.Get(context);
			if (!encounterTemp.HasValue)
				return;
			var encounterDate = encounterTemp.Value;

			var transcribedDate = TranscribedDate.Get(context);

			var instId = Department.Get(context);
			var accountId = Account.Get(context);

			var template = Template.Get(context);

			var openAfterCreation = OpenAfterCreation.Get(context).Key == (int)OpenAfterCreationOptions.Yes;
			var status = (byte) Status.Get(context);

			if (CreateIfDuplicateExists.Get(context).Key == (int) CreateIfDuplicateExistsOptions.No)
			{
				// Check for a duplicate encounter
				var query = new ImpacRdbQuery(typeof (ObjectTable));
				query.AddClause(ObjectTableDataRow.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);
				query.AddClause(ObjectTableDataRow.DocTypeEntityColumn, EntityQueryOp.EQ, docType);

				query.AddClause(ObjectTableDataRow.Dict_IDEntityColumn, EntityQueryOp.EQ, dictId);
				query.AddClause(ObjectTableDataRow.Review_IDEntityColumn, EntityQueryOp.EQ, reviewId);
				query.AddClause(ObjectTableDataRow.CoSig_IDEntityColumn, EntityQueryOp.EQ, cosignId);
				query.AddClause(ObjectTableDataRow.Trans_IDEntityColumn, EntityQueryOp.EQ, transId);

                // Defect 11605, the Encounter_DtTm and Trans_DtTm store date time rather than the previous date part 
				// so we should check items if it exists within the day of datetime
                query.AddClause(ObjectTableDataRow.Encounter_DtTmEntityColumn, EntityQueryOp.Between, encounterDate.Date, encounterDate.Date.AddDays(1));

				if (transcribedDate.HasValue)
                    query.AddClause(ObjectTableDataRow.Trans_DtTmEntityColumn, EntityQueryOp.Between, transcribedDate.Value.Date, transcribedDate.Value.Date.AddDays(1));

				query.AddClause(ObjectTableDataRow.Inst_IDEntityColumn, EntityQueryOp.EQ, instId);
				query.AddClause(ObjectTableDataRow.Account_IDEntityColumn, EntityQueryOp.EQ, accountId);
				query.AddClause(ObjectTableDataRow.DocumentTemplateEntityColumn, EntityQueryOp.EQ, template);

				var objectEntity = PM.GetEntity(query);
				if (objectEntity != null && !objectEntity.IsNullEntity)
					return;
			}

			EscribeOperations.CreateDocument(patId1, docType,
			                                 dictId, reviewId, cosignId, transId,
			                                 encounterDate, transcribedDate,
			                                 instId, accountId, template,
			                                 status, openAfterCreation);
		}
		#endregion
	}
}

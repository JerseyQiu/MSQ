using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Charting.CommonUtils;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.General.Flowsheet
{
	/// <summary>
	/// Take an input DataItemThresholds value (i.e. an ObsDef and its thresholds and an ObrId) and an ObrId, then look up 
	/// an Observe value for that ObsDef item that belongs to the specified ObrId and check it against the threshoolds
	/// </summary>
	[CheckDataItemThresholdsActivity_DisplayName]
	[Flowsheet_Category]
	[GeneralCharting_ActivityGroup]
	public class CheckDataItemThresholds : MosaiqCodeActivity
	{
		#region Properties (Input Parameters)
		/// <summary>
		///  The DataItemThresholds item to check
		/// </summary>
		[RequiredArgument]
		[InputParameterCategory]
		[CheckDataItemThresholdsActivity_DataItemThresholds_DisplayName]
		[CheckDataItemThresholdsActivity_DataItemThresholds_Description]
		public InArgument<DataItemThresholds> DataItemThresholds { get; set; }

		/// <summary>
		/// The primary key of the ObsReq record which we will check
		/// </summary>
		[RequiredArgument]
		[InputParameterCategory]
		[CheckDataItemThresholdsActivity_ObrId_DisplayName]
		[CheckDataItemThresholdsActivity_ObrId_Description]
		public InArgument<int> ObrId { get; set; }

		#endregion

		#region Properties (Output Parameters)
		/// <summary>
		/// The result of the check
		/// </summary>
		[OutputParameterCategory]
		[CheckDataItemThresholdsActivity_Success_DisplayName]
		[CheckDataItemThresholdsActivity_Success_Description]
		public OutArgument<bool> Success { get; set; }
		#endregion

		#region Overriden Methods
		/// <summary> </summary>
		protected override void DoWork(CodeActivityContext context)
		{
			Success.Set(context, true);

			var obrId = ObrId.Get(context);
			var dataItemThresholds = DataItemThresholds.Get(context);
			if (dataItemThresholds == null)
				return;

			ObsDef obsDefEntity = ObsDef.GetEntityByObdGuid(dataItemThresholds.ObdGuid, PM);
			if (obsDefEntity.IsNullEntity)
				return;

			var query = new EntityQuery(typeof(ObsReq));
			query.AddClause(ObsReqDataRow.OBR_IDEntityColumn, EntityQueryOp.EQ, obrId);
			var obsReqEntity = PM.GetEntity<ObsReq>(query);
			if (obsReqEntity.IsNullEntity)
				return;

			query = new EntityQuery(typeof(Observe));
			query.AddClause(ObserveDataRow.OBD_IDEntityColumn, EntityQueryOp.EQ, obsDefEntity.OBD_ID);
			query.AddClause(ObserveDataRow.Pat_ID1EntityColumn, EntityQueryOp.EQ, obsReqEntity.Pat_ID1);
			query.AddClause(ObserveDataRow.OBR_SET_IDEntityColumn, EntityQueryOp.EQ, obsReqEntity.OBR_Set_ID);
			var observeEntity = PM.GetEntity<Observe>(query);
			if (observeEntity.IsNullEntity)
				return;

			var success = CommonCharting.CheckDataItemThresholds(observeEntity, dataItemThresholds);
			Success.Set(context, success);
		}
		#endregion
	}
}

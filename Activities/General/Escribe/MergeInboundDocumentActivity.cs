using System.Activities;
using Impac.Mosaiq.Charting.Documents.DocumentFunctions;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.General.Escribe
{
	/// <summary>
	/// </summary>
	[GeneralCharting_ActivityGroup]
	[Escribe_Category]
	[MergeInboundDocumentActivity_DisplayName]
	[MergeInboundDocumentActivity_Description]
	public class MergeInboundDocumentActivity : MosaiqCodeActivity
	{
		#region Properties (Input Parameters)
		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[MergeInboundDocumentActivity_ObjId_DisplayName]
		[MergeInboundDocumentActivity_ObjId_Description]
		public InArgument<int> ObjId { get; set; }

		/// <summary> </summary>
		[InputParameterCategory]
		[RequiredArgument]
		[MergeInboundDocumentActivity_Template_DisplayName]
		[MergeInboundDocumentActivity_Template_Description]
		public InArgument<string> Template { get; set; }
		#endregion

		#region Properties (Output Parameters)
		#endregion

		#region Overrides of MosaiqCodeActivity
		/// <summary>
		/// 
		/// </summary>
		protected override void DoWork(CodeActivityContext context)
		{
			int objId = ObjId.Get(context);
			string template = Template.Get(context);
			EscribeOperations.MergeInboundDocument(objId, template);
		}
		#endregion
	}
}

using System;
using System.Activities;
using System.Windows.Forms;

using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Core.Security.SecurityLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using Impac.Mosaiq.UI.Framework.UserDefinedData;

namespace Impac.Mosaiq.IQ.Activities.General.Flowsheet
{
    /// <summary> Opens the observation form and allows the user to enter assessment data in a popup dialog. </summary>
    [FlowsheetFormActivity_DisplayName]
    [Flowsheet_Category]
    [GeneralCharting_ActivityGroup]
    public class FlowsheetFormActivity : MosaiqUICodeActivity
    {
        #region constants

        private const string FLOWSHEET_UDDF_WIDGET_INSTANCE_ID = "ObserveForm:NewRecord";

        #endregion

        #region Properties (Input Parameters)
        /// <summary> The Obd_Guid of the ObsDef tab view which will be displayed </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FlowsheetFormActivity_ObdGuid_Description]
        [FlowsheetFormActivity_ObdGuid_DisplayName]
        public InArgument<Guid> ObdGuid { get; set; }

        /// <summary> Determines whether the assessment created will be marked as completed </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FlowsheetFormActivity_MarkReviewed_Description]
        [FlowsheetFormActivity_MarkReviewed_DisplayName]
        public InArgument<bool> MarkReviewed { get; set; }

        /// <summary> The Patient ID for the assessment form to be associated with. </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FlowsheetFormActivity_PatientId_Description]
        [FlowsheetFormActivity_PatientId_DisplayName]
        public InArgument<int> PatientId { get; set; }

		/// <summary> A flag to indicate whether the activity shold return a result, i.e. display the UDD form synchronously. </summary>
		[RequiredArgument]
		[InputParameterCategory]
		[FlowsheetFormActivity_ReturnResult_Description]
		[FlowsheetFormActivity_ReturnResult_DisplayName]
		public InArgument<bool> ReturnResult { get; set; }

        /// <summary> A flag to indicate whether the form should be popup in modal or not. </summary>
        //[RequiredArgument]
        [InputParameterCategory]
        [FlowsheetFormActivity_ModalDialog_Description]
        [FlowsheetFormActivity_ModalDialog_DisplayName]
        public InArgument<bool> ModalDialog { get; set; }

        #endregion

        #region Properties (Output Parameters)
        /// <summary> The dialog result of the form </summary>
        [OutputParameterCategory]
        [FlowsheetFormActivity_DialogResult_Description]
        [FlowsheetFormActivity_DialogResult_DisplayName]
        public OutArgument<DialogResult> DialogResult { get; set; }

        /// <summary> The OBR_ID of the ObsReq record created by this form. </summary>
        [OutputParameterCategory]
        [FlowsheetFormActivity_ObrId_Description]
        [FlowsheetFormActivity_ObrId_DisplayName]
        public OutArgument<int> ObrId { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Open the observation form and let the user enter data.
        /// Sets the observe form to the patient of the activity not the global patient.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            //Get parameter values
            bool markReviewed = MarkReviewed.Expression != null ? MarkReviewed.Get(context) : false;
            Guid obdGuid = ObdGuid.Get(context);

            var parms = new UddWidgetParams
                            {
                                MarkReviewed = markReviewed,
                                UddDataParams = {PatId1 = PatientId.Get(context)},
                                TabViewObdGuid = obdGuid,
                                Mode = UddWidgetMode.Insert,
								IsFlowsheetDisplay = true 
                            };

			var view = ObsDef.GetEntityByObdGuid(obdGuid, PM);
			
			var securityEnum = SecurityUtility.ByteArrayToEnum(view.Security_Mask);

			bool returnResult = ReturnResult.Get(context);

			if (!returnResult)
			{
                // open an instance of the UDD with a distinct instanceid from the standard UDD indstance id
                var host = new UddWidgetModalHost(parms, securityEnum, null, Strings.FlowsheetFormActivity_UddInstanceId);

                // Defect 6398 - Clicking OK in error window cause Mosaiq crashed when trigger IQ script (create assessment by QCL task)
                // if user doesn't have modify rights in Assessment/Labs/Vital signs
				// Defect 7055: Replace IsHandleCreated with !IsDisposed because the window's handle is never created at this point
                if (!host.IsDisposed)
                {
                    var modalDialog = ModalDialog.Get(context);
                    if (modalDialog)
                        host.ShowDialog();
                    else
                    {
                        host.Show();
                        host.ForceToFront();
                    }
                }
                return;
			}

            DialogResult res = UddWidgetModalHost.ShowModal(parms, securityEnum, Strings.FlowsheetFormActivity_UddInstanceId);
            if (res == System.Windows.Forms.DialogResult.OK)
            {
               ObrId.Set(context, parms.UddDataParams.ObrId);
            }
            //Record output parameters
            DialogResult.Set(context, res);
        }
        #endregion
    }
}
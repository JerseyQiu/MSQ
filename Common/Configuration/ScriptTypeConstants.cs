using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impac.Mosaiq.IQ.Common.Configuration
{
    /// <summary>
    /// Defines constants for IQ Script Types
    /// </summary>
    public static class ScriptTypeConstants
    {
        /// <summary> Argument name for a cancel operation </summary>
        public static readonly string ArgCancelOperation = "_cancelOperation";

        /// <summary> Argument name for an order set id </summary>
        public static readonly string ArgOrdSetId = "_orcSetId";

        /// <summary> Argument name for a checklist set id </summary>
        public static readonly string ArgChkId = "_chkId";

        /// <summary> Argument name for a patient id </summary>
        public static readonly string ArgPatId = "_patId1";

        /// <summary> Argument name for a generate approved </summary>
        public static readonly string ArgGenerateApproved = "_generateApproved";

        /// <summary> Argument name for a selected observations </summary>
        public static readonly string ArgSelectedObservations = "_selectedObservations";

		/// <summary> Argument name for HTML data </summary>
		public static readonly string ArgHtml = "_html";

		/// <summary> Argument name for plain text data </summary>
		public static readonly string ArgPlainText = "_plainText";

        /// <summary> Argument name for output ObsReqs entities (return from some MergeFields) </summary>
        public static readonly string ArgObsReqs = "outObsReqsByDate";

        /// <summary> Argument name for the form request used with clarion IQ Forms. </summary>
        public static readonly string ArgFormRequest = "_formRequest";

		/// <summary> Argument name for a document type </summary>
		public static readonly string ArgDocumentType = "_docType";

		/// <summary> Argument name for a selected eScribe template </summary>
		public static readonly string ArgSelectedTemplate = "_template";

		/// <summary> Argument name for an object id </summary>
		public static readonly string ArgObjId = "_objId";

        /// <summary> Argument name for an ObsReq id </summary>
        public static readonly string ArgObrId = "_obrId";

        /// <summary> Argument name for an Schedule id </summary>
        public static readonly string ArgSchId = "_schId";

        /// <summary> Argument name for an Charge id </summary>
        public static readonly string ArgChgId = "_chgId";

        /// <summary> Argument name for session id </summary>
        public static readonly string ArgPciId = "_pciId";

		/// <summary> Argument name for diagnosis (Medical) id </summary>
		public static readonly string ArgMedId = "_medId";

        /// <summary> The argument care plan description  </summary>
        public static readonly string ArgCarePlanNotes = "_carePlanNotes";
    }
}

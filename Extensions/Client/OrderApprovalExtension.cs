using System.Collections.Generic;
using Impac.Mosaiq.IQ.Extensions.Interface;

namespace Impac.Mosaiq.IQ.Extensions.Client
{
    /// <summary>
    /// This interface defines methods that allows the user to read and write values that must be communicated between IQ Scripts
    /// during batch order approval processing
    /// </summary>
    public class OrderApprovalExtension : IOrderApprovalExtension
    {
        #region Static Constructor
        /// <summary> Perform dictionary initialization. </summary>
        static OrderApprovalExtension()
        {
            AcceptedDoseCollection = new List<AcceptedDoseInfo>();
        }
        #endregion

        #region Static Fields
        /// <summary>
        /// A collection of accepted drug/dose combinations which must be communicated between orders.
        /// </summary>
        private static readonly List<AcceptedDoseInfo> AcceptedDoseCollection;
        #endregion

        #region Static Methods
        /// <summary> Clears out any variances stored internally in preparation for the next approval batch. </summary>
        public static void ClearAllowedVariances()
        {
            AcceptedDoseCollection.Clear();
        }
        #endregion

        #region Implementation of IOrderApprovalExtension
        /// <summary>
        /// Records a dose as accepted
        /// </summary>
        /// <param name="pDrgId"></param>
        /// <param name="pAcceptedDose"></param>
        public void AcceptDose(int pDrgId, double pAcceptedDose)
        {
            AcceptedDoseCollection.Add(new AcceptedDoseInfo { DrgId = pDrgId, AcceptedDose = pAcceptedDose });
        }

        /// <summary>
        /// Checks if a dose is accepted for a given drug.
        /// </summary>
        /// <param name="pDrgId"></param>
        /// <param name="pDose"></param>
        /// <returns></returns>
        public bool IsDoseAccepted(int pDrgId, double pDose)
        {
            return AcceptedDoseCollection.Exists(e => e.DrgId == pDrgId && e.AcceptedDose == pDose);
        }
        #endregion

        #region Helper Classes
        /// <summary> Helper class for storing drug/dose combinations </summary>
        private class AcceptedDoseInfo
        {
            /// <summary> The drug the accepted dose applies to </summary>
            public int DrgId { get; set; }

            /// <summary> The accepted dose </summary>
            public double AcceptedDose { get; set; }
        }
        #endregion
    }
}

namespace Impac.Mosaiq.IQ.Extensions.Interface
{
    /// <summary>
    /// This interface defines the methods required on the pharmacy order approval processing extension used to support communication
    /// between IQ Scripts run in a single approval batch.
    /// </summary>
    public interface IOrderApprovalExtension
    {
        #region Accepted Dose Support

        /// <summary>
        /// Marks a particular dose as accepted in order processing.
        /// </summary>
        /// <param name="pDrgId"></param>
        /// <param name="pAcceptedDose"></param>
        void AcceptDose(int pDrgId, double pAcceptedDose);

        /// <summary>
        /// Checks to see if a dose is accepted
        /// </summary>
        /// <param name="pDrgId"></param>
        /// <param name="pDose"></param>
        /// <returns></returns>
        bool IsDoseAccepted(int pDrgId, double pDose);

        #endregion
    }
}

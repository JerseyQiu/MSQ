using System;
using Impac.Mosaiq.IQ.Core.Definitions.IQForms;

namespace Impac.Mosaiq.IQ.Extensions.Interface
{
    /// <summary>
    /// Non-generic properties of the IIQFormExtension interface.
    /// </summary>
    public interface IIQFormExtension
    {
        ///<summary></summary>
        Object GetControlValue(IIQFormControl control);

        ///<summary></summary>
        void SetControlValue(IIQFormControl control, Object value);

        /// <summary> </summary>
        bool CancelPendingOperation { get; set; }
    }
}

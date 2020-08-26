using System;
using Impac.Mosaiq.IQ.Core.Definitions.Configuration;
using Impac.Mosaiq.IQ.Core.Definitions.IQForms;
using Impac.Mosaiq.IQ.Extensions.Interface;

namespace Impac.Mosaiq.IQ.Extensions.Client
{
    /// <summary>
    /// Workflow Foundation runtime extension which provides a generic interface between a "Form" (which could
    /// be a clarion form, a .NET dialog, a .NET widget, etc) and an IQ Script.  All communication between an IQ
    /// Script and the UI component it is servicing goes through this extension.
    /// </summary>
    public class IQFormExtension<TScriptType> : IIQFormExtension
        where TScriptType : IScriptType, new()
    {
        #region Constructors
        /// <summary></summary>
        public IQFormExtension(Func<IIQFormControl, object> pGetValueImpl,
            Action<IIQFormControl, object> pSetValueImpl)
        {
            if (pGetValueImpl == null)
                throw new ArgumentNullException("pGetValueImpl");

            if(pSetValueImpl == null)
                throw new ArgumentNullException("pSetValueImpl");

            _getValueImpl = pGetValueImpl;
            _setValueImpl = pSetValueImpl;
            CancelPendingOperation = false;
            ScriptType = new TScriptType();
        }
        #endregion

        #region Private Fields
        private readonly Func<IIQFormControl, object> _getValueImpl;
        private readonly Action<IIQFormControl, object> _setValueImpl;
        #endregion

        #region IFormInteropService Members
        ///<summary>Returns the script type which this extension is servicing.</summary>
        public TScriptType ScriptType { get; set; }

        /// <summary></summary>
        public object GetControlValue(IIQFormControl control)
        {
            return _getValueImpl(control);
        }

        /// <summary></summary>
        public void SetControlValue(IIQFormControl control, object value)
        {
            _setValueImpl(control, value);
        }

        /// <summary> </summary>
        public bool CancelPendingOperation { get; set; }
        #endregion
    }
}

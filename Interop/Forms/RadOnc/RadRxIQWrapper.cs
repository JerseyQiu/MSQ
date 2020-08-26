using System.Collections.Generic;
using System.Runtime.InteropServices;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Definitions.Identifiers;
using Impac.Mosaiq.IQ.Interop.Base;

namespace Impac.Mosaiq.IQ.Interop.Forms.RadOnc
{
    /// <summary> </summary>
    [Guid("6a22eba6-fbc8-423d-96e3-a337267a4b50")]
    [ClassInterface(ClassInterfaceType.AutoDual), ComVisible(true), ClarionExport]
    public class RadRxIQWrapper : FormIQWrapper
    {
        #region Public Methods
        public void Initialize(string pSiteName, int pPcpId, double pDoseTx, double pDoseTtl)
        {
            //Intentionally assigning private field as not to invoke property change logic in the setter.
            _sit_Site_Name = pSiteName;
            _sit_Pcp_Id = pPcpId;
            _sit_Dose_Tx = pDoseTx;
            _sit_Dose_Ttl = pDoseTtl;

            IDictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("argSiteName", pSiteName);
            parms.Add("argPcpId", pPcpId);
            parms.Add("argDoseTx", pDoseTx);
            parms.Add("argDoseTtl", pDoseTtl);
            Load(FeatureGuids.RadiationPrescriptionForm, parms, GetValueImpl, SetValueImpl, null);
        }
        #endregion

        #region Public Properties
        /// <summary> Site_Name of selected site </summary>
        public string Sit_Site_Name
        {
            get { return _sit_Site_Name; }
            set
            {
                if (_sit_Site_Name != value)
                {
                    _sit_Site_Name = value;
                    OnPropertyChanged("Site_Name");
                }
            }
        }
        private string _sit_Site_Name;

        /// <summary> Pcp_ID of selected site </summary>
        public int Sit_Pcp_Id
        {
            get { return _sit_Pcp_Id; }
            set
            {
                if (_sit_Pcp_Id != value)
                {
                    _sit_Pcp_Id = value;
                    OnPropertyChanged("Pcp_Id");
                }
            }
        }
        private int _sit_Pcp_Id;

        /// <summary> Dose_Tx of selected site </summary>
        public double Sit_Dose_Tx
        {
            get { return _sit_Dose_Tx; }
            set
            {
                if (_sit_Dose_Tx != value)
                {
                    _sit_Dose_Tx = value;
                    OnPropertyChanged("Dose_Tx");
                }
            }
        }
        private double _sit_Dose_Tx;

        /// <summary> Dose_Ttl of selected site </summary>
        public double Sit_Dose_Ttl
        {
            get { return _sit_Dose_Ttl; }
            set
            {
                if (_sit_Dose_Ttl != value)
                {
                    _sit_Dose_Ttl = value;
                    OnPropertyChanged("Dose_Ttl");
                }
            }
        }
        private double _sit_Dose_Ttl;
        #endregion

        #region Private Methods (Delegate Implementation)
        [ComVisible(false)]
        private object GetValueImpl(string pPropertyName)
        {
            switch (pPropertyName)
            {
                case "Site_Name":
                    return Sit_Site_Name;
                case "Pcp_Id":
                    return Sit_Pcp_Id;
                case "Dose_Tx":
                    return Sit_Dose_Tx;
                case "Dose_Ttl":
                    return Sit_Dose_Ttl;

            }
            return null;
        }

        [ComVisible(false)]
        private void SetValueImpl(string pPropertyName, object pObjectValue)
        {
            switch (pPropertyName)
            {
                case "Site_Name":
                    _sit_Site_Name = pObjectValue is string ? (string)pObjectValue : _sit_Site_Name;
                    break;

                case "Pcp_Id":
                    _sit_Pcp_Id = pObjectValue is int ? (int)pObjectValue : _sit_Pcp_Id;
                    break;

                case "Dose_Tx":
                    _sit_Dose_Tx = pObjectValue is double ? (double)pObjectValue : _sit_Dose_Tx;
                    break;

                case "Dose_Ttl":
                    _sit_Dose_Ttl = pObjectValue is double ? (double)pObjectValue : _sit_Dose_Ttl;
                    break;
            }
        }

        #endregion
    }
}


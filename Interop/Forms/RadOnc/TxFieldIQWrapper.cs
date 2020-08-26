using System.Collections.Generic;
using System.Runtime.InteropServices;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Definitions.Identifiers;
using Impac.Mosaiq.IQ.Interop.Base;

namespace Impac.Mosaiq.IQ.Interop.Forms.RadOnc
{
    /// <summary> </summary>
    [Guid("39bb28f9-5ca9-4c12-9b47-91689671b59b")]
    [ClassInterface(ClassInterfaceType.AutoDual), ComVisible(true), ClarionExport]
    public class TxFieldIQWrapper : FormIQWrapper
    {
        #region Public Methods
        public void Initialize(byte pModalityEnum, int pSitSetID, short pEnergy,
           double pMeterset, int pMachineIDStaffID, byte pTypeEnum, string pCompFDA,
           string pWdgAppl)
        {
            //db columns:                   Clarion        -> .NET
            //TxField.Modality_Enum         byte           -> byte
            //TxField.Sit_Set_ID            long           -> int
            //TxFieldPoint.Energy           short          -> short
            //TxField.Meterset              PDECIMAL(9,3)  -> double
            //TxField.Machine_ID_Staff_ID   long           -> int
            //TxField.Type_Enum             byte           -> byte
            //TxField.Comp_FDA              CSTRING(11)    -> string
            //TxField.Wdg_Appl              CSTRING(11)    -> string

            //Intentionally assigning private field as not to invoke property change logic in the setter.
            _fld_Modality_Enum = pModalityEnum;
            _fld_Sit_Set_ID = pSitSetID;
            _tfp_Energy = pEnergy;
            _fld_Meterset = pMeterset;
            _fld_Machine_ID_Staff_ID = pMachineIDStaffID;
            _fld_Type_Enum = pTypeEnum;
            _fld_Comp_FDA = pCompFDA;
            _fld_Wdg_Appl = pWdgAppl;

            IDictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("argModalityEnum", pModalityEnum);
            parms.Add("argSitSetID", pSitSetID);
            parms.Add("argEnergy", pEnergy);
            parms.Add("argMeterset", pMeterset);
            parms.Add("argMachineIDStaffID", pMachineIDStaffID);
            parms.Add("argTypeEnum", pTypeEnum);
            parms.Add("argCompFDA", pCompFDA);
            parms.Add("argWdgAppl", pWdgAppl);
            Load(FeatureGuids.TreatmentFieldForm, parms, GetValueImpl, SetValueImpl, null);
        }
        #endregion

        #region Public Properties
        /// <summary> Modality_Enum of selected field </summary>
        public byte Fld_Modality_Enum
        {
            get { return _fld_Modality_Enum; }
            set
            {
                if (_fld_Modality_Enum != value)
                {
                    _fld_Modality_Enum = value;
                    OnPropertyChanged("Modality_Enum");
                }
            }
        }
        private byte _fld_Modality_Enum;

        /// <summary> Sit_Set_ID of selected field </summary>
        public int Fld_Sit_Set_ID
        {
            get { return _fld_Sit_Set_ID; }
            set
            {
                if (_fld_Sit_Set_ID != value)
                {
                    _fld_Sit_Set_ID = value;
                    OnPropertyChanged("Sit_Set_ID");
                }
            }
        }
        private int _fld_Sit_Set_ID;

        /// <summary> Energy of selected field </summary>
        public short Tfp_Energy
        {
            get { return _tfp_Energy; }
            set
            {
                if (_tfp_Energy != value)
                {
                    _tfp_Energy = value;
                    OnPropertyChanged("Energy");
                }
            }
        }
        private short _tfp_Energy;

        /// <summary> Meterset of selected field </summary>
        public double Fld_Meterset
        {
            get { return _fld_Meterset; }
            set
            {
                if (_fld_Meterset != value)
                {
                    _fld_Meterset = value;
                    OnPropertyChanged("Meterset");
                }
            }
        }
        private double _fld_Meterset;

        /// <summary> Machine_ID_Staff_ID of selected field </summary>
        public int Fld_Machine_ID_Staff_ID
        {
            get { return _fld_Machine_ID_Staff_ID; }
            set
            {
                if (_fld_Machine_ID_Staff_ID != value)
                {
                    _fld_Machine_ID_Staff_ID = value;
                    OnPropertyChanged("Machine_ID_Staff_ID");
                }
            }
        }
        private int _fld_Machine_ID_Staff_ID;

        /// <summary> Type_Enum of selected field </summary>
        public byte Fld_Type_Enum
        {
            get { return _fld_Type_Enum; }
            set
            {
                if (_fld_Type_Enum != value)
                {
                    _fld_Type_Enum = value;
                    OnPropertyChanged("Type_Enum");
                }
            }
        }
        private byte _fld_Type_Enum;

        /// <summary> Comp_FDA of selected field </summary>
        public string Fld_Comp_FDA
        {
            get { return _fld_Comp_FDA; }
            set
            {
                if (_fld_Comp_FDA != value)
                {
                    _fld_Comp_FDA = value;
                    OnPropertyChanged("Comp_FDA");
                }
            }
        }
        private string _fld_Comp_FDA;

        /// <summary> Wdg_Appl of selected field </summary>
        public string Fld_Wdg_Appl
        {
            get { return _fld_Wdg_Appl; }
            set
            {
                if (_fld_Wdg_Appl != value)
                {
                    _fld_Wdg_Appl = value;
                    OnPropertyChanged("Wdg_Appl");
                }
            }
        }
        private string _fld_Wdg_Appl;

        #endregion

        #region Private Methods (Delegate Implementation)
        [ComVisible(false)]
        private object GetValueImpl(string pPropertyName)
        {
            switch (pPropertyName)
            {
                case "Modality_Enum":
                    return Fld_Modality_Enum;
                case "Sit_Set_ID":
                    return Fld_Sit_Set_ID;
                case "Energy":
                    return Tfp_Energy;
                case "Meterset":
                    return Fld_Meterset;
                case "Machine_ID_Staff_ID":
                    return Fld_Machine_ID_Staff_ID;
                case "Type_Enum":
                    return Fld_Type_Enum;
                case "Comp_FDA":
                    return Fld_Comp_FDA;
                case "Wdg_Appl":
                    return Fld_Wdg_Appl;

            }
            return null;
        }

        [ComVisible(false)]
        private void SetValueImpl(string pPropertyName, object pObjectValue)
        {
            switch (pPropertyName)
            {
                case "Modality_Enum":
                    _fld_Modality_Enum = pObjectValue is byte ? (byte)pObjectValue : _fld_Modality_Enum;
                    break;

                case "Sit_Set_ID":
                    _fld_Sit_Set_ID = pObjectValue is int ? (int)pObjectValue : _fld_Sit_Set_ID;
                    break;

                case "Energy":
                    _tfp_Energy = pObjectValue is short ? (short)pObjectValue : _tfp_Energy;
                    break;

                case "Meterset":
                    _fld_Meterset = pObjectValue is double ? (double)pObjectValue : _fld_Meterset;
                    break;

                case "Machine_ID_Staff_ID":
                    _fld_Machine_ID_Staff_ID = pObjectValue is int ? (int)pObjectValue : _fld_Machine_ID_Staff_ID;
                    break;

                case "Type_Enum":
                    _fld_Type_Enum = pObjectValue is byte ? (byte)pObjectValue : _fld_Type_Enum;
                    break;

                case "Comp_FDA":
                    _fld_Comp_FDA = pObjectValue is string ? (string)pObjectValue : _fld_Comp_FDA;
                    break;

                case "Wdg_Appl":
                    _fld_Wdg_Appl = pObjectValue is string ? (string)pObjectValue : _fld_Wdg_Appl;
                    break;

            }
        }

        #endregion
    }
}

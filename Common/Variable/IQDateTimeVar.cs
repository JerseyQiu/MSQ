using System;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;

namespace Impac.Mosaiq.IQ.Common.Variable
{
    /// <summary> IQ Variable type for selecting a date. </summary>
    [Serializable]
    public class IQDateTimeVar : IQVariableValue<DateTime, IQDateTimeElement>
    {
    }

    /// <summary> Custom IQElementVar class for DateTime. </summary>
    [Serializable]
    public class IQDateTimeElement : IQVarElement<DateTime>
    {
        #region Constructor
        /// <summary> Default Ctor </summary>
        public IQDateTimeElement()
        {
            Type = IQDateTimeType.Today;
            SetRuntimeValue();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Includes the DateTime type.
        /// </summary>
        public IQDateTimeType Type { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Allows an element to set it's runtime value if the value picked must be resolved at runtime.
        /// </summary>
        public sealed override void SetRuntimeValue()
        {
            //Sets the element's runtime value if a type other than "custom" is selected.
            switch(Type)
            {
                case IQDateTimeType.Now:
                    Value = DateTime.Now;
                    break;
                case IQDateTimeType.Today:
                    Value = DateTime.Today;
                    break;
                case IQDateTimeType.Yesterday:
                    Value = DateTime.Today.AddDays(-1);
                    break;
                case IQDateTimeType.Tomorrow:
                    Value = DateTime.Today.AddDays(1);
                    break;
                case IQDateTimeType.Other:
                    break;
                default:
                    throw new InvalidOperationException(Type + " is not supported.");
            }
        }

        /// <summary>
        /// Overrides the equal method so that two objects with matching properties will evaluate as equal, even if they
        /// are two separate instances being compared.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var typedObj = obj as IQDateTimeElement;

            if (typedObj == null)
                return false;

            if (Type != typedObj.Type)
                return false;

            return base.Equals(obj);
        }

        /// <summary> Overriden to support the custom equality check for this class. </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode()
                ^ Type.GetHashCode();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns a string representing the state of this object.
        /// </summary>
        /// <returns></returns>
        public string GetDisplayString()
        {
            switch (Type)
            {
                case IQDateTimeType.Now:
                    return Strings.Now;
                case IQDateTimeType.Today:
                    return Strings.Today;
                case IQDateTimeType.Yesterday:
                    return Strings.Yesterday;
                case IQDateTimeType.Tomorrow:
                    return Strings.Tomorrow;
                case IQDateTimeType.Other:
                    return Value.ToShortDateString();
                default:
                    throw new InvalidOperationException(Type + " is not supported.");
            }
        }
        #endregion
    }


    /// <summary> Enumerates various pre-defined date values. </summary>
    public enum IQDateTimeType
    {
        /// <summary> The current DateTime represented by DateTime.Now </summary>
        Now,

        /// <summary> The current DateTime represented by DateTime.Today </summary>
        Today,

        /// <summary> The DateTime of "Yesterday" represented by DateTime.Today.AddDays(-1) </summary>
        Yesterday,

        /// <summary> The DateTime of "Tomorrow" represented by DateTime.Today.AddDays(1) </summary>
        Tomorrow,

        /// <summary> A specific DateTime selected in the UI.</summary>
        Other
    }
}

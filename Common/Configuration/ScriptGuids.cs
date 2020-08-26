using System;

namespace Impac.Mosaiq.IQ.Common.Configuration
{
    /// <summary> Guid values used to identify specific IQ Scripts</summary>
    public static class ScriptGuids
    {
        /// <summary> Guid for eSCRIBE Merge Field - Assessments script</summary>
        public static readonly Guid EScribeMergeFieldAssessments = new Guid("33381130-255E-4B3D-9A2B-8535CF44B199");

        /// <summary> Guid for eSCRIBE Merge Field - Dose Site Summary script</summary>
        public static readonly Guid EScribeMergeFieldDoseSiteSummary = new Guid("C0067CA1-A431-4A4C-B7D1-703FF5ECC430");

        /// <summary> Guid for eSCRIBE Merge Field - Labs</summary>
        public static readonly Guid EScribeMergeFieldLabs = new Guid("0AEC82F8-EBAA-4C15-9EBA-1F5C51F48E1E");

        /// <summary> Guid for eSCRIBE Merge Field - Vitals</summary>
        public static readonly Guid EScribeMergeFieldVitals = new Guid("DC0DD3F3-A035-4B8C-BC36-FB5EE65BFE68");
    }
}

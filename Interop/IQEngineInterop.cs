using System.Runtime.InteropServices;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Core.Framework.Export;

namespace Impac.Mosaiq.IQ.Interop
{
    /// <summary> This class provides methods for calling into the IQ Engine </summary>
    [Guid("6934364C-8469-4800-A276-AA49E4773B58")]
    [ClassInterface(ClassInterfaceType.AutoDual), ComVisible(true), ClarionExport]
    public class IQEngineInterop
    {
        #region Import/Export
        /// <summary>
        /// Method which can be called from clarion which automatically imports all files with the "iq" extension
        /// in the "IQ Scripts" folder in the application directory.
        /// </summary>
        public void ImportIQFilesOnUpgrade()
        {
            ImportExportUtil.ImportAllIQFiles();
            ImportExportUtil.ImportConfiguredAssignments();
        }
        #endregion
    }
}

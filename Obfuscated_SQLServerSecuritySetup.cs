using System;
using System.Diagnostics;

// Just to avoid putting it in a terribly obvious place, this doesn't have an obvious namespace either
// (we retired Impac.Mosaiq.Core.Security.DBConnectionLib actually in favor of Impac.Mosaiq.Core).
namespace Impac.Mosaiq.Core.Security.DBConnectionLib
{
	/// <summary>
	/// If including this file, make sure to reference Impac.Mosaiq.Core.Security.DBConnectionLib.
	/// Please only include this from a unit test harness or .EXE; do not
	/// provide a mechanism to invoke it automatically from a DLL.
	/// Ask Todd C. Gleason for more information about this class.
	/// </summary>
	internal class ObSS
	{
		// This is static to the class, but not static to the process.
		// This is an internal class, so call it wherever in your application and it will
		// only take effect the first time.
		// But, do not call it from multiple DLLs within the same process.
		private static bool _security = false;
		/// <summary>
		/// Constructor.  Performs all necessary setup.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">Indicates
		/// that the necessary setup has already occurred and should not
		/// have been invoked from the project.</exception>
		public ObSS()
		{
			if (!_security)
			{
                // Debug-mode check that this file is in the right path;
                // copies are not allowed!
                // This is not possible to do under Release mode so we do it in Debug only.
                // This requires .PDB files to actually work; otherwise we ignore it.
#if DEBUG
                string myFilename = null;
			    string match = null;
                
                try
                {
                    StackFrame callerStack = new StackFrame(true);

                    // Check for a match between the file path of this file,
                    // and the portion of the file path computed based on the namespace.
                    // It seems that somehow the real filename can be correct, but that the StackFrame may give
                    // a filename with the incorrect case, so we use a case-insensitive match.
                    myFilename = callerStack.GetFileName().ToLower();
                    string nameSpace = GetType().Namespace;
                    match = nameSpace.Substring(nameSpace.IndexOf("Mosaiq")).Replace('.',System.IO.Path.DirectorySeparatorChar).ToLower();
                } 
                catch (Exception)
                {
                    // Ignore it; it just means that user ran in debug mode, but did not have PDB files.
                    // This happens when a DE compiles a debug EXE and sends it (but not the PDB files)
                    // to somebody else.
                }
                
			    if (!string.IsNullOrEmpty(myFilename) && (match != null) && !myFilename.Contains(match))
                {
                    throw new NotSupportedException(
                        string.Format("The ObSS file must not be copied.  It should be linked using Add->Add As Link.  Delete this file and re-add from the {0} path.  See also TRF99002 section 2.1.5.", match));
                }
#endif  // DEBUG

                if (Impac.Mosaiq.Core.Security.DBConnectionLib.ConnectionManager.IsSecondaryKeySet)
				//if (Impac.Mosaiq.Core.Toolbox.ProcessArguments.IsSecondaryKeySet)
				{
					throw new InvalidOperationException("Connection has already been initiated");
				}

				// the stuff here is just a little obfuscation, really the algorithm is this:
				// 1.  Take "C8CF043D-4429-C47B-DEA5-2BF5D2E736FE"
				// 2.  Pass this as a secondary key
				// 3.  Deep down further in the system, it will
				//     replace each "-" with "*IMPACELEKTA*"
				// 3.  This resulting string is then fed as the final secondary key
				//     to the encryption/decryption mechanism.
				// Of course, it won't stop a dedicated hacker, but it should
				// block casual attempts to use IMPAC software from third-party DLLs.

				// put our character strings backwards
				//string secondaryKey2a = "ATKELE";
				//string secondaryKey2b = "CAPMI";
				//string secondaryKey2ab = secondaryKey2a + secondaryKey2b;
				//secondaryKey2a = null;
				//secondaryKey2b = null;
				//char[] secondaryKey2c = secondaryKey2ab.ToCharArray();
				string secondaryKey1 = "EF637E2D5FB2-5AED-B74C-9244-D340FC8C";
				//Array.Reverse(secondaryKey2c);
				//string secondaryKey2d = "*" + new string(secondaryKey2c) + "*";
				//secondaryKey2c = null;
				char[] secondaryKey1b = secondaryKey1.ToCharArray();
				secondaryKey1 = null;
				Array.Reverse(secondaryKey1b);
				//string secondaryKey = new string(secondaryKey1b).Replace("-", secondaryKey2d);
				string secondaryKey = new string(secondaryKey1b);
				secondaryKey1b = null;
				//secondaryKey2d = null;

				//string secondaryKey1 = "C8CF043D-4429-C47B-DEA5-2BF5D2E736FE";
				//string secondaryKey2 = secondaryKey1.Replace("-", "*IMPACELEKTA*");
				//Impac.Mosaiq.Core.Toolbox.ProcessArguments.SecondaryKey = secondaryKey;
				Impac.Mosaiq.Core.Security.DBConnectionLib.ConnectionManager.PrepareSecondaryKey(secondaryKey);

				_security = true;
			}

		}
	}
}
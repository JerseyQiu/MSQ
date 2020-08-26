using System;
using System.Windows.Forms;
using System.IO;

using Impac.Mosaiq.Charting.Documents.DocumentFunctions;
using Impac.Mosaiq.Charting.Documents.EscribeCommon;
using Impac.Mosaiq.IQ.Activities.Variables.Common.Details;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
	#region Activity Class
	/// <summary>
	/// An IQ variable for eScribe template selection
	/// </summary>
	[IQEscribeTemplateVarActivity_DisplayName]
	[GeneralCharting_Category]
	[Variables_ActivityGroup]
	public sealed class IQEscribeTemplateVarActivity : IQVariableActivitySimple<string, IQStringVar, IQStringVarDetail, IQEscribeTemplateVarConfig>
	{
	}
	#endregion

	#region Configuration Class
	/// <summary> The configuration class for the IQVarStringActivity class</summary>
    [Serializable]
    public class IQEscribeTemplateVarConfig : IQVariableConfigSimple<string, IQStringVar, IQStringVarDetail>
    {
		/// <summary>
		/// The root folder for the templates within the Other Files Folder tree
		/// (i.e. this is just a single folder name which does not include the Other_Files_Folder\eScribe path)
		/// </summary>
		[RestoreInclude]
		[ConfigurationActivity_Category]
		[TemplatesRoot_DisplayName]
		public string TemplatesRoot { get; set; }

        /// <summary> Opens the template selection window. </summary>
        /// <returns></returns>
        protected override IQOpResult<string> OpenEditor(string defaultValue, IQVarElementTarget target)
        {
			// other_files_folder\eSCRIBE
        	string otherFilesEscribe = EscribeOperations.EscribeRoot;

			// other_files_folder\eSCRIBE\<TemplatesRoot>
			// default TemplatesRoot to the eScribe templates, i.e. 'WKGroup'
        	string templatesRoot = !String.IsNullOrEmpty(TemplatesRoot)
        	                       	? TemplatesRoot
        	                       	: EscribeConstants.EscribeTemplatesRoot;

			string otherFilesEscribeTemplatesRoot = Path.Combine(otherFilesEscribe, templatesRoot);

        	string selectedTemplate = SelectFileDialog.IsValidTemplate(defaultValue)
        	                          	? Path.Combine(otherFilesEscribe, defaultValue)
        	                          	: defaultValue;

			var editor = new SelectFileDialog(otherFilesEscribeTemplatesRoot, String.Empty, selectedTemplate);
			if (editor.ShowDialog() != DialogResult.OK)
				return new IQOpResult<string> { Result = OpResultEnum.Cancelled };

			// trim the 'other_files_folder\eSCRIBE\' part
			selectedTemplate = SelectFileDialog.TrimTemplateRoot(otherFilesEscribe, editor.SelectedFile);
        	return new IQOpResult<string> {Result = OpResultEnum.Completed, Value = selectedTemplate};
        }
    }
	#endregion
}

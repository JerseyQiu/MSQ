using System;
using System.Windows.Forms;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Details;
using Impac.Mosaiq.IQ.Activities.Variables.Charting.Editors;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.Charting
{
    #region Activity Classs
    /// <summary>
    /// This activity allows the user to select an observation order in the UI and 
    /// outputs the primary id key to the output parameter.
    /// </summary>
    [IQDrugVarActivity_DisplayName]
    [GeneralCharting_Category]
    [Variables_ActivityGroup]
    public sealed class IQDrugVarActivity : IQVariableActivityEntity<int, Drug, IQIntegerVar, IQDrugVarDetail, IQDrugVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> Configuration class for the IQProcedureVarActivity class</summary>
    [Serializable]
    public class IQDrugVarConfig : IQVariableConfigEntity<int, Drug, IQIntegerVar, IQDrugVarDetail>
    {
        /// <summary>
        /// Opens the observation orders selection browse in clarion
        /// </summary>
        /// <returns></returns>
		protected override IQOpResult<int> OpenEditor(int currentValue, IQVarElementTarget target)
        {
        	//OnBeforeClarionEditorInvoke(this);
        	//int prsId = CallClarion.CallDrgBro(true, true);
        	//OnAfterClarionEditorInvoke(this);

        	//If the user hit "close", don't update the primaryKeyValue
        	//return (prsId != 0)
        	//		   ? new IQOpResult<int> { Result = OpResultEnum.Completed, Value = Drug.GetEntityByID(prsId).DRG_ID }
        	//		   : new IQOpResult<int> { Result = OpResultEnum.Cancelled };

        	var editor = new DrugSelector {Value = currentValue};

        	return editor.ShowDialog() == DialogResult.OK
        	       	? new IQOpResult<int> {Result = OpResultEnum.Completed, Value = editor.Value}
        	       	: new IQOpResult<int> {Result = OpResultEnum.Cancelled};
        }

    	/// <summary> Display a observation order's subcategory </summary>
        public override bool ShowCategories
        {
            get { return true; }
        }
    }
    #endregion
}

using System;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.Interop;
using Impac.Mosaiq.IQ.Activities.Variables.PM.Details;
using Impac.Mosaiq.IQ.Common.Variable;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Variables;
using Impac.Mosaiq.IQ.Core.Framework;
using Impac.Mosaiq.IQ.Core.Framework.Variable.Classes;

namespace Impac.Mosaiq.IQ.Activities.Variables.PM
{
    #region Activity Classs
    /// <summary>
    /// This activity allows the user to select an observation order in the UI and 
    /// outputs the primary id key to the output parameter.
    /// </summary>
    [IQProcedureVarActivity_DisplayName]
    [PracticeManagement_Category]
    [Variables_ActivityGroup]
    public sealed class IQProcedureVarActivity : IQVariableActivityEntity<int, Cpt, IQIntegerVar, IQProcedureVarDetail, IQProcAndSuppVarConfig>
    {
    }
    #endregion

    #region Configuration Class
    /// <summary> Configuration class for the IQProcedureVarActivity class</summary>
    [Serializable]
    public class IQProcAndSuppVarConfig : IQVariableConfigEntity<int, Cpt, IQIntegerVar, IQProcedureVarDetail>
    {
        /// <summary>
        /// The root folder for the templates within the Other Files Folder tree
        /// (i.e. this is just a single folder name which does not include the Other_Files_Folder\eScribe path)
        /// </summary>
        [RestoreInclude]
        [ConfigurationActivity_Category]
        [ShowSupplies_DisplayName]
        public bool ShowSupplies { get; set; }

        /// <summary>
        /// Opens the observation orders selection browse in clarion
        /// </summary>
        /// <returns></returns>
        protected override IQOpResult<int> OpenEditor(int defaultValue, IQVarElementTarget target)
        {
            OnBeforeClarionEditorInvoke(this);

            int prsId;
            if (ShowSupplies)
                prsId = CallClarion.CallCodeBro(string.Empty, true, true, 0);
            else
                prsId = CallClarion.CallActivityBro(string.Empty, string.Empty, true, false, false, true, string.Empty, string.Empty, false, true, true);

            OnAfterClarionEditorInvoke(this);

            //If the user hit "close", don't update the primaryKeyValue
            return (prsId != 0)
                       ? new IQOpResult<int> { Result = OpResultEnum.Completed, Value = Cpt.GetEntityByID(prsId).PRS_ID}
                       : new IQOpResult<int> { Result = OpResultEnum.Cancelled };
        }
        
        /// <summary> Display a observation order's subcategory </summary>
        public override bool ShowCategories
        {
            get { return false; }
        }
    }
    #endregion
}

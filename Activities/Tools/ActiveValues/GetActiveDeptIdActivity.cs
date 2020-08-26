using System.Activities;
using Impac.Mosaiq.Core.Globals;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.ActiveValues
{
    /// <summary>
    /// Gets Active Department ID
    /// </summary>
    [Tools_ActivityGroup]
    [ActiveValue_Category]
    [GetActiveDeptIdActivity_DisplayName]
    public class GetActiveDeptIdActivity : MosaiqCodeActivity
    {
        #region Properties (Output Arugments)
        /// <summary> Active Department Id. </summary>
        [OutputParameterCategory]
        [GetActiveDeptIdActivity_DeptId_DisplayName]
        [GetActiveDeptIdActivity_DeptId_Description]
        public OutArgument<int> DeptId { get; set; }
        #endregion

        /// <summary>
        /// Returns the Department ID
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            DeptId.Set(context, Global.StaffInstID);
        }
    }
}

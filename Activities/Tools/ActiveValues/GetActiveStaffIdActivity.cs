using System.Activities;
using Impac.Mosaiq.Core.Globals;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.ActiveValues
{
    /// <summary>
    /// Gets Active Staff ID
    /// </summary>
    [Tools_ActivityGroup]
    [ActiveValue_Category]
    [GetActiveStaffIdActivity_DisplayName]
    public class GetActiveStaffIdActivity : MosaiqCodeActivity
    {
        #region Properties (Output Arugments)
        /// <summary> Active Staff Id, if the value is less then 1 then it is null. </summary>
        [OutputParameterCategory]
        [GetActiveStaffIdActivity_StaffId_DisplayName]
        [GetActiveStaffIdActivity_StaffId_Description]
        public OutArgument<int?> StaffId { get; set; }
        #endregion

        /// <summary>
        /// Returns the StaffId for the active user, null otherwise
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            StaffId.Set(context, Global.StaffId > 0 ? (int?)Global.StaffId : null);
        }
    }
}

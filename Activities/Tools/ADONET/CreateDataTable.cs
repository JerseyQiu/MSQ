using System.Activities;
using System.Data;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.ADONET
{
    /// <summary>
    /// Creates a new ADO.NET DataTable
    /// </summary>
    [CreateDataTable_DisplayName]
    [ADONet_Category]
    [Tools_ActivityGroup]
    public class CreateTable : MosaiqCodeActivity 
    {
        /// <summary> Name of the DataTable </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [DataTableName_DisplayName]
        [DataTableName_Description]
        public InArgument<string> TableName { get; set; }

        /// <summary> DataTable created </summary>
        [OutputParameterCategory]
        [RequiredArgument]
        [DataTable_DisplayName]
        [DataTable_Description]
        public OutArgument<DataTable> Result { get; set; }

        /// <summary> Creates a new DataTable object. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            Result.Set(context, new DataTable(TableName.Get(context)));
        }
    }
}
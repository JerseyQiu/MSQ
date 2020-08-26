using System.Activities;
using System.Data;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.ADONET
{
    /// <summary>
    /// Add new DataRow to the given DataTable
    /// </summary>
    [AddDataRow_DisplayName]
    [ADONet_Category]
    [Tools_ActivityGroup]
    public class AddDataRow : MosaiqCodeActivity 
    {
        /// <summary> DataTable to add a row </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [DataTable_DisplayName]
        [DataTable_Description]
        public InArgument<DataTable> TargetTable { get; set; }

        /// <summary> DataRow to be added </summary>
        [InputParameterCategory]
        [RequiredArgument]
        [DataRow_DisplayName]
        [DataRow_Description]
        public InArgument<DataRow> Row { get; set; }

        /// <summary> Return DataTable </summary>
        [OutputParameterCategory]
        [RequiredArgument]
        [DataTable_DisplayName]
        [DataTable_Description]
        public OutArgument<DataTable> Result { get; set; }

        /// <summary> Creates a new DataTable object. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            DataTable table = TargetTable.Get(context);
            table.Rows.Add(Row.Get(context));
                
            Result.Set(context, table);
        }
    }
}
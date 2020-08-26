using System.Activities;
using System.ComponentModel;
using System.Data;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.ADONET
{
    /// <summary>
    /// Add a new column to the given table.
    /// </summary>
    [AddDataColumn_DisplayName]
    [ADONet_Category]
    [Tools_ActivityGroup]
    public class AddDataColumn : MosaiqCodeActivity 
    {
        /// <summary>DataTable to add a new column</summary>
        [InputParameterCategory]
        [RequiredArgument]
        [DataTable_DisplayName]
        [DataTable_Description]
        public InArgument<DataTable> TargetTable { get; set; }

        /// <summary>Name of the new column</summary>
        [InputParameterCategory]
        [RequiredArgument]
        [ColumnName_DisplayName]
        [ColumnName_Description]
        public InArgument<string> ColumnName { get; set; }

        /// <summary>DataType of the new column</summary>
        [InputParameterCategory]
        [RequiredArgument]
        [ColumnDataType_DisplayName]
        [ColumnDataType_Description]
        public InArgument<System.Type> ColumnType { get; set; }

        /// <summary>Return DataTable</summary>
        [OutputParameterCategory]
        [RequiredArgument]
        [DataTable_DisplayName]
        [DataTable_Description]
        public OutArgument<DataTable> Result { get; set; }

        /// <summary> Performs the query requested. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            DataTable table = TargetTable.Get(context);
            DataColumn column = new DataColumn();
            column.DataType = ColumnType.Get(context);
            column.ColumnName = ColumnName.Get(context);
            table.Columns.Add(column);

            Result.Set(context, table);
        }
    }
}
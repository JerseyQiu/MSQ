using System.Activities;
using System.Data;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.ADONET
{
    /// <summary>
    /// Assign value to the column of the given DataRow
    /// </summary>
    [AssignColumnValue_DisplayName]
    [ADONet_Category]
    [Tools_ActivityGroup]
    public class AssignColumnValue : MosaiqCodeActivity 
    {
        /// <summary>DataRow to add a new column</summary>
        [InputParameterCategory]
        [RequiredArgument]
        [DataRow_DisplayName]
        [DataRow_Description]
        public InArgument<DataRow> TargetRow { get; set; }

        /// <summary>Name of the new column</summary>
        [InputParameterCategory]
        [RequiredArgument]
        [ColumnName_DisplayName]
        [ColumnName_Description]
        public InArgument<string> ColumnName { get; set; }

        /// <summary>Value to be assigned</summary>
        [InputParameterCategory]
        [RequiredArgument]
        [DataValue_DisplayName]
        [DataValue_Description]
        public InArgument<object> Value { get; set; }

        /// <summary> Return DataRow </summary>
        [OutputParameterCategory]
        [RequiredArgument]
        [DataRow_DisplayName]
        [DataRow_Description]
        public OutArgument<DataRow> Result { get; set; }

        /// <summary> Performs the query requested. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            DataRow row = TargetRow.Get(context);
            string column = ColumnName.Get(context);
            object value = Value.Get(context);

            if (value != null)
                row[column] = value;
            else
                row[column] = System.DBNull.Value;
        }
    }
}
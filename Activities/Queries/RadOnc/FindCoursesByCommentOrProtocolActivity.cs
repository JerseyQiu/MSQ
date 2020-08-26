using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using System;

namespace Impac.Mosaiq.IQ.Activities.Queries.RadOnc
{

    /// <summary>
    /// Returns a list of Course (Care Plan) objects from the database.
    /// </summary>
    [FindCoursesByCommentOrProtocolActivity_DisplayName]
    [RadiationOncology_Category]
    [Queries_ActivityGroup]
    public class FindCoursesByCommentOrProtocolActivity : MosaiqQueryListActivity<Course>
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The primary key of the patient to query. 
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindCoursesByCommentOrProtocolActivity_PatId1_DisplayName]
        [FindCoursesByCommentOrProtocolActivity_PatId1_Description]
        public InArgument<int> PatId1 { get; set; }

        /// <summary> 
        /// The string to query for in the Comment field of the Course 
        /// </summary>
        [InputParameterCategory]
        [FindCoursesByCommentOrProtocolActivity_CommentFieldString_DisplayName]
        [FindCoursesByCommentOrProtocolActivity_CommentFieldString_Description]
        public InArgument<string> CommentFieldString { get; set; }

        /// <summary> 
        /// The string to query for in the Protocol field of the Course
        /// </summary>
        [InputParameterCategory]
        [FindCoursesByCommentOrProtocolActivity_ProtocolFieldString_DisplayName]
        [FindCoursesByCommentOrProtocolActivity_ProtocolFieldString_Description]
        public InArgument<string> ProtocolFieldString { get; set; }

        /// <summary> 
        /// Logical operator on how to combine the CommentFieldString and ProtocolFieldString 
        /// in case both are supplied. Default is "OR".
        /// </summary>
        [InputParameterCategory]
        [FindCoursesByCommentOrProtocolActivity_LogicalOperator_DisplayName]
        [FindCoursesByCommentOrProtocolActivity_LogicalOperator_Description]
        public InArgument<EntityBooleanOp> LogicalOperator { get; set; }
        #endregion
 
        #region Overriden Methods
        /// <summary> Populates a query object created by the base class. </summary>
        /// <param name="context"></param>
        /// <param name="pQuery"></param>
        protected override void PopulateQuery(CodeActivityContext context, ImpacRdbQuery pQuery)
        {
            // Activity inputs
            int patId1 = PatId1.Get(context);
            string commentFieldString =  CommentFieldString.Get(context);
            string protocolFieldString = ProtocolFieldString.Get(context);
            EntityBooleanOp logicalOperator = ((LogicalOperator.Expression != null)
                                                  ? LogicalOperator.Get(context)
                                                   : EntityBooleanOp.Or) ?? EntityBooleanOp.Or;

            // The logical NOT operator is not supported, because the meaning of the result is vague.
            if (logicalOperator == EntityBooleanOp.Not)
            {
                string message = String.Format("The Not value of the logical operator is not supported by the {0}", 
                                               GetType().Name);

                throw new ArgumentException(message);
            }

            // Build the query
            pQuery.AddClause(Course.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);
            if ((commentFieldString == null) && (protocolFieldString == null))
            {
                // Don't want to change the base class to handle "empty" queries, so just perform a ridiculous query that will return 
                // an empty result (in case none of the string identifiers have been supplied)
                pQuery.AddClause(Course.NotesEntityColumn.ColumnName, EntityQueryOp.EQ,
                                 "Dummy string to force empty result!");
            }
            else
            {
                if (commentFieldString != null)
                {
                    if (commentFieldString == string.Empty)
                    {
                        pQuery.AddClause(Course.NotesEntityColumn, EntityQueryOp.EQ, string.Empty);
                    }
                    else
                    {
                        pQuery.AddClause(Course.NotesEntityColumn, EntityQueryOp.Contains, commentFieldString);
                    }
                }
                if (protocolFieldString != null)
                {
                    if (protocolFieldString == string.Empty)
                    {
                        pQuery.AddClause(Course.ProtocolEntityColumn, EntityQueryOp.EQ, string.Empty);
                    }
                    else
                    {
                        pQuery.AddClause(Course.ProtocolEntityColumn, EntityQueryOp.Contains, protocolFieldString);
                    }
                }
                if ((commentFieldString != null) && (protocolFieldString != null))
                {
                    pQuery.AddOperator(logicalOperator);
                }
            }
        }
        #endregion
    }
}

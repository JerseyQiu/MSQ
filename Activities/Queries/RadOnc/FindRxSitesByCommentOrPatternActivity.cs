using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Queries.RadOnc
{
    
    /// <summary>
    /// Returns a list of prescription site objects from the database.
    /// </summary>
    [FindRxSitesByCommentOrPatternActivity_DisplayName]
    [RadiationOncology_Category]
    [Queries_ActivityGroup]
    public class FindRxSitesByCommentOrPatternActivity : MosaiqQueryListActivity<PrescriptionSite>
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The primary key of the patient to query for.
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindRxSitesByCommentOrPatternActivity_PatId1_DisplayName]
        [FindRxSitesByCommentOrPatternActivity_PatId1_Description]
        public InArgument<int> PatId1 { get; set; }

        /// <summary> 
        /// The string to query for in the Comment field of the Rad Rx 
        /// </summary>
        [InputParameterCategory]
        [FindRxSitesByCommentOrPatternActivity_CommentFieldString_DisplayName]
        [FindRxSitesByCommentOrPatternActivity_CommentFieldString_Description]
        public InArgument<string> CommentFieldString { get; set; }

        /// <summary> 
        /// The string to query for in the Pattern field of the Rad Rx 
        /// </summary>
        [InputParameterCategory]
        [FindRxSitesByCommentOrPatternActivity_PatternFieldString_DisplayName]
        [FindRxSitesByCommentOrPatternActivity_PatternFieldString_Description]
        public InArgument<string> PatternFieldString { get; set; }

        /// <summary> 
        /// The logical operator in case both Comment and Pattern are supplied.
        /// Default = OR 
        /// </summary>
        [InputParameterCategory]
        [FindRxSitesByCommentOrPatternActivity_LogicalOperator_DisplayName]
        [FindRxSitesByCommentOrPatternActivity_LogicalOperator_Description]
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
            string commentFieldString = CommentFieldString.Get(context);
            string patternFieldString = PatternFieldString.Get(context);
            EntityBooleanOp logicalOperator = ((LogicalOperator.Expression != null)
                                                  ? LogicalOperator.Get(context)
                                                   : EntityBooleanOp.Or) ?? EntityBooleanOp.Or;

            // Build the query
            pQuery.AddClause(PrescriptionSite.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);
            pQuery.AddClause(PrescriptionSite.VersionEntityColumn, EntityQueryOp.EQ, 0); 
            if ((commentFieldString == null) && (patternFieldString == null))
            {
                // Don't want to change the base class to handle "empty" queries, so just perform a ridiculous query that will return 
                // an empty result (in case none of the string identifiers have been supplied)
                pQuery.AddClause(PrescriptionSite.PrescriptionDescriptionEntityColumn.ColumnName,
                                 EntityQueryOp.EQ, "Dummy string to force empty result!");

            }
            else
            {
                if (commentFieldString != null)
                {
                    if (commentFieldString == string.Empty)
                    {
                        pQuery.AddClause(PrescriptionSite.PrescriptionDescriptionEntityColumn, EntityQueryOp.EQ, string.Empty);
                    }
                    else
                    {
                        pQuery.AddClause(PrescriptionSite.PrescriptionDescriptionEntityColumn, EntityQueryOp.Contains, commentFieldString);
                    }

                }
                if (patternFieldString != null)
                {
                    if (patternFieldString == string.Empty)
                    {
                        pQuery.AddClause(PrescriptionSite.Frac_PatternEntityColumn, EntityQueryOp.EQ, string.Empty);
            }
                    else
            {
                        pQuery.AddClause(PrescriptionSite.Frac_PatternEntityColumn, EntityQueryOp.Contains, patternFieldString);
                    }
            }
                if ((commentFieldString != null) && (patternFieldString != null))
            {
                pQuery.AddOperator(logicalOperator);

                }
            }
            pQuery.AddOrderBy(PrescriptionSite.DisplaySequenceEntityColumn);
        }       
        #endregion
    }
}

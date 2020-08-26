using System.Activities;
using IdeaBlade.Persistence;
using IdeaBlade.Persistence.Rdb;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;
using System;

namespace Impac.Mosaiq.IQ.Activities.Tools.Database
{
    /// <summary>
    /// Performs a sql query which returns multiple records.  The query must return data elements which match the entity type selected
    /// as the generic paramter used.
    /// </summary>
    /// <typeparam name="T">The type of entity returned by the query.</typeparam>
    [GetEntitiesSqlActivity_DisplayName]
    [Database_Category]
    [Tools_ActivityGroup]
    public class GetEntitiesSqlActivity<T> : MosaiqCodeActivity where T : Entity
    {
        /// <summary>
        /// The persistence manager used to run the query.  If no persistence manager is supplied, the activity will create and use
        /// a new one.
        /// </summary>
        [InputParameterCategory]
        [PersistenceManager_DisplayName]
        [PersistenceManager_Description]
        public InArgument<ImpacPersistenceManager> PersistenceManager { get; set; }

        /// <summary>The query to be executed</summary>
        [InputParameterCategory]
        [RequiredArgument]
        [Query_DisplayName]
        [Query_Description]
        public InArgument<string> Query { get; set; }

        /// <summary>The query strategy to be used.  If no query strategy is provided, QueryStrategy.Normal will be used.</summary>
        [InputParameterCategory]
        [QueryStrategy_DisplayName]
        [QueryStrategy_Description]
        public InArgument<QueryStrategy> Strategy { get; set; }

        /// <summary>The result of the query.</summary>
        [OutputParameterCategory]
        [QueryResult_DisplayName]
        [QueryResult_Description]
        public OutArgument<EntityList<T>> Result { get; set; }

        /// <summary> Performs the query requested. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            //Get argument values.
            ImpacPersistenceManager pm = PersistenceManager.Expression != null
                                             ? PersistenceManager.Get(context)
                                             : PM;

            QueryStrategy strategy = Strategy.Get(context) ?? QueryStrategy.Normal;
            string sqlQuery = Query.Get(context);
            //Defect-11131：Medication field in eScribe get error when the PC Regional Settings are changed to DD/MM/YY--Check the MAX date format, if exist possible issue format, change the format to standard format.
            if ((sqlQuery.IndexOf(DateTime.MaxValue.ToString("dd/MM/yyyy")) > 0) || (sqlQuery.IndexOf(DateTime.MaxValue.ToString("MM/dd/yyyy")) > 0))
            {
                if (sqlQuery.IndexOf(DateTime.MaxValue.ToString("dd/MM/yyyy")) > 0) sqlQuery = sqlQuery.Replace(DateTime.MaxValue.ToString("dd/MM/yyyy"), DateTime.MaxValue.ToString("yyyy/MM/dd"));
                if (sqlQuery.IndexOf(DateTime.MaxValue.ToString("MM/dd/yyyy")) > 0) sqlQuery = sqlQuery.Replace(DateTime.MaxValue.ToString("MM/dd/yyyy"), DateTime.MaxValue.ToString("yyyy/MM/dd"));
            }
            //Create and execute the query
            var passthroughQuery = new PassthruRdbQuery(typeof (T), sqlQuery);
            EntityList<T> entities = pm.GetEntities<T>(passthroughQuery, strategy);

            //Assign the result set.
            Result.Set(context, entities);
        }
    }
}
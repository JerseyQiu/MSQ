using System.Activities;
using IdeaBlade.Persistence;
using IdeaBlade.Persistence.Rdb;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.Database
{
    /// <summary>
    /// Performs a query that returns a single entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [GetEntitySqlActivity_DisplayName]
    [Database_Category]
    [Tools_ActivityGroup]
    public class GetEntitySqlActivity<T> : MosaiqCodeActivity where T : Entity
    {
        /// <summary>
        /// The persistence manager used to run the query.  If no persistence manager is supplied, the activity will create and use
        /// a new one.
        /// </summary>
        [InputParameterCategory]
        [PersistenceManager_DisplayName]
        [PersistenceManager_Description]
        public InArgument<ImpacPersistenceManager> PersistenceManager { get; set; }

        /// <summary>The query to be executed.  This query must return a single value.</summary>
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
        public OutArgument<T> Result { get; set; }

        /// <summary>
        /// Performs the query.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            //Get arguments
            //Get argument values.
            ImpacPersistenceManager pm = PersistenceManager.Expression != null
                                             ? PersistenceManager.Get(context)
                                             : PM;

            QueryStrategy strategy = Strategy.Get(context) ?? QueryStrategy.Normal;
            string sqlQuery = Query.Get(context);

            //Run query and set results.
            var passthroughQuery = new PassthruRdbQuery(typeof (T), sqlQuery);
            var entity = pm.GetEntity<T>(passthroughQuery, strategy);
            Result.Set(context, entity);
        }
    }
}
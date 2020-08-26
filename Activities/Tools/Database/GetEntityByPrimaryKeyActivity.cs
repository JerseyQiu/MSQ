using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.Database
{
    /// <summary>
    /// Performs a sql query which looks up a record by it's primary key.
    /// </summary>
    /// <typeparam name="T">The type of entity returned by the query.</typeparam>
    [GetEntityByPrimaryKeyActivity_DisplayName]
    [Database_Category]
    [Tools_ActivityGroup]
    public class GetEntityByPrimaryKeyActivity<T> : MosaiqCodeActivity where T : Entity
    {
        /// <summary>
        /// The persistence manager used to run the query.  If no persistence manager is supplied, the activity will create and use
        /// a new one.
        /// </summary>
        [InputParameterCategory]
        [PersistenceManager_DisplayName]
        [PersistenceManager_Description]
        public InArgument<ImpacPersistenceManager> PersistenceManager { get; set; }

        /// <summary>The primary key value which will be passed to the query.</summary>
        [InputParameterCategory]
        [RequiredArgument]
        [PrimaryKey_DisplayName]
        [PrimaryKey_Description]
        public InArgument<object> PrimaryKey { get; set; }

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

        /// <summary> Performs the query requested. </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            //Get argument values
            //Get argument values.
            ImpacPersistenceManager pm = PersistenceManager.Expression != null
                                             ? PersistenceManager.Get(context)
                                             : PM;

            QueryStrategy strategy = Strategy.Get(context) ?? QueryStrategy.Normal;
            object key = PrimaryKey.Get(context);

            //Execute the query and return the result.
            Entity entity = pm.GetEntity<T>(new PrimaryKey(typeof (T), key), strategy);
            Result.Set(context, entity);
        }
    }
}
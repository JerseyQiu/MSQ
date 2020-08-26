using System.Activities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Definitions.Attributes;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Tools.Database
{
    /// <summary>
    /// Creates a new persistence manager which can be shared by various GetEntity activities.
    /// </summary>
    [GetPersistenceManagerActivity_DisplayName]
    [Database_Category]
    [Tools_ActivityGroup]
    public class GetPersistenceManagerActivity : MosaiqCodeActivity
    {
        /// <summary>The persistence manager created.</summary>
        [OutputParameterCategory]
        [PersistenceManager_DisplayName]
        [GetPersistenceManagerActivity_PersistenceManager_Description]
        public OutArgument<ImpacPersistenceManager> PersistenceManager { get; set; }

        /// <summary>
        /// Creates a new persistence manager and assigns it to the output argument.
        /// </summary>
        /// <param name="context"></param>
        protected override void DoWork(CodeActivityContext context)
        {
            //Set the output to a new persistence manager
            PersistenceManager.Set(context, ImpacPersistenceManagerFactory.CreatePersistenceManager());
        }
    }
}
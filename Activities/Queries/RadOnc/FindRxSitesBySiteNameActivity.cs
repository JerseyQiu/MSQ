using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Queries.RadOnc
{
    /// <summary>
    /// Returns a list of Site objects from the database.
    /// </summary>
    [FindRxSitesBySiteNameActivity_DisplayName]
    [RadiationOncology_Category]
    [Queries_ActivityGroup]
    public class FindRxSitesBySiteNameActivity : MosaiqQueryListActivity<PrescriptionSite>
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The primary key of the patient of which to return the Site objects from the database. 
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindRxSitesBySiteNameActivity_PatId1_DisplayName]
        [FindRxSitesBySiteNameActivity_PatId1_Description]
        public InArgument<int> PatId1 { get; set; }

        /// <summary> 
        /// The Site Name to query for in the database
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindRxSitesBySiteNameActivity_SiteName_DisplayName]
        [FindRxSitesBySiteNameActivity_SiteName_Description]
        public InArgument<string> SiteName { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Populates a query object created by the base class. </summary>
        /// <param name="context"></param>
        /// <param name="pQuery"></param>
        protected override void PopulateQuery(CodeActivityContext context, ImpacRdbQuery pQuery)
        {
            // Activity inputs
            int patId1 = PatId1.Get(context);
            string siteName = SiteName.Get(context);

            // Build the query
            pQuery.AddClause(PrescriptionSite.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);
            pQuery.AddClause(PrescriptionSite.VersionEntityColumn, EntityQueryOp.EQ, 0);
            if (siteName == null)
            {
                pQuery.AddClause(PrescriptionSite.SiteNameEntityColumn.ColumnName, EntityQueryOp.EQ,
                                 "Dummy string to force empty result!");
            }
            else
            {
                // Keep string.Empty in just for consistency since Site Name cannot be an empty string
                if (siteName == string.Empty)
                {
                    pQuery.AddClause(PrescriptionSite.SiteNameEntityColumn, EntityQueryOp.EQ, string.Empty);
                }
                else
                {
                    pQuery.AddClause(PrescriptionSite.SiteNameEntityColumn, EntityQueryOp.Contains, siteName);
        }

    }
            pQuery.AddOrderBy(PrescriptionSite.DisplaySequenceEntityColumn);
}
        #endregion
    }
}

using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Queries.RadOnc
{
    /// <summary>
    /// Returns a list of Dose Tracking Sites items from the database.
    /// </summary>
    [FindDoseSiteCoefficientByRegIdActivity_DisplayName]
    [RadiationOncology_Category]
    [Queries_ActivityGroup]
    public class FindDoseSiteCoefficientByRegIdActivity : MosaiqQueryListActivity<Coeff>
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The primary key of the Patient of which to return the Dose Tracking Sites
        /// from the database. 
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindDoseSiteCoefficientByRegIdActivity_PatId1_DisplayName]
        [FindDoseSiteCoefficientByRegIdActivity_PatId1_Description]
        public InArgument<int> PatId1 { get; set; }

        /// <summary> 
        /// The primary key of the Patient of which to return the Dose Tracking Sites
        /// from the database. 
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindDoseSiteCoefficientByRegIdActivity_RegId_DisplayName]
        [FindDoseSiteCoefficientByRegIdActivity_RegId_Description]
        public InArgument<int> RegId { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Populates a query object created by the base class. </summary>
        /// <param name="context"></param>
        /// <param name="pQuery"></param>
        protected override void PopulateQuery(CodeActivityContext context, ImpacRdbQuery pQuery)
        {
            // Activity input
            int patId1 = PatId1.Get(context);
            int regId = RegId.Get(context);

            // Build the query
            pQuery.AddClause(Coeff.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);
            pQuery.AddClause(Coeff.REG_IDEntityColumn, EntityQueryOp.EQ, regId);


        }
        #endregion
    }
}

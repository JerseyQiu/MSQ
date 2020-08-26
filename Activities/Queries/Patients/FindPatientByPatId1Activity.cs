using System.Activities;
using IdeaBlade.Persistence;
using Impac.Mosaiq.BOM.Entities;
using Impac.Mosaiq.BOM.SupportLib;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
using Impac.Mosaiq.IQ.Core.Framework.Activities;

namespace Impac.Mosaiq.IQ.Activities.Queries.Patients
{
    /// <summary>
    /// Returns a patient object from the database.
    /// </summary>
    [Queries_ActivityGroup]
    [FindPatientByPatId1Activity_DisplayName]
    [Patient_Category]
    public class FindPatientByPatId1Activity : MosaiqQueryActivity<Patient>
    {
        #region Properties (Input Parameters)
        /// <summary> 
        /// The primary key of the patient object to return from the database. 
        /// </summary>
        [RequiredArgument]
        [InputParameterCategory]
        [FindPatientByPatId1Activity_PatId1_DisplayName]
        [FindPatientByPatId1Activity_PatId1_Description]
        public InArgument<int> PatId1 { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary> Populates a query object created by the base class. </summary>
        /// <param name="context"></param>
        /// <param name="pQuery"></param>
        protected override void PopulateQuery(CodeActivityContext context, ImpacRdbQuery pQuery)
        {
            int patId1 = PatId1.Get(context);
            pQuery.AddClause(Patient.Pat_ID1EntityColumn, EntityQueryOp.EQ, patId1);
        }
        #endregion
    }
}

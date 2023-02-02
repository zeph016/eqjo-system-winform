using FGCIJOROSystem.Domain.JobOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.JORepo
{
    public class JOMechTransactionLogRepository : IRepository<clsJOMechTransactionLogs>
    {
        public void Add(clsJOMechTransactionLogs obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[JOMechanicTransLogs]
                                       ([JOTransLogId]
                                       ,[JOMechanicId]
                                       ,[JOId]
                                       ,[Tag]
                                       ,[MLMechanicsId]
                                       ,[EmployeeType]
                                       ,[IsActive])
                                 VALUES
                                       (@JOTransLogId
                                       ,@JOMechanicId
                                       ,@JOId
                                       ,@Tag
                                       ,@EmployeeId
                                       ,@EmploymentType
                                       ,@IsActive)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsJOMechTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(clsJOMechTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public List<clsJOMechTransactionLogs> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsJOMechTransactionLogs FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsJOMechTransactionLogs> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOMechTransactionLogs> List = new List<clsJOMechTransactionLogs>();
                String query = @"SELECT JM.[Id]
                                      ,JM.[JOId]
                                      ,JM.[Tag]
                                      ,JM.[MLMechanicsId]
                                      ,P.PositionName
	                                  ,GI.FirstName
	                                  ,GI.MiddleName
	                                  ,GI.LastName
	                                  ,GI.NameExtension
                                      ,JM.IsActive
                                  FROM [dbo].[JOMechanicTransLogs] AS JM
                                  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = JM.MLMechanicsId
								  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
								  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS P ON P.Id = EI.PositionsId" + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOMechTransactionLogs>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

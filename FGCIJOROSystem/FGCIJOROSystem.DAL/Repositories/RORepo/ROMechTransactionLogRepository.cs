using FGCIJOROSystem.Domain.RescueOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.RORepo
{
    public class ROMechTransactionLogRepository : IRepository<clsROMechTransactionLogs>
    {
        public void Add(clsROMechTransactionLogs obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ROMechanicTransLogs]
                                       ([ROTransLogId]
                                       ,[ROMechanicId]
                                       ,[ROId]
                                       ,[Tag]
                                       ,[MLMechanicsId]
                                       ,[EmployeeType]
                                       ,[IsActive])
                                 VALUES
                                       (@ROTransLogId
                                       ,@ROMechanicId
                                       ,@ROId
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

        public void Update(clsROMechTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(clsROMechTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public List<clsROMechTransactionLogs> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsROMechTransactionLogs FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsROMechTransactionLogs> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsROMechTransactionLogs> List = new List<clsROMechTransactionLogs>();
                String query = @"SELECT RM.[Id]
                                      ,RM.[ROId]
                                      ,RM.[Tag]
                                      ,RM.EmployeeType As EmploymentType
                                      ,RM.[MLMechanicsId] As EmployeeId
                                      ,P.PositionName
	                                  ,GI.FirstName
	                                  ,GI.MiddleName
	                                  ,GI.LastName
	                                  ,GI.NameExtension
                                  FROM [dbo].[ROMechanicTransLogs] AS RM
                                  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = RM.MLMechanicsId
								  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
								  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS P ON P.Id = EI.PositionsId " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsROMechTransactionLogs>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

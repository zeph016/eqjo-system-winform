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
    public class ROMechanicRepository : IRepository<clsROMechanics>
    {
        public void Add(clsROMechanics obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ROMechanics]
                                       ([ROId]
                                       ,[Tag]
                                       ,[MLMechanicsId]
                                       ,[EmployeeType],IsActive)
                                 VALUES
                                       (@ROId
                                       ,@Tag
                                       ,@EmployeeId
                                       ,@EmploymentType,@IsActive)
                                 SELECT SCOPE_IDENTITY() As Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                obj.Id = returnId;
                connection.Close();
            }
        }

        public void Update(clsROMechanics obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ROMechanics]
                                   SET [ROId] = @ROId
                                      ,[Tag] = @Tag
                                      ,[MLMechanicsId] = @EmployeeId
                                      ,[EmployeeType] = @EmploymentType
                                      ,IsActive = @IsActive
                                 WHERE Id = @Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsROMechanics obj)
        {
            throw new NotImplementedException();
        }

        public List<clsROMechanics> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsROMechanics FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsROMechanics> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsROMechanics> List = new List<clsROMechanics>();
                String query = @"SELECT RM.[Id]
                                      ,RM.[ROId]
                                      ,RM.[Tag]
                                      ,RM.[MLMechanicsId] As EmployeeId
                                      ,P.PositionName
	                                  ,GI.FirstName
	                                  ,GI.MiddleName
	                                  ,GI.LastName
	                                  ,GI.NameExtension
                                      ,P.PositionName As Position
                                      ,RM.IsActive
                                  FROM [dbo].[ROMechanics] AS RM                                  
                                  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = RM.MLMechanicsId
								  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
								  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS P ON P.Id = EI.PositionsId  " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsROMechanics>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

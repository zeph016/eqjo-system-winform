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
    public class JOMechanicRepository : IRepository<clsMechanics>
    {

        public void Add(clsMechanics obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[JOMechanics]
                                       ([JOId]
                                       ,[Tag]
                                       ,[MLMechanicsId]
                                        ,[EmployeeType],IsActive)
                                 VALUES
                                       (@JOId
                                       ,@Tag
                                       ,@EmployeeId
                                       ,@EmploymentType,@IsActive)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsMechanics obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JOMechanics]
                                   SET [JOId] = @JOId
                                      ,[Tag] = @Tag
                                      ,[MLMechanicsId] = @EmployeeId
                                      ,[EmployeeType] = @EmploymentType
                                      ,IsActive = @IsActive
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsMechanics obj)
        {
            throw new NotImplementedException();
        }

        public List<clsMechanics> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsMechanics> List = new List<clsMechanics>();
                String query = @"SELECT JM.[Id]
                                      ,JM.[JOId]
                                      ,JM.[Tag]
                                      ,JM.[MLMechanicsId] As EmployeeId
	                                  ,GI.FirstName
	                                  ,GI.MiddleName
	                                  ,GI.LastName
	                                  ,GI.NameExtension
                                      ,JM.IsActive
                                      ,P.PositionName As Position
                                  FROM [dbo].[JOMechanics] AS JM
                                  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = JM.MLMechanicsId
								  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
								  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS P ON P.Id = EI.PositionsId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsMechanics>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public clsMechanics FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsMechanics> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsMechanics> List = new List<clsMechanics>();
                String query = @"SELECT JM.[Id]
                                      ,JM.[JOId]
                                      ,JM.[Tag]
                                      ,JM.[MLMechanicsId] As EmployeeId
									  ,P.PositionName
	                                  ,GI.FirstName
	                                  ,GI.MiddleName
	                                  ,GI.LastName
	                                  ,GI.NameExtension
                                      ,JM.IsActive
                                      ,P.PositionName As Position
                                  FROM [dbo].[JOMechanics] AS JM
                                  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = JM.MLMechanicsId
								  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
								  LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS P ON P.Id = EI.PositionsId " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsMechanics>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

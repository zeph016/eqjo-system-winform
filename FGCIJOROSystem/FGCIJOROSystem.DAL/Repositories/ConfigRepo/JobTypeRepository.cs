using FGCIJOROSystem.Domain.Configurations.JobType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
    public class JobTypeRepository : IRepository<clsJobType>
    {
        public void Add(clsJobType obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[JobTypes]
                                                   ([Name]
                                                   ,[Description]
                                                   ,[Active])
                                             VALUES
                                                   (@JobTypeName
                                                   ,@JobTypeDescription
                                                   ,@IsActive)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsJobType obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JobTypes]
                                           SET [Name] = @JobTypeName
                                              ,[Description] = @JobTypeDescription
                                              ,[Active] = @IsActive
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsJobType obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                string query = @"UPDATE [dbo].[JobTypes]
                               SET Active ='0'
                             WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsJobType> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJobType> Lists = new List<clsJobType>();
                String query = @"SELECT [Id]
                                          ,[Name] as JobTypeName
                                          ,[Description] as JobTypeDescription
                                          ,[Active] As IsActive
                                      FROM [dbo].[JobTypes]
									  where Active = 1";
                
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsJobType>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsJobType FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsJobType List = new clsJobType();
                String query = "";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJobType>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        public List<clsJobType> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJobType> Lists = new List<clsJobType>();
                String query = "";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsJobType>(query, new { whereQuery = whereQuery }).ToList();
                connection.Close();
                return Lists;
            }
        }

        public List<clsJobType> GetAllActiveJobTypes()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJobType> Lists = new List<clsJobType>();

                #region Previous query
                //String query = @"SELECT     Name AS JobTypeName, 
                //                            Description AS JobTypeDescription, 
                //                            Active AS IsActive
                //                FROM            dbo.JobTypes
                //                WHERE        (Active = 1)
                //                GROUP BY Name, Description, Active";
                #endregion

                String query = @" SELECT[Id]
                                        ,[Name] AS JobTypeName
                                        ,[Description] AS JobTypeDescription
                                        ,[Active] AS IsActive
                                    FROM[dbo].[JobTypes]
                                    WHERE(Active = 1)";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsJobType>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

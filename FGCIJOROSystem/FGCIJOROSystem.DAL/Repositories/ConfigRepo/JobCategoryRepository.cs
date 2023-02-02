using FGCIJOROSystem.Domain.Configurations.JobCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
    public class JobCategoryRepository : IRepository<clsJobCategory>
    {

        public void Add(clsJobCategory obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[JobCategories]
                                       ([Name]
                                       ,[Description]
                                       ,[Active])
                                 VALUES
                                       (@JobCategoryName
                                       ,@Description
                                       ,@IsActive)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsJobCategory obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JobCategories]
                                   SET [Name] = @JobCategoryName
                                      ,[Description] = @Description
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

        public void Delete(clsJobCategory obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JobCategories]
                                   SET 
                                      [Active] = '0'
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsJobCategory> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJobCategory> Lists = new List<clsJobCategory>();
                String query = @"SELECT JC.Id
                                      ,JC.Name as JobCategoryName
                                      ,JC.[Description]
                                      ,JC.Active as IsActive
                                  FROM [FGCIJOROSystemDB].[dbo].[JobCategories] JC
								  where Active = 1";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsJobCategory>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsJobCategory FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsJobCategory List = new clsJobCategory();
                String query = "";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJobCategory>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        public List<clsJobCategory> SearchBy(string whereQuery)
        {
            throw new NotImplementedException(); 
        }

        public List<clsJobCategory> GetAllActiveJobCategory()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJobCategory> Lists = new List<clsJobCategory>();

                #region Previous code
                //      String query = @"SELECT JC.Id
                //                            ,JC.Name as JobCategoryName
                //                            ,JC.[Description]
                //                            ,JC.Active as IsActive
                //                        FROM [FGCIJOROSystemDB].[dbo].[JobCategories] JC
                //                      where Active = 1";

                #endregion

                String query = @"SELECT         JC.Id as JobCategoryId,
                                                JC.Name as JobCategoryName, 
                                                JC.Description, 
                                                JC.Active as IsActive
                                        FROM            dbo.JobCategories AS JC
                                        WHERE        (Active = 1)
                                        GROUP BY JC.Id, JC.Name, JC.Description, JC.Active";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsJobCategory>(query).ToList();
                connection.Close();
                return Lists;
            }
        }


    }
}

using FGCIJOROSystem.Domain.Configurations.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
    public class ModulesRepository : IRepository<clsModLevelAssignment>
    {

        public void Add(clsModLevelAssignment obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ModLevel_Assignment]
                                                   ([ModuleId]
                                                   ,[UserLevelId]
                                                   ,[AllowAdd]
                                                   ,[AllowEdit]
                                                   ,[AllowDelete]
                                                   ,[AllowView])
                                             VALUES
                                                   (@ModuleId
                                                   ,@UserLevelId
                                                   ,@AllowAdd
                                                   ,@AllowEdit
                                                   ,@AllowDelete
                                                   ,@AllowView)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsModLevelAssignment obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ModLevel_Assignment]
                                       SET [ModuleId] = @ModuleId
                                          ,[UserLevelId] = @UserLevelId
                                          ,[AllowAdd] = @AllowAdd
                                          ,[AllowEdit] = @AllowEdit
                                          ,[AllowDelete] = @AllowDelete
                                          ,[AllowView] = @AllowView
                                     WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsModLevelAssignment obj)
        {
            throw new NotImplementedException();
        }

        public List<clsModLevelAssignment> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsModLevelAssignment> Lists = new List<clsModLevelAssignment>();
                String query = @"SELECT [Id]
                                      ,[ModuleId]
                                      ,[UserLevelId]
                                      ,[AllowAdd]
                                      ,[AllowEdit]
                                      ,[AllowDelete]
                                      ,[AllowView]
                                  FROM [dbo].[ModLevel_Assignment]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsModLevelAssignment>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsModLevelAssignment FindByID(long id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsModLevelAssignment List = new clsModLevelAssignment();
                String query = @"";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsModLevelAssignment>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        public List<clsModLevelAssignment> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsModLevelAssignment> Lists = new List<clsModLevelAssignment>();
                String query = @"SELECT [Id]
                                      ,[ModuleId]
                                      ,[UserLevelId]
                                      ,[AllowAdd]
                                      ,[AllowEdit]
                                      ,[AllowDelete]
                                      ,[AllowView]
                                  FROM [dbo].[ModLevel_Assignment] " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsModLevelAssignment>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

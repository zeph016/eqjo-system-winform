using FGCIJOROSystem.Domain.Configurations.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
    public class UserAccessLevelRepository : IRepository<clsUserLevel>
    {

        public void Add(clsUserLevel obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[UserLevels]
                                               ([UserLevelName]
                                               ,[Active])
                                         VALUES
                                               (@UserLevelName
                                               ,@Active)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsUserLevel obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[UserLevels]
                                           SET [UserLevelName] = @UserLevelName
                                              ,[Active] = @Active
                                         WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsUserLevel obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[UserLevels]
                                           SET [Active] = '0'
                                         WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsUserLevel> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUserLevel> Lists = new List<clsUserLevel>();
                String query = @"SELECT [Id]
                                          ,[UserLevelName]
                                          ,[Active]
                                      FROM [dbo].[UserLevels]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUserLevel>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsUserLevel FindByID(long id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsUserLevel Item = new clsUserLevel();
                String query = @"";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Item = connection.Query<clsUserLevel>(query).FirstOrDefault();
                connection.Close();
                return Item;
            }
        }

        public List<clsUserLevel> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUserLevel> Lists = new List<clsUserLevel>();
                String query = @"SELECT [Id]
                                          ,[UserLevelName]
                                          ,[Active]
                                      FROM [dbo].[UserLevels]
                                        WHERE Active = '1' ";// like '%" + whereQuery + @"%'";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUserLevel>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

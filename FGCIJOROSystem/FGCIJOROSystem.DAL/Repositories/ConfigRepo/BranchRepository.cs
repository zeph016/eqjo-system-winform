using FGCIJOROSystem.Domain.Configurations.Branches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
    public class BranchRepository : IRepository<clsBranch>
    {
        public void Add(clsBranch obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[Branches]
                                       ([Name]
                                       ,[Active])
                                 VALUES
                                        (@BranchName
                                       ,@Active)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsBranch obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[Branches]
                                   SET [Name] = @BranchName
                                      ,[Active] = @Active
                                 WHERE Id = @Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsBranch obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[Branches]
                                   SET [Active] = '0'
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsBranch> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsBranch> Lists = new List<clsBranch>();
                String query = "SELECT Id, Name As BranchName, Active FROM Branches";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsBranch>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsBranch> GetAllActive()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsBranch> Lists = new List<clsBranch>();
                String query = "SELECT Id, Name As BranchName, Active FROM Branches Where Active = 1";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsBranch>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsBranch FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsBranch List = new clsBranch();
                String query = "SELECT Id, Name As BranchName, Active FROM Branches WHERE Id = Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsBranch>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        public List<clsBranch> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsBranch> Lists = new List<clsBranch>();
                String query = @"SELECT Id, Name As BranchName, Active FROM Branches "+ whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsBranch>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

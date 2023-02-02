using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FGCIJOROSystem.Domain.Global;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.GlobalRepo
{
    public class ChecklistItemRepository : IRepository<clsChecklistItem>
    {

        public void Add(clsChecklistItem obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ChecklistItems]
                                           ([Name]
                                           ,[Description]
                                           ,[Active])
                                     VALUES
                                           (@Name
                                           ,@Description
                                           ,@Active)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }            
        }

        public void Update(clsChecklistItem obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ChecklistItems]
                                   SET [Name] = @Name
                                      ,[Description]=@Description
                                      ,Active=@Active
                                 WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }            
        }

        public void Delete(clsChecklistItem obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"DELETE FROM [dbo].[ChecklistItems]
                                        WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }            
        }

        public List<clsChecklistItem> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklistItem> Lists = new List<clsChecklistItem>();
                String query = @"SELECT [Id]
                                      ,Name as Name
                                      ,Description as Description
                                      ,Active as Active
                                  FROM ChecklistItems WHERE Active = 1";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklistItem>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsChecklistItem FindByID(Int64 Id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsChecklistItem List = new clsChecklistItem();
                String query = "";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsChecklistItem>(query, new { Id = Id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        public List<clsChecklistItem> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklistItem> Lists = new List<clsChecklistItem>();
                String query = "";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklistItem>(query, new { whereQuery = whereQuery }).ToList();
                connection.Close();
                return Lists;
            }
        }

        }
    }

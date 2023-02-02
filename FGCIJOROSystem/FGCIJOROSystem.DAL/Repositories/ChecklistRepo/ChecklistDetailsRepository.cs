using FGCIJOROSystem.Domain.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.GlobalRepo
{
    public class ChecklistDetailsRepository : IRepository<clsChecklistDetails>
    {
        public void Add(clsChecklistDetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ChecklistDetails]
                                       ([ChecklistId]
                                       ,[SectionId]
                                       ,[ChecklistItemId]
                                       ,[IsActive])

                                 VALUES
                                       (@ChecklistId
                                       ,@SectionId
                                       ,@ChecklistItemId
                                       ,@IsActive)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                connection.Close();
            }            
        }

        public void Update(clsChecklistDetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ChecklistDetails]
                                   SET [ChecklistId] = @ChecklistId
                                      ,[SectionId] = @SectionId
                                      ,[ChecklistItemId] = @ChecklistItemId
                                      ,[IsActive] = @IsActive
                                 WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }            
        }

        public void Delete(clsChecklistDetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"DELETE FROM [dbo].[ChecklistDetails]
                                    WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }            
        }

        public List<clsChecklistDetails> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklistDetails> Lists = new List<clsChecklistDetails>();
                String query = @"SELECT Id
                                          ,ChecklistId
                                          ,SectionId
                                          ,ChecklistItemId
                                          ,IsActive
                                      FROM [dbo].[ChecklistDetails]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklistDetails>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public List<clsChecklistDetails> GetEquipmentChild(Int64 EquipmentID)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklistDetails> Lists = new List<clsChecklistDetails>();
                String query = @"SELECT ChecklistId
                                          ,SectionId
                                          ,ChecklistItemId
                                          ,IsActive
                                      FROM [dbo].[ChecklistDetails]
                                  WHERE Id = @EquipmentID";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklistDetails>(query, new
                {
                    EquipmentID = EquipmentID
                }).ToList();
                connection.Close();
                return Lists;
            }
        }
        

        public clsChecklistDetails FindByID(Int64 Id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsChecklistDetails List = new clsChecklistDetails();
                String query = @"SELECT cd.[Id]
                                          ,cd.[ChecklistId]
                                          ,cd.[SectionId]
                                          ,cd.[ChecklistItemId]
                                          ,cd.[IsActive]
	                                      ,ci.Name
	                                      ,s.SectionName
                                      FROM [dbo].[ChecklistDetails] as cd
                                    LEFT JOIN ChecklistItems as ci on ci.Id = cd.ChecklistItemId
	                                    LEFT JOIN Sections as s on s.Id = cd.SectionId Where Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsChecklistDetails>(query, new { Id = Id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        public List<clsChecklistDetails> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklistDetails> Lists = new List<clsChecklistDetails>();
                String query = @"SELECT cd.[Id]
                                          ,cd.[ChecklistId]
                                          ,cd.[SectionId]
                                          ,cd.[ChecklistItemId]
                                          ,cd.[IsActive]
	                                      ,ci.Name as ChecklistItemName
	                                      ,s.GroupDescription as SectionName
                                      FROM [dbo].[ChecklistDetails] as cd
                                        LEFT JOIN ChecklistItems as ci on ci.Id = cd.ChecklistItemId
	                                        LEFT JOIN Sections as s on s.Id = cd.SectionId " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklistDetails>(query, new { whereQuery = whereQuery }).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

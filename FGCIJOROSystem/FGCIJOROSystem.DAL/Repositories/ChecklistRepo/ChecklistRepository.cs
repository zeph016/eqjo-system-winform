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
    public class ChecklistRepository : IRepository<clsChecklist>
    {
        public Int64 ChecklistId { get; set; }
        #region Method
        public void Add(clsChecklist obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {

                String query = @"IF EXISTS (Select * from FGCIJOROSystemDB.dbo.Checklists WHERE EquipmentTypeId=@EquipmentTypeId) 
                                    BEGIN
                                     UPDATE FGCIJOROSystemDB.dbo.Checklists
                                           SET [EquipmentTypeId] = @EquipmentTypeId
                                              ,[Description] = @Description
                                                WHERE Id=@Id;
                                    END
                                    ELSE
                                    BEGIN
                                    Insert INTO FGCIJOROSystemDB.dbo.Checklists
                                           ([EquipmentTypeId]
                                           ,[Description])
                                     VALUES
                                           (@EquipmentTypeId
                                           ,@Description);
                                     SELECT CAST(SCOPE_IDENTITY()AS BIGINT) as Id;
                                    END"; 
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                ChecklistId = returnId;
                connection.Close();
                foreach (var x in obj.ListOfChecklistDetails)
                {
                    if (x.ChecklistId == 0)
                    {
                        if (returnId != 0)
                        {
                            x.ChecklistId = returnId;
                        }
                        else
                        {
                            x.ChecklistId = obj.Id;
                        }                       
                    }               
                  
                    if (x.Id == 0)
                    {
                        new ChecklistDetailsRepository().Add(x);
                    }
                    else
                    {
                        new ChecklistDetailsRepository().Update(x);
                    }
                }
               
            }            
        }

        public void Update(clsChecklist obj)
        {
                using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @" UPDATE [dbo].[Checklists]
                                           SET [EquipmentTypeId] = @EquipmentTypeId
                                              ,[Description] = @Description
                                         WHERE EquipmentTypeId=@EquipmentTypeId";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }            

        }

        public void Delete(clsChecklist obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklist> Lists = new List<clsChecklist>();
                String query = @"DELETE FROM [dbo].[Checklists]
                                  WHERE EquipmentTypeId=@EquipmentTypeId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklist>(query).ToList();
                connection.Close();
               // return Lists;
            }
            
        }
        public void DeleteChecklist(clsChecklist obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklist> Lists = new List<clsChecklist>();
                String query = @"DELETE FROM [dbo].[ChecklistDetails]
                                 WHERE id = @id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklist>(query).ToList();
                connection.Close();
                // return Lists;
            }

        }

        public List<clsChecklist> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklist> Lists = new List<clsChecklist>();
                String query = @"SELECT [Id]
                                      ,[EquipmentTypeId]
                                      ,[Description]
                                  FROM [dbo].[Checklists]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklist>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsChecklist FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsChecklist List = new clsChecklist();
                String query = @"SELECT C.Id
	                                    ,EQ.PPEName As EquipmentCode
									    ,ET.PPETypeName as EquipmentName
	                                    ,EC.PPEClassName
	                                    ,EQ.ActualLocation
				                        ,[ChecklistNo]
                                        ,[DateEncoded]
                                        ,[EquipmentId]
                                        ,[Location]
                                        ,[OdometerReading]
                                        ,[DateReceived]
                                        ,[DateCompleted]
                                        FROM [dbo].[ChecklistGenerator] AS C
								        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = C.Id
								        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.PPETypes AS ET ON EQ.PPETypeId = ET.Id
                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsChecklist>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }

        }

       


        public List<clsChecklist> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklist> Lists = new List<clsChecklist>();
                String query = @"SELECT c.[Id]
                                      ,c.[EquipmentTypeId]
                                      ,c.[Description]
	                                  ,p.PPETypeName as EquipmentName
	                                  ,EQ.PPEName As EquipmentCode
	                                  ,EC.PPEClassName
	                                  ,EQ.ActualLocation
                                      FROM [FGCIJOROSystemDB].[dbo].[Checklists] as C
	                                    LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.PPETypes as p ON c.EquipmentTypeId = p.Id 
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.PPETypeId = P.Id
                                                 LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id "+ whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklist>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        #endregion
    }
}
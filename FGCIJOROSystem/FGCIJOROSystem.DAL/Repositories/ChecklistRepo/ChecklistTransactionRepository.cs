using FGCIJOROSystem.Domain.Checklist;
using FGCIJOROSystem.Domain.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.Checklist
{
    public class ChecklistTransactionRepository: IRepository<clsChecklistTransaction>
    {
        public void Add(clsChecklistTransaction obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ChecklistGeneratorDetails]
                                                   ([ChecklistGeneratorId]
                                                   ,[ChecklistDetailId]
                                                   ,[IncomingStatus]
                                                   ,[IncomingRemarks]
                                                   ,[OutgoingStatus]
                                                   ,[OutgoingRemarks])
                                             VALUES
                                                   (@ChecklistGeneratorId
                                                   ,@ChecklistDetailId
                                                   ,@IncomingStatus
                                                   ,@IncomingRemarks
                                                   ,@OutgoingStatus
                                                   ,@OutgoingRemarks)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
                
            }
        }

        public void Update(clsChecklistTransaction obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ChecklistGeneratorDetails]
                                        SET [ChecklistGeneratorId] = @ChecklistGeneratorId
                                            ,[ChecklistDetailId] = @ChecklistDetailId
                                            ,[IncomingStatus] = @IncomingStatus
                                            ,[IncomingRemarks] = @IncomingRemarks
                                            ,[OutgoingStatus] = @OutgoingStatus
                                            ,[OutgoingRemarks] = @OutgoingRemarks
                                        WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }            
        }

        public void Delete(clsChecklistTransaction obj)
        {
            throw new NotImplementedException();
        }

        public List<clsChecklistTransaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsChecklistTransaction FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsChecklistTransaction> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklistTransaction> Lists = new List<clsChecklistTransaction>();
                String query = @" SELECT 0 Id
                                          ,CD.[Id] As ChecklistDetailId
                                          ,CD.[ChecklistId]
                                          ,CD.[SectionId]
                                          ,CD.[ChecklistItemId]
                                          ,CD.[IsActive]
	                                      ,CI.Name AS ChecklistItemName
	                                      ,S.GroupDescription As SectionName
	                                      --,ET.Id
                                      FROM [FGCIJOROSystemDB].[dbo].[ChecklistDetails] AS CD
	                                    LEFT JOIN FGCIJOROSystemDB.dbo.ChecklistItems CI ON CI.Id =CD.ChecklistItemId
		                                    LEFT JOIN FGCIJOROSystemDB.dbo.Sections S ON S.Id = cd.SectionId
			                                    LEFT JOIN [FGCIJOROSystemDB].[dbo].[Checklists] C ON C.Id=cD.ChecklistId
				                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON C.EquipmentTypeId = ET.Id  
																		" + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklistTransaction>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
      public List<clsChecklistTransaction> LoadDetails(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklistTransaction> Lists = new List<clsChecklistTransaction>();
                String query = @" SELECT  genDet.Id as Id,
                                          CD.[Id] As ChecklistDetailId
                                          ,genDet.ChecklistGeneratorId
                                          ,CD.[ChecklistId]
                                          ,CD.[SectionId]
                                          ,CD.[ChecklistItemId]
                                          ,CD.[IsActive]
	                                      ,CI.Name AS ChecklistItemName
	                                      ,S.GroupDescription As SectionName
	                                      ,genDet.IncomingRemarks
										  ,genDet.IncomingStatus
										  ,genDet.OutgoingRemarks
										  ,genDet.OutgoingStatus
								 FROM ChecklistGeneratorDetails genDet
								LEFT JOIN ChecklistDetails CD ON genDet.ChecklistDetailId = CD.Id
	                                    LEFT JOIN FGCIJOROSystemDB.dbo.ChecklistItems CI ON CI.Id =CD.ChecklistItemId
		                                    LEFT JOIN FGCIJOROSystemDB.dbo.Sections S ON S.Id = cd.SectionId
			                                    LEFT JOIN [FGCIJOROSystemDB].[dbo].[Checklists] C ON C.Id=cD.ChecklistId
				                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON C.EquipmentTypeId = ET.Id 
																		" + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklistTransaction>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

using FGCIJOROSystem.Domain.Checklist;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FGCIJOROSystem.DAL.Repositories.Checklist;

namespace FGCIJOROSystem.DAL.Repositories.JORepo
{
    public class ChecklistGeneratorRepository : IRepository<clsChecklistGenerator>
    {
        public Int64 EquipmentId { get; set; }
        public Int64 ChecklistGeneratorId { get; set; }
        public void Add(clsChecklistGenerator obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"IF EXISTS (Select * from ChecklistGenerator WHERE Id=@Id) 
                                    BEGIN
                                     UPDATE [dbo].[ChecklistGenerator]
                                       SET
                                          [ChecklistNo] = @ChecklistNo
                                          ,[DateEncoded] = @DateEncoded
                                          ,[EquipmentId] = @EquipmentId
                                          ,[Location] = @Location
                                          ,[OdometerReading] = @OdometerReading
                                          ,[DateReceived] = @DateReceived
                                          ,[DateCompleted] = @DateCompleted
                                     WHERE Id = @Id;
                                    END
                                    ELSE
                                    BEGIN
                                    DECLARE @MAXId as BIGINT;
                                    SET @MaxId = (SELECT MAX(CHECKLISTGEN.CHECKLISTNO) FROM [dbo].[ChecklistGenerator] As CHECKLISTGEN);
                                                INSERT INTO [dbo].[ChecklistGenerator]
                                               ([ChecklistNo]
                                               ,[DateEncoded]
                                               ,[EquipmentId]
                                               ,[Location]
                                               ,[OdometerReading]
                                               ,[DateReceived]
                                               ,[DateCompleted])
                                         VALUES
                                               (
                                               (SELECT CASE WHEN @MAXId is null OR @MaxId = 0 THEN 1  ELSE @MaxId + 1 END)
                                               
                                               ,GETDATE()
                                               ,@EquipmentId
                                               ,@Location
                                               ,@OdometerReading
                                               ,@DateReceived
                                               ,@DateCompleted);
                                     SELECT CAST(SCOPE_IDENTITY()AS BIGINT) as Id;
                                    END";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                if (obj.Id == 0)
                {
                    obj.Id = returnId;
                    EquipmentId = returnId;
                }
                else
                {
                    EquipmentId = obj.Id;
                }
             
                connection.Close();
                foreach (var x in obj.ListOfChecklistTransaction)
                {
                    if (x.OutgoingStatus == true)
                    {
                        //var d = "";
                    }
                    x.ChecklistGeneratorId = obj.Id;
                    if (x.Id == 0)
                    {
                        new ChecklistTransactionRepository().Add(x);
                    }
                    else
                    {
                        new ChecklistTransactionRepository().Update(x);
                    }
                }
            }
        }
        public void Update(clsChecklistGenerator obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ChecklistGenerator]
                                       SET
                                          [ChecklistNo] = @ChecklistNo
                                          ,[DateEncoded] = @DateEncoded
                                          ,[EquipmentId] = @EquipmentId
                                          ,[Location] = @Location
                                          ,[OdometerReading] = @OdometerReading
                                          ,[DateReceived] = @DateReceived
                                          ,[DateCompleted] = @DateCompleted
                                     WHERE EquipmentId = @EquipmentId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                obj.Id = returnId;
                EquipmentId = returnId;
                connection.Close();
                foreach (var x in obj.ListOfChecklistTransaction)
                {
                    if (x.ChecklistGeneratorId == 0)
                    {
                        if (returnId != 0)
                        {
                            x.ChecklistGeneratorId = returnId;
                        }
                        else
                        {
                            x.ChecklistGeneratorId = obj.Id;
                        }
                    }

                    if (x.Id == 0)
                    {
                        new ChecklistTransactionRepository().Add(x);
                    }
                    else
                    {
                        new ChecklistTransactionRepository().Update(x);
                    }
                }
            }
        }

        public void Delete(clsChecklistGenerator obj)
        {
            throw new NotImplementedException();
        }

        public List<clsChecklistGenerator> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklistGenerator> List = new List<clsChecklistGenerator>();
                String query = @"SELECT [Id]
                                          ,[ChecklistNo]
                                          ,[DateEncoded]
                                          ,[EquipmentId]
                                          ,[Location]
                                          ,[OdometerReading]
                                          ,[DateReceived]
                                          ,[DateCompleted]
                                      FROM [dbo].[ChecklistGenerator]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsChecklistGenerator>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public clsChecklistGenerator FindByID(long id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsChecklistGenerator currentRow = new clsChecklistGenerator();
                String query = @"SELECT [Id]
                                          ,[ChecklistNo]
                                          ,[DateEncoded]
                                          ,[EquipmentId]
                                          ,[Location]
                                          ,[OdometerReading]
                                          ,[DateReceived]
                                          ,[DateCompleted]
                                      FROM [dbo].[ChecklistGenerator]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                currentRow = connection.Query<clsChecklistGenerator>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return currentRow;
            }
        }

        public List<clsChecklistGenerator> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklistGenerator> Lists = new List<clsChecklistGenerator>();
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
									  LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = C.EquipmentId
									   LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.PPETypes AS ET ON EQ.PPETypeId = ET.Id
                                           LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklistGenerator>(query, new { whereQuery = whereQuery }).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}
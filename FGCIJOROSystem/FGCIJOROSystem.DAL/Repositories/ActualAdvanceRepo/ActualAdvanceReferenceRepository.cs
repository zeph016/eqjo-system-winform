using FGCIJOROSystem.Domain.ActualAdvance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.ActualAdvanceRepo
{
    public class ActualAdvanceReferenceRepository : IRepository<clsActualAdvanceReference>
    {
        public void Add(clsActualAdvanceReference obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ActualAdvanceRefNos]
                                       ([ReferenceNo]
                                       ,[ReferenceType]
                                       ,[RefYear]
                                       ,[ActualAdvanceId]
                                       ,[EquipmentType]
                                       ,[EquipmentId]
                                       ,[Activity]
                                       ,[Remarks]
                                       ,[EncoderId]
                                       ,[DateOfUpdate]
                                       ,[TimeStarted]
                                       ,[TimeEnded]
                                       ,WorkPercentage
                                       ,JODetailId
                                       ,[BranchId]
                                       ,[DateEncoded]
                                       ,[Type])
                                 VALUES
                                       (@ReferenceNo
                                       ,@ReferenceType
                                       ,@RefYear
                                       ,@ActualAdvanceId
                                       ,@EquipmentType
                                       ,@EquipmentId
                                       ,@Activity
                                       ,@Remarks
                                       ,@EncoderId
                                       ,@DateOfUpdate
                                       ,@TimeStarted
                                       ,@TimeEnded
                                       ,@WorkPercentage
                                       ,@JODetailId
                                       ,@BranchId
                                       ,GETDATE()
                                       ,@Type)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsActualAdvanceReference obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ActualAdvanceRefNos]
                                   SET [ReferenceNo] = @ReferenceNo
                                      ,[ReferenceType] = @ReferenceType
                                      ,RefYear = @RefYear
                                      ,[ActualAdvanceId] = @ActualAdvanceId
                                      ,[EquipmentType] = @EquipmentType
                                      ,[EquipmentId] = @EquipmentId
                                      ,[Activity] = @Activity
                                      ,[Remarks] = @Remarks
                                      ,[EncoderId] = @EncoderId
                                      ,[DateOfUpdate] = @DateOfUpdate
                                      ,[TimeStarted] = @TimeStarted
                                      ,[TimeEnded] = @TimeEnded
                                      ,WorkPercentage = @WorkPercentage
                                      ,JODetailId = @JODetailId
                                      ,[BranchId] = @BranchId
                                      --,[DateEncoded] = @DateEncoded
                                      ,[Type] = @Type
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsActualAdvanceReference obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"DELETE FROM [dbo].[ActualAdvanceRefNos]
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsActualAdvanceReference> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsActualAdvanceReference FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsActualAdvanceReference> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsActualAdvanceReference> Lists = new List<clsActualAdvanceReference>();
                String query = @"SELECT AR.[Id]
                                  ,AR.[ReferenceNo]
                                  ,AR.[ReferenceType]
                                  ,AR.[RefYear]
                                  ,AR.[ActualAdvanceId]
                                  ,AR.[EquipmentType]
                                  ,AR.[EquipmentId]
                                  ,AR.[Activity]
                                  ,AR.[Remarks]
                                  ,AR.[EncoderId]
                                  ,AR.[DateOfUpdate]
                                  ,AR.[TimeStarted]
                                  ,AR.[TimeEnded]
                                  ,AR.WorkPercentage
                                  ,AR.JODetailId
                                  ,AR.[BranchId]
                                  ,AR.[DateEncoded]
                                  ,AR.[Type]
	                              ,AD.EmployeeId
                                  ,EquipmentName = CONCAT((CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName 
					                            WHEN AR.EquipmentType = 1 THEN SD.ToolName
					                            WHEN AR.EquipmentType = 2 THEN OT.[Name]  
					                            ELSE  '' END), ' '
                                  ,(CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
					                            WHEN AR.EquipmentType = 1 THEN 'STE' 
					                            WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
					                            ELSE  '' END))
                                FROM [ActualAdvanceRefNos] AS AR
                                LEFT JOIN [ActualAdvanceDetails] AS AD ON AR.ActualAdvanceId = AD.Id 

                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsActualAdvanceReference>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public List<clsActualAdvanceReference> SearchByJoNO()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsActualAdvanceReference> Lists = new List<clsActualAdvanceReference>();
                String query = @"SELECT     dbo.ActualAdvanceDetails.EmployeeId, 
                                            dbo.ActualAdvanceDetails.EncoderId, 
                                            dbo.ActualAdvanceDetails.DateOfUpdate, 
                                            dbo.ActualAdvanceDetails.BranchId, 
                                            dbo.ActualAdvanceDetails.DateEncoded, 
                                            dbo.ActualAdvanceRefNos.Activity, 
                                            dbo.ActualAdvanceRefNos.Remarks, 
                                            dbo.ActualAdvanceRefNos.Id, 
                                            dbo.ActualAdvanceDetails.Id AS ActualAdvanceDetails, 
                                            dbo.ActualAdvanceRefNos.ReferenceNo, 
                                            GI.FirstName, 
                                            GI.LastName
                                FROM        dbo.ActualAdvanceDetails INNER JOIN
                                            dbo.ActualAdvanceRefNos ON dbo.ActualAdvanceDetails.Id = dbo.ActualAdvanceRefNos.ActualAdvanceId LEFT OUTER JOIN
                                            FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = dbo.ActualAdvanceDetails.EmployeeId LEFT OUTER JOIN
                                            FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                                WHERE       (dbo.ActualAdvanceRefNos.ReferenceNo = 5817)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsActualAdvanceReference>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

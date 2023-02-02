using FGCIJOROSystem.Domain.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Domain.Enums;

namespace FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo
{
    public class EquipmentReleasalRepository
    {
        //public long equipmentId;
        public List<clsEquipment> SearchBy(String whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipment> Lists = new List<clsEquipment>();
                String query = @"SELECT * FROM (
                                    SELECT JO.BranchId
										,B.Name
										, JO.[ItemType] As EquipmentType
                                        ,JO.[EquipmentId] As EquipmentMasterlistId
	                                    ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                                    WHEN JO.ItemType = 1 THEN SD.ToolName
							                                    WHEN JO.ItemType = 2 THEN OT.[Name]  
							                                    ELSE  '' END)
	                                    ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							                                    WHEN JO.ItemType = 1 THEN 'STE' 
							                                    WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							                                    ELSE  '' END)
	                                    ,(SELECT COUNT(STATUS) FROM JOs WHERE Status != 8 and Status != 9 and Status != 5 and Status != 4 and EquipmentId = JO.EquipmentId) As CNT
										,JOROStatus = (CASE WHEN (SELECT COUNT(Status) FROM JODetails As JODet
											LEFT JOIN JOs ON JOs.Id = JODet.JOId 
											LEFT JOIN Status On Status.Id = JODet.StatusId
											WHERE JOs.Status != 5 and JOs.Status != 4 and Status != 9 and JOs.EquipmentId = JO.EquipmentId) = 0 THEN 4 ELSE 8 END)
                                    FROM [dbo].[JOs] AS JO
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
										LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
											LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
													LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
														LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
														LEFT JOIN Branches B ON B.Id = JO.BranchId
	                                WHERE JO.Status != 5
                                    GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, SD.ToolName, OT.[Name] , EQ.PPEName, JO.BranchId,B.Name) As List
                                         WHERE LIST.CNT = 0 " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipment>(query).OrderByDescending(x => x.EquipmentName).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsEquipment> SearchBy(Int64 JOReleasalTransactionId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipment> Lists = new List<clsEquipment>();
                String query = @"SELECT * FROM (
                                        SELECT JO.[ItemType] As EquipmentType
                                            ,JO.[EquipmentId] As EquipmentMasterlistId
	                                        ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                                        WHEN JO.ItemType = 1 THEN SD.ToolName
							                                        WHEN JO.ItemType = 2 THEN OT.[Name]  
							                                        ELSE  '' END)
	                                        ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							                                        WHEN JO.ItemType = 1 THEN 'STE' 
							                                        WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							                                        ELSE  '' END)
	                                        ,JOROStatus = (CASE WHEN (SELECT COUNT(Status) FROM JODetails As JODet
											LEFT JOIN JOs ON JOs.Id = JODet.JOId 
											LEFT JOIN Status On Status.Id = JODet.StatusId
											WHERE JOs.Status != 5 and JOs.Status != 4 and Status != 9 and JOs.EquipmentId = JO.EquipmentId) = 0 THEN 4 ELSE 8 END)
											,(SELECT STUFF((								SELECT ', ' +  
'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
FROM  JOReleased AS JR 
LEFT JOIN JOs ON JOs.Id = JR.JOId
WHERE JOs.EquipmentId = JO.EquipmentId FOR XML PATH('')),1,1,'')) AS JORONos
                                        FROM [dbo].[JOs] AS JO
                                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                        LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                        LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
                                        WHERE JO.Id IN (SELECT JOId FROM JOReleased AS JR WHERE JR.JOReleasalTransactionId = @JOReleasalTransactionId)
                                        GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, SD.ToolName, OT.[Name] , EQ.PPEName) As List";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipment>(query, new { JOReleasalTransactionId = JOReleasalTransactionId }).OrderByDescending(x => x.EquipmentName).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsEquipment> SearchROBy(String whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipment> Lists = new List<clsEquipment>();
                String query = @"SELECT DISTINCT * FROM (
                                    SELECT RO.BranchId
										,B.Name
										, RO.[ItemType] As EquipmentType
                                        ,RO.[EquipmentId] As EquipmentMasterlistId
	                                    ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
							                                    WHEN RO.ItemType = 1 THEN SD.ToolName
							                                    WHEN RO.ItemType = 2 THEN OT.[Name]  
							                                    ELSE  '' END)
	                                    ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
							                                    WHEN RO.ItemType = 1 THEN 'STE' 
							                                    WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							                                    ELSE  '' END)
	                                    ,(SELECT COUNT(STATUS) FROM ROs WHERE Status != 4 and Status != 5 and Status != 8 and Status != 9 and EquipmentId = RO.EquipmentId) As CNT
                                        ,JOROStatus = (CASE WHEN (SELECT COUNT(Status) FROM RODetails As RODet
											LEFT JOIN ROs ON ROs.Id = RODet.ROId
											LEFT JOIN Status On Status.Id = RODet.StatusId
											WHERE RODet.StatusId = 5 and RODet.StatusId = 4 ) = 0 THEN 4 ELSE 8 END)
										,RO.Status
                                    FROM [dbo].[ROs] AS RO
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
										LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
											LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
													LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
														LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
															LEFT JOIN Branches B ON B.Id = RO.BranchId
                                    where RO.Status != 5 and RO.Status != 9
                                    GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, SD.ToolName, OT.[Name] , EQ.PPEName,RO.BranchId,B.Name, RO.Status) As LIST
                                        WHERE LIST.CNT = 0  " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipment>(query).OrderByDescending(x => x.EquipmentName).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsEquipment> SearchROBy(Int64 ROReleasalTransactionId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipment> Lists = new List<clsEquipment>();
                String query = @"SELECT * FROM (
                                    SELECT RO.[ItemType] As EquipmentType
                                        ,RO.[EquipmentId] As EquipmentMasterlistId
	                                    ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
							                                    WHEN RO.ItemType = 1 THEN SD.ToolName
							                                    WHEN RO.ItemType = 2 THEN OT.[Name]  
							                                    ELSE  '' END)
	                                    ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName + ' ' + EQ.PlateNo
							                                    WHEN RO.ItemType = 1 THEN 'STE' 
							                                    WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							                                    ELSE  '' END)
                                        ,JOROStatus = (CASE WHEN (SELECT COUNT(Status) FROM RODetails As RODet
											LEFT JOIN ROs ON ROs.Id = RODet.ROId 
											LEFT JOIN Status On Status.Id = RODet.StatusId
											WHERE ROs.Status != 5 and ROs.Status != 4 and Status.IsSustained = 1 and ROs.EquipmentId = RO.EquipmentId) = 0 THEN 4 ELSE 8 END)
                                   ,(SELECT STUFF((								SELECT ', ' +  
'RO' + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()] 
FROM  ROReleased AS RR 
LEFT JOIN ROs ON ROs.Id = RR.ROId
WHERE ROs.EquipmentId = RO.EquipmentId FOR XML PATH('')),1,1,'')) AS JORONos
								    FROM [dbo].ROs AS RO
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
                                    WHERE RO.Id IN (SELECT ROId FROM ROReleased AS RR WHERE RR.ROReleasalTransactionId = @ROReleasalTransactionId)
                                    GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, SD.ToolName, OT.[Name] , EQ.PPEName, EQ.PlateNo) As List ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipment>(query, new { ROReleasalTransactionId = ROReleasalTransactionId }).OrderByDescending(x => x.EquipmentName).ToList();
                connection.Close();
                return Lists;
            }
        }
        public clsEquipment FindEquipmentIfReleasedJO(Int64 EquipmentMasterlistId, clsEnums.EquipmentType equipmentType)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsEquipment Item = new clsEquipment();
                String query = @"SELECT * FROM (
                                    SELECT JO.BranchId, JO.[ItemType] As EquipmentType
                                        ,JO.[EquipmentId] As EquipmentMasterlistId
	                                    ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                                    WHEN JO.ItemType = 1 THEN SD.ToolName
							                                    WHEN JO.ItemType = 2 THEN OT.[Name]  
							                                    ELSE  '' END)
	                                    ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							                                    WHEN JO.ItemType = 1 THEN 'STE' 
							                                    WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							                                    ELSE  '' END)
	                                    ,(SELECT COUNT(STATUS) FROM JOs WHERE Status != 8 and Status != 9 and Status != 5 and Status != 4 and EquipmentId = JO.EquipmentId) As CNT
										,JOROStatus = (CASE WHEN (SELECT COUNT(Status) FROM JODetails As JODet
											LEFT JOIN JOs ON JOs.Id = JODet.JOId 
											LEFT JOIN Status On Status.Id = JODet.StatusId
											WHERE JOs.Status != 5 and JOs.Status != 4 and Status != 9 and JOs.EquipmentId = JO.EquipmentId) = 0 THEN 4 ELSE 8 END) 
                                    FROM [dbo].[JOs] AS JO
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
	                                WHERE JO.Status != 5 AND  JO.ItemType = @equipmentType
                                    GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, SD.ToolName, OT.[Name] , EQ.PPEName, JO.BranchId) As List
                                         WHERE LIST.CNT = 0 AND EquipmentMasterlistId = @EquipmentMasterlistId ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Item = connection.Query<clsEquipment>(query, new { EquipmentMasterlistId = EquipmentMasterlistId, equipmentType = equipmentType }).FirstOrDefault();
                connection.Close();
                return Item;
            }
        }
        public clsEquipment FindEquipmentIfReleasedRO(Int64 EquipmentMasterlistId, clsEnums.EquipmentType equipmentType)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsEquipment Item = new clsEquipment();
                String query = @"SELECT DISTINCT * FROM (
                                    SELECT RO.BranchId
										,B.Name
										, RO.[ItemType] As EquipmentType
                                        ,RO.[EquipmentId] As EquipmentMasterlistId
	                                    ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
							                                    WHEN RO.ItemType = 1 THEN SD.ToolName
							                                    WHEN RO.ItemType = 2 THEN OT.[Name]  
							                                    ELSE  '' END)
	                                    ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
							                                    WHEN RO.ItemType = 1 THEN 'STE' 
							                                    WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							                                    ELSE  '' END)
	                                    ,(SELECT COUNT(STATUS) FROM ROs WHERE Status != 4 and Status != 5 and Status != 8 and Status != 9 and EquipmentId = RO.EquipmentId) As CNT
                                        ,JOROStatus = (CASE WHEN (SELECT COUNT(Status) FROM RODetails As RODet
											LEFT JOIN ROs ON ROs.Id = RODet.ROId
											LEFT JOIN Status On Status.Id = RODet.StatusId
											WHERE RODet.StatusId = 5 and RODet.StatusId = 4 ) = 0 THEN 4 ELSE 8 END)
										,RO.Status
                                    FROM [dbo].[ROs] AS RO
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
										LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
											LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
													LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
														LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
															LEFT JOIN Branches B ON B.Id = RO.BranchId
                                    WHERE RO.Status != 5 and RO.Status != 9 AND RO.ItemType = @equipmentType
                                    GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, SD.ToolName, OT.[Name] , EQ.PPEName,RO.BranchId,B.Name, RO.Status) As LIST
                                        WHERE LIST.CNT = 0  AND EquipmentMasterlistId = @EquipmentMasterlistId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Item = connection.Query<clsEquipment>(query, new { EquipmentMasterlistId = EquipmentMasterlistId, equipmentType = equipmentType}).FirstOrDefault();
                connection.Close();
                return Item;
            }
        }
        public List<clsEquipment> PrintSummary()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipment> Lists = new List<clsEquipment>();
                String query = @"SELECT * FROM (
                                    SELECT JO.BranchId
										,B.Name
										, JO.[ItemType] As EquipmentType
                                        ,JO.[EquipmentId] As EquipmentMasterlistId
	                                    ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                                    WHEN JO.ItemType = 1 THEN SD.ToolName
							                                    WHEN JO.ItemType = 2 THEN OT.[Name]  
							                                    ELSE  '' END)
	                                    ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							                                    WHEN JO.ItemType = 1 THEN 'STE' 
							                                    WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							                                    ELSE  '' END)
	                                    ,(SELECT COUNT(STATUS) FROM JOs WHERE Status != 8 and Status != 9 and Status != 5 and Status != 4 and EquipmentId = JO.EquipmentId) As CNT
										,JOROStatus = (CASE WHEN (SELECT COUNT(Status) FROM JODetails As JODet
											LEFT JOIN JOs ON JOs.Id = JODet.JOId 
											LEFT JOIN Status On Status.Id = JODet.StatusId
											WHERE JOs.Status != 5 and JOs.Status != 4 and Status != 9 and JOs.EquipmentId = JO.EquipmentId) = 0 THEN 4 ELSE 8 END)
                                    FROM [dbo].[JOs] AS JO
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
										LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
											LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
													LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
														LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
														LEFT JOIN Branches B ON B.Id = JO.BranchId
	                                WHERE JO.Status != 5
                                    GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, SD.ToolName, OT.[Name] , EQ.PPEName, JO.BranchId,B.Name) As List
                                         WHERE LIST.CNT = 0 ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipment>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsEquipment> PrintSummaryRO()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipment> Lists = new List<clsEquipment>();
                String query = @"SELECT * FROM (
                                    SELECT RO.BranchId
										,B.Name
										, RO.[ItemType] As EquipmentType
                                        ,RO.[EquipmentId] As EquipmentMasterlistId
	                                    ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
							                                    WHEN RO.ItemType = 1 THEN SD.ToolName
							                                    WHEN RO.ItemType = 2 THEN OT.[Name]  
							                                    ELSE  '' END)
	                                    ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
							                                    WHEN RO.ItemType = 1 THEN 'STE' 
							                                    WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							                                    ELSE  '' END)
	                                    ,(SELECT COUNT(STATUS) FROM ROs WHERE Status != 4 and Status != 5 and Status != 8 and Status != 9 and EquipmentId = RO.EquipmentId) As CNT
                                        ,JOROStatus = (CASE WHEN (SELECT COUNT(Status) FROM RODetails As RODet
											LEFT JOIN ROs ON ROs.Id = RODet.ROId 
											LEFT JOIN Status On Status.Id = RODet.StatusId
											WHERE ROs.Status != 5 and ROs.Status != 4 and Status != 9 and ROs.EquipmentId = RO.EquipmentId) = 0 THEN 4 ELSE 8 END)
                                    FROM [dbo].[ROs] AS RO
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
										LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
											LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
													LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
														LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
															LEFT JOIN Branches B ON B.Id = RO.BranchId
                                    where RO.Status != 5
                                    GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, SD.ToolName, OT.[Name] , EQ.PPEName,RO.BranchId,B.Name) As LIST
                                        WHERE LIST.CNT = 0 ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipment>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}
using FGCIJOROSystem.Domain.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.DashboardRepo
{
    public class UREquipmentRepository
    {
        public List<clsEquipment> GetAll(Int64 BranchId, String JORODate, String whereQuery)  
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipment> List = new List<clsEquipment>();
                String query = @"SELECT * FROM (SELECT JO.[ItemType] As EquipmentType
                                ,0 ReferenceType
                                --,JO.BranchId
                                ,EquipmentClass = (CASE WHEN JO.ItemType = 0 THEN EC.PPEClassName 
						                                WHEN JO.ItemType = 1 THEN '' 
						                                WHEN JO.ItemType = 2 THEN ''
						                                ELSE  '' END)

                                ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
						                                WHEN JO.ItemType = 1 THEN SD.ToolName
						                                WHEN JO.ItemType = 2 THEN OT.[Name]  
						                                ELSE  '' END)
                                ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
						                                WHEN JO.ItemType = 1 THEN SD.ToolName
						                                WHEN JO.ItemType = 2 THEN OT.[Name]  
						                                ELSE  '' END)
                                ,PlateNo = (CASE WHEN JO.ItemType = 0 THEN  EQ.PlateNo
						                                WHEN JO.ItemType = 1 THEN '' 
						                                WHEN JO.ItemType = 2 THEN ''
						                                ELSE  '' END)
                                ,Location = (CASE WHEN JO.ItemType = 0 THEN  EQ.ActualLocation
						                                WHEN JO.ItemType = 1 THEN '' 
						                                WHEN JO.ItemType = 2 THEN ''
						                                ELSE  '' END)
                                ,JOROStatus = 0
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 9 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
                                WHERE (JO.Status != 5 AND  JO.Status != 4 AND JO.Status != 9) AND Year(JO.JODate) = @JORODate AND JO.BranchId = @BranchId
                                GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name] --, JO.Status
                                UNION ALL
                                SELECT RO.[ItemType] As EquipmentType
                                ,1 ReferenceType
                                --,RO.[EquipmentId] As EquipmentMasterlistId
                                ,EquipmentClass = (CASE WHEN RO.ItemType = 0 THEN EC.PPEClassName 
						                                WHEN RO.ItemType = 1 THEN '' 
						                                WHEN RO.ItemType = 2 THEN ''
						                                ELSE  '' END)

                                ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
						                                WHEN RO.ItemType = 1 THEN SD.ToolName
						                                WHEN RO.ItemType = 2 THEN OT.[Name]  
						                                ELSE  '' END)
                                ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
						                                WHEN RO.ItemType = 1 THEN SD.ToolName
						                                WHEN RO.ItemType = 2 THEN OT.[Name]  
						                                ELSE  '' END)
                                ,PlateNo = (CASE WHEN RO.ItemType = 0 THEN  EQ.PlateNo
						                                WHEN RO.ItemType = 1 THEN '' 
						                                WHEN RO.ItemType = 2 THEN ''
						                                ELSE  '' END)
                                ,Location = (CASE WHEN RO.ItemType = 0 THEN  EQ.ActualLocation
						                                WHEN RO.ItemType = 1 THEN '' 
						                                WHEN RO.ItemType = 2 THEN ''
						                                ELSE  '' END)
                                ,JOROStatus = 0
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 9 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
                                WHERE (RO.Status != 5 AND  RO.Status != 4 AND RO.Status != 9) AND Year(RO.RODate) = @JORODate AND RO.BranchId = @BranchId 
                                GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name]) T " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsEquipment>(query, new { BranchId = BranchId, JORODate = JORODate}).ToList();
                connection.Close();
                return List;
            }
            
        }
    }
}

using FGCIJOROSystem.Domain.EquipmentUR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.EquipmentURRepo
{
    public class EquipmentURRepository
    {
        public Int64 optionId { get; set; }
        public void Add(clsEquipmentUR obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Int64 Id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE FGCIAccountingPPEMonitoringDB.dbo.DescriptionAndStatus
                                        SET [EquipmentStatusId] = 3
                                         WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query,new {Id = Id});
                connection.Close();
            }
        }
        public void UpdateAsOperational(Int64 Id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE FGCIAccountingPPEMonitoringDB.dbo.DescriptionAndStatus
                                        SET [EquipmentStatusId] = 1
                                         WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, new { Id = Id });
                connection.Close();
            }
        }

        public void Delete(clsEquipmentUR obj)
        {
            throw new NotImplementedException();
        }
        #region GetAllEquipmentStatus
        public List<clsEquipmentUR> GetAll()
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"SELECT EquipmentStatusId
                                          ,StatusDescription AS EquipmentStatus
                                          ,IsActive
                                          ,ShortcutName
                                      FROM EquipmentStatus
                                            WHERE IsActive = 1";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }
        #endregion
        #region GetAllEquipmentPPEClass
        public List<clsEquipmentClasses> GetAllClass()
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                List<clsEquipmentClasses> List = new List<clsEquipmentClasses>();
                String query = @"SELECT Id
                                          ,PPEClassName AS EquipmentClass
                                      FROM PPEClasses";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsEquipmentClasses>(query).ToList();
                connection.Close();
                return List;
            }
        }
        #endregion
        #region GetAllPPETypes
        public List<clsEquipmentType> GetAllTypes()
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                List<clsEquipmentType> List = new List<clsEquipmentType>();
                String query = @"SELECT Id
                                      ,PPETypeName AS EquipmentTypeName
                                  FROM PPETypes";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsEquipmentType>(query).ToList();
                connection.Close();
                return List;
            }
        }
        #endregion
        #region GetAllUR
        public List<clsEquipmentUR> GetAllUR(Int64 BranchId, String JORODate, String whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
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
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                    WHERE (JO.Status != 5 AND  JO.Status != 4 AND JO.Status != 7) AND Year(JO.JODate) = @JORODate AND JO.BranchId = @BranchId
                                GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.StatusDescription --, JO.Status
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
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                    WHERE (RO.Status != 5 AND  RO.Status != 4 AND RO.Status != 7) AND Year(RO.RODate) = @JORODate AND RO.BranchId = @BranchId
                                GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.StatusDescription) T " + whereQuery;
										
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsEquipmentUR>(query, new { BranchId = BranchId, JORODate = JORODate }).ToList();
                connection.Close();
                return List;
            }
        }
        
        #endregion
        public clsEquipmentUR FindByID(long id)
        {
            throw new NotImplementedException();
        }
        #region FilterEquipmentUR
        public List<clsEquipmentUR> SearchBy(String WhereQuery)
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsEquipmentUR> GetAllURFiltered(bool isEquipmentStatusId, Int64 equipmentStatusId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"SELECT * FROM (SELECT JO.[ItemType] As EquipmentType
                                ,0 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription --, JO.Status
                        UNION ALL
                                SELECT RO.[ItemType] As EquipmentType
                                ,1 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription) T ";
                if (isEquipmentStatusId)
                {
                    query += " Where T.EquipmentStatusId IN ( " + equipmentStatusId + " )";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }
        #endregion
        #region GenerateAll
        public List<clsEquipmentUR> GenerateAll(bool isEquipment, string equipmentIds, bool isPPEClass, string ppeClassId, bool isPPEType, string ppeTypeId, bool isSection, string sectionIds
                                                    ,bool isSectionGroup, Int64 sectionGroupId, bool isProject, string projectIds)
            {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"SELECT * FROM (SELECT JO.[ItemType] As EquipmentType
                                ,0 ReferenceType
								,ET.Id AS TypeId
								,EC.Id AS ClassId
								,JO.EquipmentId AS EquipId
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
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
																	
                                    WHERE (JO.Status != 5 AND  JO.Status != 4 AND JO.Status != 7) --AND Year(JO.JODate) = @JORODate AND JO.BranchId = @BranchId
                                GROUP BY JO.ItemType,ET.Id,EC.Id, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.StatusDescription --, JO.Status
                        UNION ALL
                                SELECT RO.[ItemType] As EquipmentType
                                ,1 ReferenceType
								,ET.Id AS TypeId
								,EC.Id AS ClassId
                                ,RO.EquipmentId AS EquipId
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
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
								FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                    
									WHERE (RO.Status != 5 AND  RO.Status != 4 AND RO.Status != 7) --AND Year(RO.RODate) = @JORODate AND RO.BranchId = @BranchId
                                GROUP BY RO.ItemType,ET.Id,EC.Id, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.StatusDescription) T";
                if (isEquipment)
                {
                    query += " WHERE T.EquipId IN ( " + equipmentIds + " ) ";
                }
                if (isPPEClass)
                {
                    query += " WHERE T.ClassId IN ( " + ppeClassId + " ) ";
                }
                if (isPPEType)
                {
                    query += " WHERE T.TypeId IN ( " + ppeTypeId + " ) ";
                }
                if (isSection)
                {
                   // query += " WHERE T.TypeId IN ( " + ppeTypeId + " ) ";
                }
                if (isSectionGroup)
                {
                    //query += " WHERE T.TypeId IN ( " + ppeTypeId + " ) ";
                }
                if (isProject)
                {
                    //query += " WHERE T.TypeId IN ( " + ppeTypeId + " ) ";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                query += "ORDER BY T.EquipmentCode ASC";
                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsEquipmentUR> GenerateAllURWithJORO()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"SELECT * FROM (SELECT JO.[ItemType] As EquipmentType
                                ,0 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription --, JO.Status
                        UNION ALL
                                SELECT RO.[ItemType] As EquipmentType
                                ,1 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription) T 
                                WHERE T.EquipmentStatusId = 3 AND JORONos IS NOT NULL ORDER BY T.EquipmentCode";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public List<clsEquipmentUR> GenerateAllURWithJOROSearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"SELECT * FROM (SELECT JO.[ItemType] As EquipmentType
                                ,0 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription --, JO.Status
                        UNION ALL
                                SELECT RO.[ItemType] As EquipmentType
                                ,1 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription) T 
                                WHERE T.EquipmentStatusId = 3 AND JORONos IS NOT NULL " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsEquipmentUR> GenerateAllURUnattended()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"SELECT * FROM (SELECT JO.[ItemType] As EquipmentType
                                ,0 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription --, JO.Status
                        UNION ALL
                                SELECT RO.[ItemType] As EquipmentType
                                ,1 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription) T 
                                WHERE T.EquipmentStatusId = 3 AND JORONos IS NULL";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public List<clsEquipmentUR> GenerateAllURUnattendedSearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"SELECT * FROM (SELECT JO.[ItemType] As EquipmentType
                                ,0 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription --, JO.Status
                        UNION ALL
                                SELECT RO.[ItemType] As EquipmentType
                                ,1 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription) T 
                                WHERE T.EquipmentStatusId = 3 AND JORONos IS NULL " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }

       
        public List<clsEquipmentUR> GenerateAllNonURButWithJORO()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"SELECT * FROM (SELECT JO.[ItemType] As EquipmentType
                                ,0 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription --, JO.Status
                        UNION ALL
                                SELECT RO.[ItemType] As EquipmentType
                                ,1 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription) T 
                                WHERE T.EquipmentStatusId != 3 AND JORONos IS NOT NULL";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public List<clsEquipmentUR> GenerateAllNonURButWithJOROSearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"SELECT * FROM (SELECT JO.[ItemType] As EquipmentType
                                ,0 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription --, JO.Status
                        UNION ALL
                                SELECT RO.[ItemType] As EquipmentType
                                ,1 ReferenceType
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
								,ES.EquipmentStatusId AS EquipmentStatusId
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.EquipmentStatusId,ES.StatusDescription) T 
                                WHERE T.EquipmentStatusId != 3 AND JORONos IS NOT NULL " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsEquipmentUR> GenerateAllEquipment()
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"SELECT *FROM(SELECT PPETypeId
	                                    ,PPEClassId
	                                    ,EquipmentClass
	                                    ,EquipmentCode
	                                    ,EquipmentName
	                                    ,PlateNo
	                                    ,Location
	                                    ,ES.EquipmentStatusId
	                                    ,ES.StatusDescription AS EquipmentStatus
	                                    ,JONos AS JORONOs
                                    FROM (SELECT DS.PPETypeId AS PPETypeId
	                                    ,DS.PPEName AS EquipmentCode
	                                    ,DS.PPEClassId AS PPEClassId
	                                    ,DS.PlateNo AS PlateNo
	                                    ,DS.ActualLocation AS Location
	                                    ,PT.PPETypeName AS EquipmentName
	                                    ,PC.PPEClassName AS EquipmentClass
	                                    ,DS.EquipmentStatusId AS EquipmentStatusId
	                                    ,STUFF((SELECT ', ' + 'JO' + Substring(JO.RefYear, LEN(JO.RefYear) - 1,4) + CAST(FORMAT(JO.JONo,'0000') AS VARCHAR(MAX))
		                                    FROM FGCIJOROSystemDB.dbo.JOs AS JO
		                                        WHERE (JO.EquipmentId = DS.Id) 
		                                            FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)'),1,2,'') JONos

	                                    FROM DescriptionAndStatus DS
	                                    LEFT JOIN FGCIJOROSystemDB.dbo.JOs JO ON JO.EquipmentId = DS.Id
		                                    LEFT JOIN PPETypes AS PT ON PT.Id = DS.PPETypeId
			                                    LEFT JOIN PPEClasses AS PC ON PC.Id = DS.PPEClassId) DS
				                                    LEFT JOIN EquipmentStatus AS ES ON ES.EquipmentStatusId = DS.EquipmentStatusId

	                                    GROUP BY PPETypeId,EquipmentName,PPEClassId,EquipmentClass, EquipmentCode, PPEClassId, PlateNo ,Location,ES.EquipmentStatusId,ES.StatusDescription, JONos

                                    UNION ALL

                                    SELECT PPETypeId
	                                    ,PPEClassId
	                                    ,EquipmentClass
	                                    ,EquipmentCode
	                                    ,EquipmentName
	                                    ,PlateNo
	                                    ,Location
	                                    ,ES.EquipmentStatusId
	                                    ,ES.StatusDescription AS EquipmentStatus
	                                    ,RONos AS JORONOs
                                    FROM (SELECT DS.PPETypeId AS PPETypeId
	                                    ,DS.PPEName AS EquipmentCode
	                                    ,DS.PPEClassId AS PPEClassId
	                                    ,DS.PlateNo AS PlateNo
	                                    ,DS.ActualLocation AS Location
	                                    ,PT.PPETypeName AS EquipmentName
	                                    ,PC.PPEClassName AS EquipmentClass
	                                    ,DS.EquipmentStatusId AS EquipmentStatusId
	                                    ,STUFF((SELECT ', ' + 'RO'+ Substring(RO.RefYear, LEN(RO.RefYear) - 1,4) + CAST(FORMAT(RO.RONo,'0000')AS VARCHAR(MAX)) 
		                                    FROM FGCIJOROSystemDB.dbo.ROs AS RO
		                                        WHERE (RO.EquipmentId = DS.Id) 
		                                            FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
	                                        ,1,2,'') RONos
	                                    FROM DescriptionAndStatus DS
	                                    LEFT JOIN FGCIJOROSystemDB.dbo.ROs RO ON RO.EquipmentId = DS.Id
		                                    LEFT JOIN PPETypes AS PT ON PT.Id = DS.PPETypeId
			                                    LEFT JOIN PPEClasses AS PC ON PC.Id = DS.PPEClassId) DS
				                                    LEFT JOIN EquipmentStatus AS ES ON ES.EquipmentStatusId = DS.EquipmentStatusId

	                                    GROUP BY PPETypeId,EquipmentName,PPEClassId,EquipmentClass, EquipmentCode, PPEClassId, PlateNo ,Location,ES.EquipmentStatusId,ES.StatusDescription, RONos)T";
               if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public List<clsEquipmentUR> SummaryOfEquipment(bool isEquipmentStatus, Int64 equipmentStatusId, bool isPPEClass, String ppeClassIds, bool isPPEType, String ppeTypeIds)
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
                String query = @"SELECT DISTINCT DS.Id AS EquipmentMasterlistId
	                                              ,DS.PPEName AS EquipmentCode
                                                  ,ET.Id AS PPETypeId
	                                              ,ET.PPETypeName AS EquipmentName
                                                  ,DS.ActualLocation AS Location
                                                  ,DS.EquipmentStatusId
	                                              ,ES.StatusDescription AS EquipmentStatus
                                                  ,EC.Id AS PPEClassId
	                                              ,EC.PPEClassName AS EquipmentClass
                                              FROM DescriptionAndStatus AS DS
	                                            LEFT JOIN FGCIJOROSystemDB.dbo.JOs JO ON JO.EquipmentId = DS.Id
		                                            LEFT JOIN EquipmentStatus ES ON ES.EquipmentStatusId = DS.EquipmentStatusId
			                                            LEFT JOIN PPETypes ET ON ET.Id = DS.PPETypeId
				                                            LEFT JOIN PPEClasses EC ON EC.Id = DS.PPEClassId
															WHERE DS.Id <> 0";
                if (isEquipmentStatus)
                {
                    query += " AND DS.EquipmentStatusId IN ( " + equipmentStatusId + " ) ";
                }
                if (isPPEClass)
                {
                    query += " AND EC.Id IN ( " + ppeClassIds + " ) ";
                }
                if (isPPEType)
                {
                    query += " AND ET.Id IN ( " + ppeTypeIds + " ) ";
                }
                query += "ORDER BY DS.PPEName ASC";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                    List = connection.Query<clsEquipmentUR>(query).ToList();
                    connection.Close();
                    return List;
                }
        }
        public List<clsEquipmentUR> GenerateAllSummary()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
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
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
								,JO.EquipmentId
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                    WHERE (JO.Status != 5 AND  JO.Status != 4 AND JO.Status != 7) --AND Year(JO.JODate) = @JORODate AND JO.BranchId = @BranchId
                                GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.StatusDescription --, JO.Status
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
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                ,RO.EquipmentId
								FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                    WHERE (RO.Status != 5 AND  RO.Status != 4 AND RO.Status != 7) --AND Year(RO.RODate) = @JORODate AND RO.BranchId = @BranchId
                                GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.StatusDescription) T ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public List<clsEquipmentUR> GenerateAllSummarySearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentUR> List = new List<clsEquipmentUR>();
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
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((								SELECT ', ' +  
                                'JO'  + Substring(JOs.RefYear, LEN(JOs.RefYear) - 1,4) + FORMAT(JOs.JONo,'0000') AS [text()]
                                FROM  JOs 
                                WHERE JOs.EquipmentId = JO.EquipmentId AND JOs.Status != 5 AND  JOs.Status != 4 AND JOs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
								,JO.EquipmentId
                                FROM [dbo].[JOs] AS JO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                    WHERE (JO.Status != 5 AND  JO.Status != 4 AND JO.Status != 7) --AND Year(JO.JODate) = @JORODate AND JO.BranchId = @BranchId
                                GROUP BY JO.ItemType, JO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.StatusDescription --, JO.Status
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
								,ES.StatusDescription AS EquipmentStatus
                                ,(SELECT STUFF((SELECT ', ' +  
                                'RO'  + Substring(ROs.RefYear, LEN(ROs.RefYear) - 1,4) + FORMAT(ROs.RONo,'0000') AS [text()]
                                FROM  ROs 
                                WHERE ROs.EquipmentId = RO.EquipmentId AND ROs.Status != 5 AND  ROs.Status != 4 AND ROs.Status != 7 FOR XML PATH('')),1,1,'')) AS JORONos
                                ,RO.EquipmentId
								FROM [dbo].[ROs] AS RO
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
	                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
		                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                                    LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
								                        LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = EQ.EquipmentStatusId
                                WHERE (RO.Status != 5 AND  RO.Status != 4 AND RO.Status != 7)   --AND Year(RO.RODate) = @JORODate AND RO.BranchId = @BranchId
                                GROUP BY RO.ItemType, RO.EquipmentId, ET.PPETypeName, EQ.PPEName, EQ.PlateNo, EQ.ActualLocation, EC.PPEClassName , SD.ToolName, OT.[Name],ES.StatusDescription) T "
                                + whereQuery ;
                  if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List = connection.Query<clsEquipmentUR>(query).ToList();
                connection.Close();
                return List;
            }
        }
        #endregion
    }
}



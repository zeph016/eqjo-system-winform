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
    public class ActivityReportRepository
    {
        public List<clsActivityReport> GetList(DateTime Date)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsActivityReport> Lists = new List<clsActivityReport>();
                                    String query = @"SELECT *, Type = (CASE WHEN TypeId = 0 THEN 'Actual' ELSE 'Advance' END), Type = (CASE WHEN CategoryTypeId = 0 THEN 'Current' ELSE 'Previous' END) FROM (
                    SELECT
	                     CategoryTypeId = 0
	                    ,TypeId=AR.type
                        ,EmployeeId=Per.[EmployeeId]
                        ,EmployeeName=CONCAT(GI.LastName, ' ', GI.NameExtension,', ', GI.FirstName,' ', GI.MiddleName)
                        ,Section=S.GroupDescription
                        ,Position=Pos.PositionName
                        ,ReferenceNo=AR.ReferenceNo    
	                    ,AR.Activity	
                        ,EquipmentName = (CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName 
					                        WHEN AR.EquipmentType = 1 THEN SD.ToolName
					                        WHEN AR.EquipmentType = 2 THEN OT.[Name]  
					                        ELSE  '' END)
                        ,EquipmentCode = (CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
					                        WHEN AR.EquipmentType = 1 THEN 'STE' 
					                        WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
					                        ELSE  '' END)
                    FROM [FGCIJOROSystemDB].[dbo].[Personnels] AS Per
                    LEFT JOIN Sections As S ON S.Id = Per.SectionId
                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId
                    LEFT JOIN ActualAdvanceDetails AS AD ON AD.EmployeeId = Per.EmployeeId
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AR.DateOfUpdate = CONVERT(varchar, @Date, 101) --OR AR.DateOfUpdate is null
                    UNION ALL
                    SELECT
	                     CategoryTypeId = 1	                                
	                    ,TypeId=AR.type
                        ,EmployeeId=Per.[EmployeeId]
                        ,EmployeeName=CONCAT(GI.LastName, ' ', GI.NameExtension,', ', GI.FirstName,' ', GI.MiddleName)
                        ,Section=S.GroupDescription
                        ,Position=Pos.PositionName
                        ,ReferenceNo=AR.ReferenceNo   
	                    ,AR.Activity 
                        ,EquipmentName = (CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName 
					                        WHEN AR.EquipmentType = 1 THEN SD.ToolName
					                        WHEN AR.EquipmentType = 2 THEN OT.[Name]  
					                        ELSE  '' END)
                        ,EquipmentCode = (CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
					                        WHEN AR.EquipmentType = 1 THEN 'STE' 
					                        WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
					                        ELSE  '' END)
                    FROM [FGCIJOROSystemDB].[dbo].[Personnels] AS Per
                    LEFT JOIN Sections As S ON S.Id = Per.SectionId
                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId
                    LEFT JOIN ActualAdvanceDetails AS AD ON AD.EmployeeId = Per.EmployeeId
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AR.DateOfUpdate = DATEADD(d,-1,CONVERT(varchar, @Date, 101)) --OR AR.DateOfUpdate is null
                    ) T;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsActivityReport>(query, new { Date = Date}).ToList();
                return Lists;
            }
           
        }

        public List<clsActualAdvance> GetListACD(DateTime Date)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsActualAdvance> Lists = new List<clsActualAdvance>();
                String query = @"
                    SELECT 
                    Per.EmployeeId AS EmployeeId, 
                    S.GroupDescription As Section,
                    CONCAT(GI.FirstName, ' ', GI.MiddleName, ' ', GI.LastName) AS EmployeeName, 
                    Pos.PositionName as Position,
                    (SELECT STUFF((								SELECT ', ' +  
                    (CASE WHEN AR.ReferenceType = 0 THEN 'JO'
                    ELSE  'RO' END) + FORMAT(AR.ReferenceNo,'d4') AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = convert(varchar, @Date, 101) AND EmployeeId = Per.EmployeeId AND AR.Type = 0 FOR XML PATH('')),1,1,'')) AS CAcReferenceNo,
                    (SELECT STUFF((								SELECT ', ' +  
                    (CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName + ' '+ EQ.PPEName 
                    WHEN AR.EquipmentType = 1 THEN SD.ToolName
                    WHEN AR.EquipmentType = 2 THEN OT.[Name]  
                    ELSE  '' END) AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = convert(varchar, @Date, 101) AND EmployeeId = Per.EmployeeId AND AR.Type = 0 FOR XML PATH('')),1,1,'')) AS CAcEquipmentName,
                    (SELECT STUFF((								SELECT ', ' +  
                    AR.Activity AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = convert(varchar, @Date, 101) AND EmployeeId = Per.EmployeeId AND AR.Type = 0 FOR XML PATH('')),1,1,'')) AS CAcActivity,
                    (SELECT STUFF((SELECT ', ' +  
                    CONVERT(varchar(15),CAST(convert(varchar, AR.TimeStarted, 108) AS TIME),100)+'-'+CONVERT(varchar(15),CAST(convert(varchar, AR.TimeEnded, 108) AS TIME),100) AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = convert(varchar, @Date, 101) AND EmployeeId = Per.EmployeeId AND AR.Type = 0 FOR XML PATH('')),1,1,'')) AS CAcTime,

                    (SELECT STUFF((								SELECT ', ' +  
                    (CASE WHEN AR.ReferenceType = 0 THEN 'JO'
                    ELSE  'RO' END) + FORMAT(AR.ReferenceNo,'d4') AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = convert(varchar, @Date, 101) AND EmployeeId = Per.EmployeeId AND AR.Type = 1 FOR XML PATH('')),1,1,'')) AS CAdReferenceNo,
                    (SELECT STUFF((								SELECT ', ' +  
                    (CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName + ' '+ EQ.PPEName 
                    WHEN AR.EquipmentType = 1 THEN SD.ToolName
                    WHEN AR.EquipmentType = 2 THEN OT.[Name]  
                    ELSE  '' END) AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = convert(varchar, @Date, 101) AND EmployeeId = Per.EmployeeId AND AR.Type = 1 FOR XML PATH('')),1,1,'')) AS CAdEquipmentName,
                    (SELECT STUFF((								SELECT ', ' +  
                    AR.Activity AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = convert(varchar, @Date, 101) AND EmployeeId = Per.EmployeeId AND AR.Type = 1 FOR XML PATH('')),1,1,'')) AS CAdActivity,
                    (SELECT STUFF((SELECT ', ' +  
                    CONVERT(varchar(15),CAST(convert(varchar, AR.TimeStarted, 108) AS TIME),100)+'-'+CONVERT(varchar(15),CAST(convert(varchar, AR.TimeEnded, 108) AS TIME),100) AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = convert(varchar, @Date, 101) AND EmployeeId = Per.EmployeeId AND AR.Type = 1 FOR XML PATH('')),1,1,'')) AS CAdTime,

                    (SELECT STUFF((								SELECT ', ' +  
                    (CASE WHEN AR.ReferenceType = 0 THEN 'JO'
                    ELSE  'RO' END) + FORMAT(AR.ReferenceNo,'d4') AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = DATEADD(d,-1,convert(varchar, @Date, 101)) AND EmployeeId = Per.EmployeeId AND AR.Type = 0 FOR XML PATH('')),1,1,'')) AS PAcReferenceNo,
                    (SELECT STUFF((SELECT ', ' +  
                    (CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName + ' '+ EQ.PPEName 
                    WHEN AR.EquipmentType = 1 THEN SD.ToolName
                    WHEN AR.EquipmentType = 2 THEN OT.[Name]  
                    ELSE  '' END) AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = DATEADD(d,-1,convert(varchar, @Date, 101)) AND EmployeeId = Per.EmployeeId AND AR.Type = 0 FOR XML PATH('')),1,1,'')) AS PAcEquipmentName,
                    (SELECT STUFF((SELECT ', ' +  
                    AR.Activity AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = DATEADD(d,-1,convert(varchar, @Date, 101)) AND EmployeeId = Per.EmployeeId AND AR.Type = 0 FOR XML PATH('')),1,1,'')) AS PAcActivity,
                    (SELECT STUFF((SELECT ', ' +  
                    CONVERT(varchar(15),CAST(convert(varchar, AR.TimeStarted, 108) AS TIME),100)+'-'+CONVERT(varchar(15),CAST(convert(varchar, AR.TimeEnded, 108) AS TIME),100) AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = DATEADD(d,-1,convert(varchar, @Date, 101)) AND EmployeeId = Per.EmployeeId AND AR.Type = 0 FOR XML PATH('')),1,1,'')) AS PAcTime,

                    (SELECT STUFF((SELECT ', ' +  
                    (CASE WHEN AR.ReferenceType = 0 THEN 'JO'
                    ELSE  'RO' END) + FORMAT(AR.ReferenceNo,'d4') AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = DATEADD(d,-1,convert(varchar, @Date, 101)) AND EmployeeId = Per.EmployeeId AND AR.Type = 1 FOR XML PATH('')),1,1,'')) AS PAdReferenceNo,
                    (SELECT STUFF((SELECT ', ' +  
                    (CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName + ' '+ EQ.PPEName 
                    WHEN AR.EquipmentType = 1 THEN SD.ToolName
                    WHEN AR.EquipmentType = 2 THEN OT.[Name]  
                    ELSE  '' END) AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = DATEADD(d,-1,convert(varchar, @Date, 101)) AND EmployeeId = Per.EmployeeId AND AR.Type = 1 FOR XML PATH('')),1,1,'')) AS PAdEquipmentName,
                    (SELECT STUFF((SELECT ', ' +  
                    AR.Activity AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = DATEADD(d,-1,convert(varchar, @Date, 101)) AND EmployeeId = Per.EmployeeId AND AR.Type = 1 FOR XML PATH('')),1,1,'')) AS PAdActivity,
                    (SELECT STUFF((SELECT ', ' +  
                    CONVERT(varchar(15),CAST(convert(varchar, AR.TimeStarted, 108) AS TIME),100)+'-'+CONVERT(varchar(15),CAST(convert(varchar, AR.TimeEnded, 108) AS TIME),100) AS [text()] 
                    FROM  ActualAdvanceDetails AS AD 
                    LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                    WHERE AD.DateOfUpdate = DATEADD(d,-1,convert(varchar, @Date, 101)) AND EmployeeId = Per.EmployeeId AND AR.Type = 1 FOR XML PATH('')),1,1,'')) AS PAdTime

                    FROM [FGCIJOROSystemDB].[dbo].[Personnels] AS Per
                    LEFT JOIN Sections As S ON S.Id = Per.SectionId
                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId                                    
                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsActualAdvance>(query, new { Date = Date }).ToList();
                return Lists;
            }

        }

        public List<clsActivityReport> GetListActivityByRefNo(long refNo, int catType)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsActivityReport> Lists = new List<clsActivityReport>();
                String query = @"
                    SELECT *, Type = (CASE WHEN TypeId = 0 THEN 'Actual' ELSE 'Advance' END), Type = (CASE WHEN CategoryTypeId = 0 THEN 'Current' ELSE 'Previous' END) FROM (
                        SELECT
	                         CategoryTypeId = 0
	                        ,TypeId=AR.type
                            ,EmployeeId=Per.[EmployeeId]
                            ,EmployeeName=CONCAT(GI.LastName, ' ', GI.NameExtension,', ', GI.FirstName,' ', GI.MiddleName)
                            ,Section=S.GroupDescription
                            ,Position=Pos.PositionName
                            ,ReferenceNo=AR.ReferenceNo    
	                        ,AR.Activity	
                            ,EquipmentName = (CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName 
					                            WHEN AR.EquipmentType = 1 THEN SD.ToolName
					                            WHEN AR.EquipmentType = 2 THEN OT.[Name]  
					                            ELSE  '' END)
                            ,EquipmentCode = (CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
					                            WHEN AR.EquipmentType = 1 THEN 'STE' 
					                            WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
					                            ELSE  '' END)
                            ,AD.DateOfUpdate
                        FROM [FGCIJOROSystemDB].[dbo].[Personnels] AS Per
                        LEFT JOIN Sections As S ON S.Id = Per.SectionId
                        LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
                        LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                        LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId
                        LEFT JOIN ActualAdvanceDetails AS AD ON AD.EmployeeId = Per.EmployeeId
                        LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                        LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                        LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                        WHERE ReferenceNo = @RefNo
                        UNION ALL
                        SELECT
	                         CategoryTypeId = 1	                                
	                        ,TypeId=AR.type
                            ,EmployeeId=Per.[EmployeeId]
                            ,EmployeeName=CONCAT(GI.LastName, ' ', GI.NameExtension,', ', GI.FirstName,' ', GI.MiddleName)
                            ,Section=S.GroupDescription
                            ,Position=Pos.PositionName
                            ,ReferenceNo=AR.ReferenceNo   
	                        ,AR.Activity 
                            ,EquipmentName = (CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName 
					                            WHEN AR.EquipmentType = 1 THEN SD.ToolName
					                            WHEN AR.EquipmentType = 2 THEN OT.[Name]  
					                            ELSE  '' END)
                            ,EquipmentCode = (CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
					                            WHEN AR.EquipmentType = 1 THEN 'STE' 
					                            WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
					                            ELSE  '' END)
                            ,AD.DateOfUpdate
                        FROM [FGCIJOROSystemDB].[dbo].[Personnels] AS Per
                        LEFT JOIN Sections As S ON S.Id = Per.SectionId
                        LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
                        LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                        LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId
                        LEFT JOIN ActualAdvanceDetails AS AD ON AD.EmployeeId = Per.EmployeeId
                        LEFT JOIN ActualAdvanceRefNos AS AR ON AR.ActualAdvanceId = AD.Id
                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                        LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                        LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId
                        ) T WHERE ReferenceNo = @RefNo AND CategoryTypeId = @CatType; ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsActivityReport>(query, new { RefNo = refNo, CatType = catType }).ToList();
                return Lists;
            }

        }
    }
}

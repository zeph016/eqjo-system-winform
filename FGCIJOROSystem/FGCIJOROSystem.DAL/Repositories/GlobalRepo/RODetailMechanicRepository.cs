using FGCIJOROSystem.Domain.RescueOrder;
using FGCIJOROSystem.Domain.WorkAssignment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.GlobalRepo
{
    public class RODetailMechanicRepository
    {
        public List<clsWorkAssignments> SearchByMechanics(clsRODetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsWorkAssignments> Lists = new List<clsWorkAssignments>();
                String query = @"SELECT * FROM (SELECT RW.[Id]
      ,RW.[RODetailId] As JODetailId
      ,RW.[EmployeeId]
      ,RW.[DateEncoded]
      ,RW.[IsActive]
	  ,Tariff.[WorkDescription]
	  ,S.GroupDescription As Section
	  ,JC.Name As JobCategory
	  ,JT.Name As JobType
	  ,CONCAT(GI.FirstName,' ' ,GI.MiddleName,' ',GI.LastName,' ' ,GI.NameExtension) As MechanicName
	  ,Pos.PositionName As Position
	  ,RO.RONo As ReferenceNo
       ,RO.RefYear
      ,1 ReferenceType
,EquipmentType = RO.ItemType
       ,RO.EquipmentId
	   ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
							                             WHEN RO.ItemType = 1 THEN SD.ToolName
							                             WHEN RO.ItemType = 2 THEN OT.[Name]  
							                             ELSE  '' END)
	,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
							WHEN RO.ItemType = 1 THEN 'STE' 
							WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							ELSE  '' END)

	,EquipmentLocation = (CASE WHEN RO.ItemType = 0 THEN EQ.ActualLocation 
							WHEN RO.ItemType = 1 THEN '' 
							WHEN RO.ItemType = 2 THEN ''
							ELSE  '' END)
	,ContractorName = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
							WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
							WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
							WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
							WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
							ELSE  '' END)

	,ContractorSectionHead = RO.SectionHead
	,RO.[Status]
FROM [ROWorkAssignment] AS RW
LEFT JOIN  [dbo].RODetails AS RD ON RD.Id = RW.RODetailId
LEFT JOIN Tariff ON Tariff.Id = RD.TariffId
LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
LEFT JOIN [Status] ON STATUS.Id = RD.StatusId
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS MEI ON MEI.Id = RW.EmployeeId 
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id =  MEI.GeneralInformationsId

LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = MEI.PositionsId
LEFT JOIN ROs As RO ON RO.Id = RD.ROId 
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
	LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
		LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId

LEFT JOIN Contractors C ON C.Id = RO.ContractorId
LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId

LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId
WHERE RD.Id = @Id
UNION ALL
SELECT 0 [Id]
      ,'' [JODetailId]
      ,RM.MLMechanicsId
      ,'' [DateEncoded]
      ,0 [IsActive]
	  ,'' [WorkDescription]
	  ,'' Section
	  ,'' JobCategory
	  ,'' JobType
	  ,CONCAT(GI.FirstName, ' ', GI.MiddleName, ' ', GI.LastName, ' ', GI.LastName) MechanicName
	  ,Pos.PositionName As Position
	  ,RO.RONo As ReferenceNo
       ,RO.RefYear
      ,1 ReferenceType
,EquipmentType = RO.ItemType
       ,RO.EquipmentId
	   ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
						WHEN RO.ItemType = 1 THEN SD.ToolName
						WHEN RO.ItemType = 2 THEN OT.[Name]  
						ELSE  '' END)
	,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
							WHEN RO.ItemType = 1 THEN 'STE' 
							WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							ELSE  '' END)

	,EquipmentLocation = (CASE WHEN RO.ItemType = 0 THEN EQ.ActualLocation 
							WHEN RO.ItemType = 1 THEN '' 
							WHEN RO.ItemType = 2 THEN ''
							ELSE  '' END)
	,ContractorName = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
							WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
							WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
							WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
							WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
							ELSE  '' END)

	,ContractorSectionHead = RO.SectionHead
	,RO.[Status]
FROM ROMechanics As RM
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS MEI ON MEI.Id =  RM.MLMechanicsId
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = MEI.GeneralInformationsId

LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = MEI.PositionsId
LEFT JOIN ROs As RO ON RO.Id = RM.ROId 
	LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
	LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
		LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId

LEFT JOIN Contractors C ON C.Id = RO.ContractorId
LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId

LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId

WHERE RM.MLMechanicsId NOT IN (SELECT RW.EmployeeId FROM ROWorkAssignment As RW
								LEFT JOIN RODetails As RD ON RW.EmployeeId = RD.Id
								LEFT JOIN ROs As RO ON RD.ROId = RO.Id WHERE RW.RODetailId = @Id) 
	AND RM.ROId IN (SELECT RD.ROId FROM RODetails As RD WHERE RD.Id = @Id)) T
	WHERE T.Status != 4 AND T.Status != 5 AND T.Status != 7";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsWorkAssignments>(query, obj).ToList();
                connection.Close();
                return Lists;
            }

        }
        public List<clsWorkAssignments> SearchByJobDetails(clsROMechanics obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsWorkAssignments> Lists = new List<clsWorkAssignments>();
                String query = @"SELECT * FROM (SELECT RW.[Id]
      ,RW.[RODetailId] As JODetailId
      ,RW.[EmployeeId]
      ,RW.[DateEncoded]
      ,RW.[IsActive]
	  ,Tariff.[WorkDescription]
	  ,S.GroupDescription As Section
	  ,JC.Name As JobCategory
	  ,JT.Name As JobType
	  ,CONCAT(GI.FirstName,' ' ,GI.MiddleName,' ',GI.LastName,' ' ,GI.NameExtension) As MechanicName
	  ,RO.RONo As ReferenceNo
      ,RO.RefYear
      ,1 ReferenceType
,EquipmentType = RO.ItemType
       ,RO.EquipmentId
	   ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
							                             WHEN RO.ItemType = 1 THEN SD.ToolName
							                             WHEN RO.ItemType = 2 THEN OT.[Name]  
							                             ELSE  '' END)
	,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
							WHEN RO.ItemType = 1 THEN 'STE' 
							WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							ELSE  '' END)

	,EquipmentLocation = (CASE WHEN RO.ItemType = 0 THEN EQ.ActualLocation 
							WHEN RO.ItemType = 1 THEN '' 
							WHEN RO.ItemType = 2 THEN ''
							ELSE  '' END)
	,ContractorName = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
							WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
							WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
							WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
							WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
							ELSE  '' END)

	,ContractorSectionHead = RO.SectionHead
	,RO.Status
FROM [ROWorkAssignment] AS RW
LEFT JOIN  [dbo].RODetails AS RD ON RD.Id = RW.RODetailId
LEFT JOIN Tariff ON Tariff.Id = RD.TariffId
LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
LEFT JOIN [Status] ON STATUS.Id = RD.StatusId
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS MEI ON MEI.Id =  RW.EmployeeId
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = MEI.GeneralInformationsId
LEFT JOIN ROs As RO ON RO.Id = RD.ROId 
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
	LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
		LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId

LEFT JOIN Contractors C ON C.Id = RO.ContractorId
LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId

LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId
WHERE RW.EMployeeId = @EmployeeId
UNION ALL
SELECT 0 [Id]
      ,RD.Id [JODetailId]
      ,0 EmployeeId
      ,'' [DateEncoded]
      ,0 [IsActive]
	  ,Tariff.[WorkDescription]
	  ,S.GroupDescription As Section
	  ,JC.Name As JobCategory
	  ,JT.Name As JobType
	  ,'' MechanicName
	  ,RO.RONo As ReferenceNo
      ,RO.RefYear
      ,1 ReferenceType
      ,EquipmentType = RO.ItemType
      ,RO.EquipmentId
	  ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
							                             WHEN RO.ItemType = 1 THEN SD.ToolName
							                             WHEN RO.ItemType = 2 THEN OT.[Name]  
							                             ELSE  '' END)
	  ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
							WHEN RO.ItemType = 1 THEN 'STE' 
							WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							ELSE  '' END)

	  ,EquipmentLocation = (CASE WHEN RO.ItemType = 0 THEN EQ.ActualLocation 
							WHEN RO.ItemType = 1 THEN '' 
							WHEN RO.ItemType = 2 THEN ''
							ELSE  '' END)
	  ,ContractorName = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
							WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
							WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
							WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
							WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
							ELSE  '' END)

	,ContractorSectionHead = RO.SectionHead
	,RO.Status
FROM [dbo].RODetails AS RD
LEFT JOIN Tariff ON Tariff.Id = RD.TariffId
LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
LEFT JOIN Status ON STATUS.Id = RD.StatusId
LEFT JOIN [dbo].[ROs] AS RO ON RO.Id = RD.ROId
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
	LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
		LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId

LEFT JOIN Contractors C ON C.Id = RO.ContractorId
LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId

LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId

WHERE RD.Id NOT IN (SELECT RW.RODetailId FROM ROWorkAssignment As RW
										LEFT JOIN ROMechanics As RM ON RW.EmployeeId = RM.MLMechanicsId
										LEFT JOIN ROs As RO ON RM.ROId = RO.Id WHERE RM.MLMechanicsId = @EmployeeId) 
	AND RD.ROId IN (SELECT RM.ROId FROM ROMechanics As RM WHERE RM.MLMechanicsId = @EmployeeId)) T
	WHERE T.Status != 4 AND T.Status != 5 AND T.Status != 7  ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsWorkAssignments>(query, obj).ToList();
                connection.Close();
                return Lists;
            }

        }
    }
}

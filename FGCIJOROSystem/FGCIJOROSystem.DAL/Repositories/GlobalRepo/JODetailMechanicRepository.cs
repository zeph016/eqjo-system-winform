using FGCIJOROSystem.Domain.WorkAssignment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FGCIJOROSystem.Domain.JobOrder;
namespace FGCIJOROSystem.DAL.Repositories.GlobalRepo
{
    public class JODetailMechanicRepository
    {
        public List<clsWorkAssignments> SearchByMechanics(clsJODetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsWorkAssignments> Lists = new List<clsWorkAssignments>();
                String query = @"SELECT * FROM (SELECT JW.[Id]
        ,JW.[JODetailId]
        ,JW.[EmployeeId]
        ,JW.[DateEncoded]
        ,JW.[IsActive]
	    ,Tariff.[WorkDescription]
	    ,S.GroupDescription As Section
	    ,JC.Name As JobCategory
	    ,JT.Name As JobType
	    ,CONCAT(GI.FirstName,' ' ,GI.MiddleName,' ',GI.LastName,' ' ,GI.NameExtension) As MechanicName
	    ,Pos.PositionName As Position
	    ,JO.JONo As ReferenceNo
        ,JO.RefYear
       ,EquipmentType = JO.ItemType
       ,JO.EquipmentId
	   ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                             WHEN JO.ItemType = 1 THEN SD.ToolName
							                             WHEN JO.ItemType = 2 THEN OT.[Name]  
							                             ELSE  '' END)
	,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							WHEN JO.ItemType = 1 THEN 'STE' 
							WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							ELSE  '' END)

	,EquipmentLocation = (CASE WHEN JO.ItemType = 0 THEN EQ.ActualLocation 
							WHEN JO.ItemType = 1 THEN '' 
							WHEN JO.ItemType = 2 THEN ''
							ELSE  '' END)
	,ContractorName = (CASE WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 0 THEN C.CompanyName 
							WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 1 THEN Sec.GroupDescription
							WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 1 THEN SC.CompanyName
							WHEN JO.ContractorCategory = 2 AND JO.ContractorType = 1 THEN SDp.DepartmentName
							WHEN JO.ContractorCategory = 3 AND JO.ContractorType = 1 THEN SS.SectionName
							ELSE  '' END)

	,ContractorSectionHead = JO.SectionHead
	,JO.Status
FROM [JOWorkAssignment] AS JW
LEFT JOIN  [dbo].JODetails AS JD ON JD.Id = JW.JODetailId
LEFT JOIN Tariff ON Tariff.Id = JD.TariffId
LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
LEFT JOIN [Status] ON STATUS.Id = JD.StatusId
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS MEI ON JW.EmployeeId   =  MEI.Id 
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = MEI.GeneralInformationsId
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = MEI.PositionsId
LEFT JOIN JOs As JO ON JO.Id = JD.JOId 
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
	LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
		LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId

LEFT JOIN Contractors C ON C.Id = JO.ContractorId
LEFT JOIN Sections Sec ON Sec.Id = JO.ContractorId
/*LEFT JOIN Personnels P ON Sec.Id = P.SectionId 
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id */
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = JO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = JO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = JO.ContractorId
WHERE JD.Id = @Id
UNION ALL
SELECT 0 [Id]
      ,'' [JODetailId]
      ,JM.MLMechanicsId
      ,'' [DateEncoded]
      ,0 [IsActive]
	  ,'' [WorkDescription]
	  ,'' Section
	  ,'' JobCategory
	  ,'' JobType
	  ,CONCAT(GI.FirstName, ' ', GI.MiddleName, ' ', GI.LastName, ' ', GI.LastName) MechanicName
	  ,Pos.PositionName As Position
	  ,JO.JONo As ReferenceNo
      ,JO.RefYear
       ,EquipmentType = JO.ItemType
       ,JO.EquipmentId
	   ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                             WHEN JO.ItemType = 1 THEN SD.ToolName
							                             WHEN JO.ItemType = 2 THEN OT.[Name]  
							                             ELSE  '' END)
	,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							WHEN JO.ItemType = 1 THEN 'STE' 
							WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							ELSE  '' END)

	,EquipmentLocation = (CASE WHEN JO.ItemType = 0 THEN EQ.ActualLocation 
							WHEN JO.ItemType = 1 THEN '' 
							WHEN JO.ItemType = 2 THEN ''
							ELSE  '' END)
	,ContractorName = (CASE WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 0 THEN C.CompanyName 
							WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 1 THEN Sec.GroupDescription
							WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 1 THEN SC.CompanyName
							WHEN JO.ContractorCategory = 2 AND JO.ContractorType = 1 THEN SDp.DepartmentName
							WHEN JO.ContractorCategory = 3 AND JO.ContractorType = 1 THEN SS.SectionName
							ELSE  '' END)

	,ContractorSectionHead = JO.SectionHead
	,JO.Status
FROM JOMechanics As JM
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS MEI ON JM.MLMechanicsId  =  MEI.Id 
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = MEI.GeneralInformationsId

LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = MEI.PositionsId
LEFT JOIN JOs As JO ON JO.Id = JM.JOId 
	LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
	LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
		LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId

LEFT JOIN Contractors C ON C.Id = JO.ContractorId
LEFT JOIN Sections Sec ON Sec.Id = JO.ContractorId
/*LEFT JOIN Personnels P ON Sec.Id = P.SectionId 
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id */
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = JO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = JO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = JO.ContractorId

WHERE JM.MLMechanicsId NOT IN (SELECT JW.EmployeeId FROM JOWorkAssignment As JW
								LEFT JOIN JODetails As JD ON JW.EmployeeId = JD.Id
								LEFT JOIN JOs As JO ON JD.JOId = JO.Id WHERE JW.JODetailId = @Id) 
	AND JM.JOId IN (SELECT JD.JOId FROM JODetails As JD WHERE JD.Id = @Id)) T
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
        public List<clsWorkAssignments> SearchByJobDetails(clsMechanics obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsWorkAssignments> Lists = new List<clsWorkAssignments>();
                String query = @"SELECT * FROM (SELECT JW.[Id]
      ,JW.[JODetailId]
      ,JW.[EmployeeId]
      ,JW.[DateEncoded]
      ,JW.[IsActive]
	  ,Tariff.[WorkDescription]
	  ,S.GroupDescription As Section
	  ,JC.Name As JobCategory
	  ,JT.Name As JobType
	  ,CONCAT(GI.FirstName,' ' ,GI.MiddleName,' ',GI.LastName,' ' ,GI.NameExtension) As MechanicName
	  ,JO.JONo As ReferenceNo
      ,JO.RefYear
      ,EquipmentType = JO.ItemType
      ,JO.EquipmentId
	   ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                             WHEN JO.ItemType = 1 THEN SD.ToolName
							                             WHEN JO.ItemType = 2 THEN OT.[Name]  
							                             ELSE  '' END)
	,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							WHEN JO.ItemType = 1 THEN 'STE' 
							WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							ELSE  '' END)

	,EquipmentLocation = (CASE WHEN JO.ItemType = 0 THEN EQ.ActualLocation 
							WHEN JO.ItemType = 1 THEN '' 
							WHEN JO.ItemType = 2 THEN ''
							ELSE  '' END)
	,ContractorName = (CASE WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 0 THEN C.CompanyName 
							WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 1 THEN Sec.GroupDescription
							WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 1 THEN SC.CompanyName
							WHEN JO.ContractorCategory = 2 AND JO.ContractorType = 1 THEN SDp.DepartmentName
							WHEN JO.ContractorCategory = 3 AND JO.ContractorType = 1 THEN SS.SectionName
							ELSE  '' END)

	,ContractorSectionHead = JO.SectionHead
	,JO.Status
FROM [JOWorkAssignment] AS JW
LEFT JOIN  [dbo].JODetails AS JD ON JD.Id = JW.JODetailId
LEFT JOIN Tariff ON Tariff.Id = JD.TariffId
LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
LEFT JOIN [Status] ON STATUS.Id = JD.StatusId
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS MEI ON JW.EmployeeId   =  MEI.Id 
LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = MEI.GeneralInformationsId
LEFT JOIN JOs As JO ON JO.Id = JD.JOId 
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
	LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
		LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId

LEFT JOIN Contractors C ON C.Id = JO.ContractorId
LEFT JOIN Sections Sec ON Sec.Id = JO.ContractorId
/*LEFT JOIN Personnels P ON Sec.Id = P.SectionId 
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id */
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = JO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = JO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = JO.ContractorId
WHERE JW.EMployeeId = @EmployeeId
UNION ALL
SELECT 0 [Id]
      ,JD.Id [JODetailId]
      ,0 EmployeeId
      ,'' [DateEncoded]
      ,0 [IsActive]
	  ,Tariff.[WorkDescription]
	  ,S.GroupDescription As Section
	  ,JC.Name As JobCategory
	  ,JT.Name As JobType
	  ,'' MechanicName
	  ,JO.JONo As ReferenceNo
       ,JO.RefYear
       ,EquipmentType = JO.ItemType
       ,JO.EquipmentId
	   ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                             WHEN JO.ItemType = 1 THEN SD.ToolName
							                             WHEN JO.ItemType = 2 THEN OT.[Name]  
							                             ELSE  '' END)
	,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							WHEN JO.ItemType = 1 THEN 'STE' 
							WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							ELSE  '' END)

	,EquipmentLocation = (CASE WHEN JO.ItemType = 0 THEN EQ.ActualLocation 
							WHEN JO.ItemType = 1 THEN '' 
							WHEN JO.ItemType = 2 THEN ''
							ELSE  '' END)
	,ContractorName = (CASE WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 0 THEN C.CompanyName 
							WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 1 THEN Sec.GroupDescription
							WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 1 THEN SC.CompanyName
							WHEN JO.ContractorCategory = 2 AND JO.ContractorType = 1 THEN SDp.DepartmentName
							WHEN JO.ContractorCategory = 3 AND JO.ContractorType = 1 THEN SS.SectionName
							ELSE  '' END)

	,ContractorSectionHead = JO.SectionHead
	,JO.Status
FROM [dbo].JODetails AS JD
LEFT JOIN Tariff ON Tariff.Id = JD.TariffId
LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
LEFT JOIN Status ON STATUS.Id = JD.StatusId
LEFT JOIN [dbo].[JOs] AS JO ON JO.Id = JD.JOId
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
	LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
		LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId

LEFT JOIN Contractors C ON C.Id = JO.ContractorId
LEFT JOIN Sections Sec ON Sec.Id = JO.ContractorId
/*LEFT JOIN Personnels P ON Sec.Id = P.SectionId 
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id */
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = JO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = JO.ContractorId
LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = JO.ContractorId

WHERE JD.Id NOT IN (SELECT JW.JODetailId FROM JOWorkAssignment As JW
										LEFT JOIN JOMechanics As JM ON JW.EmployeeId = JM.MLMechanicsId
										LEFT JOIN JOs As JO ON JM.JOId = JO.Id WHERE JM.MLMechanicsId = @EmployeeId) 
	AND JD.JOId IN (SELECT JM.JOId FROM JOMechanics As JM WHERE JM.MLMechanicsId = @EmployeeId) ) T
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
    }
}

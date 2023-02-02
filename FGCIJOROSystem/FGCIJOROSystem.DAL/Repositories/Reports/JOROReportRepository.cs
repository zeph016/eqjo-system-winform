using FGCIJOROSystem.Domain.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FGCIJOROSystem.Domain.Enums;

namespace FGCIJOROSystem.DAL.Repositories.Reports
{
    public class JOROReportRepository
    {
        #region GenerateBySection
        public List<clsJOROReports> GetAllReportBySection(bool isMechanic, string mechanicIds, bool isEquipment, string equipmentIds, bool isSection, string sectionIds,
                                                        bool IsJO, bool IsRO, bool isAttendanceGroup, bool isUnderRepair, Int64 GroupId)
        { 
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOROReports> List = new List<clsJOROReports>();
                String query = @"SELECT * FROM (SELECT DISTINCT JO.Id As JOROId,JD.Id As JORODetailId
				                            ,JO.[Status]	 
				                            ,JO.[JONo]
				                            ,jo.ChecklistNo
				                            ,CONVERT(DATETIME ,JO.[JODate]) As [JODate]
				                            ,CustomerName = (CASE WHEN JO.CustomerType = 0 THEN PCust.Name
																			                            WHEN JO.CustomerType = 1 THEN CustCom.CompanyName 
																			                            WHEN JO.CustomerType = 2 THEN CustDep.DepartmentName
																			                            WHEN JO.CustomerType = 3 THEN CustSec.SectionName
																			                            ELSE  '' END)
											,JO.EquipmentId
				                            ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
																			                            WHEN JO.ItemType = 1 THEN 'STE' 
																			                            WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
																			                            ELSE  '' END)
				                            ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
																			                            WHEN JO.ItemType = 1 THEN SD.ToolName
																			                            WHEN JO.ItemType = 2 THEN OT.[Name]  
																			                            ELSE  '' END)
					                        ,ContractorName  = (CASE WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
																			WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 0 THEN C.CompanyName 
																			WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 1 THEN Sec.GroupDescription
																			WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 1 THEN SC.CompanyName
																			WHEN JO.ContractorCategory = 2 AND JO.ContractorType = 1 THEN SDp.DepartmentName
																			WHEN JO.ContractorCategory = 3 AND JO.ContractorType = 1 THEN SS.SectionName
																			ELSE  '' END)
											,JO.ContractorId AS ContractorId
											,JO.SectionHead As ContractorSectionHead
				                            ,T.WorkDescription
											,S.Id AS SectionId
											,s.GroupDescription AS SectionName
				                            ,JD.[Amount]
				                            ,T.[WorkTimeSpan]
				                            ,[Type] = 0
				                            ,EI.Id AS EmployeeId
				                            ,sgi.FirstName
				                            ,sgi.MiddleName
				                            ,sgi.LastName
				                            ,sgi.NameExtension
				                            ,sgi.LastName + ',' + sgi.FirstName + ' ' + sgi.MiddleName + ' ' + sgi.NameExtension As FullName 
				                            ,PositionName
				                            ,WorkPercentage = (SELECT AVG(AR.WorkPercentage) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0 AND AD.DateOfUpdate = (SELECT MAX(A.DateOfUpdate) FROM ActualAdvanceDetails As A WHERE A.EmployeeId = AD.EmployeeId))
				                            ,DateOfUpdate = (SELECT Max(AD.DateOfUpdate) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0)
				                            ,jo.RefYear
											,ag.Id As GroupId
											,Ag.Name AS GroupName
			                            FROM 
				                            JOWorkAssignment AS JW
				                            LEFT JOIN Personnels as P ON JW.EmployeeId = P.EmployeeId
				                            LEFT JOIN Sections AS S on S.Id = p.SectionId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id 
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS POs ON POs.Id = EI.PositionsId												
											
											LEFT JOIN AttendanceGroups As AG on ag.Id = S.AttendanceGroupId
				                            LEFT JOIN JODetails AS JD on JD.Id = JW.JODetailId

				                            LEFT JOIN Tariff AS T ON T.Id = JD.TariffId

				                            LEFT JOIN JOs AS JO ON JD.JOId = JO.Id
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
													LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
														 LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
												LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
												LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId

				                            LEFT JOIN Contractors C ON C.Id = JO.ContractorId
				                            LEFT JOIN Sections Sec ON Sec.Id = JO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = JO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = JO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = JO.ContractorId

				                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = JO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = JO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = JO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = JO.CustomerId
			                            UNION ALL
			                             SELECT DISTINCT RO.Id As JOROId,RD.Id As JORODetailId
				                            ,RO.[Status]	 
				                            ,RO.[RONo]
				                            ,Ro.ChecklistNo
				                            ,CONVERT(DATETIME ,RO.[RODate]) As [RODate]
				                            ,CustomerName = (CASE WHEN RO.CustomerType = 0 THEN PCust.Name
																			                            WHEN RO.CustomerType = 1 THEN CustCom.CompanyName 
																			                            WHEN RO.CustomerType = 2 THEN CustDep.DepartmentName
																			                            WHEN RO.CustomerType = 3 THEN CustSec.SectionName
																			                            ELSE  '' END)
											,RO.EquipmentId
				                            ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
																			                            WHEN RO.ItemType = 1 THEN 'STE' 
																			                            WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
																			                            ELSE  '' END)
				                            ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
																			                            WHEN RO.ItemType = 1 THEN SD.ToolName
																			                            WHEN RO.ItemType = 2 THEN OT.[Name]  
																			                            ELSE  '' END)
					                        ,ContractorName  = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
											                            WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
											                            WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
											                            WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
											                            WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
											                            WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
											                            ELSE  '' END)
											,RO.ContractorId AS ContractorId
											,RO.SectionHead As ContractorSectionHead
				                            ,T.WorkDescription
											,S.Id AS SectionId
											,s.GroupDescription AS SectionName
				                            ,RD.[Amount]
				                            ,T.[WorkTimeSpan]
				                            ,[Type] = 1
				                            ,EI.Id AS EmployeeId
				                            ,sgi.FirstName
				                            ,sgi.MiddleName
				                            ,sgi.LastName
				                            ,sgi.NameExtension
				                            ,sgi.LastName + ',' + sgi.FirstName + ' ' + sgi.MiddleName + ' ' + sgi.NameExtension As FullName 
				                            ,PositionName
				                            ,WorkPercentage = (SELECT AVG(AR.WorkPercentage) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = RD.Id AND AR.ReferenceType = 0 AND AR.Type = 0 AND AD.DateOfUpdate = (SELECT MAX(A.DateOfUpdate) FROM ActualAdvanceDetails As A WHERE A.EmployeeId = AD.EmployeeId))
				                            ,DateOfUpdate = (SELECT Max(AD.DateOfUpdate) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = RD.Id AND AR.ReferenceType = 0 AND AR.Type = 0)
				                            ,RO.RefYear
											,ag.Id As GroupId
											,Ag.Name AS GroupName
			                            FROM 
				                            ROWorkAssignment AS RW
				                            LEFT JOIN Personnels as P ON RW.EmployeeId = P.EmployeeId
				                            LEFT JOIN Sections AS S on S.Id = p.SectionId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id 
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS POs ON POs.Id = EI.PositionsId											
											
											LEFT JOIN AttendanceGroups As AG on AG.Id = s.AttendanceGroupId
				                            LEFT JOIN RODetails AS RD on RD.Id = RW.RODetailId											
				                           
				                            LEFT JOIN Tariff AS T ON T.Id = RD.TariffId

				                            LEFT JOIN ROs AS RO ON RD.ROId = RO.Id
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
													LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
														LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
												LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
												LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId

				                            LEFT JOIN Contractors C ON C.Id = RO.ContractorId
				                            LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId

				                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = RO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = RO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = RO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = RO.CustomerId ) T WHERE JOROId <> 0";
                if (isEquipment)
                {
                    query += " AND T.EquipmentId IN ( " + equipmentIds + " )";
                }
                if (isMechanic)
                {
                    query += " AND T.EmployeeId IN ( " + mechanicIds + " )";
                }
                if (isSection)
                {
                    query += " AND T.SectionId IN ( " + sectionIds + " )";
                }
                if (isAttendanceGroup)
                {
                    query += " AND GroupId IN ( " + GroupId + " )";
                }
                if (IsJO)
                {
                    query += " AND Type = 0";
                }
                if (IsRO)
                {
                    query += " AND Type = 1";
                }
                if (isUnderRepair)
                {
                    query += " AND Status <> 5 AND Status <> 4";
                }
                query += " ORDER BY [Type] ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOROReports>(query).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsJOROReports> GetAllEquipmentOnly(bool isMechanic, string mechanicIds, bool isEquipment, string equipmentIds, bool isSection, string sectionIds,
                                                        bool IsJO, bool IsRO, bool isAttendanceGroup, Int64 GroupId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOROReports> List = new List<clsJOROReports>();
                String query = @"SELECT DISTINCT
				                            JO.[Status]
											,JO.EquipmentId
											,PPEName AS EquipmentCode
											,PPETypeId
											,PPETypeName AS EquipmentName
											,PPEClassId AS ClassId
											,PPEClassName AS EquipmentClass
			                            FROM  JOs AS JO
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
													LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
														 LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
															LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
																LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId

																WHERE EquipmentId <> 0";
                if (isEquipment)
                {
                    query += " AND EquipmentId IN ( " + equipmentIds + " )";
                }
                if (isMechanic)
                {
                    query += " AND T.EmployeeId IN ( " + mechanicIds + " )";
                }
                if (isSection)
                {
                    query += " AND T.SectionId IN ( " + sectionIds + " )";
                }
                if (isAttendanceGroup)
                {
                    query += " AND GroupId IN ( " + GroupId + " )";
                }
                if (IsJO)
                {
                    query += " AND Type = 0";
                }
                if (IsRO)
                {
                    query += " AND Type = 1";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                query += " ORDER BY PPEName";
                List = connection.Query<clsJOROReports>(query).ToList();
                connection.Close();
                return List;
            }
        }
        #endregion
        #region GenerateByEquipment
        public List<clsJOROReports> GetAllReportByEquipment(bool isEquipment, string equipmentIds,
                                                        bool IsJO, bool IsRO, bool isAttendanceGroup, Int64 GroupId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOROReports> List = new List<clsJOROReports>();
                String query = @"SELECT * FROM (SELECT DISTINCT JW.[Id],JO.Id As JOROId,JD.Id As JORODetailId
				                            ,JO.[Status]	 
				                            ,JO.[JONo]
				                            ,jo.ChecklistNo
				                            ,CONVERT(DATETIME ,JO.[JODate]) As [JODate]
				                            ,CustomerName = (CASE WHEN JO.CustomerType = 0 THEN PCust.Name
																			                            WHEN JO.CustomerType = 1 THEN CustCom.CompanyName 
																			                            WHEN JO.CustomerType = 2 THEN CustDep.DepartmentName
																			                            WHEN JO.CustomerType = 3 THEN CustSec.SectionName
																			                            ELSE  '' END)
											,JO.EquipmentId
				                            ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
																			                            WHEN JO.ItemType = 1 THEN 'STE' 
																			                            WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
																			                            ELSE  '' END)
				                            ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
																			                            WHEN JO.ItemType = 1 THEN SD.ToolName
																			                            WHEN JO.ItemType = 2 THEN OT.[Name]  
																			                            ELSE  '' END)
					                        ,ContractorName  = (CASE WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
																			WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 0 THEN C.CompanyName 
																			WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 1 THEN Sec.GroupDescription
																			WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 1 THEN SC.CompanyName
																			WHEN JO.ContractorCategory = 2 AND JO.ContractorType = 1 THEN SDp.DepartmentName
																			WHEN JO.ContractorCategory = 3 AND JO.ContractorType = 1 THEN SS.SectionName
																			ELSE  '' END)
											,JO.ContractorId AS ContractorId
											,JO.SectionHead As ContractorSectionHead
				                            ,T.WorkDescription
											,S.Id AS SectionId
											,s.GroupDescription AS SectionName
				                            ,JD.[Amount]
				                            ,T.[WorkTimeSpan]
				                            ,[Type] = 0
				                            ,EI.Id AS EmployeeId
				                            ,sgi.FirstName
				                            ,sgi.MiddleName
				                            ,sgi.LastName
				                            ,sgi.NameExtension
				                            ,sgi.LastName + ',' + sgi.FirstName + ' ' + sgi.MiddleName + ' ' + sgi.NameExtension As FullName 
				                            ,PositionName
				                            ,WorkPercentage = (SELECT AVG(AR.WorkPercentage) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0 AND AD.DateOfUpdate = (SELECT MAX(A.DateOfUpdate) FROM ActualAdvanceDetails As A WHERE A.EmployeeId = AD.EmployeeId))
				                            ,DateOfUpdate = (SELECT Max(AD.DateOfUpdate) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0)
				                            ,jo.RefYear
											,ag.Id As GroupId
											,Ag.Name AS GroupName
			                            FROM 
				                            JOWorkAssignment AS JW
				                            LEFT JOIN Personnels as P ON JW.EmployeeId = P.EmployeeId
				                            LEFT JOIN Sections AS S on S.Id = p.SectionId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id 
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS POs ON POs.Id = EI.PositionsId												
											
											LEFT JOIN AttendanceGroups As AG on ag.Id = S.AttendanceGroupId
				                            LEFT JOIN JODetails AS JD on JD.Id = JW.JODetailId

				                            LEFT JOIN Tariff AS T ON T.Id = JD.TariffId

				                            LEFT JOIN JOs AS JO ON JD.JOId = JO.Id
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
													LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
														 LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
												LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
												LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId

				                            LEFT JOIN Contractors C ON C.Id = JO.ContractorId
				                            LEFT JOIN Sections Sec ON Sec.Id = JO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = JO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = JO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = JO.ContractorId

				                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = JO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = JO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = JO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = JO.CustomerId
			                            UNION ALL
			                             SELECT DISTINCT RW.[Id],RO.Id As JOROId,RD.Id As JORODetailId
				                            ,RO.[Status]	 
				                            ,RO.[RONo]
				                            ,Ro.ChecklistNo
				                            ,CONVERT(DATETIME ,RO.[RODate]) As [RODate]
				                            ,CustomerName = (CASE WHEN RO.CustomerType = 0 THEN PCust.Name
																			                            WHEN RO.CustomerType = 1 THEN CustCom.CompanyName 
																			                            WHEN RO.CustomerType = 2 THEN CustDep.DepartmentName
																			                            WHEN RO.CustomerType = 3 THEN CustSec.SectionName
																			                            ELSE  '' END)
											,RO.EquipmentId
				                            ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
																			                            WHEN RO.ItemType = 1 THEN 'STE' 
																			                            WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
																			                            ELSE  '' END)
				                            ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
																			                            WHEN RO.ItemType = 1 THEN SD.ToolName
																			                            WHEN RO.ItemType = 2 THEN OT.[Name]  
																			                            ELSE  '' END)
					                        ,ContractorName  = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
											                            WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
											                            WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
											                            WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
											                            WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
											                            WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
											                            ELSE  '' END)
											,RO.ContractorId AS ContractorId
											,RO.SectionHead As ContractorSectionHead
				                            ,T.WorkDescription
											,S.Id AS SectionId
											,s.GroupDescription AS SectionName
				                            ,RD.[Amount]
				                            ,T.[WorkTimeSpan]
				                            ,[Type] = 1
				                            ,EI.Id AS EmployeeId
				                            ,sgi.FirstName
				                            ,sgi.MiddleName
				                            ,sgi.LastName
				                            ,sgi.NameExtension
				                            ,sgi.LastName + ',' + sgi.FirstName + ' ' + sgi.MiddleName + ' ' + sgi.NameExtension As FullName 
				                            ,PositionName
				                            ,WorkPercentage = (SELECT AVG(AR.WorkPercentage) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = RD.Id AND AR.ReferenceType = 0 AND AR.Type = 0 AND AD.DateOfUpdate = (SELECT MAX(A.DateOfUpdate) FROM ActualAdvanceDetails As A WHERE A.EmployeeId = AD.EmployeeId))
				                            ,DateOfUpdate = (SELECT Max(AD.DateOfUpdate) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = RD.Id AND AR.ReferenceType = 0 AND AR.Type = 0)
				                            ,RO.RefYear
											,ag.Id As GroupId
											,Ag.Name AS GroupName
			                            FROM 
				                            ROWorkAssignment AS RW
				                            LEFT JOIN Personnels as P ON RW.EmployeeId = P.EmployeeId
				                            LEFT JOIN Sections AS S on S.Id = p.SectionId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id 
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS POs ON POs.Id = EI.PositionsId											
											
											LEFT JOIN AttendanceGroups As AG on AG.Id = s.AttendanceGroupId
				                            LEFT JOIN RODetails AS RD on RD.Id = RW.RODetailId											
				                           
				                            LEFT JOIN Tariff AS T ON T.Id = RD.TariffId

				                            LEFT JOIN ROs AS RO ON RD.ROId = RO.Id
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
													LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
														LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
												LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
												LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId

				                            LEFT JOIN Contractors C ON C.Id = RO.ContractorId
				                            LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId

				                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = RO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = RO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = RO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = RO.CustomerId ) T WHERE Id <> 0";
                if (isEquipment)
                {
                    query += " AND T.EquipmentId IN ( " + equipmentIds + " )";
                }
                if (isAttendanceGroup)
                {
                    query += " AND GroupId IN ( " + GroupId + " )";
                }
                if (IsJO)
                {
                    query += " AND Type = 0";
                }
                if (IsRO)
                {
                    query += " AND Type = 1";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                query += " ORDER BY [Type] ";
                List = connection.Query<clsJOROReports>(query).ToList();
                connection.Close();
                return List;
            }
        }
        #endregion
        #region GenerateByMechanic
        public List<clsJOROReports> GetAllReportByMechanic(bool isMechanic, string mechanicIds,
                                                        bool IsJO, bool IsRO)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOROReports> List = new List<clsJOROReports>();
                String query = @"SELECT * FROM (SELECT DISTINCT JW.[Id],JO.Id As JOROId,JD.Id As JORODetailId
				                            ,JO.[Status]	 
				                            ,JO.[JONo]
				                            ,jo.ChecklistNo
				                            ,CONVERT(DATETIME ,JO.[JODate]) As [JODate]
				                            ,CustomerName = (CASE WHEN JO.CustomerType = 0 THEN PCust.Name
																			                            WHEN JO.CustomerType = 1 THEN CustCom.CompanyName 
																			                            WHEN JO.CustomerType = 2 THEN CustDep.DepartmentName
																			                            WHEN JO.CustomerType = 3 THEN CustSec.SectionName
																			                            ELSE  '' END)
											,JO.EquipmentId
				                            ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
																			                            WHEN JO.ItemType = 1 THEN 'STE' 
																			                            WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
																			                            ELSE  '' END)
				                            ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
																			                            WHEN JO.ItemType = 1 THEN SD.ToolName
																			                            WHEN JO.ItemType = 2 THEN OT.[Name]  
																			                            ELSE  '' END)
					                        ,ContractorName  = (CASE WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
																			WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 0 THEN C.CompanyName 
																			WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 1 THEN Sec.GroupDescription
																			WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 1 THEN SC.CompanyName
																			WHEN JO.ContractorCategory = 2 AND JO.ContractorType = 1 THEN SDp.DepartmentName
																			WHEN JO.ContractorCategory = 3 AND JO.ContractorType = 1 THEN SS.SectionName
																			ELSE  '' END)
											,JO.ContractorId AS ContractorId
											,JO.SectionHead As ContractorSectionHead
				                            ,T.WorkDescription
											,S.Id AS SectionId
											,s.GroupDescription AS SectionName
				                            ,JD.[Amount]
				                            ,T.[WorkTimeSpan]
				                            ,[Type] = 0
				                            ,EI.Id AS EmployeeId
				                            ,sgi.FirstName
				                            ,sgi.MiddleName
				                            ,sgi.LastName
				                            ,sgi.NameExtension
				                            ,sgi.LastName + ',' + sgi.FirstName + ' ' + sgi.MiddleName + ' ' + sgi.NameExtension As FullName 
				                            ,PositionName
				                            ,WorkPercentage = (SELECT AVG(AR.WorkPercentage) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0 AND AD.DateOfUpdate = (SELECT MAX(A.DateOfUpdate) FROM ActualAdvanceDetails As A WHERE A.EmployeeId = AD.EmployeeId))
				                            ,DateOfUpdate = (SELECT Max(AD.DateOfUpdate) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0)
				                            ,jo.RefYear
											,ag.Id As GroupId
											,Ag.Name AS GroupName
			                            FROM 
				                            JOWorkAssignment AS JW
				                            LEFT JOIN Personnels as P ON JW.EmployeeId = P.EmployeeId
				                            LEFT JOIN Sections AS S on S.Id = p.SectionId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id 
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS POs ON POs.Id = EI.PositionsId												
											
											LEFT JOIN AttendanceGroups As AG on ag.Id = S.AttendanceGroupId
				                            LEFT JOIN JODetails AS JD on JD.Id = JW.JODetailId

				                            LEFT JOIN Tariff AS T ON T.Id = JD.TariffId

				                            LEFT JOIN JOs AS JO ON JD.JOId = JO.Id
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
													LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
														 LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
												LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
												LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId

				                            LEFT JOIN Contractors C ON C.Id = JO.ContractorId
				                            LEFT JOIN Sections Sec ON Sec.Id = JO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = JO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = JO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = JO.ContractorId

				                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = JO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = JO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = JO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = JO.CustomerId
			                            UNION ALL
			                             SELECT DISTINCT RW.[Id],RO.Id As JOROId,RD.Id As JORODetailId
				                            ,RO.[Status]	 
				                            ,RO.[RONo]
				                            ,Ro.ChecklistNo
				                            ,CONVERT(DATETIME ,RO.[RODate]) As [RODate]
				                            ,CustomerName = (CASE WHEN RO.CustomerType = 0 THEN PCust.Name
																			                            WHEN RO.CustomerType = 1 THEN CustCom.CompanyName 
																			                            WHEN RO.CustomerType = 2 THEN CustDep.DepartmentName
																			                            WHEN RO.CustomerType = 3 THEN CustSec.SectionName
																			                            ELSE  '' END)
											,RO.EquipmentId
				                            ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
																			                            WHEN RO.ItemType = 1 THEN 'STE' 
																			                            WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
																			                            ELSE  '' END)
				                            ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
																			                            WHEN RO.ItemType = 1 THEN SD.ToolName
																			                            WHEN RO.ItemType = 2 THEN OT.[Name]  
																			                            ELSE  '' END)
					                        ,ContractorName  = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
											                            WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
											                            WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
											                            WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
											                            WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
											                            WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
											                            ELSE  '' END)
											,RO.ContractorId AS ContractorId
											,RO.SectionHead As ContractorSectionHead
				                            ,T.WorkDescription
											,S.Id AS SectionId
											,s.GroupDescription AS SectionName
				                            ,RD.[Amount]
				                            ,T.[WorkTimeSpan]
				                            ,[Type] = 1
				                            ,EI.Id AS EmployeeId
				                            ,sgi.FirstName
				                            ,sgi.MiddleName
				                            ,sgi.LastName
				                            ,sgi.NameExtension
				                            ,sgi.LastName + ',' + sgi.FirstName + ' ' + sgi.MiddleName + ' ' + sgi.NameExtension As FullName 
				                            ,PositionName
				                            ,WorkPercentage = (SELECT AVG(AR.WorkPercentage) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = RD.Id AND AR.ReferenceType = 0 AND AR.Type = 0 AND AD.DateOfUpdate = (SELECT MAX(A.DateOfUpdate) FROM ActualAdvanceDetails As A WHERE A.EmployeeId = AD.EmployeeId))
				                            ,DateOfUpdate = (SELECT Max(AD.DateOfUpdate) FROM ActualAdvanceRefNos As AR 
							                            LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
							                            WHERE AR.JODetailId = RD.Id AND AR.ReferenceType = 0 AND AR.Type = 0)
				                            ,RO.RefYear
											,ag.Id As GroupId
											,Ag.Name AS GroupName
			                            FROM 
				                            ROWorkAssignment AS RW
				                            LEFT JOIN Personnels as P ON RW.EmployeeId = P.EmployeeId
				                            LEFT JOIN Sections AS S on S.Id = p.SectionId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id 
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS POs ON POs.Id = EI.PositionsId											
											
											LEFT JOIN AttendanceGroups As AG on AG.Id = s.AttendanceGroupId
				                            LEFT JOIN RODetails AS RD on RD.Id = RW.RODetailId											
				                           
				                            LEFT JOIN Tariff AS T ON T.Id = RD.TariffId

				                            LEFT JOIN ROs AS RO ON RD.ROId = RO.Id
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
													LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
														LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
												LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
												LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId

				                            LEFT JOIN Contractors C ON C.Id = RO.ContractorId
				                            LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
				                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId

				                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = RO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = RO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = RO.CustomerId
				                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = RO.CustomerId ) T WHERE Id <> 0";
                if (isMechanic)
                {
                    query += " AND T.EmployeeId IN ( " + mechanicIds + " )";
                }
                if (IsJO)
                {
                    query += " AND Type = 0";
                }
                if (IsRO)
                {
                    query += " AND Type = 1";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                query += " ORDER BY [Type] ";
                List = connection.Query<clsJOROReports>(query).ToList();
                connection.Close();
                return List;
            }
        }
        #endregion
        #region LoadAllEquipment
        public List<clsJOROReports> LoadAllEquipment()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOROReports> List = new List<clsJOROReports>();
                String query = @"SELECT 
                                    '0' AS EquipmentType,
                                    EQ.Id AS EquipmentId,
                                    EQ.Id As EquipmentMasterlistId,
                                    EQ.PPEName As EquipmentCode,                                        
                                    ET.PPETypeName AS EquipmentName,                                        
                                    EQ.ActualLocation As Location, 
                                    GID.FirstName AS DFirstname, GID.MiddleName AS DMiddlename, GID.LastName AS DLastName, GID.NameExtension AS DNameExtension                                        
                                FROM [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ 
                                    INNER JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GID ON ER.MLDriversNameId = GID.Id 
                                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EID ON GID.Id = EID.GeneralInformationsId 
                                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Positions] PD ON EID.PositionsId = PD.Id ";
                //query += " AND AttendanceGroupId IN ( " + attendanceGroupId + " )";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOROReports>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public List<clsJOROReports> LoadAllEquipmentBySite()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOROReports> List = new List<clsJOROReports>();
                String query = @"SELECT DISTINCT
                                    '0' AS EquipmentType,
                                    EQ.Id AS EquipmentId,
                                    EQ.Id As EquipmentMasterlistId,
                                    EQ.PPEName As EquipmentCode,                                        
                                    ET.PPETypeName AS EquipmentName,                                        
                                    EQ.ActualLocation As Location, 
                                    GID.FirstName AS DFirstname, GID.MiddleName AS DMiddlename, GID.LastName AS DLastName, GID.NameExtension AS DNameExtension                                        
                                FROM [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ 
                                    INNER JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GID ON ER.MLDriversNameId = GID.Id 
                                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EID ON GID.Id = EID.GeneralInformationsId 
                                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Positions] PD ON EID.PositionsId = PD.Id 
                                                    WHERE ActualLocation NOT LIKE 'ab%' AND ActualLocation NOT LIKE ''";
                //query += " AND AttendanceGroupId IN ( " + attendanceGroupId + " )";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOROReports>(query).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsJOROReports> LoadAllEquipmentLocations()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOROReports> List = new List<clsJOROReports>();
                String query = @"SELECT DISTINCT
                                        [ActualLocation] AS Location
                                        FROM [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus]
										WHERE ActualLocation NOT LIKE 'ab%' AND ActualLocation NOT LIKE ''
										ORDER BY ActualLocation";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOROReports>(query).ToList();
                connection.Close();
                return List;
            }
        }
        #endregion
    }
}

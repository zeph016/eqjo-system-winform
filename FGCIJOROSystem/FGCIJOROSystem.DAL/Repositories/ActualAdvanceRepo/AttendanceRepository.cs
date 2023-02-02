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
    public class AttendanceRepository
    {
        public List<clsAttendance> GetAll(DateTime DateOfUpdate)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendance> List = new List<clsAttendance>();
                String query = @"SELECT * FROM (SELECT
                                                PR.Active
                                                ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                                ,PR.EmployeeId
                                                ,PR.Active AS PRActive
                                                ,JSec.Id as SectionId
                                                ,JSec.Active as SecACtive
                                                ,EquipmentCode = ''
                                                ,PR.Location	  
                                                ,JSec.GroupDescription As Section
                                                ,DateOfUpdate = null
                                                ,JSEC.BranchId
                                                ,AM = ''
                                                ,PM = ''
                                                ,AJODetailId = 0
                                                ,Activity = ''
                                                ,EstimatedTime = ''
                                                ,MinMaj = 0
                                                ,ActualTimeWorked = ''
                                                ,Status = ''
                                                ,Advance = ''                         
                                                ,AttendanceGroupId  
                                                FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                                LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                                                WHERE PR.EmployeeId NOT IN (SELECT AD.EmployeeId FROM ActualAdvanceDetails As AD WHERE AD.DateOfUpdate LIKE @DateOfUpdate)
                                                UNION ALL
                                                SELECT
                                                PR.Active
                                                ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                                ,PR.EmployeeId
                                                ,PR.Active AS PRActive
                                                ,JSec.Id as SectionId
                                                ,JSec.Active as SecACtive
                                                ,EquipmentCode = (CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
						                                                WHEN AR.EquipmentType = 1 THEN 'STE' 
						                                                WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
					                                                ELSE  '' END)
                                                ,PR.Location	  
                                                ,JSec.GroupDescription As Section
                                                ,AD.DateOfUpdate
                                                ,JSEC.BranchId
                                                ,AM.Symbol As AM
                                                ,PM.Symbol As PM
                                                ,AR.JODetailId
                                                ,CAST(Case WHEN  AR.JODetailId != 0 THEN (CASE WHEN AR.ReferenceType = 0 THEN '#JO' + SUBSTRING(JO.RefYear, 3, 3) + FORMAT(JO.JONo,'0000') ELSE '#RO' + SUBSTRING(RO.RefYear, 3, 3) + FORMAT(RO.RONo,'0000') END)
                                                 ELSE '' END as varchar) 
                                                + ' ' + AR.Activity As Activity
                                                ,EstimatedTime = (CASE WHEN AR.ReferenceType = 0 THEN JT.WorkTimeSpan ELSE RT.WorkTimeSpan END)
                                                ,MinMaj = (CASE WHEN AR.ReferenceType = 0 THEN JT.Major ELSE RT.Major END)
                                                ,(CASE WHEN AR.JODetailId != 0 
	                                                THEN (SELECT CAST(SUM(T.TimeCount) As VARCHAR) +' hr(s) or ' + CAST(SUM(CAST(T.TimeCount As FLOAT))/8 As VARCHAR) + ' day(s)' 
		                                                FROM
			                                                (SELECT 
			                                                CASE WHEN CONVERT(Time(0),AAR.TimeStarted) < '12:00:00' OR CONVERT(Time(0),AAR.TimeEnded) > '13:30:00' 
			                                                THEN (CAST(DATEDIFF(mi,CONVERT(Time(0),AAR.TimeStarted),CONVERT(Time(0),AAR.TimeEnded)) As float)/60) - 1.5 
			                                                ELSE (CAST(DATEDIFF(mi,CONVERT(Time(0),AAR.TimeStarted),CONVERT(Time(0),AAR.TimeEnded)) As float)/60) END As TimeCount
			                                                FROM [ActualAdvanceRefNos] As AAR
				                                                LEFT JOIN ActualAdvanceDetails As AAD ON AAR.ActualAdvanceId = AAD.Id
			                                                WHERE AAD.EmployeeId = AD.EmployeeId AND AAR.JODetailId = AR.JODetailId AND AAR.Type = 0
					                                                AND (AAD.DateOfUpdate BETWEEN AAD.DateOfUpdate AND AD.DateOfUpdate) 
			                                                ) AS T ) 
	                                                ELSE 			
		                                                (SELECT CAST(SUM(T.TimeCount) As VARCHAR) +' hr(s) or ' + CAST(SUM(CAST(T.TimeCount As FLOAT))/8 As VARCHAR) + ' day(s)' 
			                                                FROM 
			                                                (Select (CASE WHEN CONVERT(Time(0),AR.TimeStarted) < '12:00:00' OR CONVERT(Time(0),AR.TimeEnded) > '13:30:00' 
			                                                THEN (CAST(DATEDIFF(mi,CONVERT(Time(0),AR.TimeStarted),CONVERT(Time(0),AR.TimeEnded)) As float)/60) - 1.5 
			                                                ELSE (CAST(DATEDIFF(mi,CONVERT(Time(0),AR.TimeStarted),CONVERT(Time(0),AR.TimeEnded)) As float)/60) END) as TimeCount) T)
		                                                END) as ActualTimeWorked 
                                                ,Status = (CASE WHEN AR.ReferenceType = 0 THEN JS.Name ELSE RS.Name END)
                                                ,Advance = CAST(Case WHEN  ARD.JODetailId != 0 THEN (CASE WHEN ARD.ReferenceType = 0 THEN '#JO' + SUBSTRING(JOD.RefYear, 3, 3) + FORMAT(JOD.JONo,'0000') ELSE '#RO' + SUBSTRING(ROD.RefYear, 3, 3) + FORMAT(ROD.RONo,'0000') END)
                                                                                         ELSE '' END as varchar)  + ' ' + ARD.Activity
                                                ,AttendanceGroupId   
                                                FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                                LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
	
                                                LEFT JOIN ActualAdvanceDetails As AD ON PR.EmployeeId = AD.EmployeeId 
	                                                LEFT JOIN ActualAdvanceRefNos As AR ON AR.ActualAdvanceId = AD.Id
                                                    LEFT JOIN ActualAdvanceRefNos As ARD ON ARD.ActualAdvanceId = AD.Id AND ARD.JODetailId = AR.JODetailId AND ARD.Type = 1	

                                                LEFT JOIN JODetails As JD ON JD.Id = AR.JODetailId
	                                                LEFT JOIN JOs As JO ON JD.JOId = JO.Id
		                                                LEFT JOIN Tariff As JT ON JT.Id = JD.TariffId
			                                                LEFT JOIN Status As JS ON JS.Id = JD.StatusId

                                                LEFT JOIN RODetails As RD ON RD.Id = AR.JODetailId
	                                                LEFT JOIN ROs As RO ON RD.ROId = RO.Id
		                                                LEFT JOIN Tariff As RT ON RT.Id = RD.TariffId
			                                                LEFT JOIN Status As RS ON RS.Id = RD.StatusId

                                                LEFT JOIN JODetails As JDD ON JDD.Id = ARD.JODetailId
	                                                                                        LEFT JOIN JOs As JOD ON JD.JOId = JOD.Id
		                                                                                        LEFT JOIN Tariff As JTD ON JTD.Id = JDD.TariffId
			                                                                                        LEFT JOIN Status As JSD ON JSD.Id = JDD.StatusId

                                                                                        LEFT JOIN RODetails As RDD ON RDD.Id = ARD.JODetailId
	                                                                                        LEFT JOIN ROs As ROD ON RDD.ROId = ROD.Id
		                                                                                        LEFT JOIN Tariff As RTD ON RTD.Id = RDD.TariffId
			                                                                                        LEFT JOIN Status As RSD ON RSD.Id = RDD.StatusId
	
                                                LEFT JOIN AttendanceStatus As AM ON AM.Id = AD.AMStatus
	                                                LEFT JOIN AttendanceStatus As PM ON PM.Id = AD.PMStatus
		                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
			                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
				                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
					                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
						                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
							                                                LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId

                                                WHERE (AR.Type = 0 OR AR.Type Is Null) AND AD.DateOfUpdate LIKE @DateOfUpdate AND PR.Active = 1
                                                ) T  WHERE T.Active = 1";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsAttendance>(query, new { DateOfUpdate = DateOfUpdate }).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsAttendance> GetAllFiltered(DateTime DateOfUpdate, bool isSection, string sectionIds, bool isBranch, Int64 branchId
                                                    ,bool isAttendanceGroup, Int64 attendanceGroupId, bool isPersonnel, String personnelId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendance> List = new List<clsAttendance>();
                String query = @"SELECT * FROM (
                                            SELECT
                                            PR.Active
                                            ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                            ,PR.EmployeeId
                                            ,PR.Active AS PRActive
                                            ,JSec.Id as SectionId
                                            ,JSec.Active as SecACtive
                                            ,EquipmentCode = ''
                                            ,JO.Location	  
                                            ,JSec.GroupDescription As Section
                                            ,DateOfUpdate = null
                                            ,JSEC.BranchId
                                            ,AM = ''
                                            ,PM = ''
                                            ,AJODetailId = 0
                                            ,Activity = ''
                                            ,EstimatedTime = ''
                                            ,MinMaj = 0
                                            ,ActualTimeWorked = ''
                                            ,Status = ''
                                            ,Advance = ''    
											,AttendanceGroupId
                                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                            LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                                            LEFT JOIN ActualAdvanceDetails As AD ON PR.EmployeeId = AD.EmployeeId 
	                                            LEFT JOIN ActualAdvanceRefNos As AR ON AR.ActualAdvanceId = AD.Id
                                            LEFT JOIN JODetails As JD ON JD.Id = AR.JODetailId
	                                            LEFT JOIN JOs As JO ON JD.JOId = JO.Id
                                            WHERE PR.EmployeeId NOT IN (SELECT AD.EmployeeId FROM ActualAdvanceDetails As AD WHERE AD.DateOfUpdate LIKE CONVERT(Date,@DateOfUpdate,101))
                                            UNION ALL
                                            SELECT
                                            PR.Active
                                            ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                            ,PR.EmployeeId
                                            ,PR.Active AS PRActive
                                            ,JSec.Id as SectionId
                                            ,JSec.Active as SecACtive
                                            ,EquipmentCode = (CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
						                                            WHEN AR.EquipmentType = 1 THEN 'STE' 
						                                            WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
					                                            ELSE  '' END)
                                            ,RO.Location	  
                                            ,JSec.GroupDescription As Section
                                            ,AD.DateOfUpdate
                                            ,JSEC.BranchId
                                            ,AM.Symbol As AM
                                            ,PM.Symbol As PM
                                            ,AR.JODetailId
                                            ,CAST(Case WHEN  AR.JODetailId != 0 THEN (CASE WHEN AR.ReferenceType = 0 THEN '#JO' + SUBSTRING(JO.RefYear, 3, 3) + FORMAT(JO.JONo,'0000') ELSE '#RO' + SUBSTRING(RO.RefYear, 3, 3) + FORMAT(RO.RONo,'0000') END)
                                                                ELSE '' END as varchar) 
                                            + ' ' + AR.Activity As Activity
                                            ,EstimatedTime = (CASE WHEN AR.ReferenceType = 0 THEN JT.WorkTimeSpan ELSE RT.WorkTimeSpan END)
                                            ,MinMaj = (CASE WHEN AR.ReferenceType = 0 THEN JT.Major ELSE RT.Major END)
                                            ,(CASE WHEN AR.JODetailId != 0 
	                                            THEN (SELECT CAST(SUM(T.TimeCount) As VARCHAR) +' hr(s) or ' + CAST(SUM(CAST(T.TimeCount As FLOAT))/8 As VARCHAR) + ' day(s)' 
		                                            FROM
			                                            (SELECT 
			                                            CASE WHEN CONVERT(Time(0),AAR.TimeStarted) < '12:00:00' OR CONVERT(Time(0),AAR.TimeEnded) > '13:30:00' 
			                                            THEN (CAST(DATEDIFF(mi,CONVERT(Time(0),AAR.TimeStarted),CONVERT(Time(0),AAR.TimeEnded)) As float)/60) - 1.5 
			                                            ELSE (CAST(DATEDIFF(mi,CONVERT(Time(0),AAR.TimeStarted),CONVERT(Time(0),AAR.TimeEnded)) As float)/60) END As TimeCount
			                                            FROM [ActualAdvanceRefNos] As AAR
				                                            LEFT JOIN ActualAdvanceDetails As AAD ON AAR.ActualAdvanceId = AAD.Id
			                                            WHERE AAD.EmployeeId = AD.EmployeeId AND AAR.JODetailId = AR.JODetailId AND AAR.Type = 0
					                                            AND (AAD.DateOfUpdate BETWEEN AAD.DateOfUpdate AND AD.DateOfUpdate) 
			                                            ) AS T ) 
	                                            ELSE 			
		                                            (SELECT CAST(SUM(T.TimeCount) As VARCHAR) +' hr(s) or ' + CAST(SUM(CAST(T.TimeCount As FLOAT))/8 As VARCHAR) + ' day(s)' 
			                                            FROM 
			                                            (Select (CASE WHEN CONVERT(Time(0),AR.TimeStarted) < '12:00:00' OR CONVERT(Time(0),AR.TimeEnded) > '13:30:00' 
			                                            THEN (CAST(DATEDIFF(mi,CONVERT(Time(0),AR.TimeStarted),CONVERT(Time(0),AR.TimeEnded)) As float)/60) - 1.5 
			                                            ELSE (CAST(DATEDIFF(mi,CONVERT(Time(0),AR.TimeStarted),CONVERT(Time(0),AR.TimeEnded)) As float)/60) END) as TimeCount) T)
		                                            END) as ActualTimeWorked 
                                            ,Status = (CASE WHEN AR.ReferenceType = 0 THEN JS.Name ELSE RS.Name END)
                                            ,Advance = CAST(Case WHEN  ARD.JODetailId != 0 THEN (CASE WHEN ARD.ReferenceType = 0 THEN '#JO' + SUBSTRING(JOD.RefYear, 3, 3) + FORMAT(JOD.JONo,'0000') ELSE '#RO' + SUBSTRING(ROD.RefYear, 3, 3) + FORMAT(ROD.RONo,'0000') END)
                                             ELSE '' END as varchar)  + ' ' + ARD.Activity 
											,AttendanceGroupId
                                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                            LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
	
                                            LEFT JOIN ActualAdvanceDetails As AD ON PR.EmployeeId = AD.EmployeeId 
	                                            LEFT JOIN ActualAdvanceRefNos As AR ON AR.ActualAdvanceId = AD.Id
                                                LEFT JOIN ActualAdvanceRefNos As ARD ON ARD.ActualAdvanceId = AD.Id AND ARD.JODetailId = AR.JODetailId AND ARD.Type = 1	
	
                                            LEFT JOIN JODetails As JD ON JD.Id = AR.JODetailId
	                                            LEFT JOIN JOs As JO ON JD.JOId = JO.Id
		                                            LEFT JOIN Tariff As JT ON JT.Id = JD.TariffId
			                                            LEFT JOIN Status As JS ON JS.Id = JD.StatusId

                                            LEFT JOIN RODetails As RD ON RD.Id = AR.JODetailId
	                                            LEFT JOIN ROs As RO ON RD.ROId = RO.Id
		                                            LEFT JOIN Tariff As RT ON RT.Id = RD.TariffId
			                                            LEFT JOIN Status As RS ON RS.Id = RD.StatusId

                                            LEFT JOIN JODetails As JDD ON JDD.Id = ARD.JODetailId
	                                            LEFT JOIN JOs As JOD ON JD.JOId = JOD.Id
		                                            LEFT JOIN Tariff As JTD ON JTD.Id = JDD.TariffId
			                                            LEFT JOIN Status As JSD ON JSD.Id = JDD.StatusId

                                            LEFT JOIN RODetails As RDD ON RDD.Id = ARD.JODetailId
	                                            LEFT JOIN ROs As ROD ON RDD.ROId = ROD.Id
		                                            LEFT JOIN Tariff As RTD ON RTD.Id = RDD.TariffId
			                                            LEFT JOIN Status As RSD ON RSD.Id = RDD.StatusId
	
                                            LEFT JOIN AttendanceStatus As AM ON AM.Id = AD.AMStatus
	                                            LEFT JOIN AttendanceStatus As PM ON PM.Id = AD.PMStatus
		                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
			                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
				                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
					                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
						                                            LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
							                                            LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId

                                            WHERE (AR.Type = 0 OR AR.Type Is Null) AND AD.DateOfUpdate LIKE CONVERT(Date,@DateOfUpdate,101) AND PR.Active = 1
                                             ) T  WHERE T.Active = 1 AND ( T.AM IS NOT NULL AND T.PM IS NOT NULL AND T.AM = 'A' OR T.AM = 'AL' OR T.AM = 'AWOL' OR T.AM ='LT' OR T.AM = 'NC' OR T.AM = 'P' OR T.AM = 'RO' OR T.AM = 'R' OR T.AM = 'S' OR T.AM = 'PW')"; //Added Line- Report Fix
                if (isSection)
                {
                    query += " AND T.SectionId IN ( " + sectionIds + ") ";
                }
                if (isBranch)
                {
                    query += " AND T.BranchId IN ( " + branchId + " )";
                }
                if (isAttendanceGroup)
                {
                    query += " AND AttendanceGroupId IN ( " + attendanceGroupId + " )";
                }
                if (isPersonnel)
                {
                    query += " AND T.EmployeeId IN ( " + personnelId + " )";
                }
                query += " order by T.EmployeeName";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsAttendance>(query, new { DateOfUpdate = DateOfUpdate }).ToList();//, new { DateOfUpdate = DateOfUpdate }
                connection.Close();
                return List;
            }
        }
        public List<clsAttendance> GetAllFilteredByDate(bool isSection, string sectionIds, bool isBranch, Int64 branchId
                                                    , bool isAttendanceGroup, Int64 attendanceGroupId, DateTime startDate, DateTime endDate
                                                    ,bool isPersonnel, string personnelIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendance> List = new List<clsAttendance>();
                String query = @"SELECT * FROM (SELECT
                                            PR.Active
                                            ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                            ,PR.EmployeeId
                                            ,PR.Active AS PRActive
                                            ,JSec.Id as SectionId
                                            ,JSec.Active as SecACtive
                                            ,EquipmentCode = ''
                                            ,PR.Location	  
                                            ,JSec.GroupDescription As Section
                                            ,DateOfUpdate = null
                                            ,JSEC.BranchId
                                            ,AM = ''
                                            ,PM = ''
                                            ,AJODetailId = 0
                                            ,Activity = ''
                                            ,EstimatedTime = ''
                                            ,MinMaj = 0
                                            ,ActualTimeWorked = ''
                                            ,Status = ''
                                            ,Advance = ''    
											,AttendanceGroupId
                                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                            LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                                            WHERE PR.EmployeeId NOT IN (SELECT DISTINCT AD.EmployeeId FROM ActualAdvanceDetails As AD)
                                            UNION ALL
                                            SELECT
                                            PR.Active
                                            ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                            ,PR.EmployeeId
                                            ,PR.Active AS PRActive
                                            ,JSec.Id as SectionId
                                            ,JSec.Active as SecACtive
                                            ,EquipmentCode = (CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
						                                            WHEN AR.EquipmentType = 1 THEN 'STE' 
						                                            WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
					                                            ELSE  '' END)
                                            ,PR.Location	  
                                            ,JSec.GroupDescription As Section
                                            ,CONVERT(varchar(20),AD.DateOfUpdate,101) DateOfUpdate
                                            ,JSEC.BranchId
                                            ,AM.Symbol As AM
                                            ,PM.Symbol As PM
                                            ,AR.JODetailId
                                            ,CAST(Case WHEN  AR.JODetailId != 0 THEN (CASE WHEN AR.ReferenceType = 0 THEN '#JO' + SUBSTRING(JO.RefYear, 3, 3) + FORMAT(JO.JONo,'0000') ELSE '#RO' + SUBSTRING(RO.RefYear, 3, 3) + FORMAT(RO.RONo,'0000') END)
                                                                ELSE '' END as varchar) 
                                            + ' ' + AR.Activity As Activity
                                            ,EstimatedTime = (CASE WHEN AR.ReferenceType = 0 THEN JT.WorkTimeSpan ELSE RT.WorkTimeSpan END)
                                            ,MinMaj = (CASE WHEN AR.ReferenceType = 0 THEN JT.Major ELSE RT.Major END)
                                            ,(CASE WHEN AR.JODetailId != 0 
	                                            THEN (SELECT CAST(SUM(T.TimeCount) As VARCHAR) +' hr(s) or ' + CAST(SUM(CAST(T.TimeCount As FLOAT))/8 As VARCHAR) + ' day(s)' 
		                                            FROM
			                                            (SELECT 
			                                            CASE WHEN CONVERT(Time(0),AAR.TimeStarted) < '12:00:00' OR CONVERT(Time(0),AAR.TimeEnded) > '13:30:00' 
			                                            THEN (CAST(DATEDIFF(mi,CONVERT(Time(0),AAR.TimeStarted),CONVERT(Time(0),AAR.TimeEnded)) As float)/60) - 1.5 
			                                            ELSE (CAST(DATEDIFF(mi,CONVERT(Time(0),AAR.TimeStarted),CONVERT(Time(0),AAR.TimeEnded)) As float)/60) END As TimeCount
			                                            FROM [ActualAdvanceRefNos] As AAR
				                                            LEFT JOIN ActualAdvanceDetails As AAD ON AAR.ActualAdvanceId = AAD.Id
			                                            WHERE AAD.EmployeeId = AD.EmployeeId AND AAR.JODetailId = AR.JODetailId AND AAR.Type = 0
					                                            AND (AAD.DateOfUpdate BETWEEN AAD.DateOfUpdate AND AD.DateOfUpdate) 
			                                            ) AS T ) 
	                                            ELSE 			
		                                            (SELECT CAST(SUM(T.TimeCount) As VARCHAR) +' hr(s) or ' + CAST(SUM(CAST(T.TimeCount As FLOAT))/8 As VARCHAR) + ' day(s)' 
			                                            FROM 
			                                            (Select (CASE WHEN CONVERT(Time(0),AR.TimeStarted) < '12:00:00' OR CONVERT(Time(0),AR.TimeEnded) > '13:30:00' 
			                                            THEN (CAST(DATEDIFF(mi,CONVERT(Time(0),AR.TimeStarted),CONVERT(Time(0),AR.TimeEnded)) As float)/60) - 1.5 
			                                            ELSE (CAST(DATEDIFF(mi,CONVERT(Time(0),AR.TimeStarted),CONVERT(Time(0),AR.TimeEnded)) As float)/60) END) as TimeCount) T)
		                                            END) as ActualTimeWorked 
                                            ,Status = (CASE WHEN AR.ReferenceType = 0 THEN JS.Name ELSE RS.Name END)
                                            ,Advance = CAST(Case WHEN  ARD.JODetailId != 0 THEN (CASE WHEN ARD.ReferenceType = 0 THEN '#JO' + SUBSTRING(JOD.RefYear, 3, 3) + FORMAT(JOD.JONo,'0000') ELSE '#RO' + SUBSTRING(ROD.RefYear, 3, 3) + FORMAT(ROD.RONo,'0000') END)
                                             ELSE '' END as varchar)  + ' ' + ARD.Activity 
											,AttendanceGroupId
                                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                            LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
	
                                            LEFT JOIN ActualAdvanceDetails As AD ON PR.EmployeeId = AD.EmployeeId 
	                                            LEFT JOIN ActualAdvanceRefNos As AR ON AR.ActualAdvanceId = AD.Id
                                                LEFT JOIN ActualAdvanceRefNos As ARD ON ARD.ActualAdvanceId = AD.Id AND ARD.JODetailId = AR.JODetailId AND ARD.Type = 1	
	
                                            LEFT JOIN JODetails As JD ON JD.Id = AR.JODetailId
	                                            LEFT JOIN JOs As JO ON JD.JOId = JO.Id
		                                            LEFT JOIN Tariff As JT ON JT.Id = JD.TariffId
			                                            LEFT JOIN Status As JS ON JS.Id = JD.StatusId

                                            LEFT JOIN RODetails As RD ON RD.Id = AR.JODetailId
	                                            LEFT JOIN ROs As RO ON RD.ROId = RO.Id
		                                            LEFT JOIN Tariff As RT ON RT.Id = RD.TariffId
			                                            LEFT JOIN Status As RS ON RS.Id = RD.StatusId

                                            LEFT JOIN JODetails As JDD ON JDD.Id = ARD.JODetailId
	                                            LEFT JOIN JOs As JOD ON JD.JOId = JOD.Id
		                                            LEFT JOIN Tariff As JTD ON JTD.Id = JDD.TariffId
			                                            LEFT JOIN Status As JSD ON JSD.Id = JDD.StatusId

                                            LEFT JOIN RODetails As RDD ON RDD.Id = ARD.JODetailId
	                                            LEFT JOIN ROs As ROD ON RDD.ROId = ROD.Id
		                                            LEFT JOIN Tariff As RTD ON RTD.Id = RDD.TariffId
			                                            LEFT JOIN Status As RSD ON RSD.Id = RDD.StatusId
	
                                            LEFT JOIN AttendanceStatus As AM ON AM.Id = AD.AMStatus
	                                            LEFT JOIN AttendanceStatus As PM ON PM.Id = AD.PMStatus
		                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
			                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
				                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
					                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
						                                            LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
							                                            LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId

                                            WHERE (AR.Type = 0 OR AR.Type Is Null)  and PR.Active = 1
                                            ) T  WHERE T.Active = 1
											AND CONVERT(datetime,DateOfUpdate,102) BETWEEN CONVERT(datetime,@startDate,102) AND CONVERT(datetime,@endDate,102)
											";
                if (isSection)
                {
                    query += " AND T.SectionId IN ( " + sectionIds + ") ";
                }
                if (isBranch)
                {
                    query += " AND T.BranchId IN ( " + branchId + " ) ";
                }
                if (isAttendanceGroup)
                {
                    query += " AND AttendanceGroupId IN ( " + attendanceGroupId + " ) ";
                }
                if (isPersonnel)
                {
                    query += " AND T.EmployeeId IN ( " + personnelIds + " ) ";
                }
                query += " order by T.EmployeeName";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsAttendance>(query, new { startDate = startDate, endDate = endDate }).ToList();
                return List;
            }
        }
        public List<clsAttendance> GetSummaryWithSundays(bool isSection, string sectionIds, bool isBranch, Int64 branchId
                                                    , bool isAttendanceGroup, Int64 attendanceGroupId, DateTime startDate, DateTime endDate
                                                    , bool isPersonnel, string personnelIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendance> List = new List<clsAttendance>();
                String query = @"SELECT * FROM (SELECT
                                            PR.Active
                                            ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                            ,PR.EmployeeId
                                            ,PR.Active AS PRActive
                                            ,JSec.Id as SectionId
                                            ,JSec.Active as SecACtive
                                            ,PR.Location	  
                                            ,JSec.GroupDescription As Section
                                            ,DateOfUpdate = null
                                            ,JSEC.BranchId
                                            ,AM = ''
                                            ,PM = ''
                                            ,MinMaj = 0
                                            ,Advance = ''    
											,AttendanceGroupId
											, DATEDIFF(DAY,@startDate,@endDate) AS NoOfDays
                                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                            LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                                            WHERE PR.EmployeeId NOT IN (SELECT DISTINCT AD.EmployeeId FROM ActualAdvanceDetails As AD)
                                            UNION ALL
                                            SELECT
                                            PR.Active
                                            ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                            ,PR.EmployeeId
                                            ,PR.Active AS PRActive
                                            ,JSec.Id as SectionId
                                            ,JSec.Active as SecACtive
                                            ,PR.Location	  
                                            ,JSec.GroupDescription As Section
                                            , CONVERT(Date,AD.DateOfUpdate,101)DateOfUpdate
                                            ,JSEC.BranchId
                                            ,AM.Symbol As AM
                                            ,PM.Symbol As PM
                                            ,MinMaj = (CASE WHEN AR.ReferenceType = 0 THEN JT.Major ELSE RT.Major END)
                                            ,Advance = CAST(Case WHEN  ARD.JODetailId != 0 THEN (CASE WHEN ARD.ReferenceType = 0 THEN '#JO' + SUBSTRING(JOD.RefYear, 3, 3) + FORMAT(JOD.JONo,'0000') ELSE '#RO' + SUBSTRING(ROD.RefYear, 3, 3) + FORMAT(ROD.RONo,'0000') END)
                                             ELSE '' END as varchar)  + ' ' + ARD.Activity 
											,AttendanceGroupId
											, DATEDIFF(DAY,@startDate,@endDate) AS NoOfDays
                                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                            LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
	
                                            LEFT JOIN ActualAdvanceDetails As AD ON PR.EmployeeId = AD.EmployeeId 
	                                            LEFT JOIN ActualAdvanceRefNos As AR ON AR.ActualAdvanceId = AD.Id
                                                LEFT JOIN ActualAdvanceRefNos As ARD ON ARD.ActualAdvanceId = AD.Id AND ARD.JODetailId = AR.JODetailId AND ARD.Type = 1	
	
                                            LEFT JOIN JODetails As JD ON JD.Id = AR.JODetailId
	                                            LEFT JOIN JOs As JO ON JD.JOId = JO.Id
		                                            LEFT JOIN Tariff As JT ON JT.Id = JD.TariffId
			                                            LEFT JOIN Status As JS ON JS.Id = JD.StatusId

                                            LEFT JOIN RODetails As RD ON RD.Id = AR.JODetailId
	                                            LEFT JOIN ROs As RO ON RD.ROId = RO.Id
		                                            LEFT JOIN Tariff As RT ON RT.Id = RD.TariffId
			                                            LEFT JOIN Status As RS ON RS.Id = RD.StatusId

                                            LEFT JOIN JODetails As JDD ON JDD.Id = ARD.JODetailId
	                                            LEFT JOIN JOs As JOD ON JD.JOId = JOD.Id
		                                            LEFT JOIN Tariff As JTD ON JTD.Id = JDD.TariffId
			                                            LEFT JOIN Status As JSD ON JSD.Id = JDD.StatusId

                                            LEFT JOIN RODetails As RDD ON RDD.Id = ARD.JODetailId
	                                            LEFT JOIN ROs As ROD ON RDD.ROId = ROD.Id
		                                            LEFT JOIN Tariff As RTD ON RTD.Id = RDD.TariffId
			                                            LEFT JOIN Status As RSD ON RSD.Id = RDD.StatusId
	
                                            LEFT JOIN AttendanceStatus As AM ON AM.Id = AD.AMStatus
	                                            LEFT JOIN AttendanceStatus As PM ON PM.Id = AD.PMStatus
		                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
			                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
				                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
					                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
						                                            LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
							                                            LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId

                                            WHERE (AR.Type = 0 OR AR.Type Is Null)  and PR.Active = 1
                                            ) T  WHERE T.Active = 1
											AND CONVERT(date,DateOfUpdate,102) BETWEEN CONVERT(Date,@startDate,101) AND CONVERT(Date,@endDate,101)";
                if (isSection)
                {
                    query += " AND T.SectionId IN ( " + sectionIds + ") ";
                }
                if (isBranch)
                {
                    query += " AND T.BranchId IN ( " + branchId + " ) ";
                }
                if (isAttendanceGroup)
                {
                    query += " AND AttendanceGroupId IN ( " + attendanceGroupId + " ) ";
                }
                if (isPersonnel)
                {
                    query += " AND T.EmployeeId IN ( " + personnelIds + " ) ";
                }
                query += @" GROUP BY Active, EmployeeName, EmployeeId, PRActive, SectionId, SecACtive, Location,Section,DateOfUpdate,BranchId, AM, PM,
											MinMaj,Advance, AttendanceGroupId, NoOfDays ORDER BY T.EmployeeName";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsAttendance>(query, new { startDate = startDate, endDate = endDate }).ToList();
                return List;
            }
        }
        public List<clsAttendance> GetSummaryWithoutSunday(bool isSection, string sectionIds, bool isBranch, Int64 branchId
                                                    , bool isAttendanceGroup, Int64 attendanceGroupId, DateTime startDate, DateTime endDate
                                                    , bool isPersonnel, string personnelIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendance> List = new List<clsAttendance>();
                String query = @"SELECT * FROM (SELECT
                                            PR.Active
                                            ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                            ,PR.EmployeeId
                                            ,PR.Active AS PRActive
                                            ,JSec.Id as SectionId
                                            ,JSec.Active as SecACtive
                                            ,PR.Location	  
                                            ,JSec.GroupDescription As Section
                                            ,DateOfUpdate = null
                                            ,JSEC.BranchId
                                            ,AM = ''
                                            ,PM = ''
                                            ,MinMaj = 0
                                            ,Advance = ''    
											,AttendanceGroupId
											,DATEDIFF(DAY,@startDate,@endDate) - ((DATEDIFF(DAY,@startDate,@endDate) / 7) *1) AS NoOfDays
											---, DATEDIFF(DAY,@startDate,@endDate) AS NoOfDays
                                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                            LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                                            WHERE PR.EmployeeId NOT IN (SELECT DISTINCT AD.EmployeeId FROM ActualAdvanceDetails As AD)
                                            UNION ALL
                                            SELECT
                                            PR.Active
                                            ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                            ,PR.EmployeeId
                                            ,PR.Active AS PRActive
                                            ,JSec.Id as SectionId
                                            ,JSec.Active as SecACtive
                                            ,PR.Location	  
                                            ,JSec.GroupDescription As Section
                                            , CONVERT(Date,AD.DateOfUpdate,101)DateOfUpdate
                                            ,JSEC.BranchId
                                            ,AM.Symbol As AM
                                            ,PM.Symbol As PM
                                            ,MinMaj = (CASE WHEN AR.ReferenceType = 0 THEN JT.Major ELSE RT.Major END)
                                            ,Advance = CAST(Case WHEN  ARD.JODetailId != 0 THEN (CASE WHEN ARD.ReferenceType = 0 THEN '#JO' + SUBSTRING(JOD.RefYear, 3, 3) + FORMAT(JOD.JONo,'0000') ELSE '#RO' + SUBSTRING(ROD.RefYear, 3, 3) + FORMAT(ROD.RONo,'0000') END)
                                             ELSE '' END as varchar)  + ' ' + ARD.Activity 
											,AttendanceGroupId
											,DATEDIFF(DAY,@startDate,@endDate) - ((DATEDIFF(DAY,@startDate,@endDate) / 7) *1) AS NoOfDays
											---, DATEDIFF(DAY,@startDate,@endDate) AS NoOfDays
                                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                            LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
	
                                            LEFT JOIN ActualAdvanceDetails As AD ON PR.EmployeeId = AD.EmployeeId 
	                                            LEFT JOIN ActualAdvanceRefNos As AR ON AR.ActualAdvanceId = AD.Id
                                                LEFT JOIN ActualAdvanceRefNos As ARD ON ARD.ActualAdvanceId = AD.Id AND ARD.JODetailId = AR.JODetailId AND ARD.Type = 1	
	
                                            LEFT JOIN JODetails As JD ON JD.Id = AR.JODetailId
	                                            LEFT JOIN JOs As JO ON JD.JOId = JO.Id
		                                            LEFT JOIN Tariff As JT ON JT.Id = JD.TariffId
			                                            LEFT JOIN Status As JS ON JS.Id = JD.StatusId

                                            LEFT JOIN RODetails As RD ON RD.Id = AR.JODetailId
	                                            LEFT JOIN ROs As RO ON RD.ROId = RO.Id
		                                            LEFT JOIN Tariff As RT ON RT.Id = RD.TariffId
			                                            LEFT JOIN Status As RS ON RS.Id = RD.StatusId

                                            LEFT JOIN JODetails As JDD ON JDD.Id = ARD.JODetailId
	                                            LEFT JOIN JOs As JOD ON JD.JOId = JOD.Id
		                                            LEFT JOIN Tariff As JTD ON JTD.Id = JDD.TariffId
			                                            LEFT JOIN Status As JSD ON JSD.Id = JDD.StatusId

                                            LEFT JOIN RODetails As RDD ON RDD.Id = ARD.JODetailId
	                                            LEFT JOIN ROs As ROD ON RDD.ROId = ROD.Id
		                                            LEFT JOIN Tariff As RTD ON RTD.Id = RDD.TariffId
			                                            LEFT JOIN Status As RSD ON RSD.Id = RDD.StatusId
	
                                            LEFT JOIN AttendanceStatus As AM ON AM.Id = AD.AMStatus
	                                            LEFT JOIN AttendanceStatus As PM ON PM.Id = AD.PMStatus
		                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
			                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
				                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
					                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
						                                            LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
							                                            LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId

                                            WHERE (AR.Type = 0 OR AR.Type Is Null)  and PR.Active = 1
                                            ) T  WHERE T.Active = 1
											AND CONVERT(date,DateOfUpdate,102) BETWEEN CONVERT(Date,@startDate,101) AND CONVERT(Date,@endDate,101)";
                if (isSection)
                {
                    query += " AND T.SectionId IN ( " + sectionIds + ") ";
                }
                if (isBranch)
                {
                    query += " AND T.BranchId IN ( " + branchId + " ) ";
                }
                if (isAttendanceGroup)
                {
                    query += " AND AttendanceGroupId IN ( " + attendanceGroupId + " ) ";
                }
                if (isPersonnel)
                {
                    query += " AND T.EmployeeId IN ( " + personnelIds + " ) ";
                }
                query += @" GROUP BY Active, EmployeeName, EmployeeId, PRActive, SectionId, SecACtive, Location,Section,DateOfUpdate,BranchId, AM, PM,
											MinMaj,Advance, AttendanceGroupId, NoOfDays ORDER BY T.EmployeeName";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsAttendance>(query, new { startDate = startDate, endDate = endDate }).ToList();
                return List;
            }
        }
        public List<clsAttendance> GetAllAccomplishmentURPerSection( bool isSection, string sectionIds, bool isBranch, Int64 branchId
                                                   , bool isAttendanceGroup, Int64 attendanceGroupId, bool isPersonnel, String personnelId, 
                                                 bool isDateUpdate,  DateTime dateUpdatefrom, DateTime dateUpdateTo)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendance> List = new List<clsAttendance>();
                String query = @"SELECT * FROM (
                                            SELECT DISTINCT
                                            PR.Active
                                            ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                            ,PR.EmployeeId
											,GI.LastName AS LastName
											,GI.FirstName AS FirstName
											,GI.NameExtension AS NameExtension
                                            ,PR.Active AS PRActive
                                            ,JSec.Id as SectionId
                                            ,JSec.Active as SecACtive
											,EquipmentId = ''
                                            ,EquipmentCode = ''
											,EquipmentTypeId= 0
											,EquipmentTypeName = ''
                                            ,PR.Location	  
                                            ,JSec.GroupDescription As Section
                                            ,DateOfUpdate = null
                                            ,JSEC.BranchId
                                            ,AM = ''
                                            ,PM = ''
                                            ,AJODetailId = 0
                                            ,Activity = ''
                                            ,EstimatedTime = ''
                                            ,MinMaj = 0
                                            ,ActualTimeWorked = ''
                                            ,Status = ''
                                            ,Advance = ''    
											,AttendanceGroupId
                                            ,URDays = ''
                                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                            LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                                            WHERE PR.EmployeeId NOT IN (SELECT AD.EmployeeId FROM ActualAdvanceDetails As AD)
                                            UNION ALL
                                            SELECT DISTINCT
                                            PR.Active
                                            ,CONCAT(GI.LastName, ', ',GI.FirstName, ' ',  GI.MiddleName, ' ', GI.NameExtension) As EmployeeName
                                            ,PR.EmployeeId
											,GI.LastName AS LastName
											,GI.FirstName AS FirstName
											,GI.NameExtension AS NameExtension
                                            ,PR.Active AS PRActive
                                            ,JSec.Id as SectionId
                                            ,JSec.Active as SecACtive
											,EQ.Id AS EquipmentId
                                            ,EquipmentCode = (CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
						                                            WHEN AR.EquipmentType = 1 THEN 'STE' 
						                                            WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
					                                            ELSE  '' END)
											,ET.Id AS EquipmentTypeId
											,ET.PPETypeName AS EquipmentTypeName
                                            ,PR.Location	  
                                            ,JSec.GroupDescription As Section
                                            ,AD.DateOfUpdate
                                            ,JSEC.BranchId
                                            ,AM.Symbol As AM
                                            ,PM.Symbol As PM
                                            ,AR.JODetailId
                                            ,CAST(Case WHEN  AR.JODetailId != 0 THEN (CASE WHEN AR.ReferenceType = 0 THEN '#JO' + SUBSTRING(JO.RefYear, 3, 3) + FORMAT(JO.JONo,'0000') ELSE '#RO' + SUBSTRING(RO.RefYear, 3, 3) + FORMAT(RO.RONo,'0000') END)
                                                                ELSE '' END as varchar) 
                                            + ' ' + AR.Activity As Activity
                                            ,EstimatedTime = (CASE WHEN AR.ReferenceType = 0 THEN JT.WorkTimeSpan ELSE RT.WorkTimeSpan END)
                                            ,MinMaj = (CASE WHEN AR.ReferenceType = 0 THEN JT.Major ELSE RT.Major END)
                                            ,(CASE WHEN AR.JODetailId != 0 
	                                            THEN (SELECT CAST(SUM(T.TimeCount) As VARCHAR) +' hr(s) or ' + CAST(SUM(CAST(T.TimeCount As FLOAT))/8 As VARCHAR) + ' day(s)' 
		                                            FROM
			                                            (SELECT 
			                                            CASE WHEN CONVERT(Time(0),AAR.TimeStarted) < '12:00:00' OR CONVERT(Time(0),AAR.TimeEnded) > '13:30:00' 
			                                            THEN (CAST(DATEDIFF(mi,CONVERT(Time(0),AAR.TimeStarted),CONVERT(Time(0),AAR.TimeEnded)) As float)/60) - 1.5 
			                                            ELSE (CAST(DATEDIFF(mi,CONVERT(Time(0),AAR.TimeStarted),CONVERT(Time(0),AAR.TimeEnded)) As float)/60) END As TimeCount
			                                            FROM [ActualAdvanceRefNos] As AAR
				                                            LEFT JOIN ActualAdvanceDetails As AAD ON AAR.ActualAdvanceId = AAD.Id
			                                            WHERE AAD.EmployeeId = AD.EmployeeId AND AAR.JODetailId = AR.JODetailId AND AAR.Type = 0
					                                            AND (AAD.DateOfUpdate BETWEEN AAD.DateOfUpdate AND AD.DateOfUpdate) 
			                                            ) AS T ) 
	                                            ELSE 			
		                                            (SELECT CAST(SUM(T.TimeCount) As VARCHAR) +' hr(s) or ' + CAST(SUM(CAST(T.TimeCount As FLOAT))/8 As VARCHAR) + ' day(s)' 
			                                            FROM 
			                                            (Select (CASE WHEN CONVERT(Time(0),AR.TimeStarted) < '12:00:00' OR CONVERT(Time(0),AR.TimeEnded) > '13:30:00' 
			                                            THEN (CAST(DATEDIFF(mi,CONVERT(Time(0),AR.TimeStarted),CONVERT(Time(0),AR.TimeEnded)) As float)/60) - 1.5 
			                                            ELSE (CAST(DATEDIFF(mi,CONVERT(Time(0),AR.TimeStarted),CONVERT(Time(0),AR.TimeEnded)) As float)/60) END) as TimeCount) T)
		                                            END) as ActualTimeWorked 
                                            ,Status = (CASE WHEN AR.ReferenceType = 0 THEN JS.Name ELSE RS.Name END)
                                            ,Advance = CAST(Case WHEN  ARD.JODetailId != 0 THEN (CASE WHEN ARD.ReferenceType = 0 THEN '#JO' + SUBSTRING(JOD.RefYear, 3, 3) + FORMAT(JOD.JONo,'0000') ELSE '#RO' + SUBSTRING(ROD.RefYear, 3, 3) + FORMAT(ROD.RONo,'0000') END)
                                             ELSE '' END as varchar)  + ' ' + ARD.Activity 
											,AttendanceGroupId
											,DATEDIFF(Day,Ad.DateOfUpdate, GETDATE()) AS URDays
                                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] As PR
                                            LEFT JOIN Sections As JSec ON JSec.Id = PR.SectionId
	                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = PR.EmployeeId
		                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
	
                                            LEFT JOIN ActualAdvanceDetails As AD ON PR.EmployeeId = AD.EmployeeId 
	                                            LEFT JOIN ActualAdvanceRefNos As AR ON AR.ActualAdvanceId = AD.Id
                                                LEFT JOIN ActualAdvanceRefNos As ARD ON ARD.ActualAdvanceId = AD.Id AND ARD.JODetailId = AR.JODetailId AND ARD.Type = 1	
	
                                            LEFT JOIN JODetails As JD ON JD.Id = AR.JODetailId
	                                            LEFT JOIN JOs As JO ON JD.JOId = JO.Id
		                                            LEFT JOIN Tariff As JT ON JT.Id = JD.TariffId
			                                            LEFT JOIN Status As JS ON JS.Id = JD.StatusId

                                            LEFT JOIN RODetails As RD ON RD.Id = AR.JODetailId
	                                            LEFT JOIN ROs As RO ON RD.ROId = RO.Id
		                                            LEFT JOIN Tariff As RT ON RT.Id = RD.TariffId
			                                            LEFT JOIN Status As RS ON RS.Id = RD.StatusId

                                            LEFT JOIN JODetails As JDD ON JDD.Id = ARD.JODetailId
	                                            LEFT JOIN JOs As JOD ON JD.JOId = JOD.Id
		                                            LEFT JOIN Tariff As JTD ON JTD.Id = JDD.TariffId
			                                            LEFT JOIN Status As JSD ON JSD.Id = JDD.StatusId

                                            LEFT JOIN RODetails As RDD ON RDD.Id = ARD.JODetailId
	                                            LEFT JOIN ROs As ROD ON RDD.ROId = ROD.Id
		                                            LEFT JOIN Tariff As RTD ON RTD.Id = RDD.TariffId
			                                            LEFT JOIN Status As RSD ON RSD.Id = RDD.StatusId
	
                                            LEFT JOIN AttendanceStatus As AM ON AM.Id = AD.AMStatus
	                                            LEFT JOIN AttendanceStatus As PM ON PM.Id = AD.PMStatus
		                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
			                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
				                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
					                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
						                                            LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
							                                            LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId

                                            WHERE (AR.Type = 0 OR AR.Type Is Null) AND PR.Active = 1
                                            ) T 
											  WHERE T.Active = 1 ";
                if (isDateUpdate)
                {
                    query += " AND CONVERT(date, DateOfUpdate,102) BETWEEN CONVERT(Date, @dateUpdatefrom,101) AND CONVERT(Date, @dateUpdateTo,101)";
                }

                if (isSection)
                {
                    query += " AND T.SectionId IN ( " + sectionIds + ") ";
                }
                if (isBranch)
                {
                    query += " AND T.BranchId IN ( " + branchId + " )";
                }
                if (isAttendanceGroup)
                {
                    query += " AND AttendanceGroupId IN ( " + attendanceGroupId + " )";
                }
                if (isPersonnel)
                {
                    query += " AND T.EmployeeId IN ( " + personnelId + " )";
                }
                //query += " GROUP BY Active, EmployeeName, EmployeeId, PRActive, SectionId, SecACtive, EquipmentId, EquipmentCode, EquipmentTypeId, EquipmentTypeName, Location, Section, DateOfUpdate, BranchId, AM, PM, AJODetailId, Activity, EstimatedTime, MinMaj, ActualTimeWorked,Status, Advance, AttendanceGroupId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                //List = connection.Query<clsAttendance>(query, new { startDate = startDate, endDate = endDate }).ToList();
                //List = connection.Query<clsAttendance>(query).ToList();//, new { DateOfUpdate = DateOfUpdate }
                List = connection.Query<clsAttendance>(query, new { dateUpdatefrom = dateUpdatefrom, dateUpdateTo = dateUpdateTo }).ToList();//, new { DateOfUpdate = DateOfUpdate }
                connection.Close();
                return List;
            }
        }
    }
}

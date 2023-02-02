using FGCIJOROSystem.Domain.Monitoring;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.vMonitoringRepo
{
    public class MonitoringRepository
    {
        public List<clsMonitoring> SearchBy(String whereQuery)
        {           
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsMonitoring> Lists = new List<clsMonitoring>();
                String query = @"SELECT 
                                    EmployeeName
                                    ,Section
                                    ,Position
                                    ,ReferenceNo
                                    ,ReferenceType
                                    ,RefYear
                                    ,EquipmentName
                                    ,JODetailId
                                    ,WorkDescription
                                    ,EffectivityDate As EffectiveDate
                                    ,TargetDate
                                    ,WorkTimeSpan
                                    ,Unit
                                    ,MAx(DateOfUpdate) As DateOfUpdate
                                    ,MAX(WorkPercentage) As WorkPercentage
                                    ,Status
                                     FROM
                                    (SELECT 
                                    CONCAT(GI.LastName, ' ', GI.NameExtension,', ', GI.FirstName,' ', GI.MiddleName) AS EmployeeName
                                    ,S.GroupDescription As Section
                                    ,Pos.PositionName As Position
                                    ,AD.DateOfUpdate
                                    ,AR.[ReferenceNo]
                                    ,AR.[ReferenceType]
                                    ,AR.[RefYear]
                                    ,AR.WorkPercentage
                                    ,AR.JODetailId

                                    ,WorkDescription = (Case When AR.ReferenceNo != 0 THEN
						                                    (Case When AR.ReferenceType = 0 THEN JTariff.WorkDescription
							                                      When AR.ReferenceType = 1 THEN RTariff.WorkDescription
							                                      ELse '' END ) 
						                                     ELSE AR.Activity END)

                                    ,EffectivityDate = (Case When AR.ReferenceType = 0 THEN JD.EffectivityDate
								                                    When AR.ReferenceType = 1 THEN RD.EffectivityDate
								                                    ELse NULL END ) 

                                    ,TargetDate = (Case When AR.ReferenceType = 0 THEN JD.TargetDate
							                                    When AR.ReferenceType = 1 THEN RD.TargetDate
							                                    ELse NULL END ) 

                                    ,WorkTimeSpan = (Case When AR.ReferenceType = 0 THEN JD.WorkTimeSpan
							                                    When AR.ReferenceType = 1 THEN RD.WorkTimeSpan
							                                    ELse NULL END )
                                    ,Unit = (Case When AR.ReferenceType = 0 THEN PU.UnitName
							                                    When AR.ReferenceType = 1 THEN RPU.UnitName
							                                    ELse NULL END )
                                    ,EquipmentName = CONCAT((CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName 
			                                    WHEN AR.EquipmentType = 1 THEN SD.ToolName
			                                    WHEN AR.EquipmentType = 2 THEN OT.[Name]  
			                                    ELSE  '' END), ' '
                                    ,(CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
			                                    WHEN AR.EquipmentType = 1 THEN 'STE' 
			                                    WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
			                                    ELSE  '' END))
                                    ,Status = (Case When AR.ReferenceType = 0 THEN Status.Name
								                                    When AR.ReferenceType = 1 THEN RStatus.Name
								                                    ELse NULL END ) 
                                    FROM [ActualAdvanceRefNos] AS AR
                                    LEFT JOIN [ActualAdvanceDetails] AS AD ON AR.ActualAdvanceId = AD.Id 

                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId 

                                    LEFT JOIN [Personnels] AS Per ON AD.EmployeeId = Per.EmployeeId
                                    LEFT JOIN Sections As S ON S.Id = Per.SectionId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId

                                    LEFT JOIN JODetails AS JD ON JD.Id = AR.JODetailId
                                    LEFT JOIN Tariff As JTariff ON JTariff.Id = JD.TariffId
                                    LEFT JOIN Sections AS  JSec ON JSec.Id = JTariff.SectionId
                                    LEFT JOIN JobCategories AS JC ON JC.Id = JTariff.JobCategoryId
                                    LEFT JOIN JobTypes AS JT ON JT.Id = JTariff.JobTypeId
                                    LEFT JOIN Units AS U ON U.Id = JTariff.UnitId
                                    LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
                                    LEFT JOIN Status ON STATUS.Id = JD.StatusId 

                                    LEFT JOIN RODetails AS RD ON RD.Id = AR.JODetailId
                                    LEFT JOIN Tariff As RTariff ON RTariff.Id = RD.TariffId
                                    LEFT JOIN Sections AS RSec ON RSec.Id = RTariff.SectionId
                                    LEFT JOIN JobCategories AS RC ON RC.Id = RTariff.JobCategoryId
                                    LEFT JOIN JobTypes AS RT ON RT.Id = RTariff.JobTypeId
                                    LEFT JOIN Units AS RU ON RU.Id = RTariff.UnitId
                                    LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS RPU ON RPU.Id = U.UnitId
                                    LEFT JOIN Status As RStatus ON RSTATUS.Id = RD.StatusId 

                                    WHERE AR.Type = 0) T

                                    GROUP BY EmployeeName, Section, Position, ReferenceNo, ReferenceType, JODetailId, WorkDescription, EquipmentName, EffectivityDate, TargetDate, WorkTimeSpan,Unit,Status,RefYear " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsMonitoring>(query).ToList();
                connection.Close();
                return Lists;
               
            }
        }
        public List<clsMonitoring> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsMonitoring> Lists = new List<clsMonitoring>();
                String query = @"SELECT 
                                    EmployeeName
                                    ,Section
                                    ,Position
                                    ,ReferenceNo
                                    ,ReferenceType
                                    ,RefYear
                                    ,EquipmentName
                                    ,JODetailId
                                    ,WorkDescription
                                    ,EffectivityDate As EffectiveDate
                                    ,TargetDate
                                    ,WorkTimeSpan
                                    ,Unit
                                    ,MAx(DateOfUpdate) As DateOfUpdate
                                    ,MAX(WorkPercentage) As WorkPercentage
                                    ,Status
                                     FROM
                                    (SELECT 
                                    CONCAT(GI.LastName, ' ', GI.NameExtension,', ', GI.FirstName,' ', GI.MiddleName) AS EmployeeName
                                    ,S.GroupDescription As Section
                                    ,Pos.PositionName As Position
                                    ,AD.DateOfUpdate
                                    ,AR.[ReferenceNo]
                                    ,AR.[ReferenceType]
                                    ,AR.[RefYear]
                                    ,AR.WorkPercentage
                                    ,AR.JODetailId

                                    ,WorkDescription = (Case When AR.ReferenceNo != 0 THEN
						                                    (Case When AR.ReferenceType = 0 THEN JTariff.WorkDescription
							                                      When AR.ReferenceType = 1 THEN RTariff.WorkDescription
							                                      ELse '' END ) 
						                                     ELSE AR.Activity END)

                                    ,EffectivityDate = (Case When AR.ReferenceType = 0 THEN JD.EffectivityDate
								                                    When AR.ReferenceType = 1 THEN RD.EffectivityDate
								                                    ELse NULL END ) 

                                    ,TargetDate = (Case When AR.ReferenceType = 0 THEN JD.TargetDate
							                                    When AR.ReferenceType = 1 THEN RD.TargetDate
							                                    ELse NULL END ) 

                                    ,WorkTimeSpan = (Case When AR.ReferenceType = 0 THEN JD.WorkTimeSpan
							                                    When AR.ReferenceType = 1 THEN RD.WorkTimeSpan
							                                    ELse NULL END )
                                    ,Unit = (Case When AR.ReferenceType = 0 THEN PU.UnitName
							                                    When AR.ReferenceType = 1 THEN RPU.UnitName
							                                    ELse NULL END )
                                    ,EquipmentName = CONCAT((CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName 
			                                    WHEN AR.EquipmentType = 1 THEN SD.ToolName
			                                    WHEN AR.EquipmentType = 2 THEN OT.[Name]  
			                                    ELSE  '' END), ' '
                                    ,(CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
			                                    WHEN AR.EquipmentType = 1 THEN 'STE' 
			                                    WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
			                                    ELSE  '' END))
                                    ,Status = (Case When AR.ReferenceType = 0 THEN Status.Name
								                                    When AR.ReferenceType = 1 THEN RStatus.Name
								                                    ELse NULL END ) 
                                    FROM [ActualAdvanceRefNos] AS AR
                                    LEFT JOIN [ActualAdvanceDetails] AS AD ON AR.ActualAdvanceId = AD.Id 

                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId 

                                    LEFT JOIN [Personnels] AS Per ON AD.EmployeeId = Per.EmployeeId
                                    LEFT JOIN Sections As S ON S.Id = Per.SectionId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId

                                    LEFT JOIN JODetails AS JD ON JD.Id = AR.JODetailId
                                    LEFT JOIN Tariff As JTariff ON JTariff.Id = JD.TariffId
                                    LEFT JOIN Sections AS  JSec ON JSec.Id = JTariff.SectionId
                                    LEFT JOIN JobCategories AS JC ON JC.Id = JTariff.JobCategoryId
                                    LEFT JOIN JobTypes AS JT ON JT.Id = JTariff.JobTypeId
                                    LEFT JOIN Units AS U ON U.Id = JTariff.UnitId
                                    LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
                                    LEFT JOIN Status ON STATUS.Id = JD.StatusId 

                                    LEFT JOIN RODetails AS RD ON RD.Id = AR.JODetailId
                                    LEFT JOIN Tariff As RTariff ON RTariff.Id = RD.TariffId
                                    LEFT JOIN Sections AS RSec ON RSec.Id = RTariff.SectionId
                                    LEFT JOIN JobCategories AS RC ON RC.Id = RTariff.JobCategoryId
                                    LEFT JOIN JobTypes AS RT ON RT.Id = RTariff.JobTypeId
                                    LEFT JOIN Units AS RU ON RU.Id = RTariff.UnitId
                                    LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS RPU ON RPU.Id = U.UnitId
                                    LEFT JOIN Status As RStatus ON RSTATUS.Id = RD.StatusId 

                                    WHERE AR.Type = 0) T

                                    GROUP BY EmployeeName, Section, Position, ReferenceNo, ReferenceType, JODetailId, WorkDescription, EquipmentName, EffectivityDate, TargetDate, WorkTimeSpan,Unit,Status,RefYear ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsMonitoring>(query).ToList();
                connection.Close();
                return Lists;

            }
        }
        public List<clsMonitoring> GetAllRecord(bool isSection, string sectionIds, bool isPersonnel, string personnelIds, DateTime DateStart, DateTime DateEnd)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsMonitoring> Lists = new List<clsMonitoring>();
                String query = @"SELECT 
                                    EmployeeName
                                    ,EmployeeId
                                    ,SectionId
                                    ,Section
                                    ,Position
                                    ,ReferenceNo
                                    ,ReferenceType
                                    ,RefYear
                                    ,EquipmentName
                                    ,JODetailId
                                    ,WorkDescription
                                    ,EffectivityDate As EffectiveDate
                                    ,TargetDate
                                    ,WorkTimeSpan
                                    ,Unit
                                    ,MAx(DateOfUpdate) As DateOfUpdate
                                    ,MAX(WorkPercentage) As WorkPercentage
                                    ,Status
                                     FROM
                                    (SELECT 
                                    CONCAT(GI.LastName, ' ', GI.NameExtension,', ', GI.FirstName,' ', GI.MiddleName) AS EmployeeName
                                    ,Per.EmployeeId AS EmployeeId
                                    ,S.GroupDescription As Section
                                    ,S.Id As SectionId
                                    ,Pos.PositionName As Position
                                    ,AD.DateOfUpdate
                                    ,AR.[ReferenceNo]
                                    ,AR.[ReferenceType]
                                    ,AR.[RefYear]
                                    ,AR.WorkPercentage
                                    ,AR.JODetailId

                                    ,WorkDescription = (Case When AR.ReferenceNo != 0 THEN
						                                    (Case When AR.ReferenceType = 0 THEN JTariff.WorkDescription
							                                      When AR.ReferenceType = 1 THEN RTariff.WorkDescription
							                                      ELse '' END ) 
						                                     ELSE AR.Activity END)

                                    ,EffectivityDate = (Case When AR.ReferenceType = 0 THEN JD.EffectivityDate
								                                    When AR.ReferenceType = 1 THEN RD.EffectivityDate
								                                    ELse NULL END ) 

                                    ,TargetDate = (Case When AR.ReferenceType = 0 THEN JD.TargetDate
							                                    When AR.ReferenceType = 1 THEN RD.TargetDate
							                                    ELse NULL END ) 

                                    ,WorkTimeSpan = (Case When AR.ReferenceType = 0 THEN JD.WorkTimeSpan
							                                    When AR.ReferenceType = 1 THEN RD.WorkTimeSpan
							                                    ELse NULL END )
                                    ,Unit = (Case When AR.ReferenceType = 0 THEN PU.UnitName
							                                    When AR.ReferenceType = 1 THEN RPU.UnitName
							                                    ELse NULL END )
                                    ,EquipmentName = CONCAT((CASE WHEN AR.EquipmentType = 0 THEN ET.PPETypeName 
			                                    WHEN AR.EquipmentType = 1 THEN SD.ToolName
			                                    WHEN AR.EquipmentType = 2 THEN OT.[Name]  
			                                    ELSE  '' END), ' '
                                    ,(CASE WHEN AR.EquipmentType = 0 THEN EQ.PPEName 
			                                    WHEN AR.EquipmentType = 1 THEN 'STE' 
			                                    WHEN AR.EquipmentType = 2 THEN 'SPARE PARTS'
			                                    ELSE  '' END))
                                    ,Status = (Case When AR.ReferenceType = 0 THEN Status.Name
								                                    When AR.ReferenceType = 1 THEN RStatus.Name
								                                    ELse NULL END ) 
                                    FROM [ActualAdvanceRefNos] AS AR
                                    LEFT JOIN [ActualAdvanceDetails] AS AD ON AR.ActualAdvanceId = AD.Id 

                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = AR.EquipmentId
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                                    LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                    LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = AR.EquipmentId
                                    LEFT JOIN OtherEquipments OT ON OT.Id = AR.EquipmentId 

                                    LEFT JOIN [Personnels] AS Per ON AD.EmployeeId = Per.EmployeeId
                                    LEFT JOIN Sections As S ON S.Id = Per.SectionId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId

                                    LEFT JOIN JODetails AS JD ON JD.Id = AR.JODetailId
                                    LEFT JOIN Tariff As JTariff ON JTariff.Id = JD.TariffId
                                    LEFT JOIN Sections AS  JSec ON JSec.Id = JTariff.SectionId
                                    LEFT JOIN JobCategories AS JC ON JC.Id = JTariff.JobCategoryId
                                    LEFT JOIN JobTypes AS JT ON JT.Id = JTariff.JobTypeId
                                    LEFT JOIN Units AS U ON U.Id = JTariff.UnitId
                                    LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
                                    LEFT JOIN Status ON STATUS.Id = JD.StatusId 

                                    LEFT JOIN RODetails AS RD ON RD.Id = AR.JODetailId
                                    LEFT JOIN Tariff As RTariff ON RTariff.Id = RD.TariffId
                                    LEFT JOIN Sections AS RSec ON RSec.Id = RTariff.SectionId
                                    LEFT JOIN JobCategories AS RC ON RC.Id = RTariff.JobCategoryId
                                    LEFT JOIN JobTypes AS RT ON RT.Id = RTariff.JobTypeId
                                    LEFT JOIN Units AS RU ON RU.Id = RTariff.UnitId
                                    LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS RPU ON RPU.Id = U.UnitId
                                    LEFT JOIN Status As RStatus ON RSTATUS.Id = RD.StatusId 

                                    WHERE AR.Type = 0) T where t.DateOfUpdate between @DateStart and @DateEnd";
                if (isSection)
                {
                    query += " AND SectionId IN (" + sectionIds + ")";
                }
                if (isPersonnel)
                {
                    query += " AND EmployeeId IN ( " + personnelIds + " )";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                query += "GROUP BY EmployeeName, EmployeeId, Section, SectionId, Position, ReferenceNo, ReferenceType, JODetailId, WorkDescription, EquipmentName, EffectivityDate, TargetDate, WorkTimeSpan,Unit,Status,RefYear";
                Lists = connection.Query<clsMonitoring>(query, new {DateStart = DateStart, DateEnd = DateEnd }).ToList();
                connection.Close();
                return Lists;

            }
        }
    }
}

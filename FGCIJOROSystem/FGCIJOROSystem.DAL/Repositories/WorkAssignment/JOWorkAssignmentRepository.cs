using FGCIJOROSystem.Domain.WorkAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
namespace FGCIJOROSystem.DAL.Repositories.WorkAssignment
{
    public class JOWorkAssignmentRepository : IRepository<clsWorkAssignments>
    {

        public void Add(clsWorkAssignments obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
            String query = @"INSERT INTO [dbo].[JOWorkAssignment]
                                   ([JODetailId]
                                   ,[EmployeeId]
                                   ,[DateEncoded]
                                   ,[IsActive])
                             VALUES
                                   (@JODetailId
                                   ,@EmployeeId
                                   ,GETDATE()
                                   ,1);";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Query<Int64>(query, obj).FirstOrDefault();
                connection.Close();
            }
        }

        public void Update(clsWorkAssignments obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JOWorkAssignment]
                                   SET [JODetailId] = @JODetailId
                                   ,[EmployeeId] = @EmployeeId
                                   ,[IsActive] = @IsActive
                             WHERE Id = @Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Query<Int64>(query, obj).FirstOrDefault();
                connection.Close();
            }
        }

        public void Delete(clsWorkAssignments obj)
        {
            throw new NotImplementedException();
        }

        public List<clsWorkAssignments> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsWorkAssignments FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsWorkAssignments> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsWorkAssignments> Lists = new List<clsWorkAssignments>();
                String query = @"SELECT JW.[Id]
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

                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS MEI ON MEI.Id =  JW.EmployeeId
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
                                 " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsWorkAssignments>(query).ToList();
                connection.Close();
                return Lists;
            }
            
        }
        public List<clsWorkAssignments> SearchJOROBy(string whereQuery)
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
	                                ,ReferenceType = 0
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

                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS MEI ON MEI.Id =  JW.EmployeeId
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
                                WHERE JO.Status != 4 AND JO.Status != 5 AND JO.Status != 7
                                UNION ALL
                                SELECT RW.[Id]
                                    ,RW.[RODetailId]
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
	                                ,ReferenceType = 1
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
                                FROM [FGCIJOROSystemDB].[dbo].[ROWorkAssignment] AS RW
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
                                /*LEFT JOIN Personnels P ON Sec.Id = P.SectionId 
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id */
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId
                                WHERE RO.Status != 4 AND RO.Status != 5 AND RO.Status != 7
                                ) T  " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsWorkAssignments>(query).ToList();
                connection.Close();
                return Lists;
            }

        }
    }
}

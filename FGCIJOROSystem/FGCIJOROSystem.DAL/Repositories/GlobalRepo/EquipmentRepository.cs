using FGCIJOROSystem.Domain.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.GlobalRepo
{
	public class EquipmentRepository 
	{
        public List<clsEquipment> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipment> Lists = new List<clsEquipment>();
                String query = @"WITH Equipment AS
                                    (SELECT 
                                        '0' AS EquipmentType,
                                        EQ.Id As EquipmentMasterlistId, 
                                        EQ.PPEName As EquipmentCode,
                                        ET.PPETypeName AS EquipmentName, 
                                        EC.PPEClassName AS EquipmentClass,  
                                        EQ.ActualLocation As Location, 
                                        EQ.PlateNo, 
                                        ER.MLDriversNameId AS DriverId, 
                                        GID.FirstName AS DFirstname, GID.MiddleName AS DMiddlename, GID.LastName AS DLastName, GID.NameExtension AS DNameExtension, 
                                        SD.SectionName AS DSectionName, PD.PositionName AS DPositionName, DD.DepartmentName AS DDepartmentName, CD.CompanyName AS DCompanyName,
										IsActive = 1
                                    FROM [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ 
                                        INNER JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                                        INNER JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GID ON ER.MLDriversNameId = GID.Id 
                                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EID ON GID.Id = EID.GeneralInformationsId 
                                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Positions] PD ON EID.PositionsId = PD.Id 
                                                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Sections] SD ON PD.SectionsId = SD.Id 
                                                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Departments] DD ON SD.DepartmentsId = DD.Id
                                                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] CD ON DD.CompaniesId = CD.Id
                                        UNION
                                        SELECT
                                            '1' AS EquipmentType,
                                            SD.Id AS EquipmentMasterlistId,                                             
                                            'STE' AS EquipmentCode, 
                                            SD.ToolName AS EquipmentName, 
                                            '' AS EquipmentClass, 
                                            SD.Remarks AS Location, 
                                            '' AS PlateNo, 
                                            0 AS DriverId, 
                                            '' AS DFirstname, '' AS DMiddlename, ''AS DLastname, '' AS DNameExtension, 
                                            '' AS DSectionName, '' AS DpositionName, '' AS DDepartmentName, '' AS DComapanyName 
											,IsActive
                                        FROM 
                                            FGCILInventoryDB.dbo.StockDetails SD 
                               
                                        UNION 
                                        SELECT 
                                            '2' AS EquipmentType,
                                            Id AS EquipmentMasterlistId, 
                                            'Spare Parts' AS EquipmentCode, 
                                            Name AS EquipmentName,                                             
                                            'Others' AS EquipmentClass, 
                                            '' AS Location, 
                                            '' AS PlateNo, 
                                            0 AS DriverId, 
                                            '' AS DFirstname, '' AS DMiddlename, ''AS DLastname, '' AS DNameExtension, 
                                            '' AS DSectionName, '' AS DpositionName, '' AS DDepartmentName, '' AS DComapanyName 
											,Active As IsActive
                                    FROM 
                                            OtherEquipments )

                                    SELECT * FROM EQUIPMENT
										WHERE IsActive = '1' ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipment>(query).OrderBy(x => x.EquipmentName).OrderBy(x=> x.EquipmentCode).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsEquipment> GetByEquipments()
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                List<clsEquipment> Lists = new List<clsEquipment>();
                String query = @"SELECT 
                                        '0' AS EquipmentType,
                                        EQ.Id As EquipmentMasterlistId, 
                                        EQ.PPEName As EquipmentCode,
                                        ET.PPETypeName AS EquipmentName, 
                                        EC.PPEClassName AS EquipmentClass,  
                                        EQ.ActualLocation As Location, 
                                        EQ.PlateNo, 
                                        ER.MLDriversNameId AS DriverId, 
                                        GID.FirstName AS DFirstname, GID.MiddleName AS DMiddlename, GID.LastName AS DLastName, GID.NameExtension AS DNameExtension, 
                                        SD.SectionName AS DSectionName, PD.PositionName AS DPositionName, DD.DepartmentName AS DDepartmentName, CD.CompanyName AS DCompanyName 
                                    FROM [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ 
                                        INNER JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                                        INNER JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GID ON ER.MLDriversNameId = GID.Id 
                                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EID ON GID.Id = EID.GeneralInformationsId 
                                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Positions] PD ON EID.PositionsId = PD.Id 
                                                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Sections] SD ON PD.SectionsId = SD.Id 
                                                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Departments] DD ON SD.DepartmentsId = DD.Id
                                                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] CD ON DD.CompaniesId = CD.Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipment>(query).OrderBy(x => x.EquipmentName).OrderBy(x=> x.EquipmentCode).ToList();
                connection.Close();
                return Lists;
            }
         }

        public List<clsEquipment> SearchBy(string search)
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                List<clsEquipment> Lists = new List<clsEquipment>();
                String query = @"WITH Equipment AS
                                    (SELECT 
                                        '0' AS Type,
                                        EQ.Id As EquipmentSTEOthersId, 
                                        EQ.PPEName As EquipmentName,
                                        ET.PPETypeName AS EquipmentType, 
                                        EC.PPEClassName AS EquipmentClass,  
                                        EQ.ActualLocation As Location, 
                                        EQ.PlateNo, 
                                        ER.MLDriversNameId AS DriverId, 
                                        GID.FirstName AS DFirstname, GID.MiddleName AS DMiddlename, GID.LastName AS DLastName, GID.NameExtension AS DNameExtension, 
                                        SD.SectionName AS DSectionName, PD.PositionName AS DPositionName, DD.DepartmentName AS DDepartmentName, CD.CompanyName AS DCompanyName 
                                    FROM [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ 
                                        INNER JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                                        INNER JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
                                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GID ON ER.MLDriversNameId = GID.Id 
                                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EID ON GID.Id = EID.GeneralInformationsId 
                                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Positions] PD ON EID.PositionsId = PD.Id 
                                                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Sections] SD ON PD.SectionsId = SD.Id 
                                                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Departments] DD ON SD.DepartmentsId = DD.Id
                                                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] CD ON DD.CompaniesId = CD.Id
                                        UNION
                                        SELECT
                                            '1' AS Type,
                                            SD.Id AS EquipmentSTEOthersId, 
                                            SD.ToolName AS EquipmentName, 
                                            'STE' AS EquipmentType, 
                                            '' AS EquipmentClass, 
                                            SD.Remarks AS Location, 
                                            '' AS PlateNo, 
                                            0 AS DriverId, 
                                            '' AS DFirstname, '' AS DMiddlename, ''AS DLastname, '' AS DNameExtension, 
                                            '' AS DSectionName, '' AS DpositionName, '' AS DDepartmentName, '' AS DComapanyName 
                                        FROM 
                                            FGCILInventoryDB.dbo.StockDetails SD 
                               
                                        UNION 
                                        SELECT 
                                            '0' AS Type,
                                            Id AS EquipmentSTEOthersId, 
                                            Name AS EquipmentName, 
                                            'Spare Parts' AS EquipmentType, 
                                            'Others' AS EquipmentClass, 
                                            '' AS Location, 
                                            '' AS PlateNo, 
                                            0 AS DriverId, 
                                            '' AS DFirstname, '' AS DMiddlename, ''AS DLastname, '' AS DNameExtension, 
                                            '' AS DSectionName, '' AS DpositionName, '' AS DDepartmentName, '' AS DComapanyName 
                                    FROM 
                                            OtherEquipments )

                                    SELECT * FROM EQUIPMENT WHERE EquipmentName LIKE @search";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipment>(query, new { search = search }).OrderBy(x => x.EquipmentName).ToList();
                connection.Close();
                return Lists;
            }

        }
        public List<clsEquipment> SearchById(Int64 EquipmentId)
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                List<clsEquipment> Lists = new List<clsEquipment>();
                String query = @"SELECT 
                                        '0' AS EquipmentType,
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
                                    WHERE ET.Id = @EquipmentId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipment>(query, new { EquipmentId = EquipmentId }).OrderBy(x => x.EquipmentCode).ToList();
                connection.Close();
                return Lists;
            }
        }
       
        


    }
}
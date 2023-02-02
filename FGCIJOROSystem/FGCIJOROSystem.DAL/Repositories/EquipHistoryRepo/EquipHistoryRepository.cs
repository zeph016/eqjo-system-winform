using FGCIJOROSystem.Domain.EquipmentHistory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.EquipHistoryRepo
{
    public class EquipHistoryRepository
    {
        public List<clsEquipmentHistory> Searchby(String whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentHistory> Lists = new List<clsEquipmentHistory>();
                String query = @"With EquipmentHistory AS (SELECT 
	                                0 As TransType
	                                ,JO.EquipmentId
	                                ,Tariff.[WorkDescription]
	                                ,S.GroupDescription As Section
	                                ,JC.Name As JobCategory
	                                ,JT.Name As JobType
                                    ,JD.[Remarks]
                                    ,JO.[JONo] As RefNo
                                    ,CONVERT(DATETIME ,JO.[JODate]) As TransDate	
	                                ,Contractor = (CASE WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							                                WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 0 THEN C.CompanyName 
							                                WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 1 THEN Sec.GroupDescription
							                                WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 1 THEN SC.CompanyName
							                                WHEN JO.ContractorCategory = 2 AND JO.ContractorType = 1 THEN SDp.DepartmentName
							                                WHEN JO.ContractorCategory = 3 AND JO.ContractorType = 1 THEN SS.SectionName
							                                ELSE  '' END)

	                                ,ContractorSectionHead = (CASE WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 1 THEN CONCAT(SGI.FirstName, ' ', SGI.MiddleName, ' ', SGI.LastName, ' ', SGI.NameExtension)
									                                ELSE  '' END)

	                                ,Customer = (CASE WHEN JO.CustomerType = 0 THEN PCust.Name
							                                WHEN JO.CustomerType = 1 THEN CustCom.CompanyName 
							                                WHEN JO.CustomerType = 2 THEN CustDep.DepartmentName
							                                WHEN JO.CustomerType = 3 THEN CustSec.SectionName
							                                ELSE  '' END)

	                                ,CustomerAddress = (CASE WHEN JO.CustomerType = 0 THEN PCust.Address
	                                WHEN JO.CustomerType = 3 THEN CustSec.Location
	                                ELSE  '' END)

	                                ,Driver = CONCAT(DGI.FirstName, ' ', DGI.MiddleName, ' ', DGI.LastName, ' ', DGI.NameExtension)
	                                ,Status.Name As Status
	                                ,Status.IsClosure
                                FROM [dbo].[JODetails] AS JD
                                                                  LEFT JOIN Tariff ON Tariff.Id = JD.TariffId
                                                                  LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
                                                                  LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
                                                                  LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId                                  
                                                                  LEFT JOIN Status ON STATUS.Id = JD.StatusId 
                                LEFT JOIN [dbo].[JOs] AS JO ON JD.JOId = JO.Id


                                LEFT JOIN Contractors C ON C.Id = JO.ContractorId
                                LEFT JOIN Sections Sec ON Sec.Id = JO.ContractorId
                                LEFT JOIN Personnels P ON Sec.Id = P.SectionId 
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id 
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = JO.ContractorId
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = JO.ContractorId
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = JO.ContractorId

                                LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = JO.CustomerId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = JO.CustomerId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = JO.CustomerId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = JO.CustomerId

                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON JO.DriverId = DGI.Id 

                                UNION ALL

                                SELECT 
	                                 1 As TransType
	                                 ,RO.EquipmentId
	                                ,Tariff.[WorkDescription]
	                                ,S.GroupDescription As Section
	                                ,JC.Name As JobCategory
	                                ,JT.Name As JobType
                                    ,RD.[Remarks]
                                    ,RO.[RONo] As RefNo
                                    ,CONVERT(DATETIME ,RO.[RODate]) As TransDate	
	                                ,Contractor = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							                                WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
							                                WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
							                                WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
							                                WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
							                                WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
							                                ELSE  '' END)

	                                ,ContractorSectionHead = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN CONCAT(SGI.FirstName, ' ', SGI.MiddleName, ' ', SGI.LastName, ' ', SGI.NameExtension)
									                                ELSE  '' END)

	                                ,Customer = (CASE WHEN RO.CustomerType = 0 THEN PCust.Name
							                                WHEN RO.CustomerType = 1 THEN CustCom.CompanyName 
							                                WHEN RO.CustomerType = 2 THEN CustDep.DepartmentName
							                                WHEN RO.CustomerType = 3 THEN CustSec.SectionName
							                                ELSE  '' END)

	                                ,CustomerAddress = (CASE WHEN RO.CustomerType = 0 THEN PCust.Address
	                                WHEN RO.CustomerType = 3 THEN CustSec.Location
	                                ELSE  '' END)

	                                ,Driver = CONCAT(DGI.FirstName, ' ', DGI.MiddleName, ' ', DGI.LastName, ' ', DGI.NameExtension)
	                                ,Status.Name As Status
	                                ,Status.IsClosure
                                FROM [dbo].[RODetails] AS RD
                                                                  LEFT JOIN Tariff ON Tariff.Id =RD.TariffId
                                                                  LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
                                                                  LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
                                                                  LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId                                  
                                                                  LEFT JOIN Status ON STATUS.Id = RD.StatusId 
                                LEFT JOIN [dbo].[ROs] AS RO ON RD.ROId = RO.Id


                                LEFT JOIN Contractors C ON C.Id = RO.ContractorId
                                LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId
                                LEFT JOIN Personnels P ON Sec.Id = P.SectionId 
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id 
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId

                                LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = RO.CustomerId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = RO.CustomerId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = RO.CustomerId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = RO.CustomerId

                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON RO.DriverId = DGI.Id ) 

                                SELECT * FROM EquipmentHistory WHERE " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipmentHistory>(query).OrderByDescending(x => x.TransDate).ToList();
                connection.Close();
                return Lists;
            }
            
        }
    }
}

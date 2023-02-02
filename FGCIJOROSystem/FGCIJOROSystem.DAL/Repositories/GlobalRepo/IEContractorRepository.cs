using FGCIJOROSystem.Domain.Configurations.Contractors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.GlobalRepo
{
    public class IEContractorRepository
    {
        public List<clsContractor> SearchBy()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsContractor> Lists = new List<clsContractor>();
                String query = @"SELECT * FROM(
				SELECT 
                                           '1' AS ContractorType, 
                                           '0' AS ContractorCategory, 
                                           Sec.Id AS Id, CONCAT(GI.FirstName, ' ', GI.MiddleName, ' ', GI.LastName, ' ', GI.NameExtension) AS ContractorSection, 
                                           Sec.GroupDescription AS Section ,GI.FirstName, GI.MiddleName, GI.LastName, GI.NameExtension
                                      FROM 
                                           Sections Sec 
                                               INNER JOIN Personnels P ON Sec.Id = P.SectionId 
                                               INNER JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
                                                   INNER JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] GI ON EI.GeneralInformationsId = GI.Id                                                        
                                      WHERE  P.IsSectionHead = 1 and sec.Active = 1
                                           --GI.FirstName LIKE search OR GI.MiddleName LIKE search OR GI.LastName LIKE search OR GI.NameExtension LIKE search 
                                           --OR CG.GroupDescription LIKE search AND P.IsSectionHead = IsSectionHead
										  

                 UNION

                
                 SELECT 
                                            '0' AS ContractorType, 
                                            C.[Type] AS ContractorCategory, 
                                            Id, 
                                            CompanyName AS ContractorSection, 
                                            CASE WHEN C.[Type] = '0' THEN CONCAT(FirstName, ' ', MiddleName, ' ', LastName, ' ', NameExtension) ELSE CompanyName END AS Section, FirstName,  MiddleName,  LastName,  NameExtension
                                       FROM 
                                            Contractors As C
                                       WHERE Active = 1
                                            --FirstName LIKE search OR MiddleName LIKE search OR LastName LIKE search OR CompanyName LIKE search 
                  UNION 

               
                 SELECT 
                                            '1' AS ContractorType, 
                                            '1' AS ContractorCategory, 
                                            Id AS Id, 
                                            '' AS ContractorSection, 
                                            CompanyName AS Section ,'' FirstName,''  MiddleName,''  LastName,''  NameExtension
                                       FROM 
                                            [FGCIHRDMasterlistSystemDB].[dbo].[Companies]
                                            --CompanyName LIKE search 

                  UNION 

                
                 SELECT 
                                            '1' AS ContractorType, 
                                            '2' AS ContractorCategory, 
                                            Id AS Id, 
                                            '' AS ContractorSection, 
                                            DepartmentName AS Section ,'' FirstName,''  MiddleName,''  LastName,''  NameExtension
                                       FROM 
                                            [FGCIHRDMasterlistSystemDB].[dbo].[Departments] 
                                       --WHERE 
                                            --DepartmentName LIKE search 

                  UNION 

               
                 SELECT 
                                            '1' AS ContractorType, 
                                            '3' AS ContractorCategory, 
                                            Id AS Id, 
                                            '' AS ContractorSection, 
                                            SectionName AS Section ,'' FirstName,''  MiddleName,''  LastName,''  NameExtension
                                       FROM 
                                            [FGCIHRDMasterlistSystemDB].[dbo].[Sections] 
                                       --WHERE 
                                            --SectionName LIKE search
										) T ORDER BY ContractorType, ContractorCategory";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsContractor>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

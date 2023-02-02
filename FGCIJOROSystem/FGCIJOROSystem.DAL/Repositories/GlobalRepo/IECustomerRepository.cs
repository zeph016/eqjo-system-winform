using FGCIJOROSystem.Domain.Configurations.Customers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.GlobalRepo
{
    public class IECustomerRepository
    {
        public List<clsCustomer> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsCustomer> List = new List<clsCustomer>();

                String query = @"SELECT 
                                    Id, [Name] As CustomerName, [Address] As CustomerAddress,   0 As [CustomerType] --[ContactNo]
                                FROM 
                                    FGCIProjectEFileCabinetDB.dbo.PrivateCustomer
									where Active = 1
                                UNION 
                                SELECT 
                                    Id, DepartmentName As CustomerName, '' As CustomerAddress,  2 As [CustomerType] -- '' AS [ContactNo] ,
                                FROM 
                                    FGCIHRDMasterlistSystemDB.dbo.Departments
                                UNION
                                SELECT 
                                    Id, SectionName As CustomerName, ISNULL([Location], '') AS CustomerAddress,  3 As [CustomerType] -- '' AS [ContactNo] ,
                                FROM 
                                    FGCIHRDMasterlistSystemDB.dbo.Sections
                                UNION
                                SELECT 
                                    Id, CompanyName As CustomerName, '' AS CustomerAddress ,  1 As [CustomerType] --'' AS [ContactNo] ,
                                FROM 
                                    FGCIHRDMasterlistSystemDB.dbo.Companies
	                                 ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsCustomer>(query).ToList();
                return List;
            }
            
        }
    }
}

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
	public class EmployeeRepository 
	{		
		public List<clsEmployee> GetAll()
		{
			using (IDbConnection connection = DbConnection.MasterlistConnection)
			{
				List<clsEmployee> Lists = new List<clsEmployee>();
				String query = @"SELECT
									 EI.Id
									 ,GI.FirstName
									 --,(Select Concat(Substring(GI.MiddleName,1,1), '.')) AS MiddleName
									 ,GI.MiddleName
									 ,GI.LastName
									 ,GI.NameExtension
									 ,EI.DateHired
									 ,EmployeeStatusId
									 --,GI.Picture as EmpPicture
									 ,EI.PositionsId
									 ,P.PositionName AS Position
									 ,p.SectionsId
									 ,S.SectionName as Section
									 ,S.DepartmentsId
									 ,D.DepartmentName as Department
								FROM FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GI
								LEFT JOIN EmployeesInformations EI on GI.Id = Ei.GeneralInformationsId
								LEFT JOIN Positions P ON P.Id = EI.PositionsId
								LEFT JOIN Sections S on S.Id = P.SectionsId
								LEFT JOIN Departments D on D.Id = S.DepartmentsId
                                LEFT JOIN EmployeeStatus ES ON ES.Id = EI.EmployeeStatusId
								WHERE ES.Category = 0";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Lists = connection.Query<clsEmployee>(query).OrderBy(x =>x.FullName).ToList();
				connection.Close();
				return Lists;
			}
		}

		public clsEmployee FindByID(Int64 id)
		{
			using (IDbConnection connection = DbConnection.MasterlistConnection)
			{
				clsEmployee Lists = new clsEmployee();
				String query = @"SELECT
									 EI.Id
									 ,GI.FirstName
									 --,(Select Concat(Substring(GI.MiddleName,1,1), '.')) AS MiddleName
									 ,GI.MiddleName
									 ,GI.LastName
									 ,GI.NameExtension
									 ,EI.DateHired
									 ,EmployeeStatusId
									 --,GI.Picture as EmpPicture
									 ,EI.PositionsId
									 ,P.PositionName AS Position
									 ,p.SectionsId
									 ,S.SectionName as Section
									 ,S.DepartmentsId
									 ,D.DepartmentName as Department
								FROM FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GI
								LEFT JOIN EmployeesInformations EI on GI.Id = Ei.GeneralInformationsId
								LEFT JOIN Positions P ON P.Id = EI.PositionsId
								LEFT JOIN Sections S on S.Id = P.SectionsId
								LEFT JOIN Departments D on D.Id = S.DepartmentsId
                                LEFT JOIN EmployeeStatus ES ON ES.Id = EI.EmployeeStatusId
								WHERE ES.Category = 0";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Lists = connection.Query<clsEmployee>(query).FirstOrDefault();
				connection.Close();
				return Lists;
			}
		}

		public List<clsEmployee> SearchBy(string whereQuery)
		{
			using (IDbConnection connection = DbConnection.MasterlistConnection)
			{
				List<clsEmployee> Lists = new List<clsEmployee>();
				String query = @"SELECT
									 EI.Id
									 ,GI.FirstName
									 --,(Select Concat(Substring(GI.MiddleName,1,1), '.')) AS MiddleName
									 ,GI.MiddleName
									 ,GI.LastName
									 ,GI.NameExtension
									 ,EI.DateHired
									 ,EmployeeStatusId
									 --,GI.Picture as EmpPicture
									 ,EI.PositionsId
									 ,P.PositionName AS Position
									 ,p.SectionsId
									 ,S.SectionName as Section
									 ,S.DepartmentsId
									 ,D.DepartmentName as Department
									 --,GI.IsActive
								FROM FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GI
								LEFT JOIN EmployeesInformations EI on GI.Id = Ei.GeneralInformationsId
								LEFT JOIN Positions P ON P.Id = EI.PositionsId
								LEFT JOIN Sections S on S.Id = P.SectionsId
								LEFT JOIN Departments D on D.Id = S.DepartmentsId
                                LEFT JOIN EmployeeStatus ES ON ES.Id = EI.EmployeeStatusId
								WHERE ES.Category = 0 --AND IsActive = '1'
								AND Concat(GI.FirstName, ' ' , GI.MiddleName, ' ', GI.LastName, ' ', GI.NameExtension) like '%" + whereQuery + "%' OR CONCAT(GI.FirstName, ' ' , GI.LastName) like '%" + whereQuery + @"%' 
								OR Concat(GI.FirstName, ' ', GI.LastName, ' ', GI.NameExtension) like '%" + whereQuery + @"%'";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
                Lists = connection.Query<clsEmployee>(query).OrderBy(x => x.FullName).ToList();
				connection.Close();
				return Lists;
			}
		}

        public Byte[] GetPictureById(Int64 Id)
        {
            Byte[] ret;
            using (IDbConnection connection = DbConnection.MasterlistConnection)
            {
            
                String query = @"SELECT GI.Picture as EmpPicture
								FROM GeneralInformations GI
                                LEFT JOIN EmployeesInformations EI on GI.Id = Ei.GeneralInformationsId
                                WHERE (EI.Id = @Id)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                ret = connection.Query<Byte[]>(query, new { Id = Id }).FirstOrDefault();
                connection.Close();
                return ret;
            }
        }

        public List<clsEmployee> GetEmpNonEmp()
        {
            using (IDbConnection connection = DbConnection.MasterlistConnection)
            {
                List<clsEmployee> Lists = new List<clsEmployee>();
                String query = @"SELECT 0 EmployeeType ,
									 EI.Id
									 ,GI.FirstName
									 ,GI.MiddleName
									 ,GI.LastName
									 ,GI.NameExtension
									 ,EI.DateHired
									 ,EmployeeStatusId
									 ,EI.PositionsId
									 ,P.PositionName AS Position
									 ,p.SectionsId
									 ,S.SectionName as Section
									 ,S.DepartmentsId
									 ,D.DepartmentName as Department
								FROM FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GI
								LEFT JOIN EmployeesInformations EI on GI.Id = Ei.GeneralInformationsId
								LEFT JOIN Positions P ON P.Id = EI.PositionsId
								LEFT JOIN Sections S on S.Id = P.SectionsId
								LEFT JOIN Departments D on D.Id = S.DepartmentsId
								LEFT JOIN EmployeeStatus ES ON ES.Id = EI.EmployeeStatusId
								WHERE ES.Category = 0
								UNION ALL
								SELECT 1 EmployeeType ,
									 NoEmp.Id
									,NoEmp.FirstName
									,NoEmp.MiddleName
									,NoEmp.LastName
									,NoEmp.NameExtention
									,NoEmp.DateEntered As DateHired
									,NoEmp.IsActive As EmployeeStatusId
									,0 PositionsId
									 ,(Case WHEN NoEmpPos.NonEmployeePositionName Is null THEN NoEmp.Designation ELSE NoEmp.Designation END) as Position
									 ,0 SectionsId
									 ,'' as Section
									 ,0 DepartmentsId
									 ,'' as Department
								FROM NonEmployeeMasterlist NoEmp
								LEFT JOIN NonEmployeePosition NoEmpPos on NoEmp.NonEmpDesignationId = NoEmpPos.Id
								WHERE NoEmp.IsActive = 1 ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEmployee>(query).OrderBy(x => x.FullName).ToList();
                connection.Close();
                return Lists;
            }
        }
	}
}
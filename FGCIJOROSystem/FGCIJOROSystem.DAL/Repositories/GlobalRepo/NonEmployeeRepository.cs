using FGCIJOROSystem.Domain.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace FGCIJOROSystem.DAL.Repositories.GlobalRepo
{
	public class NonEmployeeRepository 
	{		
		public List<clsNonEmployee> GetAll()
		{
			using (IDbConnection connection = DbConnection.MasterlistConnection)
			{
				List<clsNonEmployee> Lists = new List<clsNonEmployee>();
				String query = @"SELECT
									NoEmp.FirstName
									,NoEmp.MiddleName
									,NoEmp.LastName
									,NoEmp.NameExtention
									,NoEmp.Designation
									,NoEmp.NonEmpDesignationId
									,NoEmpPos.NonEmployeePositionName as Position
									,NoEmp.DateEntered
									,NoEmp.DateEnded
									,NoEmp.Gender
									,NoEmp.IsActive
								FROM NonEmployeeMasterlist NoEmp
								LEFT JOIN NonEmployeePosition NoEmpPos on NoEmp.NonEmpDesignationId = NoEmpPos.Id";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Lists = connection.Query<clsNonEmployee>(query).OrderBy(x => x.FullName).ToList();
				connection.Close();
				return Lists;
			}
		}

		public clsNonEmployee FindByID(Int64 id)
		{
			using (IDbConnection connection = DbConnection.MasterlistConnection)
			{
				clsNonEmployee Lists = new clsNonEmployee();
				String query = @"SELECT
									NoEmp.FirstName
									,NoEmp.MiddleName
									,NoEmp.LastName
									,NoEmp.NameExtention
									,NoEmp.Designation
									,NoEmp.NonEmpDesignationId
									,NoEmpPos.NonEmployeePositionName as Position
									,NoEmp.DateEntered
									,NoEmp.DateEnded
									,NoEmp.Gender
									,NoEmp.IsActive
								FROM NonEmployeeMasterlist NoEmp
								LEFT JOIN NonEmployeePosition NoEmpPos on NoEmp.NonEmpDesignationId = NoEmpPos.Id";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Lists = connection.Query<clsNonEmployee>(query).SingleOrDefault();
				connection.Close();
				return Lists;
			}
		}

		public List<clsNonEmployee> SearchBy(string Name)
		{
			 using (IDbConnection connection = DbConnection.MasterlistConnection)
			{
				List<clsNonEmployee> Lists = new List<clsNonEmployee>();
				String query = @"SELECT
									NoEmp.FirstName
									,NoEmp.MiddleName
									,NoEmp.LastName
									,NoEmp.NameExtention as NameExtension
									,NoEmp.Designation
									,NoEmp.NonEmpDesignationId
									,NoEmpPos.NonEmployeePositionName as Position
									,NoEmp.DateEntered
									,NoEmp.DateEnded
									,NoEmp.Gender
									,NoEmp.IsActive
								FROM NonEmployeeMasterlist NoEmp
								LEFT JOIN NonEmployeePosition NoEmpPos on NoEmp.NonEmpDesignationId = NoEmpPos.Id
								WHERE Concat(NoEmp.FirstName, ' ' , NoEmp.MiddleName, ' ', NoEmp.LastName, ' ', NoEmp.NameExtention) like '%'+'@Name'+'%' OR CONCAT(NoEmp.FirstName, ' ' , NoEmp.LastName) like '%'+@Name+'%' 
							OR Concat(NoEmp.FirstName, ' ', NoEmp.LastName, ' ', NoEmp.NameExtention) like '%'+@Name+'%'";
				{
					connection.Open();
				}
				Lists = connection.Query<clsNonEmployee>(query, new { Name = Name }).OrderBy(x => x.FullName).ToList();
				connection.Close();
				return Lists;
			}
		}
	}
}

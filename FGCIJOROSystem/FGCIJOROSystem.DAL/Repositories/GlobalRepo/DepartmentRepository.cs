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
	public class DepartmentRepository: IRepository<clsDepartment>
	{
		public void Add(clsDepartment obj)
		{
			throw new NotImplementedException();
		}

		public void Update(clsDepartment obj)
		{
			throw new NotImplementedException();
		}

		public void Delete(clsDepartment obj)
		{
			throw new NotImplementedException();
		}

		public List<clsDepartment> GetAll()
		{
			throw new NotImplementedException();
		}

		public clsDepartment FindByID(Int64 id)
		{
			throw new NotImplementedException();
		}

		public List<clsDepartment> SearchBy(string DepartmentName)
		{
			using (IDbConnection connection = DbConnection.MasterlistConnection)
			{
				List<clsDepartment> Lists = new List<clsDepartment>();
				String query = @"Select 
									D.Id
									,D.DepartmentName as Department
									,D.CompaniesId
									,DepartmentShortcutName
									,DivisionId
								 FROM Departments D
								WHERE D.DepartmentName like '%'+ @DepartmentName +'%'";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
                Lists = connection.Query<clsDepartment>(query, new { DepartmentName = DepartmentName }).OrderBy(x => x.Department).ToList();
				connection.Close();
				return Lists;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FGCIJOROSystem.Domain.Global;
using System.Data;

namespace FGCIJOROSystem.DAL.Repositories.GlobalRepo
{
	public class SectionRepository : IRepository<clsSection>
	{
		public void Add(clsSection obj)
		{
			throw new NotImplementedException();
		}

		public void Update(clsSection obj)
		{
			throw new NotImplementedException();
		}

		public void Delete(clsSection obj)
		{
			throw new NotImplementedException();
		}

		public List<clsSection> GetAll()
		{
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsSection> Lists = new List<clsSection>();
                string query = @"SELECT Id
                              ,AttendanceGroupId
                              ,SectionName
                              ,GroupDescription
                              ,Active
                              ,IsChecklistGroup
                          FROM dbo.Sections";
//                String query = @"SELECT
//										S.Id
//										,S.SectionName as Section
//										,S.DepartmentsId
//										,S.Location
//								FROM FGCIHRDMasterlistSystemDB.dbo.Sections S";
                {
                    connection.Open();
                }
                Lists = connection.Query<clsSection>(query).OrderBy(x => x.SectionName).ToList();
                connection.Close();
                return Lists;
            }
		}

		public clsSection FindByID(Int64 id)
		{
			throw new NotImplementedException();
		}

		public List<clsSection> SearchBy(string SectionName)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				List<clsSection> Lists = new List<clsSection>();
                String query = @"SELECT Id
                              ,AttendanceGroupId
                              ,SectionName
                              ,GroupDescription
                              ,Active
                              ,IsChecklistGroup
                          FROM dbo.Sections
								WHERE SectionName LIKE '%' + @SectionName + '%'";
				{
					connection.Open();
				}
				Lists = connection.Query<clsSection>(query, new { SectionName = SectionName }).OrderBy(x => x.SectionName).ToList();
				connection.Close();
				return Lists;
			}
		}
	}
}

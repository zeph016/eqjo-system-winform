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
	public class ProjectRepository : IRepository<clsProject>
	{
		public void Add(clsProject obj)
		{
			throw new NotImplementedException();
		}

		public void Update(clsProject obj)
		{
			throw new NotImplementedException();
		}

		public void Delete(clsProject obj)
		{
			throw new NotImplementedException();
		}

		public List<clsProject> GetAll()
		{
            using (IDbConnection connection = DbConnection.ProjectEFileConnection)
            {
                List<clsProject> Lists = new List<clsProject>();
                String query = @"SELECT
										pd.Id,
										pd.ProjectId,
										pd.ProjectName,
										pd.ProjectShortcutName,
										pd.Location,
										pd.CustomerId as CustomerCategory
									FROM FGCIProjectEFileCabinetDB.dbo.ProjectDetails pd";
                {
                    connection.Open();
                }
                Lists = connection.Query<clsProject>(query).OrderBy(x => x.ProjectName).ToList();
                connection.Close();
                return Lists;
            }
		}

		public clsProject FindByID(Int64 id)
		{
			throw new NotImplementedException();
		}

		public List<clsProject> SearchBy(string ProjectName)
		{
			using (IDbConnection connection = DbConnection.ProjectEFileConnection)
			{
				List<clsProject> Lists = new List<clsProject>();
				String query = @"SELECT
										pd.Id,
										pd.ProjectId,
										pd.ProjectName,
										pd.Location,
										pd.CustomerId as CustomerCategory
									FROM FGCIProjectEFileCabinetDB.dbo.ProjectDetails pd
									WHERE pd.ProjectName LIKE '%'+ @ProjectName +'%'";
				{
					connection.Open();
				}
				Lists = connection.Query<clsProject>(query, new { ProjectName = ProjectName }).OrderBy(x => x.ProjectName).ToList();
				connection.Close();
				return Lists;
			}
		}
	}
}

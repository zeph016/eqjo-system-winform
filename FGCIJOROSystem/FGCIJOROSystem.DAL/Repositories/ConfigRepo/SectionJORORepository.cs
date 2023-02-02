using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.Domain.Configurations.Personnels;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
	public class SectionJORORepository : IRepository<clsSectionJORO> 
	{
		public void Add(clsSectionJORO obj)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
                String query = @"INSERT INTO [dbo].[Sections]
									   ([AttendanceGroupId]
									   ,[GroupDescription]
									   ,[Active]
									   ,[IsChecklistGroup]
                                       ,BranchId)
								 VALUES
									   (@AttendanceGroupId
									   ,@SectionName
									   ,@Active
									   ,@IsChecklistGroup
                                       ,@BranchId);
                                SELECT SCOPE_IDENTITY() AS Id;";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
                //connection.Execute(query, obj);
                Int64 Id = connection.Query<Int64>(query, obj).SingleOrDefault();
				connection.Close();
                foreach (var x in obj.ListOfPersonnel)
                {
                    x.SectionId = Id;
                    PersonnelRepository PersonnelRepo = new PersonnelRepository();
                    if (x.Id == 0)
                    {
                        PersonnelRepo.Add(x);
                    }
                    else if (x.Id != 0)
                    {
                        PersonnelRepo.Update(x);
                    }                    
                }
			}
		}

		public void Update(clsSectionJORO obj)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				String query = @"UPDATE [dbo].[Sections]
							   SET [AttendanceGroupId] = @AttendanceGroupId
								  ,[GroupDescription] = @SectionName
								  ,[Active] = @Active
								  ,[IsChecklistGroup] = @IsChecklistGroup
                                  ,BranchId = @BranchId
							 WHERE Id = @Id;";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
                connection.Execute(query, obj);
                connection.Close();
                foreach (var x in obj.ListOfPersonnel)
                {
                    x.SectionId = obj.Id;
                    PersonnelRepository PersonnelRepo = new PersonnelRepository();
                    if (x.Id == 0)
                    {
                        PersonnelRepo.Add(x);
                    }
                    else if (x.Id != 0)
                    {
                        PersonnelRepo.Update(x);
                    }
                }
			}
		}

		public void Delete(clsSectionJORO obj)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
                String query = @"UPDATE [dbo].[Sections] SET [Active] = '0'
									  WHERE Id = @Id";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				connection.Execute(query, obj);
				connection.Close();
			}
		}

		public List<clsSectionJORO> GetAll()
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				List<clsSectionJORO> Lists = new List<clsSectionJORO>();
                String query = @"SELECT s.Id
								, s.AttendanceGroupId
								, s.GroupDescription as SectionName
								, s.Active
								, s.IsChecklistGroup
								,ag.Name as AttendanceGroupName 
                                ,b.Id As BranchId
                                ,b.Name As BranchName
								,ag.Active as AGActive
						FROM Sections AS s 
                        Left join Branches As b on b.Id = s.branchId
						 LEFT JOIN AttendanceGroups AS ag ON ag.Id = s.AttendanceGroupId
							where s.Active = 1 AND AG.Active = 1";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Lists = connection.Query<clsSectionJORO>(query).ToList();
				connection.Close();
				return Lists;
			}
		}
        public List<clsSectionJORO> FilterSections(Int64 attendanceGroupId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsSectionJORO> Lists = new List<clsSectionJORO>();
                String query = @"SELECT s.Id
								, s.AttendanceGroupId
								, s.GroupDescription as SectionName
								, s.Active
								, s.IsChecklistGroup
								,ag.Name as AttendanceGroupName 
                                ,b.Id As BranchId
                                ,b.Name As BranchName
								,ag.Active as AGActive
						FROM Sections AS s 
                        Left join Branches As b on b.Id = s.branchId
						 LEFT JOIN AttendanceGroups AS ag ON ag.Id = s.AttendanceGroupId
							where s.Active = 1 AND AG.Active = 1";
                query += " AND AttendanceGroupId IN ( " + attendanceGroupId + " )";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsSectionJORO>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsSectionJORO> GetAllByGroup(Int64 attendanceGroupId) 
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsSectionJORO> Lists = new List<clsSectionJORO>();
                String query = @"SELECT S.Id
								,S.AttendanceGroupId
								,AG.Name as AttendanceGroupName 
								,S.GroupDescription as SectionName
								,S.Active
								,S.IsChecklistGroup
                                ,B.Id As BranchId
                                ,B.Name As BranchName
								,AG.Active as AGActive
						FROM Sections AS S
                        Left join Branches As B on B.Id = S.branchId
						 LEFT JOIN AttendanceGroups AS AG ON AG.Id = S.AttendanceGroupId
							where S.Active = 1 AND AG.Active = 1";
                    query += " AND AttendanceGroupId IN ( " + attendanceGroupId + " )"; 
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsSectionJORO>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsSectionJORO> GetAllSections()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsSectionJORO> Lists = new List<clsSectionJORO>();
                String query = @"SELECT s.Id
								, s.AttendanceGroupId
								, s.GroupDescription as SectionName
								, s.Active
								, s.IsChecklistGroup
								,ag.Name as AttendanceGroupName 
                                ,b.Id As BranchId
                                ,b.Name As BranchName
								,ag.Active as AGActive
						FROM Sections AS s 
                        Left join Branches As b on b.Id = s.branchId
						 LEFT JOIN AttendanceGroups AS ag ON ag.Id = s.AttendanceGroupId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsSectionJORO>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public List<clsSectionJORO> GetAllActiveSections()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsSectionJORO> Lists = new List<clsSectionJORO>();
                String query = @"SELECT s.Id
								, s.AttendanceGroupId
								, s.GroupDescription as SectionName
								, s.Active
								, s.IsChecklistGroup
								,ag.Name as AttendanceGroupName 
                                ,b.Id As BranchId
                                ,b.Name As BranchName
								,ag.Active as AGActive
						FROM Sections AS s 
                        Left join Branches As b on b.Id = s.branchId
						 LEFT JOIN AttendanceGroups AS ag ON ag.Id = s.AttendanceGroupId WHERE s.Active = 1";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsSectionJORO>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsSectionJORO> SearchBy(string whereQuery)
		{
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsSectionJORO> Lists = new List<clsSectionJORO>();
                String query = @"SELECT s.Id
								, s.AttendanceGroupId
								, s.GroupDescription as SectionName
								, s.Active
								, s.IsChecklistGroup
								,ag.Name as AttendanceGroupName 
                                ,b.Id As BranchId
                                ,b.Name As BranchName
						FROM Sections AS s 
                        Left join Branches As b on b.Id = s.branchId
						LEFT JOIN AttendanceGroups AS ag ON ag.Id = s.AttendanceGroupId " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsSectionJORO>(query).ToList();
                connection.Close();
                return Lists;
            }
		}


        public clsSectionJORO FindByID(long id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsSectionJORO List = new clsSectionJORO();
                String query = @"SELECT s.Id
								, s.AttendanceGroupId
								, s.SectionName
								, s.Active
								, s.IsChecklistGroup
								,ag.Name as AttendanceGroupName 
                                ,b.Id As BranchId
                                ,b.Name As BranchName
						FROM Sections AS s 
                         Left join Branches As b on b.Id = s.branchId
						 LEFT JOIN AttendanceGroups AS ag ON ag.Id = s.AttendanceGroupId WHERE Id = @Id)
											";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsSectionJORO>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }
    }
}

using FGCIJOROSystem.Domain.Configurations.Attendance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
    public class AttendanceGroupRepository : IRepository<clsAttendanceGroup>
    {
        public void Add(clsAttendanceGroup obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[AttendanceGroups]
                                       ([Name]
                                       ,[Description]
                                       ,[Color]
                                       ,[Active])
                                 VALUES
                                       (@AttendanceGroupName
                                       ,@AttendanceGroupDescription
                                       ,@intColor
                                       ,@IsActive)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsAttendanceGroup obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[AttendanceGroups]
                                       SET [Name] = @AttendanceGroupName
                                          ,[Description] = @AttendanceGroupDescription
                                          ,[Color] = @intColor
                                          ,[Active] = @IsActive
                                     WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsAttendanceGroup obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[AttendanceGroups]
                                       SET [Active] ='0'
                                     WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsAttendanceGroup> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendanceGroup> Lists = new List<clsAttendanceGroup>();
                String query = @"SELECT Id
                                  ,[Name] As AttendanceGroupName
                                  ,[Description] As AttendanceGroupDescription
                                  ,Color As intColor
                                  ,Active As IsActive
                              FROM AttendanceGroups";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsAttendanceGroup>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsAttendanceGroup> GetAllActive()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendanceGroup> Lists = new List<clsAttendanceGroup>();
                String query = @"SELECT Id
                                  ,[Name] As AttendanceGroupName
                                  ,[Description] As AttendanceGroupDescription
                                  ,Color As intColor
                                  ,Active As IsActive
                              FROM AttendanceGroups Where Active = 1";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsAttendanceGroup>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public clsAttendanceGroup FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsAttendanceGroup List = new clsAttendanceGroup();
                String query = "";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsAttendanceGroup>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        public List<clsAttendanceGroup> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendanceGroup> Lists = new List<clsAttendanceGroup>();
                String query = @"SELECT Id
                                  ,[Name] As AttendanceGroupName
                                  ,[Description] As AttendanceGroupDescription
                                  ,Color As intColor
                                  ,Active As IsActive
                              FROM AttendanceGroups ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsAttendanceGroup>(query, new { whereQuery = whereQuery }).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

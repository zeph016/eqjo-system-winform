using FGCIJOROSystem.Domain.Configurations.Attendance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using FGCIJOROSystem.Domain.Configurations.Customers;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
    public class AttendanceStatusRepository : IRepository<clsAttendanceStatus>
    {

        public void Add(clsAttendanceStatus obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[AttendanceStatus]
                                               ([Name]
                                               ,[Symbol]
                                               ,[Description]
                                               ,[Color]
                                               ,[Active])
                                         VALUES
                                               (@AttendanceStatusName
                                               ,@Symbol
                                               ,@AttendanceStatusDescription
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

        public void Update(clsAttendanceStatus obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[AttendanceStatus]
                                   SET [Name] = @AttendanceStatusName
                                      ,[Symbol] = @Symbol
                                      ,[Description] = @AttendanceStatusDescription
                                      ,[Color] = @intColor
                                      ,[Active] = @IsActive
                                 WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsAttendanceStatus obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[AttendanceStatus]
                                   SET Active ='0'
                                 WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsAttendanceStatus> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendanceStatus> Lists = new List<clsAttendanceStatus>();
                String query = @"SELECT [Id]
                                  ,[Name] As AttendanceStatusName
                                  ,[Symbol] As Symbol
                                  ,[Description] As AttendanceStatusDescription
                                  ,[Color] As intColor
                                  ,[Active] As IsActive
                              FROM [dbo].[AttendanceStatus] ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsAttendanceStatus>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsAttendanceStatus> GetAllActiveStatus()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendanceStatus> Lists = new List<clsAttendanceStatus>();
                String query = @"SELECT [Id]
                                  ,[Name] As AttendanceStatusName
                                  ,[Symbol] As Symbol
                                  ,[Description] As AttendanceStatusDescription
                                  ,[Color] As intColor
                                  ,[Active] As IsActive
                              FROM [dbo].[AttendanceStatus] where Active = '1' ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsAttendanceStatus>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public clsAttendanceStatus FindByID(long id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsAttendanceStatus List = new clsAttendanceStatus();
                String query = "";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsAttendanceStatus>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        public List<clsAttendanceStatus> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsAttendanceStatus> Lists = new List<clsAttendanceStatus>();
                String query = @"SELECT [Id]
                                  ,[Name] As AttendanceStatusName
                                  ,[Symbol] As Symbol
                                  ,[Description] As AttendanceStatusDescription
                                  ,[Color] As intColor
                                  ,[Active] As IsActive
                              FROM [dbo].[AttendanceStatus]
                          WHERE Name like '%' + @whereQuery + '%'";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsAttendanceStatus>(query, new { whereQuery = whereQuery }).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

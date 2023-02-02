using FGCIJOROSystem.Domain.Configurations.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
    public class UsersLogRepository
    {
        public long Add(clsUsersLog obj)
        {
            long id = 0;
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[UsersLog]
                                                   ([Username]
                                                   ,[MLEmployeeId]
                                                   ,[EmpName]
                                                   ,[UserLevelId]
                                                   ,[BranchId]
                                                   ,[ComputerName]
                                                   ,[DateLogin]
                                                   ,[TimeLogin]
                                                   ,[DateLogout]
                                                   ,[TimeLogout]
                                                   ,[DayActivity]
                                                   ,[ActivityType]
                                                   ,[OnlineUser])
                                             VALUES
                                                   (@Username
                                                   ,@MLEmployeeId
                                                   ,@EmpName
                                                   ,@UserLevelId
                                                   ,@BranchId
                                                   ,@ComputerName
                                                   ,@DateLogin
                                                   ,@TimeLogin
                                                   ,@DateLogout
                                                   ,@TimeLogout
                                                   ,@DayActivity
                                                   ,@ActivityType
                                                   ,@OnlineUser)
                    SELECT SCOPE_IDENTITY()";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                id = connection.Query<long>(query, obj).FirstOrDefault();
                connection.Close();
            }

            return id;
        }
        public void Update(clsUsersLog obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[UsersLog]
                                           SET	DateLogin = GETDATE()
												,[TimeLogout] = GETDATE()
                                                ,DayActivity = @DayActivity
                                                ,ActivityType = @ActivityType
                                                ,OnlineUser = @OnlineUser
                                         WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }
        public void RESET(clsUsersLog obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE UsersLog
                                               SET DayActivity = @DayActivity
                                                ,OnlineUser = 0
                                         WHERE Username = 'sa' AND DayActivity = 'Log In'";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }
        public void Delete(clsUsersLog obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }
        public List<clsUsersLog> GetAllLogs(DateTime timeStart, DateTime timeEnd)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,OnlineUser
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
			                                    WHERE ((DayActivity = 'Log In - Log Out') OR (DayActivity = 'Log In'))
                                                            AND DateLogin BETWEEN @timeStart AND @timeEnd
				                                    ORDER BY DateLogin DESC";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query, new {timeStart = timeStart, timeEnd = timeEnd}).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,OnlineUser
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
			                                    WHERE  DateLogin >= CAST(GETDATE() AS DATE)
													AND DayActivity = 'Log In - Log Out' OR DayActivity = 'Log In'
				                                    ORDER BY DateLogin DESC";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAllOnline()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT [EmpName]
		                                ,BranchId
		                                ,b.Name as BranchName   
										,UL.DateLogin
                                      FROM UsersLog AS UL
											left join dbo.Branches as B On B.Id = UL.BranchId
		                                    where OnlineUser = 1 AND UL.DateLogin = cast(getdate() as Date)
		                                    group by EmpName, BranchId, B.Name, UL.DateLogin";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAllAuditTrail(DateTime DateStart, DateTime DateEnd)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,OnlineUser
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
                                                 Where NOT (DayActivity = 'Log Out' OR DayActivity = 'Log In' OR DayActivity = 'Log In - Log Out' OR DayActivity = 'Reset Online Status')
														AND DateLogin >= DATEADD(MONTH, -1, GetDate())
                                                            AND DateLogin BETWEEN (convert(datetime, @DateStart,101)) AND (convert(datetime,@DateEnd,101))
				                                    ORDER BY DateLogin DESC";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query, new { DateStart = DateStart, DateEnd = DateEnd }).ToList();
                connection.Close();
                return Lists;
            }
        }
        public clsUsersLog IsOnlineUser(String Username)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsUsersLog Item = new clsUsersLog();
                String query = @"SELECT TOP 1 DateLogin,CONVERT(varchar(8),TimeLogin,108) as TimeLogin, Username, OnlineUser
                                                FROM [UsersLog]
                                                WHERE  Username = @Username
                                                ORDER BY DateLogin DESC";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Item = connection.Query<clsUsersLog>(query, new { Username = Username }).FirstOrDefault();
                connection.Close();
                return Item;
            }
        }
        public clsUsersLog FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsUsersLog> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @" " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query, new { whereQuery = whereQuery }).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAllAudit()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,OnlineUser
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
                                                 Where NOT (DayActivity = 'Log Out' OR DayActivity = 'Log In' OR DayActivity = 'Log In - Log Out' OR DayActivity = 'Reset Online Status')
														AND DateLogin >= DATEADD(MONTH, -1, GetDate())
				                                    ORDER BY DateLogin DESC";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAllAuditTrailFiltered3(DateTime DateStart, DateTime DateEnd, bool isPersonnel, string personnelIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,ActivityType
										  ,OnlineUser
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
                                                 Where DateLogin BETWEEN (convert(datetime, @DateStart,101)) AND (convert(datetime,@DateEnd,101))
																AND ActivityType = 3";
                if (isPersonnel)
                {
                    query += " AND uls.MLEmployeeId IN ( " + personnelIds + " )";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query, new { DateStart = DateStart, DateEnd = DateEnd }).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAllAuditTrailFiltered4(DateTime DateStart, DateTime DateEnd, bool isPersonnel, string personnelIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,ActivityType
										  ,OnlineUser
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
                                                 Where DateLogin BETWEEN (convert(datetime, @DateStart,101)) AND (convert(datetime,@DateEnd,101))
																AND ActivityType = 4";
                if (isPersonnel)
                {
                    query += " AND uls.MLEmployeeId IN ( " + personnelIds + " )";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query, new { DateStart = DateStart, DateEnd = DateEnd }).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAllAuditTrailFiltered5(DateTime DateStart, DateTime DateEnd, bool isPersonnel, string personnelIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,ActivityType
										  ,OnlineUser
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
                                                 Where DateLogin BETWEEN (convert(datetime, @DateStart,101)) AND (convert(datetime,@DateEnd,101))
																AND ActivityType = 5";
                if (isPersonnel)
                {
                    query += " AND uls.MLEmployeeId IN ( " + personnelIds + " )";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query, new { DateStart = DateStart, DateEnd = DateEnd }).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAllAuditTrailFiltered6(DateTime DateStart, DateTime DateEnd, bool isPersonnel, string personnelIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,ActivityType
										  ,OnlineUser
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
                                                 Where DateLogin BETWEEN (convert(datetime, @DateStart,101)) AND (convert(datetime,@DateEnd,101))
																AND ActivityType = 6";
                if (isPersonnel)
                {
                    query += " AND uls.MLEmployeeId IN ( " + personnelIds + " )";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query, new { DateStart = DateStart, DateEnd = DateEnd }).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAllAuditTrailFiltered34(DateTime DateStart, DateTime DateEnd, bool isPersonnel, string personnelIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,ActivityType
										  ,OnlineUser
                                          --,(SELECT 1 FROM UsersLog s WHERE s.ActivityType = 3 AND uls.Id = s.Id) as AddActivity
										  --,(SELECT 1 FROM UsersLog s WHERE s.ActivityType = 4 AND uls.Id = s.Id) as UpdateActivity
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
                                                 Where DateLogin BETWEEN (convert(datetime, @DateStart,101)) AND (convert(datetime,@DateEnd,101))
																AND (ActivityType = 3 OR ActivityType = 4)";
                if (isPersonnel)
                {
                    query += " AND uls.MLEmployeeId IN ( " + personnelIds + " )";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query, new { DateStart = DateStart, DateEnd = DateEnd }).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAllAuditTrailFiltered56(DateTime DateStart, DateTime DateEnd, bool isPersonnel, string personnelIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,ActivityType
										  ,OnlineUser
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
                                                 Where DateLogin BETWEEN (convert(datetime, @DateStart,101)) AND (convert(datetime,@DateEnd,101))
																AND (ActivityType = 5 OR ActivityType = 6)";
                if (isPersonnel)
                {
                    query += " AND uls.MLEmployeeId IN ( " + personnelIds + " )";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query, new { DateStart = DateStart, DateEnd = DateEnd }).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAllAuditTrailFiltered8(DateTime DateStart, DateTime DateEnd, bool isPersonnel, string personnelIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,ActivityType
										  ,OnlineUser
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
                                                 Where DateLogin BETWEEN (convert(datetime, @DateStart,101)) AND (convert(datetime,@DateEnd,101))
																AND ActivityType = 8";
                if (isPersonnel)
                {
                    query += " AND uls.MLEmployeeId IN ( " + personnelIds + " )";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query, new { DateStart = DateStart, DateEnd = DateEnd }).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUsersLog> GetAllAuditTrailFiltered3456(DateTime DateStart, DateTime DateEnd, bool isPersonnel, string personnelIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUsersLog> Lists = new List<clsUsersLog>();
                String query = @"SELECT ULs.[Id]
                                          ,uls.[Username]
                                          ,uls.[MLEmployeeId]
                                          ,[EmpName]
                                          ,uls.[UserLevelId]
	                                      ,ul.UserLevelName
                                          ,uls.[BranchId]
	                                      ,b.Name as BranchName
                                          ,[ComputerName]
										  ,DateLogin
                                          ,CONVERT(varchar(8),TimeLogin,108) AS TimeLogin
										  ,DateLogout
                                          ,CONVERT(varchar(8), TimeLogout, 108) AS TimeLogout
                                          ,DayActivity
										  ,ActivityType
										  ,OnlineUser
										  ,(SELECT 1 FROM UsersLog s WHERE s.ActivityType = 3 AND uls.Id = s.Id) as AddActivity
										  ,(SELECT 1 FROM UsersLog s WHERE s.ActivityType = 4 AND uls.Id = s.Id) as UpdateActivity
										  ,(SELECT 1 FROM UsersLog s WHERE s.ActivityType = 5 AND uls.Id = s.Id) as AddROActivity
										  ,(SELECT 1 FROM UsersLog s WHERE s.ActivityType = 6 AND uls.Id = s.Id) as UpdateROActivity
										  ,(SELECT 1 FROM UsersLog s WHERE s.ActivityType = 8 AND uls.Id = s.Id) as ActualAdvanceActivity
                                      FROM UsersLog as ULs
                                      LEFT JOIN dbo.Users as U on U.Id = uls.Id
	                                    LEFT JOIN dbo.UserLevels as UL on ul.Id = uls.UserLevelId
		                                    LEFT JOIN dbo.Branches as B on B.Id = uls.BranchId
                                                 Where DateLogin BETWEEN (convert(datetime, @DateStart,101)) AND (convert(datetime,@DateEnd,101))
																AND (ActivityType = 3 OR ActivityType = 4 OR ActivityType = 5 OR ActivityType = 6 OR ActivityType = 8)";
                if (isPersonnel)
                {
                    query += " AND uls.MLEmployeeId IN ( " + personnelIds + " )";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUsersLog>(query, new { DateStart = DateStart, DateEnd = DateEnd }).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

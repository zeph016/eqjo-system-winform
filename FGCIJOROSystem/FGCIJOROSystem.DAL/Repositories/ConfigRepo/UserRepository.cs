using FGCIJOROSystem.Domain.Configurations.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
   public class UserRepository : IRepository<clsUser>
    {
        public int Id { get; set; }
        public void Add(clsUser obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [Users]
		                                            ([UserName]
                                                    ,[UserPassword]
                                                    ,[MLEmployeeId]
                                                    ,[UserLevelId]
                                                    ,[Active]
                                                    ,[BranchId])
		                                            VALUES
                                                    (@UserName
                                                    ,@EncryptPassword
                                                    ,@MLEmployeeId
                                                    ,@UserLevelId
                                                    ,@Active
                                                    ,@BranchId)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }            
        }

        public void Update(clsUser obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [Users]
                                   SET [UserName] = @UserName
                                      ,[UserPassword] = @UserPassword
                                      ,[MLEmployeeId] = @MLEmployeeId
                                      ,[UserLevelId] = @UserLevelId
                                      ,[Active] = @Active
                                      ,[BranchId] = @BranchId
                                 WHERE (Id = @Id)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsUser obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [Users]
                                   SET [Active] = '0'
                                 WHERE (Id = @Id)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsUser> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUser> Lists = new List<clsUser>();
                String query = @"SELECT GI.Id AS GIID
                                        , GI.FirstName
                                        , GI.MiddleName
                                        , GI.LastName
                                        , GI.NameExtension
                                        ,GI.LastName + ', ' + FirstName + ' ' + MiddleName + ' ' + NameExtension AS FullName
                                        ,GI.Picture as EmpPicture
                                        , EI.DateHired
                                        , EI.EmployeeStatusId
                                        ,EI.PositionsId
                                        , P.PositionName AS Position
                                        , P.SectionsId
                                        , S.SectionName AS Section
                                        , S.DepartmentsId
                                        ,D.DepartmentName AS Department
                                        ,Users.Id AS Id
                                        , Users.UserName
                                        , Users.UserPassword
                                        , Users.MLEmployeeId
                                        , Users.UserLevelId
                                        , Users.Active
                                        ,Users.BranchId
                                        ,Ul.UserLevelName
                                        ,B.Name AS Branch
                                        FROM Users 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Users.MLEmployeeId
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations  AS GI ON GI.Id = EI.GeneralInformationsId 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS P ON P.Id = EI.PositionsId
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS S ON S.Id = P.SectionsId 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS D ON D.Id = S.DepartmentsId
                                            LEFT OUTER JOIN FGCIJOROSystemDB.dbo.UserLevels As UL On Users.UserLevelId = UL.Id
													LEFT JOIN FGCIJOROSystemDB.dbo.Branches As B on B.Id = users.BranchId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUser>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsUser FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsUser List = new clsUser();
                String query = "SELECT * FROM [Users] WHERE (Id = @Id)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsUser>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        public List<clsUser> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUser> Lists = new List<clsUser>();
                String query = @"SELECT
                                   U.Id
                                  ,MLEmployeeId
                                  ,U.UserName
                                  ,U.UserPassword
	                              ,gi.FirstName
	                              ,gi.MiddleName
	                              ,gi.LastName
	                              ,gi.NameExtension
	                              ,CONCAT(GI.FirstName, ' ', GI.MiddleName, ' ',GI.LastName, ' ' ,GI.NameExtension) as FullName
                                  ,[UserLevelId]
	                              ,ul.UserLevelName
	                              ,p.PositionName as Position
								  ,S.SectionName as Section
								  ,D.DepartmentName AS Department
	                              ,gi.Picture as EmpPicture
                                  ,u.[Active]
                                  ,U.BranchId
                                  ,B.Name AS Branch
                              FROM [FGCIJOROSystemDB].[dbo].[Users] as U
                              LEFT JOIN FGCIJOROSystemDB.dbo.UserLevels As UL on UL.Id = U.UserLevelId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations as EI on EI.Id = U.MLEmployeeId
		                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations as GI on GI.id = EI.GeneralInformationsId
			                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As P on P.Id = EI.PositionsId
											LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections As S on S.Id = P.SectionsId
												LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments As D on D.Id = S.DepartmentsId
													LEFT JOIN FGCIJOROSystemDB.dbo.Branches As B on B.Id = U.BranchId
                            " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUser>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        #region GetAllByCategory
        public List<clsUser> GetAllActive()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUser> Lists = new List<clsUser>();
                String query = @"SELECT GI.Id AS GIID
                                        , GI.FirstName
                                        , GI.MiddleName
                                        , GI.LastName
                                        , GI.NameExtension
                                        ,GI.LastName + ', ' + FirstName + ' ' + MiddleName + ' ' + NameExtension AS FullName
                                        ,GI.Picture as EmpPicture
                                        , EI.DateHired
                                        , EI.EmployeeStatusId
                                        ,EI.PositionsId
                                        , P.PositionName AS Position
                                        , P.SectionsId
                                        , S.SectionName AS Section
                                        , S.DepartmentsId
                                        ,D.DepartmentName AS Department
                                        ,Users.Id AS Id
                                        , Users.UserName
                                        , Users.UserPassword
                                        , Users.MLEmployeeId
                                        , Users.UserLevelId
                                        , Users.Active
                                        ,Users.BranchId
                                        ,Ul.UserLevelName
                                        ,b.Name as Branch
                                        FROM Users 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Users.MLEmployeeId
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations  AS GI ON GI.Id = EI.GeneralInformationsId 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS P ON P.Id = EI.PositionsId
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS S ON S.Id = P.SectionsId 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS D ON D.Id = S.DepartmentsId
                                            LEFT OUTER JOIN FGCIJOROSystemDB.dbo.UserLevels As UL On Users.UserLevelId = UL.Id
													LEFT JOIN FGCIJOROSystemDB.dbo.Branches As B on B.Id = users.BranchId
                                             where users.Active = 1 ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUser>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsUser> GetAllInActive()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsUser> Lists = new List<clsUser>();
                String query = @"SELECT GI.Id AS GIID
                                        , GI.FirstName
                                        , GI.MiddleName
                                        , GI.LastName
                                        , GI.NameExtension
                                        ,GI.LastName + ', ' + FirstName + ' ' + MiddleName + ' ' + NameExtension AS FullName
                                        ,GI.Picture as EmpPicture
                                        , EI.DateHired
                                        , EI.EmployeeStatusId
                                        ,EI.PositionsId
                                        , P.PositionName AS Position
                                        , P.SectionsId
                                        , S.SectionName AS Section
                                        , S.DepartmentsId
                                        ,D.DepartmentName AS Department
                                        ,Users.Id AS Id
                                        , Users.UserName
                                        , Users.UserPassword
                                        , Users.MLEmployeeId
                                        , Users.UserLevelId
                                        , Users.Active
                                        ,Users.BranchId
                                        ,Ul.UserLevelName                                        
                                        ,b.Name as Branch
                                        FROM Users 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Users.MLEmployeeId
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations  AS GI ON GI.Id = EI.GeneralInformationsId 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS P ON P.Id = EI.PositionsId
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS S ON S.Id = P.SectionsId 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS D ON D.Id = S.DepartmentsId
                                            LEFT OUTER JOIN FGCIJOROSystemDB.dbo.UserLevels As UL On Users.UserLevelId = UL.Id
													LEFT JOIN FGCIJOROSystemDB.dbo.Branches As B on B.Id = users.BranchId
                                             where users.Active = 0 ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsUser>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public clsUser FindDuplicate(string UserName)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsUser Item = new clsUser();
                String query = @"SELECT GI.Id AS GIID
                                        , GI.FirstName
                                        , GI.MiddleName
                                        , GI.LastName
                                        , GI.NameExtension
                                        ,GI.LastName + ', ' + FirstName + ' ' + MiddleName + ' ' + NameExtension AS FullName
                                        ,GI.Picture as EmpPicture
                                        , EI.DateHired
                                        , EI.EmployeeStatusId
                                        ,EI.PositionsId
                                        , P.PositionName AS Position
                                        , P.SectionsId
                                        , S.SectionName AS Section
                                        , S.DepartmentsId
                                        ,D.DepartmentName AS Department
                                        ,Users.Id AS Id
                                        , Users.UserName
                                        , Users.UserPassword
                                        , Users.MLEmployeeId
                                        , Users.UserLevelId
                                        , Users.Active
                                        ,Users.BranchId
                                        ,Ul.UserLevelName
                                  ,b.Name as Branch
                                        FROM Users 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Users.MLEmployeeId
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations  AS GI ON GI.Id = EI.GeneralInformationsId 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS P ON P.Id = EI.PositionsId
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS S ON S.Id = P.SectionsId 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS D ON D.Id = S.DepartmentsId
                                            LEFT OUTER JOIN FGCIJOROSystemDB.dbo.UserLevels As UL On Users.UserLevelId = UL.Id
													LEFT JOIN FGCIJOROSystemDB.dbo.Branches As B on B.Id = users.BranchId
                                           Where UserName = @UserName";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Item = connection.Query<clsUser>(query, new { UserName = UserName}).FirstOrDefault();
                connection.Close();
                return Item;
            }
        }
        public clsUser FindDuplicateId(Int64 MLEmployeeId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsUser Item = new clsUser();
                String query = @"SELECT GI.Id AS GIID
                                        , GI.FirstName
                                        , GI.MiddleName
                                        , GI.LastName
                                        , GI.NameExtension
                                        ,GI.LastName + ', ' + FirstName + ' ' + MiddleName + ' ' + NameExtension AS FullName
                                        ,GI.Picture as EmpPicture
                                        , EI.DateHired
                                        , EI.EmployeeStatusId
                                        ,EI.PositionsId
                                        , P.PositionName AS Position
                                        , P.SectionsId
                                        , S.SectionName AS Section
                                        , S.DepartmentsId
                                        ,D.DepartmentName AS Department
                                        ,Users.Id AS Id
                                        , Users.UserName
                                        , Users.UserPassword
                                        , Users.MLEmployeeId
                                        , Users.UserLevelId
                                        , Users.Active
                                        ,Users.BranchId
                                        ,Ul.UserLevelName
                                  ,b.Name as Branch
                                        FROM Users 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Users.MLEmployeeId
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations  AS GI ON GI.Id = EI.GeneralInformationsId 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Positions AS P ON P.Id = EI.PositionsId
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS S ON S.Id = P.SectionsId 
                                            LEFT OUTER JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS D ON D.Id = S.DepartmentsId
                                            LEFT OUTER JOIN FGCIJOROSystemDB.dbo.UserLevels As UL On Users.UserLevelId = UL.Id
													LEFT JOIN FGCIJOROSystemDB.dbo.Branches As B on B.Id = users.BranchId
                                           Where MLEmployeeId = @MLEmployeeId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Item = connection.Query<clsUser>(query, new { MLEmployeeId = MLEmployeeId }).FirstOrDefault();
                connection.Close();
                return Item;
            }
        }
        #endregion
    }
}

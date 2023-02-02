using FGCIJOROSystem.Domain.Configurations.Personnels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories
{
    public class PersonnelRepository
    {
        public void Add(clsPersonnels obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[Personnels]
                                           ([EmployeeId]
                                           ,[IsSectionHead]
                                           ,[SectionId],Location
                                           ,Active)
                                     VALUES
                                           (@EmployeeId
                                           ,@IsSectionHead
                                           ,@SectionId,@Location
                                           ,@Active)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsPersonnels obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[Personnels]
                                       SET [EmployeeId] = @EmployeeId
                                          ,[IsSectionHead] = @IsSectionHead
                                          ,[SectionId] = @SectionId                                          
                                  ,[Location] = @Location
                                         ,[Active] = @Active
                                     WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsPersonnels obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[Personnels]
                                       SET [Active] = @Active
                                     WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }
        public void DeletePersonnel(clsPersonnels obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[Personnels]
                                       SET [Active] = '0'
                                     WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }
        public List<clsPersonnels> GetAll(Int64 attendanceGroupId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsPersonnels> List = new List<clsPersonnels>();
                String query = @"SELECT 
                                 P.[Id]
                                ,P.[EmployeeId]
                                ,P.[IsSectionHead]
                                ,P.[SectionId]
                                ,P.Location                                
                                ,S.GroupDescription As SectionName
								,AG.Id
								,AG.Name AS AttendanceGroupName
                                ,GI.FirstName
                                ,GI.MiddleName
                                ,GI.LastName
                                ,GI.NameExtension
                                ,Pos.Id As PositionId
                                ,Pos.PositionName
                                ,B.Id As BranchId
                                ,B.Name As BranchName
                                ,P.Active
                                FROM [dbo].[Personnels] As P
                                LEFT JOIN Sections As S ON P.SectionId = S.Id
                                LEFT JOIN Branches AS B ON S.BranchId = B.Id  
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations As EI ON  EI.Id = P.EmployeeId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON EI.GeneralInformationsId = GI.Id          
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId
								LEFT JOIN AttendanceGroups AG ON AG.Id = S.AttendanceGroupId
                                WHERE P.Id <> 0 ";
                query += " AND AG.Id IN (" + attendanceGroupId + ")";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsPersonnels>(query).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsPersonnels> GetAllMechanic()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsPersonnels> List = new List<clsPersonnels>();
                String query = @"SELECT P.[Id]
                                ,P.[EmployeeId]
                                ,GI.FirstName
                                ,GI.MiddleName
                                ,GI.LastName
                                ,GI.NameExtension
                                ,Pos.PositionName
                                ,S.GroupDescription As SectionName
                                ,B.Id As BranchId
                                ,B.Name As BranchName
                                ,P.Active
                            FROM [dbo].[Personnels] As P
                            LEFT JOIN Sections As S ON P.SectionId = S.Id      
                            LEFT JOIN Branches AS B ON S.BranchId = B.Id  
                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON P.[EmployeeId] = GI.Id
                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations As EI ON  EI.GeneralInformationsId = GI.Id     
                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId
                            where P.Active = 1";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsPersonnels>(query).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsPersonnels> SearchBy(string whereQuery)
        {            
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsPersonnels> List = new List<clsPersonnels>();
                String query = @"SELECT 
                                 P.[Id]
                                ,P.[EmployeeId]
                                ,P.[IsSectionHead]
                                ,P.[SectionId]
                                ,P.[Location]
                                ,S.GroupDescription As SectionName
                                ,GI.FirstName
                                ,GI.MiddleName
                                ,GI.LastName
                                ,GI.NameExtension
                                ,Pos.Id As PositionId
                                ,Pos.PositionName
                                ,B.Id As BranchId
                                ,B.Name As BranchName
                                ,P.Active
                                FROM [dbo].[Personnels] As P
                                LEFT JOIN Sections As S ON P.SectionId = S.Id
                                LEFT JOIN Branches AS B ON S.BranchId = B.Id
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations As EI ON  EI.Id = P.EmployeeId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON EI.GeneralInformationsId = GI.Id                                
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsPersonnels>(query).ToList();
                connection.Close();
                return List;            
            }
           
        }


        public clsPersonnels FindByID(Int64 id)
        {
            throw new NotImplementedException();
        }
        public List<clsPersonnels> GetAll(string sectionIds = "")
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsPersonnels> List = new List<clsPersonnels>();
                String query = @"SELECT 
                                 P.[Id]
                                ,P.[EmployeeId]
                                ,P.[IsSectionHead]
                                ,P.[SectionId]
                                ,P.[Location]
                                ,S.GroupDescription As SectionName
                                ,GI.FirstName
                                ,GI.MiddleName
                                ,GI.LastName
                                ,GI.NameExtension
                                ,Pos.Id As PositionId
                                ,Pos.PositionName
                                ,P.Active
                                FROM [dbo].[Personnels] As P
                                LEFT JOIN Sections As S ON P.SectionId = S.Id
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations As EI ON  EI.Id = P.EmployeeId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON EI.GeneralInformationsId = GI.Id          
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId ";
                    query += " Where P.Active = 1 AND P.SectionId IN ( " + sectionIds + " ) Order by GI.Lastname";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsPersonnels>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public List<clsPersonnels> GetAllActive(string sectionIds = "")
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsPersonnels> List = new List<clsPersonnels>();
                String query = @"SELECT 
                                 P.[Id]
                                ,P.[EmployeeId]
                                ,P.[IsSectionHead]
                                ,P.[SectionId]
                                ,P.[Location]
                                ,S.GroupDescription As SectionName
                                ,GI.FirstName
                                ,GI.MiddleName
                                ,GI.LastName
                                ,GI.NameExtension
                                ,Pos.Id As PositionId
                                ,Pos.PositionName
                                ,P.Active
                                FROM [dbo].[Personnels] As P
                                LEFT JOIN Sections As S ON P.SectionId = S.Id
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations As EI ON  EI.Id = P.EmployeeId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON EI.GeneralInformationsId = GI.Id          
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId ";
                query += " Where P.Active = 1 "; // AND P.SectionId IN ( " + sectionIds + " ) Order by GI.Lastname";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsPersonnels>(query).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsPersonnels> GetAllMechanic(string mechanicIds ="")
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsPersonnels> List = new List<clsPersonnels>();
                String query = @"SELECT 
                                 P.[Id]
                                ,P.[EmployeeId]
                                ,P.[IsSectionHead]
                                ,P.[SectionId]
                                ,P.Location
                                ,S.GroupDescription As SectionName
                                ,GI.FirstName
                                ,GI.MiddleName
                                ,GI.LastName
                                ,GI.NameExtension
                                ,Pos.Id As PositionId
                                ,Pos.PositionName
                                ,P.Active
                                FROM [dbo].[Personnels] As P
                                LEFT JOIN Sections As S ON P.SectionId = S.Id
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations As EI ON  EI.Id = P.EmployeeId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON EI.GeneralInformationsId = GI.Id          
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId ";
                    query += " Where P.EmployeeId IN ( " + mechanicIds + " )";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsPersonnels>(query).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsPersonnels> GetAllPersonnels() 
        {
            using (IDbConnection connection = DbConnection.MasterlistConnection)
            {
                List<clsPersonnels> List = new List<clsPersonnels>();
                String query = @"SELECT
									 EI.Id AS EmployeeId
									 ,GI.FirstName
									 --,(Select Concat(Substring(GI.MiddleName,1,1), '.')) AS MiddleName
									 ,GI.MiddleName
									 ,GI.LastName
									 ,GI.NameExtension
									 ,EI.DateHired
									 ,EmployeeStatusId
									 --,GI.Picture as EmpPicture
									 ,EI.PositionsId
									 ,P.PositionName AS PositionName
									 ,p.SectionsId
									 ,S.SectionName as SectionName
									 ,S.DepartmentsId
									 ,D.DepartmentName as DepartmentName
									 ,b.Id AS BranchId
									 ,B.Name AS BranchName
								FROM FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GI
								LEFT JOIN EmployeesInformations EI on GI.Id = Ei.GeneralInformationsId
								LEFT JOIN Positions P ON P.Id = EI.PositionsId
								LEFT JOIN Sections S on S.Id = P.SectionsId
								LEFT JOIN FGCIJOROSystemDB.dbo.Branches B ON S.Id = B.Id
								LEFT JOIN Departments D on D.Id = S.DepartmentsId
                                LEFT JOIN EmployeeStatus ES ON ES.Id = EI.EmployeeStatusId
								WHERE ES.Category = 0 ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsPersonnels>(query).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsPersonnels> GetAllFiltered(Int64 attendanceGroupId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsPersonnels> List = new List<clsPersonnels>();
                String query = @"SELECT 
                                 P.[Id]
                                ,P.[EmployeeId]
                                ,P.[IsSectionHead]
                                ,P.[SectionId]
                                ,P.Location
								,S.AttendanceGroupId
                                ,S.GroupDescription As SectionName
								,AG.Id
								,AG.Name as AttendanceGroupName
                                ,GI.FirstName
                                ,GI.MiddleName
                                ,GI.LastName
                                ,GI.NameExtension
                                ,Pos.Id As PositionId
                                ,Pos.PositionName
                                ,B.Id As BranchId
                                ,B.Name As BranchName
                                ,P.Active
                                FROM [dbo].[Personnels] As P
                                LEFT JOIN Sections As S ON P.SectionId = S.Id
                                LEFT JOIN Branches AS B ON S.BranchId = B.Id  
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations As EI ON  EI.Id = P.EmployeeId
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON EI.GeneralInformationsId = GI.Id          
                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId 
								LEFT JOIN AttendanceGroups AG ON AG.Id = s.AttendanceGroupId
								WHERE P.Active = 1 ";
                query += " AND AG.Id IN ( " + attendanceGroupId + " )"; 
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsPersonnels>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

using FGCIJOROSystem.Domain.ActualAdvance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.ActualAdvanceRepo
{
    public class EmployeeActivityRepository
    {
        //original query
        //public List<clsActualAdvanceDetails> ActualAdvanceList(String whereQuery, DateTime DateofUpdate)
        //{
        //    using (IDbConnection connection = DbConnection.JOROConnection)
        //    {
        //        List<clsActualAdvanceDetails> Lists = new List<clsActualAdvanceDetails>();
        //        String query = @"SELECT * FROM (SELECT 
        //                                Per.[EmployeeId]
	       //                             ,CONCAT(GI.LastName, ' ', GI.NameExtension,', ', GI.FirstName,' ', GI.MiddleName) AS EmployeeName
        //                                ,S.Id As SectionId
        //                                ,S.GroupDescription As Section
        //                                ,S.IsChecklistGroup
        //                                ,S.BranchId
        //                                ,S.AttendanceGroupId
	       //                             ,Pos.PositionName As Position
	       //                             ,0 As Id
	       //                             ,null As DateOfUpdate
	       //                             ,0 As AMStatus
								//		,'' As AMStatusStr
	       //                             ,0 AS PMStatus
								//		,'' As PMStatusStr
	       //                             ,'' As AttendanceRemarks
        //                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] AS Per
        //                            LEFT JOIN Sections As S ON S.Id = Per.SectionId
                                    
								//	LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
        //                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
        //                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId
        //                            WHERE Per.EmployeeId NOT IN (SELECT EmployeeId FROM ActualAdvanceDetails AS AD 
        //                            WHERE AD.DateOfUpdate = convert(varchar, @DateofUpdate, 101)) AND Per.Active = 1--S.IsChecklistGroup = 1 AND
        //                            UNION ALL
        //                            SELECT 
        //                                Per.[EmployeeId]
	       //                             ,CONCAT(GI.LastName, ' ', GI.NameExtension,', ', GI.FirstName,' ', GI.MiddleName) AS EmployeeName
        //                                ,S.Id As SectionId
        //                                ,S.GroupDescription As Section
        //                                ,S.IsChecklistGroup
        //                                ,S.BranchId
        //                                ,S.AttendanceGroupId
	       //                             ,Pos.PositionName As Position
	       //                             ,AD.Id
	       //                             ,AD.DateOfUpdate
	       //                             ,AD.AMStatus
								//		,A.Symbol As AMStatusStr
	       //                             ,AD.PMStatus
								//		,P.Symbol As PMStatusStr
	       //                             ,AD.AttendanceRemarks
        //                            FROM [FGCIJOROSystemDB].[dbo].[Personnels] AS Per
        //                            LEFT JOIN Sections As S ON S.Id = Per.SectionId
								//	LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
        //                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId                                    
        //                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId
        //                            LEFT JOIN ActualAdvanceDetails AS AD ON AD.EmployeeId = Per.EmployeeId
								//	LEFT JOIN AttendanceStatus AS A ON A.Id = AMStatus
								//	LEFT JOIN AttendanceStatus AS P ON P.Id = PMStatus
        //                            WHERE AD.DateOfUpdate = convert(varchar, @DateofUpdate, 101)) T " + whereQuery +
        //                            @"ORDER BY Section,EmployeeName";
        //        if (connection.State == ConnectionState.Closed)
        //        {
        //            connection.Open();
        //        }
        //        Lists = connection.Query<clsActualAdvanceDetails>(query, new { DateofUpdate = DateofUpdate }).ToList();
        //        connection.Close();
        //        return Lists;
        //    }
        //}
        public List<clsActualAdvanceDetails> ActualAdvanceList(String whereQuery, DateTime DateofUpdate)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsActualAdvanceDetails> Lists = new List<clsActualAdvanceDetails>();
                String query = @"SELECT * FROM (SELECT 
                                        Per.[EmployeeId]
	                                    ,CONCAT(GI.LastName, ' ', GI.NameExtension,', ', GI.FirstName,' ', GI.MiddleName) AS EmployeeName
                                        ,S.Id As SectionId
                                        ,S.GroupDescription As Section
                                        ,S.IsChecklistGroup
                                        ,S.BranchId
                                        ,S.AttendanceGroupId
	                                    ,Pos.PositionName As Position
	                                    ,0 As Id
	                                    ,null As DateOfUpdate
	                                    ,0 As AMStatus
										,'' As AMStatusStr
	                                    ,0 AS PMStatus
										,'' As PMStatusStr
	                                    ,'' As AttendanceRemarks
                                    FROM [FGCIJOROSystemDB].[dbo].[Personnels] AS Per
                                    LEFT JOIN Sections As S ON S.Id = Per.SectionId
                                    
									LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId
                                    WHERE Per.EmployeeId NOT IN (SELECT EmployeeId FROM ActualAdvanceDetails AS AD 
                                    WHERE AD.DateOfUpdate = convert(varchar, @DateofUpdate, 101)) AND Per.Active = 1--S.IsChecklistGroup = 1 AND
                                    UNION ALL
                                    SELECT 
                                        Per.[EmployeeId]
	                                    ,CONCAT(GI.LastName, ' ', GI.NameExtension,', ', GI.FirstName,' ', GI.MiddleName) AS EmployeeName
                                        ,S.Id As SectionId
                                        ,S.GroupDescription As Section
                                        ,S.IsChecklistGroup
                                        ,S.BranchId
                                        ,S.AttendanceGroupId
	                                    ,Pos.PositionName As Position
	                                    ,AD.Id
	                                    ,AD.DateOfUpdate
	                                    ,AD.AMStatus
										,A.Symbol As AMStatusStr
	                                    ,AD.PMStatus
										,P.Symbol As PMStatusStr
	                                    ,AD.AttendanceRemarks
                                    FROM [FGCIJOROSystemDB].[dbo].[Personnels] AS Per
                                    LEFT JOIN Sections As S ON S.Id = Per.SectionId
									LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = Per.EmployeeId
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS GI ON GI.Id = EI.GeneralInformationsId                                    
                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions As Pos ON Pos.Id = EI.PositionsId
                                    LEFT JOIN ActualAdvanceDetails AS AD ON AD.EmployeeId = Per.EmployeeId
									LEFT JOIN AttendanceStatus AS A ON A.Id = AMStatus
									LEFT JOIN AttendanceStatus AS P ON P.Id = PMStatus
                                    WHERE AD.DateOfUpdate = convert(varchar, @DateofUpdate, 101)) T " + whereQuery +
                                    @" ORDER BY EmployeeName";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();  
                }
                Lists = connection.Query<clsActualAdvanceDetails>(query, new { DateofUpdate = DateofUpdate }).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

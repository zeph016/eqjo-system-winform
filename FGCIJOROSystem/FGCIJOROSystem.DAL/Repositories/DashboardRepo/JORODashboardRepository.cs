using FGCIJOROSystem.Domain.Dashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.DashboardRepo
{
    public class JORODashboardRepository
    {
        public List<clsJORODashboard> SearchBy(String whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJORODashboard> List = new List<clsJORODashboard>();
                String query = @"SELECT * FROM (
                                SELECT 
                                        Name As BranchName
                                        ,BranchId
	                                    ,0 As ReferenceType
	                                    ,JONo As ReferenceNo
                                        ,JODate As Date
                                        ,Status
                                        ,RefYear
                                    FROM JOs
                                        LEFT JOIN Branches ON Branches.Id = JOs.BranchId
                                UNION ALL
                                SELECT 
                                        Name As BranchName
                                        ,BranchId
	                                    ,1 As ReferenceType
	                                    ,RONo As ReferenceNo
                                        ,RODate As Date
                                        ,Status
                                        ,RefYear
                                    FROM ROs
                                        LEFT JOIN Branches ON Branches.Id = ROs.BranchId) T " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJORODashboard>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

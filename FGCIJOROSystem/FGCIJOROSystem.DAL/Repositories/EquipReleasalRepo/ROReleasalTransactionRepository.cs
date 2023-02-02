using FGCIJOROSystem.Domain.EquipmentReleasal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo
{
    public class ROReleasalTransactionRepository : IRepository<clsROReleasalTransaction>
    {

        public void Add(clsROReleasalTransaction obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ROReleasalTransaction]
                                   ([ERNo]
                                   ,BranchId
                                   ,[DateReleased]
                                   ,[UserId]
                                   ,[AprroverId])
                             VALUES
                                   ((SELECT CASE WHEN EXISTS(SELECT RT.ERNo FROM [dbo].ROReleasalTransaction As RT WHERE RT.BranchId = @BranchId) THEN (CASE WHEN (SELECT MAX(RT.ERNo) FROM [dbo].ROReleasalTransaction As RT) = 0 OR (SELECT MAX(RT.ERNo) FROM [dbo].ROReleasalTransaction As RT) is null THEN 1 ELSE (SELECT MAX(RT.ERNo) + 1 FROM [dbo].ROReleasalTransaction As RT) END) ELSE 1 END)
                                   ,@BranchId
                                   ,GETDATE()
                                   ,@UserId
                                   ,0)
                                    SELECT SCOPE_IDENTITY() As Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                obj.Id = returnId;
                connection.Close();
                foreach (var x in obj.ListOfROReleased)
                {
                    x.ROTransReleasalId = returnId;
                    if (x.Id == 0)
                    {
                        new ROReleasedRepository().Add(x);
                    }
                    else
                    {
                        new ROReleasedRepository().Update(x);
                    }
                }
            }
        }

        public void Update(clsROReleasalTransaction obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ROReleasalTransaction]
                                   SET 
                                       [UserId] = @UserId
                                      ,[AprroverId] = @AprroverId
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
                foreach (var x in obj.ListOfROReleased)
                {
                    if (x.Id == 0)
                    {
                        new ROReleasedRepository().Add(x);
                    }
                    else
                    {
                        new ROReleasedRepository().Update(x);
                    }
                }
            }
        }

        public void Delete(clsROReleasalTransaction obj)
        {
            throw new NotImplementedException();
        }

        public List<clsROReleasalTransaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsROReleasalTransaction FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsROReleasalTransaction> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsROReleasalTransaction> Lists = new List<clsROReleasalTransaction>();
                String query = @"SELECT DISTINCT RRT.[Id]
                                    ,RRT.[ERNo]
                                    ,RRT.[DateReleased]
                                    ,RRT.[UserId]
                                    ,RRT.[AprroverId]
									,RRT.BranchId
									,Branches.Name As BranchName
                                    ,RR.[ROReleasalTransactionId]
                                FROM [ROReleasalTransaction] RRT
								LEFT JOIN Branches ON Branches.Id = RRT.BranchId
									LEFT JOIN FGCIJOROSystemDB.dbo.ROReleased RR ON RR.ROReleasalTransactionId = RRT.Id
									  LEFT JOIN [dbo].[ROs] AS RO ON RO.Id = RR.ROId
										LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
											LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
													LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
														LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
															LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsROReleasalTransaction>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

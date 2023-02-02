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
    public class JOReleasalTransactionRepository : IRepository<clsJOReleasalTransaction>
    {

        public void Add(clsJOReleasalTransaction obj)
        {
            Int64 returnId = 0;
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[JOReleasalTransaction]
                                   ([ERNo]
                                   ,[DateReleased]
                                   ,[UserId]
                                   ,[ApproverId]
                                   --,Status
                                    ,BranchId)
                             VALUES
                                   ((SELECT CASE WHEN EXISTS(SELECT * FROM [dbo].JOReleasalTransaction As JT WHERE JT.BranchId = @BranchId) THEN (CASE WHEN (SELECT MAX(JT.ERNo) FROM [dbo].JOReleasalTransaction As JT) = 0 OR (SELECT MAX(JT.ERNo) FROM [dbo].JOReleasalTransaction As JT) is null THEN 1 ELSE (SELECT MAX(JT.ERNo) + 1 FROM [dbo].JOReleasalTransaction As JT) END)
                                     ELSE 1 END)
                                   ,GETDATE()
                                   ,0
                                   ,0
                                   --,@Status
                                    ,@BranchId)
                                    SELECT SCOPE_IDENTITY() As Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                obj.Id = returnId;
                connection.Close();
               
            }
            foreach (var x in obj.ListOfJOReleased)
            {
                x.JOTransReleasalId = returnId;
                if (x.Id == 0)
                {
                    new JOReleasedRepository().Add(x);
                }
                else
                {
                    new JOReleasedRepository().Update(x);
                }
            }
        }

        public void Update(clsJOReleasalTransaction obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JOReleasalTransaction]
                                   SET 
                                       [UserId] = @UserId
                                      ,[ApproverId] = @ApproverId
                                      --,Status = @StatusId
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
            foreach (var x in obj.ListOfJOReleased)
            {
                if (x.Id == 0)
                {
                    new JOReleasedRepository().Add(x);
                }
                else
                {
                    new JOReleasedRepository().Update(x);
                }
            }
        }

        public void Delete(clsJOReleasalTransaction obj)
        {
            throw new NotImplementedException();
        }

        public List<clsJOReleasalTransaction> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOReleasalTransaction> Lists = new List<clsJOReleasalTransaction>();
                String query = @"SELECT JOReleasalTransaction.[Id]
                                    ,JOReleasalTransaction.ERNo
                                    ,JOReleasalTransaction.DateReleased
                                    ,JOReleasalTransaction.UserId
                                    ,JOReleasalTransaction.ApproverId
                                    ,JOReleasalTransaction.BranchId
                                    ,Branches.Name As BranchName
                                FROM JOReleasalTransaction
                                LEFT JOIN Branches On Branches.Id = JOReleasalTransaction.BranchId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsJOReleasalTransaction>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsJOReleasalTransaction FindByID(long id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsJOReleasalTransaction Lists = new clsJOReleasalTransaction();
                String query = @"SELECT JOReleasalTransaction.[Id]
                                    ,JOReleasalTransaction.ERNo
                                    ,JOReleasalTransaction.DateReleased
                                    ,JOReleasalTransaction.UserId
                                    ,JOReleasalTransaction.AprroverId
                                    ,JOReleasalTransaction.BranchId
                                    ,Branches.Name As BranchName
                                FROM JOReleasalTransaction
                                LEFT JOIN Branches On Branches.Id = JOReleasalTransaction.BranchId WHERE JOReleasalTransaction.Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsJOReleasalTransaction>(query, new { Id = id}).FirstOrDefault();
                connection.Close();
                return Lists;
            }
        }

        public List<clsJOReleasalTransaction> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOReleasalTransaction> Lists = new List<clsJOReleasalTransaction>();
                String query = @"SELECT DISTINCT JRT.[Id]
                                    ,JRT.ERNo
                                    ,JRT.DateReleased
                                    ,JRT.UserId
                                    ,JRT.ApproverId
                                    ,JRT.BranchId
                                    ,B.Name As BranchName
                                      ,JR.[JOReleasalTransactionId]
                                FROM JOReleasalTransaction JRT
                                LEFT JOIN Branches B On B.Id = JRT.BranchId
									LEFT JOIN FGCIJOROSystemDB.dbo.JOReleased JR ON JR.JOReleasalTransactionId = JRT.Id
									  LEFT JOIN [dbo].[JOs] AS JO ON JO.Id = JR.JOId
										LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
											LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
												LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
													LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
														LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
															LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsJOReleasalTransaction>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}

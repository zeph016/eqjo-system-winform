using FGCIJOROSystem.Domain.JobOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
namespace FGCIJOROSystem.DAL.Repositories.JORepo
{
    public class JODetailTransactionLogRepository : IRepository<clsJODetailTransactionLogs>
    {

        public void Add(clsJODetailTransactionLogs obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[JODetailTransactionLogs]
                                       ([JODetailId]
                                       ,[JOTransLogId]
                                       ,[JOId]
                                       ,[TariffId]
                                       ,[Amount]
                                       ,[Price]
                                       ,[EffectivityDate]
                                       ,[TargetDate]
                                       ,[WorkTimeSpan]
                                       ,[NoOfMechanics]
                                       ,[Remarks]
                                       ,[Tag]
                                       ,[StatusId]
                                       ,[IsActive])
                                 VALUES
                                       (@JODetailId
                                       ,@JOTransLogId
                                       ,@JOId
                                       ,@TariffId
                                       ,@Amount
                                       ,@Price
                                       ,@EffectivityDate
                                       ,@TargetDate
                                       ,@WorkTimeSpan
                                       ,@NoOfMechanics
                                       ,@Remarks
                                       ,@Tag
                                       ,@StatusId
                                       ,@IsActive);";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsJODetailTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(clsJODetailTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public List<clsJODetailTransactionLogs> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsJODetailTransactionLogs FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsJODetailTransactionLogs> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJODetailTransactionLogs> List = new List<clsJODetailTransactionLogs>();
                String query = @"SELECT JD.[Id]
                                      ,JD.[JOId]
                                      ,JD.[TariffId]
	                                  ,Tariff.[WorkDescription]
	                                  ,S.GroupDescription As Section
	                                  ,JC.Name As JobCategory
	                                  ,JT.Name As JobType
	                                  ,PU.UnitName
                                      ,JD.[Amount]
                                      ,JD.[Price]
                                      ,JD.[EffectivityDate]
                                      ,JD.[TargetDate]
                                      ,JD.[WorkTimeSpan]
                                      ,JD.[NoOfMechanics]
                                      ,JD.[Remarks]
                                      ,JD.[Tag]
                                      ,JD.[StatusId]
	                                  ,STATUS.NAME As StatusName
                                  FROM [dbo].JODetailTransactionLogs AS JD
                                  LEFT JOIN Tariff ON Tariff.Id = JD.TariffId
                                  LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
                                  LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
                                  LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
                                  LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
                                  LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
                                  LEFT JOIN Status ON STATUS.Id = JD.StatusId " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJODetailTransactionLogs>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

using FGCIJOROSystem.Domain.JobOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FGCIJOROSystem.Domain.RescueOrder;
namespace FGCIJOROSystem.DAL.Repositories.RORepo
{
    public class RODetailTransactionLogRepository : IRepository<clsRODetailTransactionLogs>
    {
        public void Add(clsRODetailTransactionLogs obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[RODetailTransactionLogs]
                                       ([RODetailId]
                                       ,[ROTransLogId]
                                       ,[ROId]
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
                                       (@RODetailId
                                       ,@ROTransLogId
                                       ,@ROId
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

        public void Update(clsRODetailTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(clsRODetailTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public List<clsRODetailTransactionLogs> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsRODetailTransactionLogs FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsRODetailTransactionLogs> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsRODetailTransactionLogs> List = new List<clsRODetailTransactionLogs>();
                String query = @"SELECT RD.[Id]
                                      ,RD.[ROId]
                                      ,RD.[TariffId]
	                                  ,Tariff.[WorkDescription]
	                                  ,S.GroupDescription As Section
	                                  ,JC.Name As JobCategory
	                                  ,JT.Name As JobType
	                                  ,PU.UnitName
                                      ,RD.[Amount]
                                      ,RD.[Price]
                                      ,RD.[EffectivityDate]
                                      ,RD.[TargetDate]
                                      ,RD.[WorkTimeSpan]
                                      ,RD.[NoOfMechanics]
                                      ,RD.[Remarks]
                                      ,RD.[Tag]
                                      ,RD.[StatusId]
	                                  ,STATUS.NAME As StatusName
                                  FROM [dbo].[RODetailTransactionLogs] AS RD
                                  LEFT JOIN Tariff ON Tariff.Id = RD.TariffId
                                  LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
                                  LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
                                  LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
                                  LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
                                  LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
                                  LEFT JOIN Status ON STATUS.Id = RD.StatusId " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsRODetailTransactionLogs>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

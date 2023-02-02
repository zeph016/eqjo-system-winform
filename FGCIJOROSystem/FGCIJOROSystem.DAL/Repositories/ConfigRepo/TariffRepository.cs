using FGCIJOROSystem.Domain.Configurations.Tariff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
	public class TariffRepository : IRepository<clsTariff>
	{
		public void Add(clsTariff obj)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				String query = @"INSERT INTO [dbo].[Tariff]
									   ([Name]
									   ,[SectionId]
									   ,[JobCategoryId]
									   ,[JobTypeId]
									   ,[WorkDescription]
									   ,[NoOfMechanics]
									   ,[Price]
									   ,[UnitId]
									   ,[WorkTimeSpan]
									   ,[Active])
								 VALUES
									   (@Name
									   ,@SectionId
									   ,@JobCategoryId
									   ,@JobTypeId
									   ,@WorkDescription
									   ,@NoOfMechanics
									   ,@Price
									   ,@UnitId
									   ,@WorkTimeSpan
									   ,@Active)";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				connection.Execute(query, obj);
				connection.Close();
			}
		}

		public void Update(clsTariff obj)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				String query = @"UPDATE [dbo].[Tariff]
										   SET [Name] = @Name
											  ,[SectionId] = @SectionId
											  ,[JobCategoryId] = @JobCategoryId
											  ,[JobTypeId] = @JobTypeId
											  ,[WorkDescription] = @WorkDescription
											  ,[NoOfMechanics] = @NoOfMechanics
											  ,[Price] = @Price
											  ,[UnitId] = @UnitId
											  ,[WorkTimeSpan] = @WorkTimeSpan
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

		public void Delete(clsTariff obj)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				String query = @"UPDATE [dbo].[Tariff]
										   SET [Active] ='0'
										WHERE Id = @Id";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				connection.Execute(query, obj);
				connection.Close();
			}
		}

        public List<clsTariff> GetAll()
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				List<clsTariff> Lists = new List<clsTariff>();
				String query = @"SELECT T.[Id]
									  ,T.[Name]
									  ,T.[SectionId]
									  ,S.[GroupDescription] As SectionName
									  ,T.[JobCategoryId]
									  ,JC.[Name] as JobCategoryName
									  ,T.[JobTypeId]
									  ,JT.Name As JobTypeName
									  ,T.[WorkDescription]
									  ,T.[NoOfMechanics]
									  ,T.[Price]
									  ,T.[UnitId]
									  ,U.UnitId as ProdUnitId
									  ,ProdUnit.UnitName
									  ,T.[WorkTimeSpan]
									  ,T.[Active]
								  FROM [dbo].[Tariff] T
								LEFT JOIN [dbo].[JobCategories] JC on JC.Id = T.JobCategoryId
								LEFT JOIN dbo.JobTypes JT on JT.Id = T.JobTypeId
								LEFT JOIN dbo.Sections S on S.Id = T.SectionId
								LEFT JOIN dbo.Units U on U.Id = T.UnitId
								LEFT JOIN FGCIProductMasterlistDB.dbo.Units ProdUnit on ProdUnit.Id = U.UnitId WHERE T.Active = 1";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Lists = connection.Query<clsTariff>(query).ToList();
				connection.Close();
				return Lists;
			}
		}

		public clsTariff FindByID(Int64 id)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				clsTariff Item = new clsTariff();
				String query = @"";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Item = connection.Query<clsTariff>(query).FirstOrDefault();
				connection.Close();
				return Item;
			}
		}

		public List<clsTariff> SearchBy(string whereQuery)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				List<clsTariff> Lists = new List<clsTariff>();
                String query = @"SELECT T.[Id]
									  ,T.[Name]
									  ,T.[SectionId]
									  ,S.[GroupDescription] As SectionName
									  ,T.[JobCategoryId]
									  ,JC.[Name] as JobCategoryName
									  ,T.[JobTypeId]
									  ,JT.[Name] as JobTypeName
									  ,T.[WorkDescription]
									  ,T.[NoOfMechanics]
									  ,T.[Price]
									  ,T.[UnitId]
									  ,U.UnitId as ProdUnitId
									  ,ProdUnit.UnitName
									  ,T.[WorkTimeSpan]
									  ,T.[Active]
								  FROM [dbo].[Tariff] T
								LEFT JOIN [dbo].[JobCategories] JC on JC.Id = T.JobCategoryId
								LEFT JOIN dbo.JobTypes JT on JT.Id = T.JobTypeId
								LEFT JOIN dbo.Sections S on S.Id = T.SectionId
								LEFT JOIN dbo.Units U on U.Id = T.UnitId
								LEFT JOIN FGCIProductMasterlistDB.dbo.Units ProdUnit on ProdUnit.Id = U.UnitId " + whereQuery;
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Lists = connection.Query<clsTariff>(query).ToList();
				connection.Close();
				return Lists;
			}
		}
        public clsTariff FindDuplicate(string SectionName, string JobCategoryName, string JobTypeName, string WorkDescription)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				clsTariff item = new clsTariff();
				String query = @"SELECT T.[Id]
									  ,T.[Name]
									  ,T.[SectionId]
									  ,S.[SectionName]
									  ,T.[JobCategoryId]
									  ,JC.[Name] as JobCategoryName
									  ,T.[JobTypeId]
									  ,JT.[Name] as JobTypeName
									  ,T.[WorkDescription]
									  ,T.[NoOfMechanics]
									  ,T.[Price]
									  ,T.[UnitId]
									  ,U.UnitId as ProdUnitId
									  ,ProdUnit.UnitName
									  ,T.[WorkTimeSpan]
									  ,T.[Active]
								  FROM [dbo].[Tariff] T
								LEFT JOIN [dbo].[JobCategories] JC on JC.Id = T.JobCategoryId
								LEFT JOIN dbo.JobTypes JT on JT.Id = T.JobTypeId
								LEFT JOIN dbo.Sections S on S.Id = T.SectionId
								LEFT JOIN dbo.Units U on U.Id = T.UnitId
								LEFT JOIN FGCIProductMasterlistDB.dbo.Units ProdUnit on ProdUnit.Id = U.UnitId
									WHERE S.[SectionName] = @SectionName AND JC.Name = @JobCategoryName AND JT.Name = @JobTypeName AND WorkDescription = @WorkDescription";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
                item = connection.Query<clsTariff>(query, new { SectionName = SectionName, JobCategoryName = JobCategoryName, JobTypeName = JobTypeName, WorkDescription = WorkDescription }).FirstOrDefault();
				connection.Close();
				return item;
			}
		}
        public List<clsTariff> GenerateByTariff(string sectionIds = "")
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsTariff> Lists = new List<clsTariff>();
                String query = @"SELECT T.[Id]
									  ,T.[Name]
									  ,T.[SectionId]
									  ,S.[GroupDescription] As SectionName
									  ,T.[JobCategoryId]
									  ,JC.[Name] as JobCategoryName
									  ,T.[JobTypeId]
									  ,JT.Name As JobTypeName
									  ,T.[WorkDescription]
									  ,T.[NoOfMechanics]
									  ,T.[Price]
									  ,T.[UnitId]
									  ,U.UnitId as ProdUnitId
									  ,ProdUnit.UnitName
									  ,T.[WorkTimeSpan]
									  ,T.[Active]
								  FROM [dbo].[Tariff] T
								LEFT JOIN [dbo].[JobCategories] JC on JC.Id = T.JobCategoryId
								LEFT JOIN dbo.JobTypes JT on JT.Id = T.JobTypeId
								LEFT JOIN dbo.Sections S on S.Id = T.SectionId
								LEFT JOIN dbo.Units U on U.Id = T.UnitId
								LEFT JOIN FGCIProductMasterlistDB.dbo.Units ProdUnit on ProdUnit.Id = U.UnitId ";
                query += " Where T.SectionId IN ( " + sectionIds + " )";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsTariff>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public List<clsTariff> GenerateTariffsByJobCategory(string jobCategoryIds = "")
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsTariff> Lists = new List<clsTariff>();
                String query = @"SELECT 
									  --T.[Name]
									  --,T.[SectionId]
									  S.[GroupDescription] As SectionName
									  ,T.[JobCategoryId]
                                      ,JC.[Id] as JobCategoryId
									  ,JC.[Name] as JobCategoryName
									  ,T.[JobTypeId]
									  ,JT.Name As JobTypeName
									  ,T.[WorkDescription]
									  ,T.[NoOfMechanics]
									  ,T.[Price]
									  ,T.[UnitId]
									  ,U.UnitId as ProdUnitId
									  ,ProdUnit.UnitName
									  ,T.[WorkTimeSpan]
									  ,T.[Active]
								  FROM [dbo].[Tariff] T
								LEFT JOIN [dbo].[JobCategories] JC on JC.Id = T.JobCategoryId
								LEFT JOIN dbo.JobTypes JT on JT.Id = T.JobTypeId
								LEFT JOIN dbo.Sections S on S.Id = T.SectionId
								LEFT JOIN dbo.Units U on U.Id = T.UnitId
								LEFT JOIN FGCIProductMasterlistDB.dbo.Units ProdUnit on ProdUnit.Id = U.UnitId ";
                query += " Where T.[Active] = 1 AND JC.Id IN ( " + jobCategoryIds + " )";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsTariff>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public List<clsTariff> GenerateTariffsByJobType(string jobTypeIds = "")
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsTariff> Lists = new List<clsTariff>();
                String query = @"SELECT 
									  --T.[Name]
									  --,T.[SectionId]
									  S.[GroupDescription] As SectionName
									  ,T.[JobCategoryId]
                                      ,JC.[Id] as JobCategoryId
									  ,JC.[Name] as JobCategoryName
									  ,T.[JobTypeId]
									  ,JT.Name As JobTypeName
									  ,T.[WorkDescription]
									  ,T.[NoOfMechanics]
									  ,T.[Price]
									  ,T.[UnitId]
									  ,U.UnitId as ProdUnitId
									  ,ProdUnit.UnitName
									  ,T.[WorkTimeSpan]
									  ,T.[Active]
								  FROM [dbo].[Tariff] T
								LEFT JOIN [dbo].[JobCategories] JC on JC.Id = T.JobCategoryId
								LEFT JOIN dbo.JobTypes JT on JT.Id = T.JobTypeId
								LEFT JOIN dbo.Sections S on S.Id = T.SectionId
								LEFT JOIN dbo.Units U on U.Id = T.UnitId
								LEFT JOIN FGCIProductMasterlistDB.dbo.Units ProdUnit on ProdUnit.Id = U.UnitId ";
                query += " Where T.[Active] = 1 AND T.[JobCategoryId] IN ( " + jobTypeIds + " )";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsTariff>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

		public List<clsTariffEqJO> GenerateTariffsByEquipmentJO(string equipmentIds, bool isDate, DateTime dateFrom, DateTime dateTo)
        {
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				List<clsTariffEqJO> Lists = new List<clsTariffEqJO>();
				String query = @"SELECT        JO.EquipmentId, (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName WHEN JO.ItemType = 1 THEN SD.ToolName WHEN JO.ItemType = 2 THEN OT.[Name] ELSE '' END) AS EquipmentName, 
                         (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName WHEN JO.ItemType = 1 THEN 'STE' WHEN JO.ItemType = 2 THEN 'SPARE PARTS' ELSE '' END) AS EquipmentCode, dbo.JODetails.Amount, JO.JONo, 
                         dbo.Status.Name AS StatusName, dbo.JODetails.StatusId, JO.JODate, dbo.Tariff.WorkDescription
						FROM dbo.JOs AS JO INNER JOIN
                         dbo.JODetails ON JO.Id = dbo.JODetails.JOId INNER JOIN
                         dbo.Status ON dbo.JODetails.StatusId = dbo.Status.Id INNER JOIN
                         dbo.Tariff ON dbo.JODetails.TariffId = dbo.Tariff.Id LEFT OUTER JOIN
                         FGCIAccountingPPEMonitoringDB.dbo.DescriptionAndStatus AS EQ ON EQ.Id = JO.EquipmentId LEFT OUTER JOIN
                         FGCIAccountingPPEMonitoringDB.dbo.PPETypes AS ET ON EQ.PPETypeId = ET.Id LEFT OUTER JOIN
                         FGCIAccountingPPEMonitoringDB.dbo.PPEClasses AS EC ON EQ.PPEClassId = EC.Id LEFT OUTER JOIN
                         FGCIAccountingPPEMonitoringDB.dbo.RegistrationRenewals AS ER ON EQ.Id = ER.DescriptionAndStatusId LEFT OUTER JOIN
                         FGCILInventoryDB.dbo.StockDetails AS SD ON SD.Id = JO.EquipmentId LEFT OUTER JOIN
                         dbo.OtherEquipments AS OT ON OT.Id = JO.EquipmentId WHERE JO.EquipmentId IN (" + equipmentIds + ")";
				if (isDate)
					query += @" AND JO.JODate BETWEEN ('" + dateFrom  + "') AND ('" + dateTo  + "')";
				if (connection.State == ConnectionState.Closed)
					connection.Open();
				Lists = connection.Query<clsTariffEqJO>(query).ToList();
				connection.Close();
				return Lists;
			}
        }

		public List<clsTariffEqRO> GenerateTariffsByEquipmentRO(string equipmentIds, bool isDate, DateTime dateFrom, DateTime dateTo)
        {
			using (IDbConnection connection = DbConnection.JOROConnection)
            {
				List<clsTariffEqRO> Lists = new List<clsTariffEqRO>();
				String query = @"SELECT        RO.EquipmentId, (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName WHEN RO.ItemType = 1 THEN SD.ToolName WHEN RO.ItemType = 2 THEN OT.[Name] ELSE '' END) AS EquipmentName, 
                         (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName WHEN RO.ItemType = 1 THEN 'STE' WHEN RO.ItemType = 2 THEN 'SPARE PARTS' ELSE '' END) AS EquipmentCode, dbo.RODetails.Amount, RO.RONo, 
                         dbo.Status.Name AS StatusName, dbo.RODetails.StatusId, RO.RODate, dbo.Tariff.WorkDescription
FROM					dbo.ROs AS RO INNER JOIN
                         dbo.RODetails ON RO.Id = dbo.RODetails.ROId INNER JOIN
                         dbo.Status ON dbo.RODetails.StatusId = dbo.Status.Id INNER JOIN
                         dbo.Tariff ON dbo.RODetails.TariffId = dbo.Tariff.Id LEFT OUTER JOIN
                         FGCIAccountingPPEMonitoringDB.dbo.DescriptionAndStatus AS EQ ON EQ.Id = RO.EquipmentId LEFT OUTER JOIN
                         FGCIAccountingPPEMonitoringDB.dbo.PPETypes AS ET ON EQ.PPETypeId = ET.Id LEFT OUTER JOIN
                         FGCIAccountingPPEMonitoringDB.dbo.PPEClasses AS EC ON EQ.PPEClassId = EC.Id LEFT OUTER JOIN
                         FGCIAccountingPPEMonitoringDB.dbo.RegistrationRenewals AS ER ON EQ.Id = ER.DescriptionAndStatusId LEFT OUTER JOIN
                         FGCILInventoryDB.dbo.StockDetails AS SD ON SD.Id = RO.EquipmentId LEFT OUTER JOIN
                         dbo.OtherEquipments AS OT ON OT.Id = RO.EquipmentId WHERE RO.EquipmentId IN (" + equipmentIds + ")";
				if (isDate)
					query += @" AND RO.RODate BETWEEN ('" + dateFrom + "') AND ('" + dateTo + "')";
				if (connection.State == ConnectionState.Closed) ;
					connection.Open();
				Lists = connection.Query<clsTariffEqRO>(query).ToList();
				connection.Close();
				return Lists;
            }
        }
	}
}

using FGCIJOROSystem.Domain.Configurations.UnitsJORO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
	public class UnitsJORORepository : IRepository<clsUnitJORO>
	{
		public void Add(clsUnitJORO obj)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				String query = @"INSERT INTO [dbo].[Units]
												   ([UnitId]
												   ,[MinutesValue]
												   ,[Active])
											 VALUES
												   (@UnitId
												   ,@MinutesValue
												   ,@IsActive)";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				connection.Execute(query, obj);
				connection.Close();
			}
		}

		public void Update(clsUnitJORO obj)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				String query = @"UPDATE [dbo].[Units]
									   SET [UnitId] = @UnitId
										  ,[MinutesValue] = @MinutesValue
										  ,[Active] = @IsActive
									 WHERE Id = @Id";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				connection.Execute(query, obj);
				connection.Close();
			}
		}

		public void Delete(clsUnitJORO obj)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
                String query = @"UPDATE [dbo].[Units]
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

		public List<clsUnitJORO> GetAll()
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				List<clsUnitJORO> Lists = new List<clsUnitJORO>();
				String query = @"SELECT U.[Id]
										  ,U.[UnitId]
										  ,ProdUnit.[UnitName]
										  ,U.[MinutesValue]
										  ,U.[Active] as IsActive
									  FROM [dbo].[Units] U
									LEFT JOIN FGCIProductMasterlistDB.dbo.Units ProdUnit on ProdUnit.Id =  U.UnitId
								   ";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Lists = connection.Query<clsUnitJORO>(query).ToList();
				connection.Close();
				return Lists;
                //WHERE Active = 0
			}
		}

        public clsUnitJORO FindByID(Int64 id)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				clsUnitJORO Item = new clsUnitJORO();
				String query = @"";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Item = connection.Query<clsUnitJORO>(query).FirstOrDefault();
				connection.Close();
				return Item;
			}
		}

		public List<clsUnitJORO> SearchBy(string whereQuery)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				List<clsUnitJORO> Lists = new List<clsUnitJORO>();
				String query = @"";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Lists = connection.Query<clsUnitJORO>(query).ToList();
				connection.Close();
				return Lists;
			}
		}
		public List<clsUnitJORO> GetAllActive()
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				List<clsUnitJORO> Lists = new List<clsUnitJORO>();
				String query = @"SELECT U.[Id]
										  ,U.[UnitId]
										  ,ProdUnit.[UnitName]
										  ,U.[MinutesValue]
										  ,U.[Active] as IsActive
									  FROM [dbo].[Units] U
									LEFT JOIN FGCIProductMasterlistDB.dbo.Units ProdUnit on ProdUnit.Id =  U.UnitId
								   WHERE Active = 1";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Lists = connection.Query<clsUnitJORO>(query).ToList();
				connection.Close();
				return Lists;
			}
		}
		public clsUnitJORO FindDuplicate(Int64 UnitId)
		{
			using (IDbConnection connection = DbConnection.JOROConnection)
			{
				clsUnitJORO Item = new clsUnitJORO();
				String query = @"SELECT U.[Id]
										  ,U.[UnitId]
										  ,ProdUnit.[UnitName]
										  ,U.[MinutesValue]
										  ,U.[Active] as IsActive
									  FROM [dbo].[Units] U
									LEFT JOIN FGCIProductMasterlistDB.dbo.Units ProdUnit on ProdUnit.Id =  U.UnitId
								   WHERE U.[UnitId] = @UnitId";
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				Item = connection.Query<clsUnitJORO>(query, new { UnitId = UnitId }).FirstOrDefault();
				connection.Close();
				return Item;
			}
		}
	}
}

using FGCIJOROSystem.Domain.RescueOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.RORepo
{
    public class RODetailRepository : IRepository<clsRODetails>
    {

        public void Add(clsRODetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"
                            INSERT INTO [dbo].[RODetails]
                                ([ROId]
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
                                 ,IsActive)
                            VALUES
                                (@ROId
                                ,@TariffId
                                ,@Amount
                                ,@Price
                                ,@EffectivityDate
                                ,@TargetDate
                                ,@WorkTimeSpan
                                ,@NoOfMechanics
                                ,@Remarks
                                ,@Tag
                                ,@StatusId,@IsActive)
                        SELECT SCOPE_IDENTITY() As Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                obj.Id = returnId;
                connection.Close();               
            }
        }

        public void Update(clsRODetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"
                               UPDATE [dbo].[RODetails]
                               SET [ROId] = @ROId
                                  ,[TariffId] = @TariffId
                                  ,[Amount] = @Amount
                                  ,[Price] = @Price
                                  ,[EffectivityDate] = @EffectivityDate
                                  ,[TargetDate] = @TargetDate
                                  ,[WorkTimeSpan] = @WorkTimeSpan
                                  ,[NoOfMechanics] = @NoOfMechanics
                                  ,[Remarks] = @Remarks
                                  ,[Tag] = @Tag
                                  ,[StatusId] = @StatusId
                                  ,[IsActive] = @IsActive
                             WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void UpdateDateApproved(DateTime dateNow, long roDetailId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[RODetails]
                                   SET      [DateApproved] = @date
                                   WHERE    [Id] = @roDetailId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, new { roDetailId = roDetailId, date = dateNow });
                connection.Close();
            }
        }

        public void Delete(clsRODetails obj)
        {
            throw new NotImplementedException();
        }

        public List<clsRODetails> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsRODetails> List = new List<clsRODetails>();
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
                                      ,DateOfUpdate = (SELECT Max(AD.DateOfUpdate) FROM ActualAdvanceRefNos As AR 
					                       LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
					                       WHERE AR.JODetailId = RD.Id AND AR.ReferenceType = 1 AND AR.Type = 0)
	                                  ,WorkPercentage = (SELECT AVG(AR.WorkPercentage) FROM ActualAdvanceRefNos As AR 
					                                       LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
					                                       WHERE AR.JODetailId = RD.Id AND AR.ReferenceType = 1 AND AR.Type = 0 AND AD.DateOfUpdate = (SELECT MAX(A.DateOfUpdate) FROM ActualAdvanceDetails As A WHERE A.EmployeeId = AD.EmployeeId))
                                      ,(SELECT STUFF((SELECT ', ' +  
                                       CONCAT(Substring(GI.FirstName,1,1), '.',GI.LastName)
                                      FROM [dbo].[ROWorkAssignment] As RW
                                      Left join  FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = RW.EmployeeId
                                      Left Join FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON GI.Id = EI.GeneralInformationsId 
                                      WHERE RW.RODetailId = RD.Id FOR XML PATH('')),1,1,'')) AS AssignedMechanics
                                      ,(SELECT MAX(RA.StatusDateTime) FROM ROAuditTrail AS RA
									    LEFT JOIN Status AS S ON S.Id = RA.StatusId WHERE RA.RODetailId = RD.Id AND S.IsApproval = 1) As DateApproved
                                  FROM [dbo].[RODetails] AS RD
                                  LEFT JOIN Tariff ON Tariff.Id = RD.TariffId
                                  LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
                                  LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
                                  LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
                                  LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
                                  LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
                                  LEFT JOIN Status ON STATUS.Id = RD.StatusId ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsRODetails>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public clsRODetails FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsRODetails> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsRODetails> List = new List<clsRODetails>();
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
									  ,RD.IsActive
                                      ,RD.[DateApproved]
	                                  ,STATUS.NAME As StatusName
                                      ,DateOfUpdate = (SELECT Max(AD.DateOfUpdate) FROM ActualAdvanceRefNos As AR 
					                       LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
					                       WHERE AR.JODetailId = RD.Id AND AR.ReferenceType = 1 AND AR.Type = 0)
	                                  ,WorkPercentage = (SELECT AVG(AR.WorkPercentage) FROM ActualAdvanceRefNos As AR 
					                                       LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
					                                       WHERE AR.JODetailId = RD.Id AND AR.ReferenceType = 1 AND AR.Type = 0 AND AD.DateOfUpdate = (SELECT MAX(A.DateOfUpdate) FROM ActualAdvanceDetails As A WHERE A.EmployeeId = AD.EmployeeId))
                                      ,(SELECT STUFF((SELECT ', ' +  
                                       CONCAT(Substring(GI.FirstName,1,1), '.',GI.LastName)
                                      FROM [dbo].[ROWorkAssignment] As RW
                                      Left join  FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = RW.EmployeeId
                                      Left Join FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON GI.Id = EI.GeneralInformationsId 
                                      WHERE RW.RODetailId = RD.Id FOR XML PATH('')),1,1,'')) AS AssignedMechanics
                                      ,(SELECT MAX(RA.StatusDateTime) FROM ROAuditTrail AS RA
									    LEFT JOIN Status AS S ON S.Id = RA.StatusId WHERE RA.RODetailId = RD.Id AND S.IsApproval = 1) As DateApproved
                                  FROM [dbo].[RODetails] AS RD
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
                List = connection.Query<clsRODetails>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

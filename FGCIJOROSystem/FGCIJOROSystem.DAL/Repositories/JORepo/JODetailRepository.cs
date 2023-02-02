using FGCIJOROSystem.Domain.JobOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Security.Cryptography;

namespace FGCIJOROSystem.DAL.Repositories.JORepo
{
    public class JODetailRepository :IRepository<clsJODetails>
    {
        public Int64 JODetailId { get; set; }
        public void Add(clsJODetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[JODetails]
                                    ([JOId]
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
                                    (@JOId
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
                                    ,@IsActive);
                                SELECT SCOPE_IDENTITY() As Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                obj.Id = returnId;
                JODetailId = returnId;
                connection.Close();
            }
        }

        public void Update(clsJODetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JODetails]
                                   SET [JOId] = @JOId
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
                                      ,IsActive = @IsActive
                                 WHERE Id = @Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void UpdateDateApproved(DateTime dateNow, long joDetailId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JODetails]
                                   SET      [DateApproved] = @date
                                   WHERE    [Id] = @joDetailId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, new { joDetailId = joDetailId, date = dateNow });
                connection.Close();
            }
        }

        public void Delete(clsJODetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JODetails]
                                   SET [StatusId] = @StatusId
                                 WHERE Id = @Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsJODetails> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJODetails> List = new List<clsJODetails>();
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
                                    ,JD.IsActive
	                                ,STATUS.NAME As StatusName
	                                ,DateOfUpdate = (SELECT Max(AD.DateOfUpdate) FROM ActualAdvanceRefNos As AR 
					                                   LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
					                                   WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0)
	                                ,WorkPercentage = (SELECT AVG(AR.WorkPercentage) FROM ActualAdvanceRefNos As AR 
					                                   LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
					                                   WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0 AND AD.DateOfUpdate = (SELECT MAX(A.DateOfUpdate) FROM ActualAdvanceDetails As A WHERE A.EmployeeId = AD.EmployeeId))
                                    ,(SELECT STUFF((SELECT ', ' +  
                                       CONCAT(Substring(GI.FirstName,1,1), '.',GI.LastName)
                                      FROM [dbo].[JOWorkAssignment] As JW
                                      Left join  FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = JW.EmployeeId
                                      Left Join FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON GI.Id = EI.GeneralInformationsId 
                                      WHERE JW.JODetailId = JD.Id FOR XML PATH('')),1,1,'')) AS AssignedMechanics
                                FROM [dbo].JODetails AS JD
                                LEFT JOIN Tariff ON Tariff.Id = JD.TariffId
                                LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
                                LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
                                LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
                                LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
                                LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
                                LEFT JOIN Status ON STATUS.Id = JD.StatusId ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJODetails>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public clsJODetails FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsJODetails> SearchBy(string whereQuery)
        {            
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJODetails> List = new List<clsJODetails>();
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
                                    ,JD.IsActive
                                    ,JD.DateApproved
	                                ,STATUS.NAME As StatusName
	                                ,DateOfUpdate = (SELECT Max(AD.DateOfUpdate) FROM ActualAdvanceRefNos As AR 
					                                   LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
					                                   WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0)
	                                ,WorkPercentage = (SELECT AVG(AR.WorkPercentage) FROM ActualAdvanceRefNos As AR 
					                                   LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
					                                   WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0 AND AD.DateOfUpdate = (SELECT MAX(A.DateOfUpdate) FROM ActualAdvanceDetails As A WHERE A.EmployeeId = AD.EmployeeId))
                                    ,(SELECT STUFF((SELECT ', ' +  
                                       CONCAT(Substring(GI.FirstName,1,1), '.',GI.LastName)
                                      FROM [dbo].[JOWorkAssignment] As JW
                                      Left join  FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = JW.EmployeeId
                                      Left Join FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON GI.Id = EI.GeneralInformationsId 
                                      WHERE JW.JODetailId = JD.Id FOR XML PATH('')),1,1,'')) AS AssignedMechanics
                                    ,(SELECT MAX(JA.StatusDateTime) FROM JOAuditTrail AS JA WHERE JA.JODetailId = JD.Id AND JA.StatusId = 2) As DateApproved
                                FROM [dbo].JODetails AS JD
                                LEFT JOIN Tariff ON Tariff.Id = JD.TariffId
                                LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
                                LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
                                LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
                                LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
                                LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
                                LEFT JOIN Status ON STATUS.Id = JD.StatusId  " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJODetails>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public List<clsJODetails> GetAllByFiltered(string joIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJODetails> List = new List<clsJODetails>();
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
                                    ,JD.IsActive
	                                ,STATUS.NAME As StatusName
	                                ,DateOfUpdate = (SELECT Max(AD.DateOfUpdate) FROM ActualAdvanceRefNos As AR 
					                                   LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
					                                   WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0)
	                                ,WorkPercentage = (SELECT AVG(AR.WorkPercentage) FROM ActualAdvanceRefNos As AR 
					                                   LEFT JOIN ActualAdvanceDetails As AD ON AR.ActualAdvanceId = AD.Id
					                                   WHERE AR.JODetailId = JD.Id AND AR.ReferenceType = 0 AND AR.Type = 0 AND AD.DateOfUpdate = (SELECT MAX(A.DateOfUpdate) FROM ActualAdvanceDetails As A WHERE A.EmployeeId = AD.EmployeeId))
                                    ,(SELECT STUFF((SELECT ', ' +  
                                       CONCAT(Substring(GI.FirstName,1,1), '.',GI.LastName)
                                      FROM [dbo].[JOWorkAssignment] As JW
                                      Left join  FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations AS EI ON EI.Id = JW.EmployeeId
                                      Left Join FGCIHRDMasterlistSystemDB.dbo.GeneralInformations As GI ON GI.Id = EI.GeneralInformationsId 
                                      WHERE JW.JODetailId = JD.Id FOR XML PATH('')),1,1,'')) AS AssignedMechanics
                                FROM [dbo].JODetails AS JD
                                LEFT JOIN Tariff ON Tariff.Id = JD.TariffId
                                LEFT JOIN Sections AS S ON S.Id = Tariff.SectionId
                                LEFT JOIN JobCategories AS JC ON JC.Id = Tariff.JobCategoryId
                                LEFT JOIN JobTypes AS JT ON JT.Id = Tariff.JobTypeId
                                LEFT JOIN Units AS U ON U.Id = Tariff.UnitId
                                LEFT JOIN FGCIProductMasterlistDB.dbo.Units AS PU ON PU.Id = U.UnitId
                                LEFT JOIN Status ON STATUS.Id = JD.StatusId WHERE JD.JOId IN (" + joIds + ")";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJODetails>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

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
    public class JOAuditTrailRepository: IRepository<clsJOAuditTrail>
    {
        public void Add(clsJOAuditTrail obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                string query = @"UPDATE dbo.JODetails SET StatusId = @StatusId, Remarks = @Remarks WHERE Id = @JODetailId;
                                INSERT dbo.JOAuditTrail (JODetailId,StatusId,StatusDateTime,Remarks,UserId)
	                                VALUES (@JODetailId,@StatusId,GetDate(),@Remarks,@UserId)";
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsJOAuditTrail obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(clsJOAuditTrail obj)
        {
            throw new NotImplementedException();
        }

        public List<clsJOAuditTrail> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOAuditTrail> List = new List<clsJOAuditTrail>();
                string query = @"SELECT 
                                    JAT.Id
                                    ,JAT.JODetailId
                                    ,JAT.Remarks
                                    ,JAT.StatusDateTime
                                    ,JAT.StatusId
                                    ,T.WorkDescription
                                    ,S.[Name] As StatusName
                                    ,JAT.UserId
	                                ,UserName = CONCAT(UGI.FirstName,' ', UGI.LastName,' ', UGI.NameExtension)
                                    ,UserPosition = UP.PositionName
                                FROM dbo.JOAuditTrail AS JAT
                                LEFT JOIN JODetails As JD ON JD.Id = JAT.JODetailId
                                LEFT JOIN Tariff AS T ON T.ID = JD.TariffId
                                LEFT JOIN JOs AS JO ON JO.Id = JD.JOId
                                LEFT JOIN Status AS S ON S.ID = JAT.StatusId 
								LEFT JOIN Users As U ON U.Id = JAT.UserId
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations UEI ON U.MLEmployeeId = UEI.Id
							    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] UGI ON UEI.GeneralInformationsId = UGI.Id
							    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions UP ON UEI.PositionsId = UP.Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOAuditTrail>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public clsJOAuditTrail FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsJOAuditTrail> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOAuditTrail> List = new List<clsJOAuditTrail>();
                string query = @"SELECT 
                                    JAT.Id
                                    ,JAT.JODetailId
                                    ,JAT.Remarks
                                    ,JAT.StatusDateTime
                                    ,JAT.StatusId
                                    ,T.WorkDescription
                                    ,S.[Name] As StatusName
                                    ,JAT.UserId
	                                ,UserName = CONCAT(UGI.FirstName,' ', UGI.LastName,' ', UGI.NameExtension)
                                    ,UserPosition = UP.PositionName
                                FROM dbo.JOAuditTrail AS JAT
                                LEFT JOIN JODetails As JD ON JD.Id = JAT.JODetailId
                                LEFT JOIN Tariff AS T ON T.ID = JD.TariffId
                                LEFT JOIN JOs AS JO ON JO.Id = JD.JOId
                                LEFT JOIN Status AS S ON S.ID = JAT.StatusId 
								LEFT JOIN Users As U ON U.Id = JAT.UserId
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations UEI ON U.MLEmployeeId = UEI.Id
							    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] UGI ON UEI.GeneralInformationsId = UGI.Id
							    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions UP ON UEI.PositionsId = UP.Id " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOAuditTrail>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

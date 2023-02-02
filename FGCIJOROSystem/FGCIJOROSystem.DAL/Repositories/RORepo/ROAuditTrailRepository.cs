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
    public class ROAuditTrailRepository : IRepository<clsROAuditTrail>
    {

        public void Add(clsROAuditTrail obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                string query = @"UPDATE dbo.RODetails SET StatusId = @StatusId, Remarks = @Remarks WHERE Id = @RODetailId;
                                INSERT dbo.ROAuditTrail (RODetailId,StatusId,StatusDateTime,Remarks,UsersId)
	                                VALUES (@RODetailId,@StatusId,GetDate(),@Remarks,@UserId)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsROAuditTrail obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(clsROAuditTrail obj)
        {
            throw new NotImplementedException();
        }

        public List<clsROAuditTrail> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsROAuditTrail FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsROAuditTrail> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsROAuditTrail> List = new List<clsROAuditTrail>();
                string query = @"SELECT 
                                    RD.Id
                                    ,RAT.RODetailId
                                    ,RAT.Remarks
                                    ,RAT.StatusDateTime
                                    ,RAT.StatusId
                                    ,T.WorkDescription
                                    ,S.[Name] As StatusName
                                    ,UserName = CONCAT(UGI.FirstName,' ', UGI.LastName,' ', UGI.NameExtension)
                                    ,UserPosition = UP.PositionName
                                FROM dbo.ROAuditTrail AS RAT
                                LEFT JOIN RODetails As RD ON RD.Id = RAT.RODetailId
                                LEFT JOIN Tariff AS T ON T.ID = RD.TariffId
                                LEFT JOIN JOs AS JO ON JO.Id = RD.ROId
                                LEFT JOIN Status AS S ON S.ID = RD.StatusId 
								LEFT JOIN Users As U ON U.Id = RAT.UsersId
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations UEI ON U.MLEmployeeId = UEI.Id
							    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] UGI ON UEI.GeneralInformationsId = UGI.Id
							    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions UP ON UEI.PositionsId = UP.Id " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsROAuditTrail>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}

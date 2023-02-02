using FGCIJOROSystem.Domain.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FGCIJOROSystem.Domain.Configurations.EquipmentType;

namespace FGCIJOROSystem.DAL.Repositories.GlobalRepo
{
    public class EquipmentTypeRepository
    {
        public List<clsEquipmentType> GetAll()
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                List<clsEquipmentType> Lists = new List<clsEquipmentType>();
                String query = @"SELECT PPE.Id AS EquipmentTypeId
                                  ,PPE.PPETypeName
	                              ,Description = ''
	                              ,(SELECT CL.Id 
		                            FROM FGCIJOROSystemDB.dbo.Checklists CL 
		                            WHERE CL.EquipmentTypeId = PPE.Id) as Id
                              FROM [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS PPE";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsEquipmentType>(query).OrderBy(x => x.EquipmentTypeId).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsEquipmentType FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                clsEquipmentType Item = new clsEquipmentType();
                String query = @"";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Item = connection.Query<clsEquipmentType>(query).FirstOrDefault();
                connection.Close();
                return Item;
            }
        }

        public List<clsEquipmentType> SearchBy(string EquipmentTypeId)
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                    List<clsEquipmentType> Lists = new List<clsEquipmentType>();
                    String query = @"SELECT Id, 0 As EquipmentTypeId, '' [Description], PPETypeName FROM FGCIAccountingPPEMonitoringDB.dbo.PPETypes";
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    Lists = connection.Query<clsEquipmentType>(query).OrderBy(x => x.EquipmentTypeId).ToList();
                    connection.Close();
                    return Lists;
                }
            }
    }
   
}

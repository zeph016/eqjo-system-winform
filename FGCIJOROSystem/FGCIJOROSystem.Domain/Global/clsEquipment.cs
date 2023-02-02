using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Global
{
    public class clsEquipment
    {
        public Boolean chkSelect { get; set; }
        public Int64 EquipmentMasterlistId { get; set; }
        public clsEnums.EquipmentType EquipmentType { get; set; }        
        public String EquipmentClass { get; set; }
        public String EquipmentCode { get; set; }
        public String EquipmentName { get; set; }
        public String Location { get; set; }
        public String Driver { get; set; }
        public String PlateNo { get; set; }
        public clsEnums.JOROStatus JOROStatus { get; set; }
        public String JORONos { get; set; }
        public Int64 BranchId { get; set; }
        public String Name { get; set; }
        public clsEquipment()
        {
            EquipmentMasterlistId = 0;
            EquipmentType = clsEnums.EquipmentType.Equipment;
            EquipmentClass = "";
            EquipmentCode = "";
            EquipmentName = "";
            Location = "";
            Driver = "";
            PlateNo = "";
            JOROStatus = clsEnums.JOROStatus.Generated;
            JORONos = "";
        }
        //public clsEquipment(clsEquipment obj)
        //{
        //    this.MemberwiseClone().Equals(obj);
        //}
        //public void copy(clsEquipment obj)
        //{
        //    this.MemberwiseClone().Equals(obj);
        //}
       
        //public Int64 Id { get; set; }
        //public String PPEName { get; set; }
        //public String RegistrationOwner { get; set; }
        //public String PlateNo { get; set; }
        //public String OrigEngineNo { get; set; }
        //public String ActualEngineNo { get; set; }
        //public String DepartmentInCharge { get; set; }
        //public String ConditionStatus { get; set; }
        //public DateTime LastUpdate { get; set; }
        //public String Model { get; set; }
        //public String Make { get; set; }
        //public String YearModel { get; set; }
        //public String DateAcquired { get; set; }
        //public Int64 PPETypeId { get; set; }
        //public Int64 PPEClassId { get; set; }
        //public Int64 CompanyId { get; set; }
        //public Int64 PurchasePrice { get; set; }
        //public String Rate { get; set; }
        //public String Frequency { get; set; }
        //public String EstimatedLife { get; set; }
        //public String MVFileNo { get; set; }
        //public Decimal ResidualValue { get; set; }
        //public Decimal DepreciationPerAnnum { get; set; }
        //public Decimal DepreciatedValue { get; set; }
        //public Decimal BookValue { get; set; }
        //public Int64 EquipmentUseId { get; set; }
        //public String UseDescription { get; set; }
        //public Int64 EquipmentKindId { get; set; }
        //public String KindDescription { get; set; }
        //public Int64 EquipmentStatusId { get; set; }
        //public String Status { get; set; }
        //public Boolean IsNewEquipment { get; set; }
        //public Int64 MasterlistDepartmentId { get; set; }
        //public String DepartmentName { get; set; }

        
    }
}

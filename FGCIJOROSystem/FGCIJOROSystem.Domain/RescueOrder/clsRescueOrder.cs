using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.RescueOrder
{
    public class clsRescueOrder
    {
        public Int64 Id { get; set; }
        public Int64 RONo { get; set; }
        public String RefYear { get; set; }
        public String RONoStr { get { return (Type == clsEnums.TypeOrder.JO ? "JO" : "RO") + RefYear.Substring(RefYear.Length - 2) + RONo.ToString("0000"); } }       
        public DateTime RODate { get; set; }
        public clsEnums.TypeOrder Type { get; set; }
        public Boolean IsJobOut { get; set; }
        public Boolean IsActive { get; set; }
        public String ChecklistNo { get; set; }
        public String Mileage { get; set; } = string.Empty;
        //Customer
        public clsEnums.CustomerCategory CustomerCategory { get; set; }
        public clsEnums.CustomerType CustomerType { get; set; }
        public Int64 CustomerId { get; set; }
        public String CustomerName { get; set; }
        public String CustomerLocation { get; set; }
        public String CustomerRemarks { get; set; }

        //Branch
        public Int64 BranchId { get; set; }
        public String BranchName { get; set; }

        //Equipment
        public clsEnums.EquipmentType ItemType { get; set; }
        public Int64 EquipmentId { get; set; }
        public String EquipmentCode { get; set; }
        public String EquipmentName { get; set; }
        public String EquipmentLocation { get; set; }
        public String EquipmentRemarks { get; set; }

        //Contractor
        public Int64 ContractorId { get; set; }
        public clsEnums.ContractorCategory ContractorCategory { get; set; }
        public clsEnums.ContractorType ContractorType { get; set; }
        public String ContractorName { get; set; }
        public String ContractorSectionHead { get; set; }

        //Service Drivers
        public Int64 DriverId { get; set; }
        public String DriverName { get; set; } 

        //Service Drivers
        public Int64 ServiceDriverId { get; set; }
        public String ServiceDriverName { get; set; }

        //Service Vehicle
        public Int64 ServiceVehicleId { get; set; }
        public String ServiceVehicleName { get; set; }
        //Status
        public clsEnums.JOROStatus Status { get; set; }

        //List of JODetails
        public List<clsRODetails> ListOfRODetails { get; set; }
        //List of Mechanics
        public List<clsROMechanics> ListOfROMechanics { get; set; }
        public List<clsRescueOrder> ListOfDriversAttendance { get; set; }

        //public Int64 TotalDone { get; set; }
        public Int64 PrintCount { get; set; }
        public Int64 UserId { get; set; }
        public String EncoderName { get; set; }
        public String EncoderPosition { get; set; }
        public Int64 ApproverId { get; set; }
        public String ApproverName { get; set; }
        public String ApproverPosition { get; set; }
        //PPEStatus
        public Int64 PPETypeId { get; set; }
        public String PPETypeName { get; set; }
        public Int64 PPEClassId { get; set; }
        public String PPEClassName { get; set; }
        public String Activity { get; set; }
        public clsRescueOrder()
        {
            Id = 0;
            RONo = 0;
            RefYear = "";
            Type = clsEnums.TypeOrder.RO;
            RODate = DateTime.Now;
            IsJobOut = false;
            CustomerType = clsEnums.CustomerType.External;
            CustomerId = 0;
            BranchId = 0;
            ItemType = clsEnums.EquipmentType.Equipment;
            EquipmentId = 0;
            ContractorId = 0;
            ServiceDriverId = 0;
            ServiceVehicleId = 0;
            Status = clsEnums.JOROStatus.Generated;
            IsActive = true;
            PPETypeId = 0;
            PPEClassId = 0;
            Activity = "";
            ListOfRODetails = new List<clsRODetails>();
            ListOfROMechanics = new List<clsROMechanics>();
            ListOfDriversAttendance = new List<clsRescueOrder>();
        }
        public clsRescueOrder(clsRescueOrder obj)
        {
            Id = obj.Id;
            RONo = obj.RONo;
            RefYear = obj.RefYear;
            Type = obj.Type;
            RODate = obj.RODate;
            IsJobOut = obj.IsJobOut;
            CustomerType = obj.CustomerType;
            CustomerId = obj.CustomerId;
            BranchId = obj.BranchId;
            ItemType = obj.ItemType;
            EquipmentId = obj.EquipmentId;
            ContractorId = obj.ContractorId;
            ServiceDriverId = obj.ServiceDriverId;
            ServiceVehicleId = obj.ServiceVehicleId;
            Status = obj.Status;
            IsActive = obj.IsActive;
            PPETypeId = obj.PPETypeId;
            PPEClassId = obj.PPEClassId;
            Activity = obj.Activity;
            ListOfRODetails = obj.ListOfRODetails;
            ListOfROMechanics = obj.ListOfROMechanics;
            ListOfDriversAttendance = obj.ListOfDriversAttendance;
        }
    }
}

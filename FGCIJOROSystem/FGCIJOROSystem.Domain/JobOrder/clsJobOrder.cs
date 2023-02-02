using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.JobOrder
{
    public class clsJobOrder
    {
        //JO
        public Int64 Id { get; set; }
        public Int64 JONo { get; set; }
        public String JONoStr { get { return (Type == clsEnums.TypeOrder.JO ? "JO" : "RO") + RefYear.Substring(RefYear.Length - 2) + JONo.ToString("0000"); } }
        public String RefYear { get; set; }
        public DateTime JODate { get; set; }
        public Boolean EquipmentOnBranch { get; set; }
        public Boolean PartsRequest { get; set; }
        public clsEnums.TypeOrder Type { get; set; }
        public String ChecklistNo { get; set; }
        public String EquipmentClass { get; set; }
        public String Mileage { get; set; } = string.Empty;
        //Customer
        public clsEnums.CustomerType CustomerType { get; set; }
        public Int64 CustomerId { get; set; }
        public String CustomerName { get; set; }
        public String Location { get; set; }
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

        //Drivers
        public Int64 DriverId { get; set; }
        public String DriverName { get; set; }

        //Status
        public clsEnums.JOROStatus Status { get; set; }
        //public String StatusName { get; set; }

        //List of JODetails
        public List<clsJODetails> ListOfJODetails { get; set; }
        //List of Mechanics
        public List<clsMechanics> ListOfMechanics { get; set; }

        public Int64 UserId { get; set; }
        public String EncoderName { get; set; }
        public String EncoderPosition { get; set; }
        
        //LOGS
        public Int64 PrintCount { get; set; }
        public Int64 ApproverId { get; set; }
        public String ApproverName { get; set; }
        public String ApproverPosition { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime DateEncoded { get; set; }
        
        //PPEStatus
        public Int64 PPETypeId { get; set; }
        public String PPETypeName { get; set; }
        public Int64 PPEClassId { get; set; }
        public String PPEClassName { get; set; }

        public clsJobOrder()
        {
            Id = 0;
            JONo = 0;
            RefYear = DateTime.Now.Date.Year.ToString();
            Type = clsEnums.TypeOrder.JO;
            JODate = DateTime.Now;
            EquipmentOnBranch = true;
            PartsRequest = false;
            CustomerType = clsEnums.CustomerType.External;
            CustomerId = 0;
            ContractorSectionHead = "";
            BranchId = 0;
            ItemType = clsEnums.EquipmentType.Equipment;
            EquipmentId = 0;
            ContractorId = 0;
            DriverId = 0;
            UserId = 0;
            ApproverName = "";
            ApproverPosition = "";
            PPETypeId = 0;
            PPEClassId = 0;
            ListOfJODetails = new List<clsJODetails>();
            ListOfMechanics = new List<clsMechanics>();
        }
        public clsJobOrder(clsJobOrder obj)
        {
            Id = obj.Id;
            JONo = obj.JONo;
            RefYear = obj.RefYear;
            Type = obj.Type;
            JODate = obj.JODate;
            EquipmentOnBranch = obj.EquipmentOnBranch;
            PartsRequest = obj.PartsRequest;
            CustomerType = obj.CustomerType;
            CustomerId = obj.CustomerId;
            BranchId = obj.BranchId;
            ContractorSectionHead = obj.ContractorSectionHead;
            ItemType = obj.ItemType;
            EquipmentId = obj.EquipmentId;
            ContractorId = obj.ContractorId;
            DriverId = obj.DriverId;
            UserId = obj.UserId;
            ApproverName = obj.ApproverName;
            PPETypeId = obj.PPETypeId;
            PPEClassId = obj.PPEClassId;
            ApproverPosition = obj.ApproverPosition;
            ListOfJODetails = obj.ListOfJODetails;
            ListOfMechanics = obj.ListOfMechanics;
        }
    }
}

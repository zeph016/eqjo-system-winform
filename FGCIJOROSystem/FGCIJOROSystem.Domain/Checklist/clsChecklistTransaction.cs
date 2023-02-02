using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Checklist
{
    public class clsChecklistTransaction
    {
        public Int64 Id { get; set; }
        public Int64 ChecklistGeneratorId { get; set; }
        public Int64 ChecklistDetailId { get; set; }
        public String ChecklistDetails { get; set; }
        public bool IncomingStatus { get; set; }
        public string IncomingRemarks { get; set; }
        public bool OutgoingStatus { get; set; }
        public string OutgoingRemarks { get; set; }
        public Int64 EquipmentMasterlistId { get; set; }
        public Int64 SectionId { get; set; }
        public string SectionName { get; set; }
        public Int64 ChecklistItemId { get; set; }
        public string ChecklistItemName { get; set; }
        public List<clsChecklistTransaction> ListOfChecklistTransaction { get; set; }

        public clsChecklistTransaction()
        {
            Id = 0;
            ChecklistGeneratorId = 0;
            ChecklistDetailId = 0;
            ChecklistDetails = "";
            IncomingStatus = false;
            IncomingRemarks = "";
            OutgoingStatus = false;
            OutgoingRemarks = "";
            EquipmentMasterlistId = 0;
            ListOfChecklistTransaction = new List<clsChecklistTransaction>();

        }
        public clsChecklistTransaction(clsChecklistTransaction obj)
        {
            Id = obj.Id;
            ChecklistGeneratorId = obj.ChecklistGeneratorId;
            ChecklistDetailId = obj.ChecklistDetailId;
            ChecklistDetails = obj.ChecklistDetails;
            IncomingStatus = obj.IncomingStatus;
            IncomingRemarks = obj.IncomingRemarks;
            OutgoingStatus = obj.OutgoingStatus;
            OutgoingRemarks = obj.OutgoingRemarks;
            EquipmentMasterlistId = obj.EquipmentMasterlistId;
            ListOfChecklistTransaction = obj.ListOfChecklistTransaction;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Checklist
{
    public class clsChecklistGeneratorDetails
    {
        public Int64 Id{ get; set; }
        public Int64 ChecklistGeneratorId{ get; set; }
        public Int64 ChecklistDetailId { get; set; }
        public bool IncomingStatus { get; set; }
        public string IncomingRemarks { get; set; }
        public bool OutgoingStatus { get; set; }
        public string OutgoingRemarks { get; set; }
        public Int64 EquipmentMasterlistId { get; set; }
        public Int64 SectionId { get; set; }
        public string SectionName { get; set; }
        public Int64 ChecklistItemId { get; set; }
        public string ChecklistItemName { get; set; }
        public List<clsChecklistGeneratorDetails> ListOfChecklistGeneratorDetails { get; set; }

        public clsChecklistGeneratorDetails()
        {
            Id=0;
            ChecklistGeneratorId = 0;
            ChecklistDetailId = 0;
            IncomingStatus = false;
            IncomingRemarks = "";
            OutgoingStatus = false;
            OutgoingRemarks = "";
            EquipmentMasterlistId = 0;
            SectionId = 0;
            SectionName = "";
            ChecklistItemId = 0;
            ChecklistItemName = "";
            ListOfChecklistGeneratorDetails = new List<clsChecklistGeneratorDetails>();
        }
    }
}

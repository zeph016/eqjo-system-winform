using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Global
{
    public class clsChecklist
    {
        public Int64 Id { get; set; }
        public String EquipmentCode { get; set; }
        public Int64 EquipmentTypeId { get; set; }
        public string EquipmentType { get; set; }
        public string EquipmentName { get; set; }
        public string Description { get; set; }
        public string Location{ get; set; }

        public List<clsChecklistDetails> ListOfChecklistDetails { get; set; }
        public List<clsChecklist> ListOfChecklist { get; set; }
        public clsChecklist()
        {
            Id = 0;
            EquipmentCode = "";
            EquipmentTypeId=0;
            Description="";
            ListOfChecklistDetails = new List<clsChecklistDetails>();
            ListOfChecklist = new List<clsChecklist>();
        }
        public clsChecklist(clsChecklist obj)
        {
            Id = obj.Id;
            EquipmentCode = obj.EquipmentCode;
            EquipmentTypeId = obj.EquipmentTypeId;
            Description = obj.Description;
            ListOfChecklistDetails = obj.ListOfChecklistDetails;
            ListOfChecklist = obj.ListOfChecklist;
        }
    }
}

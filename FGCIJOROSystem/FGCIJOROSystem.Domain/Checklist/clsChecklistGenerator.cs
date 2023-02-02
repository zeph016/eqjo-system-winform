using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Checklist
{
    public class clsChecklistGenerator
    {
        public Int64 PrintCount { get; set; }
        public Int64 Id{ get; set; }
        public Int64 ChecklistNo { get; set; }
        public String ChecklistNoStr { get { return "CH" + ChecklistNo.ToString("0000"); }}
        public DateTime DateEncoded{ get; set; }
        public Int64 EquipmentId{ get; set; }
        public string Location{ get; set; }
        public decimal OdometerReading { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateCompleted { get; set; }
        public String EquipmentCode { get; set; }
        public string EquipmentName { get; set; }
        public List<clsChecklistTransaction> ListOfChecklistTransaction { get; set; }
        public String PreparedBy { get; set; }
        public String PreparedByPos { get; set; }
        public clsChecklistGenerator()
        {
            Id = 0;
            ChecklistNo = 0;
            DateEncoded = DateTime.Now;
            EquipmentId = 0;
            Location = "";
            OdometerReading= 0;
            DateReceived=DateTime.Now;
            DateCompleted=DateTime.Now;
            EquipmentCode = "";
            EquipmentName = "";
            ListOfChecklistTransaction = new List<clsChecklistTransaction>();
            PreparedBy = "";
            PreparedByPos = "";
        }

        public clsChecklistGenerator(clsChecklistGenerator obj)
        {
            Id=obj.Id;
            ChecklistNo = obj.ChecklistNo;
            DateEncoded = obj.DateEncoded;
            EquipmentId = obj.EquipmentId;
            Location=obj.Location;
            OdometerReading=obj.OdometerReading;
            DateReceived=obj.DateReceived;
            DateCompleted=obj.DateCompleted;
            EquipmentCode = obj.EquipmentCode;
            EquipmentName = obj.EquipmentName;
            ListOfChecklistTransaction = obj.ListOfChecklistTransaction;
            PreparedBy = obj.PreparedBy;
            PreparedByPos = obj.PreparedByPos;
        }
    }
}

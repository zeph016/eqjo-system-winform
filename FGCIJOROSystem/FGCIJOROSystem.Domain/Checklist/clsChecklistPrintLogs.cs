using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Checklist
{
   public class clsChecklistPrintLogs
    {
        public Int64 Id{ get; set; }
        public Int64 ChecklistNo { get; set; }
        public DateTime DateEncoded { get; set; }
        public string Username { get; set; }

        public clsChecklistPrintLogs()
        {
            Id = 0;
            ChecklistNo = 0;
            DateEncoded = DateTime.Now;
            Username = "";
        }

      public clsChecklistPrintLogs (clsChecklistPrintLogs obj)
        {
            Id = obj.Id;
            ChecklistNo = obj.ChecklistNo;
            DateEncoded = obj.DateEncoded;
            Username = obj.Username;
        }
    }
}

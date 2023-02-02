using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Global
{
    public class clsChecklistDetails
    {
        public Int64 Id { get; set; }
        public Int64 ChecklistId { get; set; }
        public Int64 SectionId { get; set; }
        public string SectionName { get; set; }
        public Int64 ChecklistItemId { get; set; }
        public string ChecklistItemName { get; set; }
        public Boolean IsActive { get; set; }
        public List<clsChecklistDetails> ListOfChecklistDetails { get; set; }

        public clsChecklistDetails()
        {
            Id = 0;
            ChecklistId = 0;
            SectionId = 0;
            SectionName = "";
            ChecklistItemId = 0;
            ChecklistItemName = "";
            IsActive = true;
        }
            

    }
}

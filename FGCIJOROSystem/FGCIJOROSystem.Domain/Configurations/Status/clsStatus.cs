using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.Status
{
    public class clsStatus : IPrototype<clsStatus>
    {        
        public Int64 Id { get; set; }
        public String StatusName { get; set; }
        public String StatusDescription { get; set; }
        public String EquipmentStatusSymbol { get; set; }
        public Boolean IsClosure { get; set; }
        public Boolean IsAlert { get; set; }
        public Boolean NeedsApproval { get; set; }
        public Int64 SequenceOrder { get; set; }
        public Boolean IsApproval { get; set; }
        public Boolean IsCompleted { get; set; }
        public Boolean IsSustained { get; set; }
        public Boolean IsBilled { get; set; }
        public Boolean IsCancelled { get; set; }
        public Boolean Active { get; set; }
        public Boolean Printable { get; set; }
        

        public clsStatus()
        {
            Id = 0;
            StatusName = "";
            StatusDescription = "";
            EquipmentStatusSymbol = "";
            IsClosure = false;
            IsAlert = false;
            NeedsApproval = false;
            SequenceOrder = 0;
            IsApproval = false;
            IsCancelled = false;
            IsCompleted = false;
            IsSustained = false;
            Printable = false;
            Active = true;
        }

        public clsStatus(clsStatus obj)
        {
            Id = obj.Id;
            StatusName = obj.StatusName;
            StatusDescription = obj.StatusDescription;
            EquipmentStatusSymbol = obj.EquipmentStatusSymbol;
            IsClosure = obj.IsClosure;
            IsAlert = obj.IsAlert;
            NeedsApproval = obj.NeedsApproval;
            SequenceOrder = obj.SequenceOrder;
            IsApproval = obj.IsApproval;
            IsCompleted = obj.IsCompleted;
            IsSustained = obj.IsSustained;
            IsBilled = obj.IsBilled;
            IsCancelled = obj.IsCancelled;
            Active = obj.Active;
            Printable = obj.Printable;
        }

        public clsStatus DeepCopy()
        {
            return (clsStatus)this.MemberwiseClone();
        }
    }
}

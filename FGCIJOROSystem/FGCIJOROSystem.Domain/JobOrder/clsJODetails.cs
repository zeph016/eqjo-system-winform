using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.JobOrder
{
    public class clsJODetails
    {
        public Int64 Id { get; set; }
        public Int64 JOId { get; set; }
        public Int64 TariffId { get; set; }
        public String WorkDescription { get; set; }
        public String Section { get; set; }
        public String JobCategory { get; set; }
        public String JobType { get; set; }
        public String Unit { get; set; }
        public Decimal NoOfMechanics { get; set; }

        public DateTime? DateOfUpdate { get; set; }
        public Decimal WorkPercentage { get; set; }

        public DateTime EffectivityDate { get; set; }
       
        public String EffectiveDateStr
        { 
            get 
            {
                return EffectivityDate.Date.ToShortDateString();
            }
        }
        public DateTime TargetDate { get; set; }

        public String TargetDateStr
        {
            get
            {
                return TargetDate.Date.ToShortDateString();
            }
        }
        public Decimal WorkTimeSpan { get; set; }
        public Decimal Price { get; set; }
        public Decimal Amount { get; set; }
        public String Remarks { get; set; }
        public Int64 StatusId { get; set; }
        public String StatusName { get; set; }
        public int Tag { get; set; }
        public Boolean IsActive { get; set; }
        public String AssignedMechanics { get; set; }
        public DateTime ? DateApproved { get; set; }
        public clsJODetails()
        {
            Id = 0;
            JOId = 0;
            TariffId = 0;
            WorkDescription = "";
            Section = "";
            JobCategory = "";
            JobType = "";
            Unit = "";
            NoOfMechanics = 0;
            EffectivityDate = DateTime.Now;
            TargetDate = DateTime.Now;
            WorkTimeSpan = 0;
            Price = 0;
            Amount = 0;
            Remarks = "";
            StatusId = 0;
            Tag = 0;
            IsActive = true;
            DateOfUpdate = null;
            WorkPercentage = 0;
            AssignedMechanics = "";
            DateApproved = null;
        }
        public clsJODetails(clsJODetails obj)
        {
            Id = obj.Id;
            JOId = obj.JOId;
            TariffId = obj.TariffId;
            WorkDescription = obj.WorkDescription;
            Section = obj.Section;
            JobCategory = obj.JobCategory;
            JobType = obj.JobType;
            Unit = obj.Unit;
            NoOfMechanics = obj.NoOfMechanics;
            EffectivityDate = obj.EffectivityDate;
            TargetDate = obj.TargetDate;
            WorkTimeSpan = obj.WorkTimeSpan;
            Price = obj.Price;
            Amount = obj.Amount;
            Remarks = obj.Remarks;
            StatusId = obj.StatusId;
            Tag = obj.Tag;
            IsActive = obj.IsActive;
            DateOfUpdate = obj.DateOfUpdate;
            WorkPercentage = obj.WorkPercentage;
            AssignedMechanics = obj.AssignedMechanics;
            DateApproved = obj.DateApproved;
        }
    }
}

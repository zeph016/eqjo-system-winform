using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.Tariff
{
    public class clsTariff
    {
        public Int64 Id { get; set; }
        public String Name { get; set; }
        public Int64 SectionId { get; set; }
        public Int64 JobCategoryId { get; set; }
        public Int64 JobTypeId { get; set; }
        public String WorkDescription { get; set; }
        public Decimal NoOfMechanics { get; set; }
        public Decimal Price { get; set; }
        public Int64 UnitId { get; set; }
        public Decimal WorkTimeSpan { get; set; }
        public Boolean Active { get; set; }
        /// <summary>
        /// For Joining
        /// </summary>
        public String JobCategoryName { get; set; }

        public String JobTypeName { get; set; }
        public String SectionName { get; set; }
        /// <summary>
        /// Product masterlist Units
        /// </summary> 
        public Int64 ProdUnitId { get; set; }
        public String UnitName { get; set; }

        public List<clsTariff> ListOfTariff { get; set; }

        public clsTariff()
        {
            Id = 0;
            SectionId = 0;
            JobCategoryId = 0;
            JobTypeId = 0;
            UnitId = 0;
            ProdUnitId = 0;
            ListOfTariff = new List<clsTariff>();
        }
        public clsTariff(clsTariff obj)
        {
            Id = obj.Id;
            SectionId = obj.SectionId;
            JobCategoryId = obj.JobCategoryId;
            JobTypeId = obj.JobTypeId;
            UnitId = obj.UnitId;
            ProdUnitId = obj.ProdUnitId;
            ListOfTariff = obj.ListOfTariff;
        }
    }

    public class clsTariffEqJO
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string EquipmentCode { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string JoNo { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime JODate { get; set;  }
        public String SectionName { get; set; } = string.Empty;
        public string WorkDescription { get; set; } = string.Empty;
    }
    public class clsTariffEqRO
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string EquipmentCode { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string RoNo { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime RODate { get; set; }
        public String SectionName { get; set; } = string.Empty;
        public string WorkDescription { get; set; } = string.Empty;
    }
}

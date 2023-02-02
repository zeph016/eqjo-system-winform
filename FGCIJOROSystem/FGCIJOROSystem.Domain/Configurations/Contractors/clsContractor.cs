using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.Contractors
{
    public class clsContractor
    {
        public Int64 Id { get; set; }
        public clsEnums.ContractorCategory ContractorCategory { get; set; } 
        public clsEnums.ContractorType ContractorType { get; set; }               
        public String ContractorSection
        {
            get { return Section; }
            //get
            //{
            //    if (ContractorType == clsEnums.ContractorType.Internal)
            //    {
            //        return Section;
            //    }
            //    else
            //    {
            //        if (ContractorCategory == clsEnums.ContractorCategory.Person)
            //        {
            //            return Firstname + " " + Middlename + " " + Lastname;
            //        }
            //        else
            //        {
            //            return CompanyName;
            //        }
            //    }
            //}
        }

        public String SectionHead
        {
            get
            {
                if (ContractorType == clsEnums.ContractorType.Internal)
                {
                    return Firstname + " " + Middlename + " " + Lastname + " " + NameExtension;
                }
                else
                {
                    return "";
                }
            }
        }
        public String FullName { get { return Firstname + " " + Middlename + " " + Lastname + " " + NameExtension; } }
        public String Firstname { get; set; }
        public String Middlename { get; set; }
        public String Lastname { get; set; }
        public string NameExtension { get; set; }
        public String CompanyName { get; set; }
        public String Section { get; set; }
        public String Address { get; set; }
        public String ContactNos { get; set; }
        public Boolean Active { get; set; }
    }
}

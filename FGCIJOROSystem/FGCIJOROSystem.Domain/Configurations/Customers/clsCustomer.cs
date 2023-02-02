using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.Customers
{
    public class clsCustomer
    {
        public Int64 Id { get; set; }
        public String CustomerName { get; set; }
        public String CustomerAddress { get; set; }
        public String ContactNo { get; set; }
        public clsEnums.CustomerType CustomerType { get; set; }
        public bool Active { get; set; } 
 
        public clsCustomer()
        {
            Id = 0;
            CustomerName = "";
            CustomerAddress = "";
            ContactNo = "";
            CustomerType = clsEnums.CustomerType.External;
            Active = true;
            
        }
        public clsCustomer(clsCustomer obj)
        {
            Id = obj.Id;
            CustomerName = obj.CustomerName;
            CustomerAddress = obj.CustomerAddress;
            ContactNo = obj.ContactNo;
            CustomerType = obj.CustomerType;
            Active = obj.Active;
            
        }
    }
}

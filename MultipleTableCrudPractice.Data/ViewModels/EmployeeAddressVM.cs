using MultipleTableCrudPractice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Data.ViewModels
{
    public class EmployeeAddressVM
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        //public AddressDetails address { get; set; }
        public int AddressId { get; set; }
        public string EmployeeAddress { get; set; }
        public string AddressType { get; set; }
        public virtual ICollection<AddressDetails> Address { get; set; }
    }
}

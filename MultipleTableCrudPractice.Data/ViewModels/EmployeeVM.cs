using MultipleTableCrudPractice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Data.ViewModels
{
    public class EmployeeVM
    {
        public EmployeeDetails EmployeeDetails { get; set; }

        public List<AddressDetails> AddressDetailsList { get; set; }
    }
}

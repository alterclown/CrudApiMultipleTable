using MultipleTableCrudPractice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Data.DTOs
{
    public class ManyEmployeeDto
    {
        public EmployeeDetails EmployeeDetails { get; set; }
        public List<AddressDetails> Address { get; set; }
    }
}

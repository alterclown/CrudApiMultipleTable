using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultipleTableCrudPractice.Data.Entities
{
    [Table("EmployeeDetails", Schema = "dbo")]
    public class EmployeeDetails
    {
        //public EmployeeDetails()
        //{
        //    this.Address = new HashSet<AddressDetails>();
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        [ForeignKey("EmployeeId")]
        public List<AddressDetails> Address { get; set; }
    }
}

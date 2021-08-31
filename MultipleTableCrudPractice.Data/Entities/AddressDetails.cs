using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultipleTableCrudPractice.Data.Entities
{
    [Table("AddressDetails", Schema = "dbo")]
    public class AddressDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public string EmployeeAddress { get; set; }
        public string AddressType { get; set; }
    }
}

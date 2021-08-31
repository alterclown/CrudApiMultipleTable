using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultipleTableCrudPractice.Data.Entities
{
    [Table("BankAccount", Schema = "dbo")]
    public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankAccountId { get; set; }
        public int EmployeeId { get; set; }
        public int SalaryId { get; set; }
        public string BankAccountName { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

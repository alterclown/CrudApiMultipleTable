using Microsoft.EntityFrameworkCore;
using MultipleTableCrudPractice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Data.DataContext
{
    public class CrudContext : DbContext
    {

        public CrudContext(DbContextOptions<CrudContext> options) : base(options)
        {
        }
        public DbSet<EmployeeDetails> EmployeeDetailes { get; set; }
        public DbSet<AddressDetails> AddressDetailes { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HR_Employees.Entities;

    public class DBContext : DbContext
    {
        public DBContext (DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        #region Security
        public DbSet<User> Users { get; set; } = default!;
        #endregion
        public DbSet<Employee> Employee { get; set; } = default!;
        public DbSet<WorkingHour> WorkingHours { get; set; } = default!;
    }

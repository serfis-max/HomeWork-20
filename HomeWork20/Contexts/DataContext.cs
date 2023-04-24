using System.IO;
using Microsoft.EntityFrameworkCore;
using HW_201.Data;

namespace HW_201.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<User> Table { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                    "Data Source = (LocalDB)\\MSSQLLocalDB;" +
                    $"AttachDbFilename={Directory.GetCurrentDirectory()}\\bin\\Debug\\db\\PhoneBookDB.mdf;" +
                    "Trusted_Connection = True;");
        }
    }
}

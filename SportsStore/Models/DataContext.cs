using Microsoft.EntityFrameworkCore;
using SportsStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    /// <summary>
    /// В powershell комманда: 
    /// dotnet ef migrations add Initial --context SampleDbContext  
    /// чтобы работала, нужно установить инструмент dotnet-ef :
    /// dotnet tool install --global dotnet-ef --version 5.*
    /// https://stackoverflow.com/questions/57066856/command-dotnet-ef-not-found
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            /*Nothing to do*/
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}

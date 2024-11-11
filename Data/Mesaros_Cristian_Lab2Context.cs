using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mesaros_Cristian_Lab2.Models;

namespace Mesaros_Cristian_Lab2.Data
{
    public class Mesaros_Cristian_Lab2Context : DbContext
    {
        public Mesaros_Cristian_Lab2Context (DbContextOptions<Mesaros_Cristian_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Mesaros_Cristian_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Mesaros_Cristian_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Mesaros_Cristian_Lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<Mesaros_Cristian_Lab2.Models.Category> Category { get; set; } = default!;
    }
}

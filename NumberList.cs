using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace NumberApp
{
    public class Context : DbContext
    {
        public DbSet<NumberList> NumberList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(local)\SQLExpress;Initial Catalog=NumbersLists;Integrated Security=SSPI;");
        }
    }
    public class NumberList
    {
        public int Id { get; set; }
        public string NumList { get; set; }
        public int MilliSecondsTaken { get; set; }
        public string SortDirection { get; set; }



        
    }
}

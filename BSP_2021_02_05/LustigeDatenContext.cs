using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;

namespace BSP_2021_02_05
{
    class LustigeDatenContext : DbContext
    {
        public LustigeDatenContext() : base("name=Bausteinpruefung")
        {
            // Connection String automatisch ermitteln lassen
            // um von Spaghetti-Code geplagten Dozis das Leben zu erleichtern xD
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            this.Database.Connection.ConnectionString += path + "\\CSH3_BSP.mdf";
        }
        public DbSet<LustigeDaten> LustigeDaten { get; set; }
    }
}

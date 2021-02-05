using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BSP_2021_02_05
{
    class LustigeDaten
    {
        [Key]
        public string Nummer { get; set; }
        [Required]
        public DateTime Datum { get; set; }
        [Required]
        public string Bezeichnung { get; set; }
        [Required]
        public string Ergebnis { get; set; }
    }
}

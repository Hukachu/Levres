using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Levres.Models
{
    public class Narudzba
    {
        [Key]
        public Guid id { get; set; }
        [ForeignKey("Automobil")]
        public Guid automobilID { get; set; }
        [ForeignKey("AspNetUsers")]
        public string kupacID { get; set; }
        public DateOnly datum { get; set; }
        public String Tip { get; set; }
       

        public Narudzba() { }

        public Narudzba(Guid automobilID, string kupacID, DateOnly datum, String tip)
        {
            this.automobilID = automobilID;
            this.kupacID = kupacID;
            this.datum = datum;
            this.Tip = tip;
        }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Levres.Models
{
    public class Servis
    {
        [Key]
        public Guid id { get; set; }
        [ForeignKey("AspNetUsers")]
        public string kupacID { get; set; }
        public Model model { get; set; }
        public String registracijskeTablice { get; set; }
        public String opis {  get; set; }

        public Servis () { }

        public Servis(string kupacID, Model model, string registracijskeTablice, string opis)
        {
            this.kupacID = kupacID;
            this.model = model;
            this.registracijskeTablice = registracijskeTablice;
            this.opis = opis;
        }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Levres.Models
{
    public class Konfiguracija
    {
        [Key]
        public Guid id { get; set; }
        public Model model { get; set; }
        public Boja boja { get; set; }
        public Felge felge { get; set; }
        public VrstaInterijera vrsta_interijera { get; set; }
        public Svjetla svjetla { get; set; }
        public Motor motor { get; set; }

        public Konfiguracija() { }

        public Konfiguracija(Model model, Boja boja, Felge felge, VrstaInterijera vrsta_interijera, Svjetla svjetla, Motor motor)
        {
            this.model = model;
            this.boja = boja;
            this.felge = felge;
            this.vrsta_interijera = vrsta_interijera;
            this.svjetla = svjetla;
            this.motor = motor;
        }
    }
}

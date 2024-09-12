using System.ComponentModel.DataAnnotations;

namespace Levres.Models
{
    public abstract class Automobil
    {
        [Key]
        public Guid id { get; set; }

        public Model model { get; set; }
        public DateOnly godinaProizvodnje { get; set; }
        public Gorivo gorivo { get; set; }
        public Transmisija transmisija { get; set; }
        public int brojVrata { get; set; }
        public Boja boja { get; set; }
        public Pogon pogon { get; set; }
        public Felge felge { get; set; }
        public EmisioniStandard emisioniStandard { get; set; }
        public int sjedecaMjesta { get; set; }
        public int masaTezina { get; set; }
        public VrstaInterijera vrstaInterijera { get; set; }
        public Svjetla svjetla { get; set; }
        public Double cijena { get; set; }
        public String slike { get; set; }
        public Motor motor { get; set; }

        protected Automobil() { }

        protected Automobil(Model model, DateOnly godina_proizvodnje, Gorivo gorivo, Transmisija transmisija, int broj_vrata, Boja boja, Pogon pogon, Felge felge, EmisioniStandard emisioni_standard, int sjedeca_mjesta, int masa_tezina, VrstaInterijera vrsta_interijera, Svjetla svjetla, double cijena, string slike, Motor motor)
        {
            this.model = model;
            this.godinaProizvodnje = godina_proizvodnje;
            this.gorivo = gorivo;
            this.transmisija = transmisija;
            this.brojVrata = broj_vrata;
            this.boja = boja;
            this.pogon = pogon;
            this.felge = felge;
            this.emisioniStandard = emisioni_standard;
            this.sjedecaMjesta = sjedeca_mjesta;
            this.masaTezina = masa_tezina;
            this.vrstaInterijera = vrsta_interijera;
            this.svjetla = svjetla;
            this.cijena = cijena;
            this.slike = slike;
            this.motor = motor;
        }

    }
}

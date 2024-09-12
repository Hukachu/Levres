namespace Levres.Models
{
    public class PolovniAutomobil : Automobil
    {
        public int kilometraza {  get; set; }
        public String stete {  get; set; }
        public int brojVlasnika { get; set; }

        public PolovniAutomobil() { } 

        public PolovniAutomobil(Model model, DateOnly godina_proizvodnje, Gorivo gorivo, Transmisija transmisija, int broj_vrata, Boja boja, Pogon pogon, Felge felge, EmisioniStandard emisioni_standard, int sjedeca_mjesta, int masa_tezina, VrstaInterijera vrsta_interijera, Svjetla svjetla, double cijena, string slike, Motor motor, int kilometraza, String stete, int broj_vlasnika)
            : base(model, godina_proizvodnje, gorivo, transmisija, broj_vrata, boja, pogon, felge, emisioni_standard, sjedeca_mjesta, masa_tezina, vrsta_interijera, svjetla, cijena, slike, motor) 
        {
            this.kilometraza = kilometraza;
            this.stete = stete;
            this.brojVlasnika = broj_vlasnika;
        }
    }
}

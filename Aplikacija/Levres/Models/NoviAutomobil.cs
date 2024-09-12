namespace Levres.Models
{
    public class NoviAutomobil : Automobil
    {
        public int garancija {  get; set; }

        public NoviAutomobil() { }
        public NoviAutomobil(Model model, DateOnly godina_proizvodnje, Gorivo gorivo, Transmisija transmisija, int broj_vrata, Boja boja, Pogon pogon, Felge felge, EmisioniStandard emisioni_standard, int sjedeca_mjesta, int masa_tezina, VrstaInterijera vrstaInterijera, Svjetla svjetla, double cijena, string slike, Motor motor, int garancija) 
            : base(model, godina_proizvodnje, gorivo, transmisija, broj_vrata, boja, pogon, felge, emisioni_standard, sjedeca_mjesta, masa_tezina, vrstaInterijera, svjetla, cijena, slike, motor)
        {
            this.garancija = garancija;
        }
    }
}

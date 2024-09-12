namespace Levres.Models
{
    public class KonfiguracijaViewModel
    {
        public string? model { get; set; }
        public string? boja { get; set; }
        public string? felge { get; set; }
        public string? vrsta_interijera { get; set; }
        public string? svjetla { get; set; }
        public string? motor { get; set; }
        public double cijena { get; set; }

        public KonfiguracijaViewModel() { }

        public KonfiguracijaViewModel(string model, string boja, string felge, string vrsta_interijera, string svjetla, string motor, double cijena)
        {
            this.model = model;
            this.boja = boja;
            this.felge = felge;
            this.vrsta_interijera = vrsta_interijera;
            this.svjetla = svjetla;
            this.motor = motor;
            this.cijena = cijena;
        }
    }
}

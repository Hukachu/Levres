using System.ComponentModel.DataAnnotations;

namespace Levres.Models
{
    public class Oprema
    {
        [Key]
        public Guid id { get; set; }
        public String nazivDijela { get; set; }
        public Model model { get; set; }
        public int kolicina { get; set; }
        public Double cijena { get; set; }

        public Oprema() { }

        public Oprema(string nazivDijela, Model model, int kolicina, double cijena)
        {
            this.nazivDijela = nazivDijela;
            this.model = model;
            this.kolicina = kolicina;
            this.cijena = cijena;
        }

    }
}

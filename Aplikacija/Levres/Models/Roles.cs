using System.ComponentModel.DataAnnotations;

namespace Levres.Models
{
    public class Roles
    {
        [Key]
        public int id { get; set; }
        public String naziv {  get; set; }

        public Roles() { }

        /*public Roles(String naziv)
        {
            this.naziv = naziv;
        }*/
    }
}

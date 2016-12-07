using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
 public class Auto
    {
        [Key]
        public int Id { get; set; }
        public string Marke { get; set; }
        public byte[] RowVersion { get; set; }
        public int Tagestarif { get; set; }
        public int Basistarif { get; set; }
        public int AutoKlasse { get; set; }
    }
}

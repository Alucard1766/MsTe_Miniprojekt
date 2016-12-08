using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
 public abstract class Auto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Marke { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [Required]
        public int Tagestarif { get; set; }
        [Required]
        public int AutoKlasse { get; set; }
    }

    public class LuxusklasseAuto : Auto
    {
        [Required]
        public int Basistarif { get; set; }
    }

    public class MittelklasseAuto : Auto
    {

    }

    public class StandardAuto : Auto
    {

    }
}

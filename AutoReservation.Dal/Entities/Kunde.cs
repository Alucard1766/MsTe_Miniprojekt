using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
  public class Kunde
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public String Nachname { get; set; }
        [StringLength(20)]
        public String Vorname { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public byte[] RowVersion { get; set; }
    }
}

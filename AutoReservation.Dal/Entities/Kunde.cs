using System;
using System.Collections.Generic;
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
        [Required]
        public String Nachname { get; set; }
        [StringLength(20)]
        [Required]
        public String Vorname { get; set; }
        [Required]
        public DateTime Geburtsdatum { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public ICollection<Reservation> Reservationen { get; set; }
    }
}

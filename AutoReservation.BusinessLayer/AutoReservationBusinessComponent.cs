using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    { 
        public List<Auto> Autos
        {
            get
            {
                using (var context = new AutoReservationContext())
                {
                    var dbReturn = from c in context.Autos select c;
                    List<Auto> autoList = new List<Auto>();
                    foreach (Auto a in dbReturn)
                    {
                        autoList.Add(a);
                    }
                    return autoList;
                }
            }
        }

        public List<Kunde> Kunden
        {
            get
            {
                using (var context = new AutoReservationContext())
                {
                    var dbReturn = from k in context.Kunden select k;
                    List<Kunde> kundeList = new List<Kunde>();
                    foreach (Kunde k in dbReturn)
                    {
                        kundeList.Add(k);
                    }
                    return kundeList;
                }
            }
        }

        public List<Reservation> Reservationen
        {
            get
            {
                using (var context = new AutoReservationContext())
                {
                    var dbReturn = from r in context.Reservationen select r;
                    List<Reservation> kundeList = new List<Reservation>();
                    foreach (Reservation r in dbReturn)
                    {
                        kundeList.Add(r);
                    }
                    return kundeList;
                }
            }
        }

        //Reservationen

        public Auto GetAutoById(int id)
        {
            using (var context = new AutoReservationContext())
            {
                var dbReturn = from a in context.Autos
                               where a.Id == id
                               select a;

                if (dbReturn.Count() == 1)
                {
                    return dbReturn.First();
                }
                else
                {
                    throw new System.Exception("no or too much auto for id"); //TODO: Wrong exception here
                }
            }
        }

        public Kunde GetKundeById(int id)
        {
            using (var context = new AutoReservationContext())
            {
                var dbReturn = from k in context.Kunden
                               where k.Id == id
                               select k;

                if (dbReturn.Count() == 1)
                {
                    return dbReturn.First();
                }
                else
                {
                    throw new System.Exception("no or too much kunde for id"); //TODO: Wrong exception here
                }
            }
        }

        public Reservation GetReservationByNr(int id)
        {
            using (var context = new AutoReservationContext())
            {
                var dbReturn = from r in context.Reservationen
                               where r.ReservationsNr == id
                               select r;

                if (dbReturn.Count() == 1)
                {
                    return dbReturn.First();
                }
                else
                {
                    throw new System.Exception("no or too much reservation for id"); //TODO: Wrong exception here
                }
            }
        }

        public Auto InsertAuto(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                context.Autos.Add(auto);
                context.SaveChanges();
            }
            return auto;
        }

        public Kunde InsertKunde(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                context.Kunden.Add(kunde);
                context.SaveChanges();
            }
            return kunde;
        }

        public Reservation InsertReservation(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                context.Reservationen.Add(reservation);
                context.SaveChanges();
            }
            return reservation;
        }



        public Auto UpdateAuto(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                var dbReturn = (from a in context.Autos
                                where a.Id == auto.Id
                                select a).FirstOrDefault();
                if (dbReturn != null)
                {
                    dbReturn.Marke = auto.Marke;
                    dbReturn.Reservationen = auto.Reservationen;
                    dbReturn.RowVersion = auto.RowVersion;
                    dbReturn.Tagestarif = auto.Tagestarif;
                    context.SaveChanges();
                    
                }
                return dbReturn;
            }
        }

        public Kunde UpdateKunde(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                var dbReturn = (from k in context.Kunden
                                where k.Id == kunde.Id
                                select k).FirstOrDefault();
                if (dbReturn != null)
                {
                    dbReturn.Geburtsdatum = kunde.Geburtsdatum;
                    dbReturn.Reservationen = kunde.Reservationen;
                    dbReturn.RowVersion = kunde.RowVersion;
                    dbReturn.Nachname = kunde.Nachname;
                    dbReturn.Vorname = kunde.Vorname;
                    context.SaveChanges();

                }
                return dbReturn;
            }
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                var dbReturn = (from r in context.Reservationen
                                where r.ReservationsNr == reservation.ReservationsNr
                                select r).FirstOrDefault();
                if (dbReturn != null)
                {
                    dbReturn.Kunde = reservation.Kunde;
                    dbReturn.KundeId = reservation.KundeId;
                    dbReturn.RowVersion = reservation.RowVersion;
                    dbReturn.Von = reservation.Von;
                    dbReturn.Auto = reservation.Auto;
                    dbReturn.AutoId = reservation.AutoId;
                    dbReturn.Bis = reservation.Bis;
                    context.SaveChanges();

                }
                return dbReturn;
            }
        }

        public void DeleteAuto(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                var dbReturn = from a in context.Autos
                               where a.Id == auto.Id
                               select a;
                foreach (var a in dbReturn)
                {
                    context.Autos.Remove(a);
                }
                context.SaveChanges();
            }
        }

        public void DeleteKunde(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                var dbReturn = from k in context.Kunden
                               where k.Id == kunde.Id
                               select k;
                foreach (var k in dbReturn)
                {
                    context.Kunden.Remove(k);
                }
                context.SaveChanges();
            }
        }

        public void DeleteReservation(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                var dbReturn = from r in context.Reservationen
                               where r.ReservationsNr == reservation.ReservationsNr
                               select r;
                foreach (var r in dbReturn)
                {
                    context.Reservationen.Remove(r);
                }
                context.SaveChanges();
            }
        }

        private static LocalOptimisticConcurrencyException<T> CreateLocalOptimisticConcurrencyException<T>(AutoReservationContext context, T entity)
            where T : class
        {
            var dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new LocalOptimisticConcurrencyException<T>($"Update {typeof(Auto).Name}: Concurrency-Fehler", dbEntity);
        }
    }
}
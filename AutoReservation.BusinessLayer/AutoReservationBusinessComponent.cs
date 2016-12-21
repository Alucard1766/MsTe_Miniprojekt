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
                    return context.Autos.ToList();
                }
            }
        }

        public List<Kunde> Kunden
        {
            get
            {
                using (var context = new AutoReservationContext())
                {
                    return context.Kunden.ToList();
                }
            }
        }

        public List<Reservation> Reservationen
        {
            get
            {
                using (var context = new AutoReservationContext())
                {
                    return context.Reservationen.Include("Auto").Include("Kunde").ToList();
                }
            }
        }

        public Auto GetAutoById(int id)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Autos.FirstOrDefault(a => a.Id == id);
            }
        }

        public Kunde GetKundeById(int id)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Kunden.FirstOrDefault(k => k.Id == id);
            }
        }

        public Reservation GetReservationByNr(int id)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Reservationen.Include("Auto").Include("Kunde").FirstOrDefault(r => r.ReservationsNr == id);
            }
        }

        public Auto InsertAuto(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                context.Autos.Add(auto);
                context.Entry(auto).State = EntityState.Added;
                context.SaveChanges();
            }
            return auto;
        }

        public Kunde InsertKunde(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                context.Kunden.Add(kunde);
                context.Entry(kunde).State = EntityState.Added;
                context.SaveChanges();
            }
            return kunde;
        }

        public Reservation InsertReservation(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                context.Reservationen.Add(reservation);
                context.Entry(reservation).State = EntityState.Added;
                context.SaveChanges();
            }
            return reservation;
        }



        public Auto UpdateAuto(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Autos.Attach(auto);
                    context.Entry(auto).State = EntityState.Modified;
                    context.SaveChanges();
                    return auto;
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateLocalOptimisticConcurrencyException(context, auto);
                }
            }
        }

        public Kunde UpdateKunde(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Kunden.Attach(kunde);
                    context.Entry(kunde).State = EntityState.Modified;
                    context.SaveChanges();
                    return kunde;
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateLocalOptimisticConcurrencyException(context, kunde);
                }
            }
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Reservationen.Attach(reservation);
                    context.Entry(reservation).State = EntityState.Modified;
                    context.Entry(reservation).Reference(r => r.Auto).Load();
                    context.Entry(reservation).Reference(r => r.Kunde).Load();
                    context.SaveChanges();
                    return reservation;
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateLocalOptimisticConcurrencyException(context, reservation);
                }

            }
        }

        public void DeleteAuto(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                context.Entry(auto).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void DeleteKunde(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                context.Entry(kunde).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void DeleteReservation(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                context.Entry(reservation).State = EntityState.Deleted;
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
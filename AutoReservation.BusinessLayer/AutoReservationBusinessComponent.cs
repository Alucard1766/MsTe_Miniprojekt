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

        //Kunden

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


        Auto InsertAuto(Auto auto)
        {
            //TODO: FOlie 64 im EntityFramework
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
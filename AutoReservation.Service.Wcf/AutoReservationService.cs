using AutoReservation.Common.Interfaces;
using System;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using AutoReservation.BusinessLayer;
using AutoReservation.Dal.Entities;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private static void WriteActualMethod()
        {
            Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
        }

        public List<AutoDto> Autos
        {
            get
            {
                WriteActualMethod();
                var businessLayer = new AutoReservationBusinessComponent();
                List<Auto> list = businessLayer.Autos;
                return DtoConverter.ConvertToDtos(list);
            }
        }

        public List<KundeDto> Kunden
        {
            get
            {
                WriteActualMethod();
                var businessLayer = new AutoReservationBusinessComponent();
                List<Kunde> list = businessLayer.Kunden;
                return DtoConverter.ConvertToDtos(list);
            }
        }

        public List<ReservationDto> Reservationen
        {
            get
            {
                WriteActualMethod();
                var businessLayer = new AutoReservationBusinessComponent();
                List<Reservation> list = businessLayer.Reservationen;
                return DtoConverter.ConvertToDtos(list);
            }
        }


        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            businessLayer.DeleteAuto(DtoConverter.ConvertToEntity(auto));
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            businessLayer.DeleteKunde(DtoConverter.ConvertToEntity(kunde));
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            businessLayer.DeleteReservation(DtoConverter.ConvertToEntity(reservation));
        }

        public AutoDto GetAutoById(int id)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            return DtoConverter.ConvertToDto(businessLayer.GetAutoById(id));
        }

        public KundeDto GetKundeById(int id)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            return DtoConverter.ConvertToDto(businessLayer.GetKundeById(id));
        }

        public ReservationDto GetReservationByNr(int reservationNr)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            return DtoConverter.ConvertToDto(businessLayer.GetReservationByNr(reservationNr));
        }

        public AutoDto InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            return DtoConverter.ConvertToDto(businessLayer.InsertAuto(DtoConverter.ConvertToEntity(auto)));
        }

        public KundeDto InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            return DtoConverter.ConvertToDto(businessLayer.InsertKunde(DtoConverter.ConvertToEntity(kunde)));
        }

        public ReservationDto InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            return DtoConverter.ConvertToDto(businessLayer.InsertReservation(DtoConverter.ConvertToEntity(reservation)));
        }

        public AutoDto UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            try
            {
                return DtoConverter.ConvertToDto(businessLayer.UpdateAuto(DtoConverter.ConvertToEntity(auto)));
            }
            catch (LocalOptimisticConcurrencyException<Auto> e)
            {
                //throw new FaultException<AutoDto>(e.MergedEntity.ConvertToDto());
                throw new FaultException("Auto Update failed");
            }
        }

        public KundeDto UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            return DtoConverter.ConvertToDto(businessLayer.UpdateKunde(DtoConverter.ConvertToEntity(kunde)));
        }

        public ReservationDto UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            var businessLayer = new AutoReservationBusinessComponent();
            return DtoConverter.ConvertToDto(businessLayer.UpdateReservation(DtoConverter.ConvertToEntity(reservation)));
        }
    }
}
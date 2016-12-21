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

        private AutoReservationBusinessComponent _businessLayer = new AutoReservationBusinessComponent();

        public List<AutoDto> Autos
        {
            get
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDtos(_businessLayer.Autos);
            }
        }

        public List<KundeDto> Kunden
        {
            get
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDtos(_businessLayer.Kunden);
            }
        }

        public List<ReservationDto> Reservationen
        {
            get
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDtos(_businessLayer.Reservationen);
            }
        }


        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            _businessLayer.DeleteAuto(DtoConverter.ConvertToEntity(auto));
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            _businessLayer.DeleteKunde(DtoConverter.ConvertToEntity(kunde));
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            _businessLayer.DeleteReservation(DtoConverter.ConvertToEntity(reservation));
        }

        public AutoDto GetAutoById(int id)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(_businessLayer.GetAutoById(id));
        }

        public KundeDto GetKundeById(int id)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(_businessLayer.GetKundeById(id));
        }

        public ReservationDto GetReservationByNr(int reservationNr)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(_businessLayer.GetReservationByNr(reservationNr));
        }

        public AutoDto InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(_businessLayer.InsertAuto(DtoConverter.ConvertToEntity(auto)));
        }

        public KundeDto InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(_businessLayer.InsertKunde(DtoConverter.ConvertToEntity(kunde)));
        }

        public ReservationDto InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(_businessLayer.InsertReservation(DtoConverter.ConvertToEntity(reservation)));
        }

        public AutoDto UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            try
            {
                return DtoConverter.ConvertToDto(_businessLayer.UpdateAuto(DtoConverter.ConvertToEntity(auto)));
            }
            catch (LocalOptimisticConcurrencyException<Auto>)
            {
                throw new FaultException("Auto Update failed");
            }
        }

        public KundeDto UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            try
            {
                return DtoConverter.ConvertToDto(_businessLayer.UpdateKunde(DtoConverter.ConvertToEntity(kunde)));
            }
            catch (LocalOptimisticConcurrencyException<Kunde>)
            {
                throw new FaultException("Kunde Update failed");
            }

        }

        public ReservationDto UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                return DtoConverter.ConvertToDto(_businessLayer.UpdateReservation(DtoConverter.ConvertToEntity(reservation)));
            }
            catch (LocalOptimisticConcurrencyException<Reservation>)
            {
                throw new FaultException("Reservation update failed");
            }
            
        }
    }
}
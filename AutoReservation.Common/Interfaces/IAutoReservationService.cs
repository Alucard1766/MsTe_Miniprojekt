using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        [OperationContract]
        List<AutoDto> getAllAuto();

        [OperationContract]
        List<KundeDto> getAllKunde();

        [OperationContract]
        List<ReservationDto> getAllReservation();



        [OperationContract]
        AutoDto getAuto(int id);

        [OperationContract]
        KundeDto getKunde(int id);

        [OperationContract]
        ReservationDto getReservation(int id);


        [OperationContract]
        void insertAuto(AutoDto auto);
        [OperationContract]
        void insertKunde(KundeDto kunde);
        [OperationContract]
        void insertReservation(ReservationDto reservation);

    }
}

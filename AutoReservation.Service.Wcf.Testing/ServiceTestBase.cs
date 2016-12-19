using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region Read all entities

        [TestMethod]
        public void GetAutosTest()
        {
            List<AutoDto> ret = Target.Autos;
            Assert.AreEqual(3, ret.Count);
        }

        [TestMethod]
        public void GetKundenTest()
        {
            List<KundeDto> ret = Target.Kunden;
            Assert.AreEqual(4, ret.Count);
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            List<ReservationDto> ret = Target.Reservationen;
            Assert.AreEqual(3, ret.Count);
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetAutoByIdTest()
        {
            AutoDto ret = Target.GetAutoById(1);
            Assert.AreEqual("Fiat Punto", ret.Marke);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            KundeDto ret = Target.GetKundeById(1);
            Assert.AreEqual("Nass", ret.Nachname);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            ReservationDto ret = Target.GetReservationByNr(1);
            Assert.AreEqual(new DateTime(2020, 01, 10), ret.Von);
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetAutoByIdWithIllegalIdTest()
        {
            AutoDto ret = Target.GetAutoById(42);
            Assert.IsNull(ret);
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
            KundeDto ret = Target.GetKundeById(42);
            Assert.IsNull(ret);
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
            ReservationDto ret = Target.GetReservationByNr(42);
            Assert.IsNull(ret);
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertAutoTest()
        {
            AutoDto auto = new AutoDto
            {
                AutoKlasse = AutoKlasse.Mittelklasse,
                Tagestarif = 100,
                Marke = "Test"
            };
            AutoDto ret = Target.InsertAuto(auto);
            Assert.AreEqual("Test", ret.Marke);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            KundeDto kunde = new KundeDto
            {
                Vorname = "Hans",
                Nachname = "Meier",
                Geburtsdatum = new DateTime(2000, 1, 1)
            };
            KundeDto ret = Target.InsertKunde(kunde);
            Assert.AreEqual("Hans", ret.Vorname);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            ReservationDto reservation = new ReservationDto
            {
                Bis = new DateTime(2016, 12, 24),
                Von = new DateTime(2016, 12, 20),
                Kunde = Target.GetKundeById(1),
                Auto = Target.GetAutoById(1)
            };
            ReservationDto ret = Target.InsertReservation(reservation);
            Assert.AreEqual(new DateTime(2016, 12, 20), ret.Von);
        }

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteAutoTest()
        {
            Target.DeleteAuto(Target.GetAutoById(2));
            Assert.AreEqual(2, Target.Autos.Count);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            Target.DeleteKunde(Target.GetKundeById(2));
            Assert.AreEqual(3, Target.Kunden.Count);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            Target.DeleteReservation(Target.GetReservationByNr(2));
            Assert.AreEqual(2, Target.Reservationen.Count);
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateAutoTest()
        {
            AutoDto auto = Target.Autos[0];
            auto.Marke = "Test";
            //AutoDto auto = new AutoDto
            //{
            //    Id = 1,
            //    Marke = "Test",
            //    Tagestarif = 60,
            //    AutoKlasse = AutoKlasse.Mittelklasse
            //};

            AutoDto ret = Target.UpdateAuto(auto);
            Assert.AreEqual("Test", ret.Marke);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            KundeDto kunde = new KundeDto
            {
                Id = 1,
                Nachname = "Nass",
                Vorname = "Ueli",
                Geburtsdatum = new DateTime(1981, 05, 05)
            };

            KundeDto ret = Target.UpdateKunde(kunde);
            Assert.AreEqual("Ueli", ret.Vorname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            ReservationDto reservation = new ReservationDto
            {
                ReservationsNr = 1,
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 30)
            };
            ReservationDto ret = Target.UpdateReservation(reservation);
            Assert.AreEqual(new DateTime(2020, 01, 30), ret.Bis);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            AutoDto auto1 = Target.GetAutoById(1);
            auto1.Marke = "Marke1";

            AutoDto auto2 = Target.GetAutoById(1);
            auto2.Marke = "Marke2";

            Target.UpdateAuto(auto2);
            Target.UpdateAuto(auto1);
        }

        [TestMethod]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion
    }
}

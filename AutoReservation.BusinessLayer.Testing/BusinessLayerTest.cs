using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {

        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }
        
        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void UpdateAutoTest()
        {
            Auto auto = new StandardAuto
            {
                Id = 1,
                Marke = "Test",
                Tagestarif = 60
            };

            Auto ret = Target.UpdateAuto(auto);
            Assert.AreEqual("Test", ret.Marke);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Kunde kunde = new Kunde
            {
                Id = 1,
                Nachname = "Nass",
                Vorname = "Ueli",
                Geburtsdatum = new DateTime(1981, 05, 05)
            };

            Kunde ret = Target.UpdateKunde(kunde);
            Assert.AreEqual("Ueli", ret.Vorname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Reservation reservation = new Reservation
            {
                ReservationsNr = 1,
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 30)
            };
            Reservation ret = Target.UpdateReservation(reservation);
            Assert.AreEqual(new DateTime(2020, 01, 30), ret.Bis);
        }

    }

}

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
            Auto auto = Target.Autos[0];
            auto.Marke = "Test";
            //Auto auto = new StandardAuto
            //{
            //    Id = 1,
            //    Marke = "Test",
            //    Tagestarif = 60
            //};

            Auto ret = Target.UpdateAuto(auto);
            Assert.AreEqual("Test", ret.Marke);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Kunde kunde = Target.Kunden[0];
            kunde.Vorname = "Ueli";

            Kunde ret = Target.UpdateKunde(kunde);
            Assert.AreEqual("Ueli", ret.Vorname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Reservation reservation = Target.Reservationen[1];
            reservation.Bis = new DateTime(2020, 01, 30);

            Reservation ret = Target.UpdateReservation(reservation);
            Assert.AreEqual(new DateTime(2020, 01, 30), ret.Bis);
        }

    }

}

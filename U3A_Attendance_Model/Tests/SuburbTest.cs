using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model.Tests
{
    [TestClass]
    public class SuburbTest
    {
        Facade _facade = new Facade();

        [TestMethod]
        public void FetchSuburbs()
        {
            IEnumerable<ISuburb> suburbs = _facade.FetchSuburbs(new Guid("031430c0-8abb-4f5d-8a6a-f1dd938d9776"));

            Assert.IsTrue(suburbs.Count() > 0, "Unable to fetch suburbs");
        }

        [TestMethod]
        public void CreateVenue()
        {
            string expectedVenueName = "Dorris RSL Club";

            IVenue venue = _facade.CreateVenue(new Guid("031430c0-8abb-4f5d-8a6a-f1dd938d9776"),
                new Guid("FDDCEB39-2204-4750-A36E-E9F52393CAA5"),
                expectedVenueName, "123 Harris St", "A");

            Assert.AreEqual(expectedVenueName, venue.Name);
        }

        [TestMethod]
        public void UpdateVenue()
        {
            string expectedVenueName = "Borris RSL Club";

            IVenue venue = _facade.UpdateVenue(new Guid("9DE28C60-30BA-4E7B-9268-2FDC66217B37"),
                new Guid("031430c0-8abb-4f5d-8a6a-f1dd938d9776"),
                new Guid("FDDCEB39-2204-4750-A36E-E9F52393CAA5"),
                expectedVenueName, "123 Harris St", "A");

            Assert.AreEqual(expectedVenueName, venue.Name, true, "Venue name hasn't been updated");
        }

        //[TestMethod]
        //public void FetchVenue()
        //{
        //    string expectedVenueName = "Borris RSL Club";

        //    IVenue venue = _facade.FetchVenue(new Guid("9DE28C60-30BA-4E7B-9268-2FDC66217B37"),
        //        new Guid("031430c0-8abb-4f5d-8a6a-f1dd938d9776"),
        //        new Guid("FDDCEB39-2204-4750-A36E-E9F52393CAA5"));

        //    Assert.AreEqual(expectedVenueName, venue.Name, true, "Venue wasn't able to be fetched");
        //}

        [TestMethod]
        public void FetchAllVenues()
        {
            IEnumerable<IVenue> venues = _facade.FetchVenues(new Guid("031430c0-8abb-4f5d-8a6a-f1dd938d9776"),
                new Guid("FDDCEB39-2204-4750-A36E-E9F52393CAA5"));

            Assert.IsTrue(venues.Count() > 0, "Unable to fetch all venues for suburb");
        }
    }
}

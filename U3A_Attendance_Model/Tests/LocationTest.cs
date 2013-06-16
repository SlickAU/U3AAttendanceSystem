using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model.Tests
{
    [TestClass]
    public class LocationTest
    {
        Facade _facade = new Facade();

        [TestMethod]
        public void FetchLocation()
        {
            string expectedName = "UNDEFINED";

            ILocation location = _facade.FetchLocation(new Guid("031430c0-8abb-4f5d-8a6a-f1dd938d9776"),
                new Guid("FDDCEB39-2204-4750-A36E-E9F52393CAA5"),
                new Guid("7a745672-cdb3-4e7f-9644-9eab47f4527d"),
                new Guid("721abdc2-709c-4b1c-9f2c-438ef9e6c2d4"));

            Assert.AreEqual(expectedName, location.Room, true, "Values are not as the same");
        }

        [TestMethod]
        public void UpdateLocation()
        {
            string expectedRoomName = "Dining Hall";

            ILocation location = _facade.UpdateLocation(new Guid("031430c0-8abb-4f5d-8a6a-f1dd938d9776"),
                new Guid("FDDCEB39-2204-4750-A36E-E9F52393CAA5"),
                new Guid("7a745672-cdb3-4e7f-9644-9eab47f4527d"),
                new Guid("721abdc2-709c-4b1c-9f2c-438ef9e6c2d4"),
                expectedRoomName);

            Assert.AreEqual(expectedRoomName, location.Room, "Values are not the same");
        }
    }
}

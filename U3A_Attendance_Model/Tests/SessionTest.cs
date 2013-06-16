using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model.Tests
{
    [TestClass]
    public class SessionTest
    {
        Facade _facade = new Facade();

        [TestMethod]
        public void FetchSessions()
        {
            IEnumerable<ISession> sessions = _facade.FetchSessions(new Guid("61b78cb6-8ec2-4214-95cf-9f62594ff67f"),
                new Guid("4b305313-ed11-4366-86f0-0449a52226c1"));

            Assert.IsTrue(sessions.Count() > 0, "Unable to fetch sessions");
        }

        [TestMethod]
        public void CreateAttendance()
        {
            string exectedPresence = "A";
            IMember member = _facade.FetchMember(1000);

            IAttendance attendance = _facade.CreateAttendance(new Guid("4b305313-ed11-4366-86f0-0449a52226c1"),
                new Guid("61b78cb6-8ec2-4214-95cf-9f62594ff67f"),
                new Guid("84adab56-0859-4b82-b8d3-0c2f000a15bb"),
                1000, exectedPresence);

            Assert.AreEqual(exectedPresence, attendance.Presence, true, "Presence Values are no the same");
        }

        [TestMethod]
        public void FetchAttendance()
        {
            IAttendance attendance = _facade.FetchAttendance(new Guid("4b305313-ed11-4366-86f0-0449a52226c1"),
                new Guid("61b78cb6-8ec2-4214-95cf-9f62594ff67f"),
                new Guid("84adab56-0859-4b82-b8d3-0c2f000a15bb"),
                new Guid("009d619e-538d-4c57-9e41-dc6f94577654"));

            Assert.IsTrue(attendance != null, "Attendance could not be located");
        }

        [TestMethod]
        public void FetchAllAttendances()
        {
            IEnumerable<IAttendance> attendances = _facade.FetchAttendances(new Guid("4b305313-ed11-4366-86f0-0449a52226c1"),
                new Guid("61b78cb6-8ec2-4214-95cf-9f62594ff67f"),
                new Guid("84adab56-0859-4b82-b8d3-0c2f000a15bb"));

            Assert.IsTrue(attendances.Count() > 0, "Unable to fetch attendances");
        }

        //[TestMethod]
        //public void UpdateAttendances()
        //{
        //    string expectedPresence = "Absent";

        //    IAttendance attendance = _facade.UpdateAttendance(new Guid("4b305313-ed11-4366-86f0-0449a52226c1"),
        //        new Guid("61b78cb6-8ec2-4214-95cf-9f62594ff67f"),
        //        new Guid("496be72e-03b6-4ce0-b0a1-56e5f327e243"),
        //        new Guid("84adab56-0859-4b82-b8d3-0c2f000a15bb"),
        //        1000, expectedPresence);

        //    Assert.AreEqual(expectedPresence, attendance.Presence, true, "Attendance Presence couldn't be updated");
        //}
    }
}

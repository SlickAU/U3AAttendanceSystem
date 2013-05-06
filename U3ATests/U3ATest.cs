using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using U3A_Attendance_Model;

namespace U3ATests
{
    [TestClass]
    public class U3ATest
    {
        MockFacade fc = new MockFacade();

        [TestMethod]
        public void CreateCourseDescription()
        {

            String expectedTitle = "Test Title";
            String expectedDescription = "";
            ICourseDescription expected = fc.CreateCourseDescription(expectedDescription, expectedTitle);
            ICourseDescription actual = fc.FetchCourseDescription(expected.Id);

            Assert.AreEqual(expected.Title, actual.Title, "Not Happy");
        }


        [TestMethod]
        public void UpdateCourseDescription()
        {
            String title = "Title was updated";
            String description = "Description was updated";

            ICourseDescription expected = fc.UpdateCourseDescription(new Guid("91cf2fc9-5a53-41f7-9bd6-702edb4e3131"), title, description);
            ICourseDescription actual = fc.FetchCourseDescription(expected.Id);

            Assert.AreSame(expected, actual, "Object are the same");
        }

        [TestMethod]
        public void CreateCourseInstance()
        {
            String courseCode = "13JH031";

            var expected = fc.CreateCourseInstance(new Guid("8b007e15-7f0d-4c63-8ee3-0031ef68be4d"),
                                                   new Guid("7e29f82a-8c48-49d7-900f-93c244a67f6d"),
                                                   new Guid("4B305313-ED11-4366-86F0-0449A52226C1"),
                                                   new Guid("3922F5EA-A4D7-4197-825E-290FC0175A90"),
                                                   new Guid("a45a90db-cb7a-4fc1-8512-0719b406ea50"),
                                                   new Guid("e6455072-4834-48bf-92f1-43f5c95f41c4"),
                                                   new DateTime(2014, 4, 15), courseCode);
            var actual = fc.FetchCourseInstance(expected.Id, new Guid("4B305313-ED11-4366-86F0-0449A52226C1"));

            Assert.AreEqual(expected.CourseCode, actual.CourseCode, "Not happy");
        }

        [TestMethod]
        public void UpdateCourseInstance()
        {
            String courseCode = "14HH134";

            var expected = fc.UpdateCourseInstance(new Guid("06bcb06d-1891-4f45-a1b8-35f2fc8c5038"),
                                                   new Guid("7e29f82a-8c48-49d7-900f-93c244a67f6d"),
                                                   new Guid("4B305313-ED11-4366-86F0-0449A52226C1"),
                                                   new Guid("3922F5EA-A4D7-4197-825E-290FC0175A90"),
                                                   new Guid("a45a90db-cb7a-4fc1-8512-0719b406ea50"),
                                                   new Guid("e6455072-4834-48bf-92f1-43f5c95f41c4"),
                                                   new DateTime(2014, 3, 10), 1, courseCode);

            var actual = fc.FetchCourseInstance(new Guid("06bcb06d-1891-4f45-a1b8-35f2fc8c5038"), new Guid("4B305313-ED11-4366-86F0-0449A52226C1"));

            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public void CreateVenue()
        {
            string name = "Test Venue";

            var expected = fc.CreateVenue(new Guid("4B305313-ED11-4366-86F0-0449A52226C1"), new Guid("3922F5EA-A4D7-4197-825E-290FC0175A90"), name, "Test Address", "F");

            var actual = fc.FetchVenue(expected.Id, new Guid("4B305313-ED11-4366-86F0-0449A52226C1"), new Guid("3922F5EA-A4D7-4197-825E-290FC0175A90"));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateVenue()
        {
            string name = "Test Venue";

            var expected = fc.UpdateVenue(new Guid("a45a90db-cb7a-4fc1-8512-0719b406ea60"), new Guid("4B305313-ED11-4366-86F0-0449A52226C1"), new Guid("3922F5EA-A4D7-4197-825E-290FC0175A90"), name, "Updated Test Address", "F");

            var actual = fc.FetchVenue(expected.Id, new Guid("4B305313-ED11-4366-86F0-0449A52226C1"), new Guid("a45a90db-cb7a-4fc1-8512-0719b406ea60"));

            Assert.AreEqual(expected, actual);
        }
    }
}

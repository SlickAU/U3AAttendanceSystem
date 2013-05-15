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

            ICourseDescription expected = fc.UpdateCourseDescription(new Guid("B2AFDDF5-32EC-4AF5-87B5-2D97224EFF90"), title, description);
            ICourseDescription actual = fc.FetchCourseDescription(expected.Id);

            Assert.AreSame(expected, actual, "Object are the same");
        }

        [TestMethod]
        public void CreateCourseInstance()
        {
            String courseCode = "13JH031";

            var expected = fc.CreateCourseInstance(new Guid("8b007e15-7f0d-4c63-8ee3-0031ef68be4d"),
                                                   new Guid("4B305313-ED11-4366-86F0-0449A52226C1"),
                                                   new Guid("7e29f82a-8c48-49d7-900f-93c244a67f6d"),
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
                                                   new Guid("4B305313-ED11-4366-86F0-0449A52226C1"),
                                                   new Guid("7e29f82a-8c48-49d7-900f-93c244a67f6d"),
                                                   new Guid("e6455072-4834-48bf-92f1-43f5c95f41c4"),
                                                   new DateTime(2014, 3, 10), courseCode);

            var actual = fc.FetchCourseInstance(new Guid("e6455072-4834-48bf-92f1-43f5c95f41c4"), new Guid("4B305313-ED11-4366-86F0-0449A52226C1"));

            Assert.AreSame(expected, actual);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using U3A_Attendance_Model;

namespace WcfServiceLibrary
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        string GetData(int courseNumber);
    }

    [DataContract]
    public class CompositeType
    {
        int courseNumber = 0;
        private string title  = "Empty Title";
        private string description = "Empty Description";


        [DataMember]
        public int CourseNumber
        {
            get { return courseNumber; }
            set { courseNumber = value; }
        }

        [DataMember]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}

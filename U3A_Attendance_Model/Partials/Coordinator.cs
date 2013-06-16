using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    [Serializable]
    internal partial class Coordinator : Teacher, ICoordinator
    {
        Coordinator() { }

        public Coordinator(string name, string email, string phoneNumber, Guid u3aId)
        {
            Name = name;
            Email = email;
            Phone = phoneNumber;
        }

        internal Coordinator update(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            Phone = phoneNumber;
            return this;
        }

        internal void delete(Action<Coordinator> action)
        {
            if (this.CourseInstances.Count() > 0)
            {
                throw new BusinessRuleException("Coordinator cannot be deleted as it is associated with other records");
            }
            action(this);
        }
    }
}

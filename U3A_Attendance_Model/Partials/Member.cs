using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model.Interfaces;

namespace U3A_Attendance_Model
{[Serializable]
    internal partial class Member : IMember, ISearchable 
    {
        
        public Member(int memberId, Guid u3aId)
        {
            MemberId = memberId;
            U3AId = u3aId;
        }

        #region Member Management

        //Updates Member
        internal Member update(Guid u3aId)
        {
            U3AId = u3aId;

            return this;
        }

        //Deletes Member
        internal void delete(Action<Member> action)
        {
            if (this.Attendances.Count > 0)
            {
                throw new BusinessRuleException("Member is associated with attendance records and cannot be Deleted");
            }

            action(this);
        } 

        #endregion

        public bool MeetsCritera(string keyword)
        {
            if (this._memberid.ToString().ToLower().Trim().Contains(keyword))
            {
                return true; 
            }

            return false;
        }
    }
}

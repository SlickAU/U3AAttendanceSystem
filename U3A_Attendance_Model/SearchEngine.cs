using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using U3A_Attendance_Model.Interfaces;

namespace U3A_Attendance_Model
{
    internal class SearchEngine
    {

        public bool Search(ISearchable target, string keyword)
        {
           return  keyword != null && keyword != string.Empty && target != null? target.MeetsCritera(keyword.Trim().ToLower()) : false; 
        }


        public IEnumerable<ISearchable> Search(IEnumerable<ISearchable> targets, string keyword)
        {
            var result = new List<ISearchable>(); 
            foreach (var t in targets)
            {
                if(Search(t,keyword))
                {
                    result.Add(t); 
                }
            }
            return result.AsEnumerable(); 
        }


        public IEnumerable<ISearchable> Search(IEnumerable<ISearchable> targets, string[] keywords)
        {
            var result = new List<ISearchable>();
            foreach (var t in targets)
            {

                foreach (var key in keywords)
                {
                    if (Search(t, key))
                    {
                        result.Add(t);
                    }
                }             
            }
            return result.AsEnumerable();
        }






    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPM_3_Course.Models
{
    public class FilterViewModel
    {
        public int? SelectId { get; private set; }
        public string SelectEmail { get; private set; }

        public FilterViewModel (int? id, string email )
        {
            SelectEmail = email;
            SelectId = id;
        }
    }
}

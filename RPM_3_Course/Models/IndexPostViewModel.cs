using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPM_3_Course.Models
{
    public class IndexPostViewModel
    {
        public IEnumerable<Post> Post { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPM_3_Course.Models
{
    public class IndexPictureViewModel
    {
        public IEnumerable<Picturee> Picturee { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }

    }
}

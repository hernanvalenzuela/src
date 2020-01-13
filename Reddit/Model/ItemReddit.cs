using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Model
{
    public class ItemReddit
    {
        public string ImgUrl { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double EntryDate { get; set; }
        public int NumberOfComments { get; set; }

        public string Status { get; set; }

    }
}

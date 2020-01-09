using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Model
{
    public class MediaEmbed
    {
        public string content { get; set; }
        public int? width { get; set; }
        public bool? scrolling { get; set; }
        public int? height { get; set; }
    }
}

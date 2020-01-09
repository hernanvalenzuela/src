using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Model
{

    public class RootObject
    {
        public string kind { get; set; }
        public Data data { get; set; }
    }
}

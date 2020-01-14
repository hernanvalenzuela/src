using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Model
{
    public partial class Data2
    {
       public string StringComments {  get { return String.Format("Comments {0}", this.num_comments); } }
    }
}

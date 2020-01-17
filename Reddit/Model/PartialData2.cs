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
        public String createdUTD { get {
                var timeSpan = TimeSpan.FromMinutes(this.created_utc);
                string s = timeSpan.Hours == 0 ? timeSpan.Minutes.ToString() + " Minutes Ago" : timeSpan.Hours.ToString() + " Hours Ago";
                return s;
            } }
    }
}

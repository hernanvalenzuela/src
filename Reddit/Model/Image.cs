using System.Collections.Generic;

namespace Reddit.Model
{
    public class Image
    {
        public Source source { get; set; }
        public List<Resolution> resolutions { get; set; }
        public Variants variants { get; set; }
        public string id { get; set; }
    }
}

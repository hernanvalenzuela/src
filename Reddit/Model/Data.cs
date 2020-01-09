using System.Collections.Generic;

namespace Reddit.Model
{
    public class Data
    {
        public string modhash { get; set; }
        public int dist { get; set; }
        public List<Child> children { get; set; }
        public string after { get; set; }
        public string before { get; set; }
    }
}

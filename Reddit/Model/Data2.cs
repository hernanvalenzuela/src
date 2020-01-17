using System.Collections.Generic;

namespace Reddit.Model
{
    public partial class Data2
    {
        public string domain { get; set; }
        public object banned_by { get; set; }
        public MediaEmbed media_embed { get; set; }
        public string subreddit { get; set; }
        public string selftext_html { get; set; }
        public string selftext { get; set; }
        public object likes { get; set; }
        public List<object> user_reports { get; set; }
        public SecureMedia secure_media { get; set; }
        public string link_flair_text { get; set; }
        public string id { get; set; }
        public int gilded { get; set; }
        public SecureMediaEmbed secure_media_embed { get; set; }
        public bool clicked { get; set; }
        public object report_reasons { get; set; }
        public string author { get; set; }
        public Media media { get; set; }
        public int score { get; set; }
        public object approved_by { get; set; }
        public bool over_18 { get; set; }
        public bool hidden { get; set; }
        public string thumbnail { get; set; }
        public string subreddit_id { get; set; }
        public object edited { get; set; }
        public string link_flair_css_class { get; set; }
        public object author_flair_css_class { get; set; }
        public int downs { get; set; }
        public List<object> mod_reports { get; set; }
        public bool saved { get; set; }
        public bool is_self { get; set; }
        public string name { get; set; }
        public string permalink { get; set; }
        public bool stickied { get; set; }
        public double created { get; set; }
        public string url { get; set; }
        public object author_flair_text { get; set; }
        public string title { get; set; }
        public double created_utc { get; set; }
        public int ups { get; set; }
        public int num_comments { get; set; }
        public bool visited { get; set; }
        public object num_reports { get; set; }
        public object distinguished { get; set; }
    }
}

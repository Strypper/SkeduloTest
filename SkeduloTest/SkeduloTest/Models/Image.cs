using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeduloTest.Models
{
    [Table("Image")]
    public class Image
    {
        public string id { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }
}

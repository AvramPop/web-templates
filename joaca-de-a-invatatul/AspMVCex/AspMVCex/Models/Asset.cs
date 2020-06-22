using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMVCex.Models
{
    public class Asset
    {
        public int id { get; set; }

        public int userId { get; set; }

        public int value { get; set; }


        public string name { get; set; }
        public string description { get; set; }
    }
}
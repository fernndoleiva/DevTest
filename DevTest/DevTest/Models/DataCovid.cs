using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevTest.Models
{
    public class DataCovid
    {
        public string Id { get; set; }

        public int NoCasos { get; set; }

        public int NoMuertes { get; set; }
        public string iso { get;  set; }

        public DataCovid() { }


    }
}
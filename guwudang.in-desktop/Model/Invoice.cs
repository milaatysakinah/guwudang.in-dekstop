using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guwudang.Model
{
    public class Invoice
    {
        public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string created_at { get; set; }
        public string total { get; set; }
    }
}

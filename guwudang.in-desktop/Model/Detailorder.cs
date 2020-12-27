using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guwudang.Model
{
    public class Detailorder
    {
        public string id { get; set; }
        public string product_name { get; set; }
        public string price { get; set; }
        public string order_quantity { get; set; }
        public string product_picture { get; set; }
        public string transaction_name { get; set; } 
        public string id_order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guwudang.Model
{
    public class OrderItemGet
    {
        public string id { get; set; }
        public string invoice_id { get; set; }
        public string product_id { get; set; }
        public string transaction_type_id { get; set; }
        public string transaction_date { get; set; }
        public string order_quantity { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}

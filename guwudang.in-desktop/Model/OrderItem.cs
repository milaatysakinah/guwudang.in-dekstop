using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guwudang.Model
{
    class OrderItem
    {
        public string id { get; set; }
        public string invoice_id { get; set; }
        public string product_id { get; set; }
        public string transaction_type_id { get; set; }
        public string transaction_date { get; set; }
        public string order_quantity { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string partner_id { get; set; }
        public string user_id { get; set; }
        public string status_invoice_id { get; set; }

    }
}

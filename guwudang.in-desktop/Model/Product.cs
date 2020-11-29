using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guwudang.Model
{
    public class Product
    {
        public string id { get; set; }
        public string product_type_id { get; set; }
        public string user_id { get; set; }
        public string product_name { get; set; }
        public string price { get; set; }
        public string units { get; set; }
        public string description { get; set; }
        public string product_picture { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}

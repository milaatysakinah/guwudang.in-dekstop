﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guwudang.Model
{
    public class Partner
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string name { get; set; }

        public string email { get; set; }
        public string address { get; set; }
        public string phone_number { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}

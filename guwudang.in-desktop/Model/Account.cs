using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guwudang.Model
{
    public class Account
    {
        public string id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string company_name { get; set; }
        public string profile_picture { get; set; }
        public string password { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}

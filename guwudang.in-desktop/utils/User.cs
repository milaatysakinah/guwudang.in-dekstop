using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guwudang.utils
{
    public class User
    {
        private static String token;

        public void initializeToken(String token)
        {
            setToken(token); 

        }

        public void setToken(String parameter)
        {
            token = parameter;
        }

        public String getToken()
        {
            return token;
        }
    }
}

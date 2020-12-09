using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using System.Collections.Generic;
using guwudang.utils;
using System;
using System.Windows.Controls;

namespace guwudang.Account
{
    public class AccountPageController : MyController
    {
        private static string id;
        public AccountPageController(IMyView _myView) : base(_myView)
        {
            //getData();
        }

        public async void getAccountData()
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var reqAccount = request
                .buildHttpRequest()
                .setEndpoint("api/authUser")
                .setRequestMethod(HttpMethod.Get);


        }
    }
}

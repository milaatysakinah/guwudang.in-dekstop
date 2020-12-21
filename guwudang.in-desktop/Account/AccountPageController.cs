using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
#pragma warning disable CS0105 // The using directive for 'System.Collections.Generic' appeared previously in this namespace
using System.Collections.Generic;
#pragma warning restore CS0105 // The using directive for 'System.Collections.Generic' appeared previously in this namespace
using guwudang.utils;
#pragma warning disable CS0105 // The using directive for 'System' appeared previously in this namespace
using System;
#pragma warning restore CS0105 // The using directive for 'System' appeared previously in this namespace
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
            client.setOnSuccessRequest(setViewAccount);
            var response = await client.sendRequest(request.getApiRequestBundle());

        }

        private void setViewAccount(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("Account : " + _response.getHttpResponseMessage().ReasonPhrase);
                Model.Account account = new Model.Account();
                account.id = _response.getJObject()["user"]["id"].ToString();
                account.email = _response.getJObject()["user"]["email"].ToString();
                account.username = _response.getJObject()["user"]["username"].ToString();
                account.company_name = _response.getJObject()["user"]["company_name"].ToString();
                account.phone_number = _response.getJObject()["user"]["phone_number"].ToString();
                account.profile_picture = _response.getJObject()["user"]["profile_picture"].ToString();
                //account.password = _response.getJObject()["user"]["password"].ToString();
                account.created_at = _response.getJObject()["user"]["created_at"].ToString();
                account.updated_at = _response.getJObject()["user"]["updated_at"].ToString();
                
                getView().callMethod("setAccountPage", account);
            }
        }
    }
}

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
using System.IO;

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

        public async void updateData(Model.Account account, MyList<MyFile> fileImage)
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();
            String endpoint = "api/users/" + account.id;

            //FileStream fileStream = File.OpenRead(account.profile_picture);
            //HttpContent fileStreamContent = new StreamContent(fileStream);

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(account.email), "email");
            content.Add(new StringContent(account.username), "username");
            content.Add(new StringContent(account.company_name), "company_name");
            content.Add(new StringContent(account.phone_number), "phone_number");
            content.Add(new StringContent(account.profile_picture), "profile_picture");
            content.Add(new StringContent("PUT"), "_method");
            if (fileImage.Count > 0)
                content.Add(new StreamContent(new MemoryStream(fileImage[0].byteArray)), "file", fileImage[0].fullFileName);

            var multiPartRequest = request
            .buildMultipartRequest(new MultiPartContent(content))
            .setEndpoint(endpoint)
            .setRequestMethod(HttpMethod.Post);

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            client.setOnFailedRequest(setViewFailed);
            client.setOnSuccessRequest(setViewSuccess);
            
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewFailed(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("setStatusError" + _response.getHttpResponseMessage().ReasonPhrase.ToString());
                getView().callMethod("setEditFailed", _response.getHttpResponseMessage().ReasonPhrase.ToString());
            }
        }

        private void setViewSuccess(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("updateSuccess");
                getView().callMethod("setEditSuccess");

            }
        }

        private void setViewSuccessEditAccount(HttpResponseBundle _response)
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

                getView().callMethod("setEditSuccess");
            }
        }
    }
}

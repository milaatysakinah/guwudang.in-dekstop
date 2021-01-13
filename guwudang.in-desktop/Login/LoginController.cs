using guwudang.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;

namespace guwudang.Login {
    public class LoginController : MyController{
        public LoginController(IMyView _myView) : base(_myView){
            
        }

        public async void login(string _email, string _password) {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("email", _email)
                .addParameters("password", _password)
                .setEndpoint("api/login/")
                .setRequestMethod(HttpMethod.Post);
            //client.setOnSuccessRequest(setViewLoginStatus);
            client.setOnFailedRequest(setViewLoginStatus);
            

            try
            {
                var response = await client.sendRequest(request.getApiRequestBundle());

                if (response.getHttpResponseMessage().ReasonPhrase.Equals("OK"))
                {
                    Console.WriteLine(response.getJObject()["token"]);
                    client.setAuthorizationToken(response.getJObject()["token"].ToString());
                    User user = new User();
                    user.setToken(response.getJObject()["token"].ToString());
                    getView().callMethod("changeToDashboard", "1");
                }
            }
            catch (Exception e)
            {
                getView().callMethod("setLoginStatus", "Server Closed");
            }  
        }

        private void setViewLoginStatus(HttpResponseBundle _response){
            if (_response.getHttpResponseMessage().Content != null) {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setLoginStatus", "Username/Password is Wrong");
            }
        }
    }
}

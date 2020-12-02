using guwudang.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Velacro.Api;
using Velacro.Basic;

namespace guwudang.Register {
    public class RegisterController : MyController{
        public RegisterController(IMyView _myView) : base(_myView) { }

        public async void register(
            string _name, 
            string _email,
            string _phoneNumber,
            string _address,
            string _companyName,
            string _password, 
            string _passwordc) {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();

                //string token = "";
                var req = request
                    .buildHttpRequest()
                    .addParameters("username", _name)
                    .addParameters("email", _email)
                    .addParameters("phoneNumber", _phoneNumber)
                    .addParameters("address", _address)
                    .addParameters("companyName", _companyName)
                    .addParameters("password", _password)
                    .addParameters("password_confirmation", _passwordc)
                    .setEndpoint("api/register/")
                    .setRequestMethod(HttpMethod.Post);
                client.setOnSuccessRequest(setViewRegisterStatus);
                client.setOnFailedRequest(setViewRegisterStatus);
                var response = await client.sendRequest(request.getApiRequestBundle());

            if (response.getHttpResponseMessage().ReasonPhrase.Equals("Created"))
            {
                User user = new User();
                user.setToken(response.getJObject()["token"].ToString());
                getView().callMethod("toDummy", null);
            }
            
        }

        private void setViewRegisterStatus(HttpResponseBundle _response) {
            if (_response.getHttpResponseMessage().Content != null) {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setRegisterStatus", status);
            }
        }

        
    }
}

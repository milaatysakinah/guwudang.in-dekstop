using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using System.Collections.Generic;
using guwudang.utils;
using System;

namespace guwudang.Partner
{
    class EditPartnerController: MyController
    {
        private static User user = new User();
        public EditPartnerController(IMyView _myView) : base(_myView)
        {

        }

        public async void getPartner(string idPartner)
        {
            Console.WriteLine("Id Partner : " + idPartner);
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/partner/:id";

            _endpoint = _endpoint.Replace(":id", idPartner);
            Console.WriteLine(_endpoint);

            string token = user.getToken();
            client.setAuthorizationToken(token);

            var reqAccount = request
                .buildHttpRequest()
                .setEndpoint("api/authUser")
                .setRequestMethod(HttpMethod.Get);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewPartnerData);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewPartnerData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                guwudang.Model.OrderItemGet getList = _response.getParsedObject<guwudang.Model.OrderItemGet>();
                getView().callMethod("setPartner", getList);
            }
        }

        public async void UpdatePartner(string _companyName, string _email, string _phone,
            string _address, string _idPartner)
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/partner/" + _idPartner;

            string token = user.getToken();
            client.setAuthorizationToken(token);

            var reqAccount = request
                .buildHttpRequest()
                .setEndpoint("api/authUser")
                .setRequestMethod(HttpMethod.Get);

            var req = request
                .buildHttpRequest()
                .addParameters("companyName", _companyName)
                .addParameters("email", _email)
                .addParameters("phone", _phone)
                .addParameters("address", _address)
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Put);
            client.setOnSuccessRequest(onSuccessUpdatePartner);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void onSuccessUpdatePartner(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("onSuccess", status);
            }
        }


    }
}


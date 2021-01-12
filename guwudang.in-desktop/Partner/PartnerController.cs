using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using guwudang.utils;
using System.Collections.Generic;
using System;

namespace guwudang.Partner
{
    public class PartnerController : MyController
    {
        private static string id;

        public PartnerController(IMyView _myView) : base(_myView)
        {
            //getData();
        }

        public async void partner()
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/searchPartnerByUserID/?id=:id";

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var reqAccount = request
                .buildHttpRequest()
                .setEndpoint("api/authUser")
                .setRequestMethod(HttpMethod.Get);

            var response1 = await client.sendRequest(request.getApiRequestBundle());
            client.setOnFailedRequest(setFailedAuthorization);
            Console.WriteLine(response1.getHttpResponseMessage().Content);
            id = response1.getJObject()["user"]["id"].ToString();
            _endpoint = _endpoint.Replace(":id", id);
            //Console.WriteLine(_endpoint);

            var client2 = new ApiClient(utils.urls.BASE_URL);
            var request2 = new ApiRequestBuilder();

            client2.setAuthorizationToken(token);

            var req = request2
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client2.setOnSuccessRequest(setViewPartnerData);
            var response = await client2.sendRequest(request2.getApiRequestBundle());
        }

        public void searchPartner(string key)
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/searchPartner/?id=:id&search=:search";

            _endpoint = _endpoint.Replace(":id", id);
            _endpoint = _endpoint.Replace(":search", key);

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewPartnerData);
            var response = client.sendRequest(request.getApiRequestBundle());
        }

        public async void deletePartner(List<string> selectedItemsID)
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            foreach (string item in selectedItemsID)
            {
                //Console.WriteLine(item);
                string _endpoint = "api/partner/:id";

                _endpoint = _endpoint.Replace(":id", item);

                User user = new User();
                string token = user.getToken();
                client.setAuthorizationToken(token);

                var req = request
                    .buildHttpRequest()
                    .setEndpoint(_endpoint)
                    .setRequestMethod(HttpMethod.Delete);
                var response = client.sendRequest(request.getApiRequestBundle());
            }
            partner();
        }

        private void setViewPartnerData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setPartner", _response.getParsedObject<List<guwudang.Model.Partner>>());
            }
        }

        private void setFailedAuthorization(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                getView().callMethod("backToLogin");
            }
        }

    }
}

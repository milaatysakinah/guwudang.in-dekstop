using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using System.Collections.Generic;
using guwudang.utils;
using System;

namespace guwudang.Product
{
    public class ProductController : MyController
    {
        private static string id;
        private static User user = new User();

        public ProductController(IMyView _myView) : base(_myView) { }

        public async void product()
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();

            string token = user.getToken();
            client.setAuthorizationToken(token);

            var reqAccount = request
                .buildHttpRequest()
                .setEndpoint("api/authUser")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setSuccessAuthorization);
            client.setOnFailedRequest(setFailedAuthorization);
            var response1 = await client.sendRequest(request.getApiRequestBundle());

            Console.WriteLine(response1.getHttpResponseMessage().Content);
        }

        public async void nextProduct(string id)
        {
            string _endpoint = "api/searchProductByUserID/?id=:id";

            _endpoint = _endpoint.Replace(":id", id);

            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewProductData);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public void searchProduct(string key)
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/searchProduct/?id=:id&search=:search";

            _endpoint = _endpoint.Replace(":id", id);
            _endpoint = _endpoint.Replace(":search", key);

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewProductData);
            var response = client.sendRequest(request.getApiRequestBundle());
        }

        public async void deleteProduct(List<string> selectedItemsID)
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            foreach (string item in selectedItemsID)
            {
                string _endpoint = "api/product/:id";

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
            product();
        }

        private void setViewProductData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setProduct", _response.getParsedObject<List<guwudang.Model.Product>>());
            }
        }

        private void setFailedAuthorization(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                getView().callMethod("backToLogin");
            }
        }

        private void setSuccessAuthorization(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                id = _response.getJObject()["user"]["id"].ToString();
                nextProduct(id);
            }
        }
    }
}

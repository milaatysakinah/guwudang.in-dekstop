using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using System;
using System.Collections.Generic;

namespace guwudang.Detail
{
    public class DetailController : MyController
    {
        public DetailController(IMyView _myView) : base(_myView)
        {
        }

        public async void detail(string id)
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/product/?id=:idUser";

            utils.User user = new utils.User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var reqAccount = request
                .buildHttpRequest()
                .setEndpoint("api/authUser")
                .setRequestMethod(HttpMethod.Get);

            var response1 = await client.sendRequest(request.getApiRequestBundle());
            client.setOnFailedRequest(setFailedAuthorization);
            // Console.WriteLine(response1.getHttpResponseMessage().Content);
            string _idUser = response1.getJObject()["user"]["id"].ToString();

            _endpoint = _endpoint.Replace(":idUser", _idUser);
            _endpoint = _endpoint.Replace(":id", id);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewDetailData);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public async void product_detail(string idProduct)
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/productDetail/?id=:idProduct";

            utils.User user = new utils.User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var reqAccount = request
                .buildHttpRequest()
                .setEndpoint("api/authUser")
                .setRequestMethod(HttpMethod.Get);

            var response1 = await client.sendRequest(request.getApiRequestBundle());
            client.setOnFailedRequest(setFailedAuthorization);
            // Console.WriteLine(response1.getHttpResponseMessage().Content);
            string _idUser = response1.getJObject()["user"]["id"].ToString();

            _endpoint = _endpoint.Replace(":idUser", _idUser);
            _endpoint = _endpoint.Replace(":idProduct", idProduct);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewDetailProduct);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }


        private void setViewDetailData(HttpResponseBundle _response)
        {
            //Console.WriteLine(_response.getParsedObject<List<guwudang.Model.Product>>());
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                //Console.WriteLine(_response.getJObject().ToString());
                List<guwudang.Model.Product> detail = _response.getParsedObject<List<guwudang.Model.Product>>();
                getView().callMethod("setProduct", detail[0]);
            }
        }

        private void setViewDetailProduct(HttpResponseBundle _response)
        {
            //Console.WriteLine(_response.getParsedObject<guwudang.Model.ProductDetail>());
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                //Console.WriteLine(_response.getParsedObject<List<guwudang.Model.ProductDetail>>[0]);
                //Console.WriteLine(_response.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
                getView().callMethod("setDetailproduct", _response.getParsedObject<List<guwudang.Model.ProductDetail>>());
                //getView().callMethod("setDetailProduct", _response.getParsedObject<guwudang.Model.ProductDetails>().productDetail);
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

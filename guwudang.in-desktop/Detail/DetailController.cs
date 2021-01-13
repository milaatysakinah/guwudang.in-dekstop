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
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/product/:id";

            utils.User user = new utils.User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

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
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/productDetailByProductID/?search=:idProduct";

            utils.User user = new utils.User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

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
                Model.Product product = new Model.Product();
                product.id = _response.getJObject()["id"].ToString();
                product.product_type_id = _response.getJObject()["product_type_id"].ToString();
                product.user_id = _response.getJObject()["user_id"].ToString();
                product.product_name = _response.getJObject()["product_name"].ToString();
                product.price = _response.getJObject()["price"].ToString();
                product.units = _response.getJObject()["units"].ToString();
                product.description = _response.getJObject()["description"].ToString();
                product.product_picture = _response.getJObject()["product_picture"].ToString();
                product.created_at = _response.getJObject()["created_at"].ToString();
                product.updated_at = _response.getJObject()["updated_at"].ToString();

                getView().callMethod("setProduct", product);
            }
        }

        public async void getProductType(String id)
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/productType/:id";

            utils.User user = new utils.User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            _endpoint = _endpoint.Replace(":id", id);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewSuccessProductType);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewSuccessProductType(HttpResponseBundle _response)
        {
            //Console.WriteLine(_response.getParsedObject<List<guwudang.Model.Product>>());
            if (_response.getHttpResponseMessage().Content != null)
            {
                getView().callMethod("setProductType", _response.getJObject()["product_type_name"].ToString());
            }
        }

        public async void getUnit(String id)
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/units/:id";

            utils.User user = new utils.User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            _endpoint = _endpoint.Replace(":id", id);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewSuccessUnits);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewSuccessUnits(HttpResponseBundle _response)
        {
            //Console.WriteLine(_response.getParsedObject<List<guwudang.Model.Product>>());
            if (_response.getHttpResponseMessage().Content != null)
            {
                getView().callMethod("setUnit", _response.getJObject()["units_name"].ToString());
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

using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using System.Collections.Generic;
using guwudang.utils;
using System;
using System.IO;

namespace guwudang.Product
{
    class EditProductController : MyController
    {
        private static string id;
        private static User user = new User();
        public EditProductController(IMyView _myView) : base(_myView)
        {

        }

        public async void productType()
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/ProductType/";

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewProductTypeData);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewProductTypeData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setProductType", _response.getParsedObject<List<guwudang.Model.ProductType>>());
            }
        }

        public async void units()
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/units/")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewUnitsData);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewUnitsData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setUnits", _response.getParsedObject<List<guwudang.Model.Units>>());
            }
        }

        public async void getProduct(string idProduct)
        {
            Console.WriteLine("Id Product : " + idProduct);
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/product/:id";

            _endpoint = _endpoint.Replace(":id", idProduct);
            Console.WriteLine(_endpoint);

            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewProductData);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewProductData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                guwudang.Model.Product getList = _response.getParsedObject<guwudang.Model.Product>();
                getView().callMethod("setProduct", getList);
            }
        }

        public async void editProduct(Model.Product product, MyList<MyFile> fileImage)
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();
            string endpoint = "api/product/" + product.id;

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(product.product_name), "product_name");
            content.Add(new StringContent(product.product_type_id), "product_type_id");
            content.Add(new StringContent(product.units), "units");
            content.Add(new StringContent(product.description), "description");
            content.Add(new StringContent(product.product_picture), "product_picture");
            content.Add(new StringContent(product.price), "price");
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
                getView().callMethod("setStatusError", _response.getHttpResponseMessage().ReasonPhrase.ToString());
            }
        }

        private void setViewSuccess(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                getView().callMethod("createSuccess");
            }
        }
    }
}


















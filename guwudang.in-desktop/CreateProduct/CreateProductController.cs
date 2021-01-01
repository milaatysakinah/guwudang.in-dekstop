using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using System.Collections.Generic;
using guwudang.utils;
using System;
using System.IO;

namespace guwudang.CreateProduct
{
    class CreateProductController : MyController
    {
        public CreateProductController(IMyView _myView) : base(_myView)
        {

        }

        public async void createProduct(string _productName, string _stock, string _price,
            string _description, MyList<MyFile> fileImage)
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            //var formContent = new MultipartFormDataContent();
            //formContent.Add(new StringContent(_productName), "product_name");
            //formContent.Add(new StringContent("1"), "product_type_id");
            //formContent.Add(new StringContent(_stock), "stock");
            //formContent.Add(new StringContent(_price), "price");
            //formContent.Add(new StringContent(_description), "descripton");
            //formContent.Add(new StringContent("pict"), "product_picture");
            //formContent.Add(new StringContent("units"), "units");
            //if (fileImage.Count > 0)
            //    formContent.Add(new StreamContent(new MemoryStream(fileImage[0].byteArray)), "picture", fileImage[0].fullFileName);

            //var multiPartRequest = request
            //.buildMultipartRequest(new MultiPartContent(formContent))
            //.setEndpoint("api/product/")
            //.setRequestMethod(HttpMethod.Post);
            //client.setOnFailedRequest(setViewFailed);
            //client.setOnSuccessRequest(setViewSuccess);
            //var response = await client.sendRequest(request.getApiRequestBundle());

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/product/")
                .addParameters("product_name", _productName)
                .addParameters("product_type_id", "1")
                .addParameters("units", "kg")
                .addParameters("price", _price)
                .addParameters("description", _description)
                .addParameters("product_picture","test")
                .setRequestMethod(HttpMethod.Post);
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


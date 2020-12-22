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

        public async void detail()
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/product/1")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewDetailData);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public async void product_detail()
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/productDetail/?=1")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewDetailProduct);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }


        private void setViewDetailData(HttpResponseBundle _response)
        {
            Console.WriteLine(_response.getParsedObject<guwudang.Model.Product>());
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                Console.WriteLine(_response.getJObject().ToString());
                getView().callMethod("setProduct", _response.getParsedObject<guwudang.Model.Product>());
            }
        }

        private void setViewDetailProduct(HttpResponseBundle _response)
        {
            //Console.WriteLine(_response.getParsedObject<guwudang.Model.ProductDetail>());
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                Console.WriteLine(_response.getParsedObject<List<guwudang.Model.ProductDetail>>()[0]);
                Console.WriteLine(_response.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
                getView().callMethod("setDetailProduct", _response.getParsedObject<List<guwudang.Model.ProductDetail>>());
                //getView().callMethod("setDetailProduct", _response.getParsedObject<guwudang.Model.ProductDetails>().productDetail);
            }
        }
    }
}
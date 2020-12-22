using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using System;
using System.Collections.Generic;

namespace guwudang.DetailInvoice
{
    public class DetailInvoiceController : MyController
    {
        public DetailInvoiceController(IMyView _myView) : base(_myView)
        {
        }

        public async void Detailinvoice()
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/detail_invoice/?id=1&detail_invoice=1")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewDetailinvoice);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public async void Detailorder()
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/detail_order/?id=1&detail_order=1")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewDetailorder);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewDetailinvoice(HttpResponseBundle _response)
        {
            //Console.WriteLine(_response.getParsedObject<guwudang.Model.Detailinvoice>());
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                //Console.WriteLine(_response.getJObject().ToString());
               //getView().callMethod("setDetailinvoice", _response.getParsedObject<guwudang.Model.Detailinvoice>());
                List<guwudang.Model.Detailinvoice> detail = _response.getParsedObject<List<guwudang.Model.Detailinvoice>>();
                getView().callMethod("setdetailinvoice", detail[0]);

            }
        }

        private void setViewDetailorder(HttpResponseBundle _response)
        {
            //Console.WriteLine(_response.getParsedObject<guwudang.Model.ProductDetail>());
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                Console.WriteLine(_response.getParsedObject<List<guwudang.Model.Detailorder>>()[0]);
                Console.WriteLine(_response.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
                getView().callMethod("setDetailorder", _response.getParsedObject<List<guwudang.Model.Detailorder>>());
                //getView().callMethod("setDetailProduct", _response.getParsedObject<guwudang.Model.ProductDetails>().productDetail);
            }
        }
    }
}

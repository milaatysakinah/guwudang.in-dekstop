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

        public async void Detailinvoice(string _idInvoice)
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/detail_invoice/?id=:idUser&detail_invoice=:idInvoice";

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
            _endpoint = _endpoint.Replace(":idInvoice", _idInvoice);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewDetailinvoice);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public async void Detailorder(string _idInvoice)
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/detail_order/?id=:idUser&detail_order=:idInvoice";

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
            _endpoint = _endpoint.Replace(":idInvoice", _idInvoice);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
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
                //Console.WriteLine(_response.getParsedObject<List<guwudang.Model.Detailorder>>()[0]);
                //Console.WriteLine(_response.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
                getView().callMethod("setDetailorder", _response.getParsedObject<List<guwudang.Model.Detailorder>>());
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

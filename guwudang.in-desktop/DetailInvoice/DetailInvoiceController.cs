using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using System;
using System.Collections.Generic;

namespace guwudang.DetailInvoice
{
    public class DetailInvoiceController : MyController
    {
        private static utils.User user = new utils.User();
        private string id_invoice;

        public DetailInvoiceController(IMyView _myView) : base(_myView)
        {
        }

        public async void Detailinvoice(string _idInvoice)
        {
            this.id_invoice = _idInvoice;
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/detail_invoice/?id=:idUser&detail_invoice=:idInvoice";

            string token = user.getToken();
            client.setAuthorizationToken(token);

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
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/detail_order/?id=:idUser&id_invoice=:idInvoice";

            string token = user.getToken();
            client.setAuthorizationToken(token);

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
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                List<guwudang.Model.Detailinvoice> detail = _response.getParsedObject<List<guwudang.Model.Detailinvoice>>();
                getView().callMethod("setdetailinvoice", detail[0]);

            }
        }

        private void setViewDetailorder(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setDetailorder", _response.getParsedObject<List<guwudang.Model.Detailorder>>());
            }
        }

        public async void delOrder(string id_order)
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();

            string _endpoint = "api/orderitem/" + id_order;

            string token = user.getToken();
            client.setAuthorizationToken(token);

            Console.WriteLine("Delete " + _endpoint);
            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Delete);
            var response = await client.sendRequest(request.getApiRequestBundle());
            Detailorder(id_invoice);
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

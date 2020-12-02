using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using System.Collections.Generic;


namespace guwudang.Invoice
{
    public class InvoiceController : MyController
    {
        public InvoiceController(IMyView _myView) : base(_myView)
        {
            //getData();
        }

        public async void listInvoice()
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/searchInvoiceByUserID/?id=1")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewInvoiceData);
            var response = await client.sendRequest(request.getApiRequestBundle());
            //Console.WriteLine(response.getJObject().ToString());
            //client.setAuthorizationToken(response.getJObject().ToString());
        }

        private void setViewInvoiceData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setInvoice", _response.getParsedObject<List<guwudang.Model.Invoice>>());
            }
        }

    }
}

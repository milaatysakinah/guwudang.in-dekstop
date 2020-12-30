using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using guwudang.utils;
using System.Collections.Generic;

namespace guwudang.CreateInvoice
{
    public class CreateInvoiceController : MyController
    {
        private static string id;

        public CreateInvoiceController(IMyView _myView) : base(_myView)
        {
            //getData();
        }

        public async void status()
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/status/")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            //Console.WriteLine(response.getJObject().ToString());
            //client.setAuthorizationToken(response.getJObject().ToString());
        }

        private void setViewStatus(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setStatus", _response.getParsedObject<List<guwudang.Model.StatusInvoice>>());
            }
        }

        public async void partner()
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/searchPartnerByUserID/?id=:id";

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var reqAccount = request
                .buildHttpRequest()
                .setEndpoint("api/authUser")
                .setRequestMethod(HttpMethod.Get);

            var response1 = await client.sendRequest(request.getApiRequestBundle());
            client.setOnFailedRequest(setFailedAuthorization);
            id = response1.getJObject()["user"]["id"].ToString();
            _endpoint = _endpoint.Replace(":id", id);
            //Console.WriteLine(_endpoint);

            var client2 = new ApiClient("http://localhost:8000/");
            
            client2.setAuthorizationToken(token);

            var request2 = new ApiRequestBuilder();
            var req = request2
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client2.setOnSuccessRequest(setViewPartnerData);
            var response = await client2.sendRequest(request2.getApiRequestBundle());
        }

        public void searchPartner(string key)
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/searchPartner/?id=:id&search=:search";

            _endpoint = _endpoint.Replace(":id", id);
            _endpoint = _endpoint.Replace(":search", key);

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewPartnerData);
            var response = client.sendRequest(request.getApiRequestBundle());
        }


        private void setViewPartnerData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setPartner", _response.getParsedObject<List<guwudang.Model.Partner>>());
            }
        }

        private void setFailedAuthorization(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                getView().callMethod("backToLogin");
            }
        }

        public async void product()
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/searchProductByUserID/?id=:id";

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var reqAccount = request
                .buildHttpRequest()
                .setEndpoint("api/authUser")
                .setRequestMethod(HttpMethod.Get);

            var response1 = await client.sendRequest(request.getApiRequestBundle());
            client.setOnFailedRequest(setFailedAuthorization);
            
            id = response1.getJObject()["user"]["id"].ToString();
            _endpoint = _endpoint.Replace(":id", id);
            //Console.WriteLine(_endpoint);

            var client2 = new ApiClient("http://localhost:8000/");

            client2.setAuthorizationToken(token);

            var request2 = new ApiRequestBuilder();
            var req = request2
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client2.setOnSuccessRequest(setViewProductData);
            var response = await client2.sendRequest(request2.getApiRequestBundle());
        }

        private void setViewProductData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setProduct", _response.getParsedObject<List<guwudang.Model.Product>>());
            }
        }

        public async void createInvoice(string _idPartner, string _idStatus)
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/searchPartnerByUserID/?id=:id";

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var reqAccount = request
                .buildHttpRequest()
                .setEndpoint("api/authUser")
                .setRequestMethod(HttpMethod.Get);

            var response1 = await client.sendRequest(request.getApiRequestBundle());
            client.setOnFailedRequest(setFailedAuthorization);
            // Console.WriteLine(response1.getHttpResponseMessage().Content);
            id = response1.getJObject()["user"]["id"].ToString();


            //var client = new ApiClient("http://127.0.0.1:8000/");
            //var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/invoice/")
                .addParameters("partner_id", _idPartner)
                .addParameters("user_id", id)
                .addParameters("status_invoice_id", _idStatus)
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(onSuccessCreateInvoice);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void onSuccessCreateInvoice(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("onSuccess", status);
            }
        }
    }
}

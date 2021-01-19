using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using guwudang.utils;
using System.Collections.Generic;

namespace guwudang.CreateOrderItems
{
    class CreateOrderItemController : MyController
    {
        private static string id;
        private static User user = new User();

        public CreateOrderItemController(IMyView _myView) : base(_myView)
        {
        }

        public async void product()
        {
            string _endpoint = "api/searchProductByUserID";
            string token = user.getToken();

            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();

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
                getView().callMethod("setProduct", _response.getParsedObject<List<guwudang.Model.Product>>());
            }
        }

        public async void type()
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();

            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/transactionType/")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewTypeData);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewTypeData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setType", _response.getParsedObject<List<guwudang.Model.TransType>>());
            }
        }


        private void setFailedAuthorization(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                getView().callMethod("backToLogin");
            }
        }

        public async void createOrderItem(string _idProduct, string _idType, string _orderQty, string _idInvoice)
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/orderitem/";

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .addParameters("invoice_id", _idInvoice)
                .addParameters("product_id", _idProduct)
                .addParameters("transaction_type_id", _idType)
                .addParameters("order_quantity", _orderQty)
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(onSuccessCreateOrderItem);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void onSuccessCreateOrderItem(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("onSuccess", status);
            }
        }
    }
}

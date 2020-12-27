using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using guwudang.utils;
using System.Collections.Generic;
using System;

namespace guwudang.EditOrderItems
{
    class EditOrderItemsController : MyController
    {
        private static string id;

        public EditOrderItemsController(IMyView _myView) : base(_myView)
        {
            //getData();
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

        public async void getOrder(string idOrder)
        {
            Console.WriteLine("Id Order : " + idOrder);
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/orderitem/:id";

            _endpoint = _endpoint.Replace(":id", idOrder);
            Console.WriteLine(_endpoint);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewOrderData);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewOrderData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                guwudang.Model.OrderItemGet getList = _response.getParsedObject<guwudang.Model.OrderItemGet>();
                getView().callMethod("setOrder", getList);
            }
        }

        public async void type()
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/transactionType/")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewTypeData);
            var response = await client.sendRequest(request.getApiRequestBundle());
            //Console.WriteLine(response.getJObject().ToString());
            //client.setAuthorizationToken(response.getJObject().ToString());
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

        public async void UpdateOrderItem(string _idProduct, string _idType, string _orderQty, string _idOrder)
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();
            string _endpoint = "api/orderitem/" + _idOrder;
            Console.WriteLine(_endpoint + " -- QTY : " + _orderQty);

            //User user = new User();
            //string token = user.getToken();
            //client.setAuthorizationToken(token);

            //var reqAccount = request
            //    .buildHttpRequest()
            //    .setEndpoint("api/authUser")
            //    .setRequestMethod(HttpMethod.Get);

            //var response1 = await client.sendRequest(request.getApiRequestBundle());
            //client.setOnFailedRequest(setFailedAuthorization);


            //var client = new ApiClient("http://127.0.0.1:8000/");
            //var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .addParameters("id", _idOrder)
                .addParameters("product_id", _idProduct)
                .addParameters("transaction_type_id", _idType)
                .addParameters("order_quantity", _orderQty)
                .setRequestMethod(HttpMethod.Put);
            client.setOnSuccessRequest(onSuccessUpdateOrderItem);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void onSuccessUpdateOrderItem(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("onSuccess", status);
            }
        }

    }
}

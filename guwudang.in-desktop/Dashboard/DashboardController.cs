using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using System.Collections.Generic;
using guwudang.utils;
using System;
using System.Windows.Controls;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace guwudang.Dashboard
{
    public class DashboardController : MyController
    {
        public DashboardController(IMyView _myView) : base(_myView)
        {
            //setUserID();
        }

        
        public async void totalCostumers()
        {
            
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/searchPartnerByUserID/";
            
            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewTotalCustomers);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewTotalCustomers(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("Total Customer : " + _response.getHttpResponseMessage().ReasonPhrase);
                String total = _response.getParsedObject<List<guwudang.Model.Partner>>().Count.ToString();
                getView().callMethod("setTotalCustomers", _response.getParsedObject<List<guwudang.Model.Partner>>());
            }
        }

        public async void weeklyOrder(String type)
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/weeklyOrderItem/?type=:type";
            _endpoint = _endpoint.Replace(":type", type);

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);

            if(type == "1")
                client.setOnSuccessRequest(setViewWeeklyOrderIN);
            else
                client.setOnSuccessRequest(setViewWeeklyOrderOUT);

            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewWeeklyOrderIN(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("Weekly Order IN : " + _response.getHttpResponseMessage().ReasonPhrase);

                List<List<TotalWeek>> listObj = _response.getParsedObject<List<List<TotalWeek>>>();
                
                guwudang.Model.WeeklyOrder weekly = new WeeklyOrder();
                weekly.name = "IN";
                weekly.mo = listObj[0][0].total;
                weekly.tu = listObj[1][0].total;
                weekly.we = listObj[2][0].total;
                weekly.th = listObj[3][0].total;
                weekly.fr = listObj[4][0].total;
                weekly.sa = listObj[5][0].total;
                weekly.su = listObj[6][0].total;
                
                getView().callMethod("setWeeklyOrder", weekly);
            }
        }

        private void setViewWeeklyOrderOUT(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("Weekly Order OUT : " + _response.getHttpResponseMessage().ReasonPhrase);

                List<List<TotalWeek>> listObj = _response.getParsedObject<List<List<TotalWeek>>>();

                guwudang.Model.WeeklyOrder weekly = new WeeklyOrder();
                weekly.name = "OUT";
                weekly.mo = listObj[0][0].total;
                weekly.tu = listObj[1][0].total;
                weekly.we = listObj[2][0].total;
                weekly.th = listObj[3][0].total;
                weekly.fr = listObj[4][0].total;
                weekly.sa = listObj[5][0].total;
                weekly.su = listObj[6][0].total;

                getView().callMethod("setWeeklyOrder", weekly);
            }
        }

        public async void totalInvoices()
        {
            
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/searchInvoiceByUserID";
            //_endpoint = _endpoint.Replace(":id", id);

            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewTotalInvoices);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public async void totalShipping()
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/searchOrderItemByUserID/";
            
            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewTotalShipping);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public async void totalProducts()
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/searchProductByUserID";
            
            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewTotalProducts);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public async void totalOrderIN()
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/orderItemIN/";
            
            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewTotalOrderIN);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewTotalOrderIN(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("Total Order IN : " + _response.getHttpResponseMessage().ReasonPhrase);
                String total = _response.getParsedObject<List<guwudang.Model.OrderItem>>().Count.ToString();
                getView().callMethod("setTotalOrderIN", total);
            }
        }

        public async void totalOrderOUT()
        {
            
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/orderItemOUT/";
            
            User user = new User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewTotalOrderOUT);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewTotalOrderOUT(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("Total Order OUT : " + _response.getHttpResponseMessage().ReasonPhrase);
                String total = _response.getParsedObject<List<guwudang.Model.OrderItem>>().Count.ToString();
                getView().callMethod("setTotalOrderOUT", total);
            }
        }

        private void setViewTotalInvoices(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("Total Invoices : " + _response.getHttpResponseMessage().ReasonPhrase);
                String total = _response.getParsedObject<List<guwudang.Model.Invoice>>().Count.ToString();
                getView().callMethod("setTotalInvoices", total);
            }
        }

        private void setViewTotalShipping(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("Total Invoices : " + _response.getHttpResponseMessage().ReasonPhrase);
                String total = _response.getParsedObject<List<guwudang.Model.OrderItem>>().Count.ToString();
                getView().callMethod("setTotalShipping", total);
            }
        }

        private void setViewTotalProducts(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("Total Product : " + _response.getHttpResponseMessage().ReasonPhrase);
                String total = _response.getParsedObject<List<guwudang.Model.Product>>().Count.ToString();
                getView().callMethod("setTotalProducts", total);
            }
        }

        private void setFailedAuthorization(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                //getView().callMethod("backToLogin");
            }
        }   
    }

    class TotalWeek
    {
        public string total { get; set; }

    }
}

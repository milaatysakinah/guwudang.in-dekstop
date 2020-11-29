using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using System.Collections.Generic;


namespace guwudang.Product
{
    public class ProductController : MyController
    {
        public ProductController(IMyView _myView) : base(_myView)
        {
            //getData();
        }

        public async void product()
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/product/")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewProductData);
            var response = await client.sendRequest(request.getApiRequestBundle());
            //Console.WriteLine(response.getJObject().ToString());
            //client.setAuthorizationToken(response.getJObject().ToString());
        }

        private void setViewProductData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setProduct", _response.getParsedObject<List<guwudang.Model.Product>>());
            }
        }

    }
}

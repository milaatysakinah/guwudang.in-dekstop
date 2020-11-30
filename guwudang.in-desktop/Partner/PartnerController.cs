using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using System.Collections.Generic;


namespace guwudang.Partner
{
    public class PartnerController : MyController
    {
        public PartnerController(IMyView _myView) : base(_myView)
        {
            //getData();
        }

        public async void partner()
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/partner/")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewPartnerData);
            var response = await client.sendRequest(request.getApiRequestBundle());
            //Console.WriteLine(response.getJObject().ToString());
            //client.setAuthorizationToken(response.getJObject().ToString());
        }

        private void setViewPartnerData(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setPartner", _response.getParsedObject<List<guwudang.Model.Partner>>());
            }
        }

    }
}

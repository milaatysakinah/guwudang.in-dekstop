using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;
using guwudang.Model;

namespace guwudang.DetailPartner
{
    class DetailPartnerC : MyController
    {
        public DetailPartnerC(IMyView _myView) : base(_myView)
        {

        }

        public async void partner()
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/partner/1")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setDetailPartner);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setDetailPartner(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                String status = _response.getHttpResponseMessage().ReasonPhrase;
                //Console.WriteLine(_response.getJObject());
                //Console.WriteLine(_response.getParsedObject<RootModelDetailPartner>().partner);
               getView().callMethod("setDetailPartner", _response.getParsedObject<guwudang.Model.Partner>());
            }
        }
    }
}
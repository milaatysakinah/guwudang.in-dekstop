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
    public class DetailPartnerC : MyController
    {
        public DetailPartnerC(IMyView _myView) : base(_myView)
        {

        }

        public async void partner(string id)
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();
            string _endpoint = "api/partner/1?id=:idUser";

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
            _endpoint = _endpoint.Replace(":id", id);


            var req = request
                .buildHttpRequest()
                .setEndpoint(_endpoint)
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

        private void setFailedAuthorization(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                getView().callMethod("backToLogin");
            }
        }
    }
}
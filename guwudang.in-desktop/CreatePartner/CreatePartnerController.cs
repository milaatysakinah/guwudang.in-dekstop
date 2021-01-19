using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using System.Collections.Generic;
using guwudang.utils;
using System;
namespace guwudang.CreatePartner
{
    class CreatePartnerController : MyController
    {
        public CreatePartnerController(IMyView _myView) : base(_myView)
        {

        }

        public async void createPartner(Model.Partner partner)
        {
            var client = new ApiClient(utils.urls.BASE_URL);
            var request = new ApiRequestBuilder();

            utils.User user = new utils.User();
            string token = user.getToken();
            client.setAuthorizationToken(token);

            var req = request
                .buildHttpRequest()
                .addParameters("name", partner.name)
                .addParameters("email", partner.email)
                .addParameters("phone_number", partner.phone_number)
                .addParameters("address", partner.address)
                .setEndpoint("api/partner/")
                .setRequestMethod(HttpMethod.Post);

            client.setOnSuccessRequest(setViewSuccess);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewSuccess(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                getView().callMethod("createSuccess");
            }
        }
    }
}


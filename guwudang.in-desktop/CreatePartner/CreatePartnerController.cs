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

            var req = request
                .buildHttpRequest()
                .addParameters("companyName", partner.name)
                .addParameters("email", partner.email)
                .addParameters("phone", partner.phone_number)
                .addParameters("address", partner.address)
                .setEndpoint("api/partner/create")
                .setRequestMethod(HttpMethod.Get);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }
    }
}


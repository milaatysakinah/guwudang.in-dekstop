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

        public async void createPartner(string _companyName, string _email, string _phone,
            string _address)
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("companyName", _companyName)
                .addParameters("email", _email)
                .addParameters("phone", _phone)
                .addParameters("address", _address)
                .setEndpoint("api/partner/create")
                .setRequestMethod(HttpMethod.Get);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }
    }
}


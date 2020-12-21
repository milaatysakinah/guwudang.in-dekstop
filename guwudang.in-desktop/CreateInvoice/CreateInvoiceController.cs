using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guwudang.CreateInvoice
{
    class CreateInvoiceController : MyController
    {
        public CreateInvoiceController(IMyView _myView) : base(_myView)
        {

        }

        public async void createInvoice(string _partnerName, string _statusInvoice)
        {
            var client = new ApiClient("http://localhost:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("partnerName", _partnerName)
                .addParameters("statusInvoice", _statusInvoice)
                .setEndpoint("api/invoice/create")
                .setRequestMethod(HttpMethod.Get);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }
    }
}


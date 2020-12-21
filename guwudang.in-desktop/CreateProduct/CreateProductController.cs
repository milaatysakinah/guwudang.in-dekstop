using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;
using guwudang.Model.Product;

namespace guwudang.CreateProduct
{
    class CreateProductController : MyController
    {
        public CreateProductController(IMyView _myView) : base(_myView)
        {

        }

        public async void createProduct(string _productName, string _stock, string _price,
            string _description, string _picture)
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("productName", _productName)
                .addParameters("stock", _stock)
                .addParameters("price", _price)
                .addParameters("description", _description)
                .addParameters("picture", _picture)
                .setEndpoint("api/product/create")
                .setRequestMethod(HttpMethod.Post);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }
    }
}


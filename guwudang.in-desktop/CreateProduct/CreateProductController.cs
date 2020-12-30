using Velacro.Api;
using Velacro.Basic;
using System.Net.Http;
using guwudang.Model;
using System.Collections.Generic;
using guwudang.utils;
using System;
using System.IO;

namespace guwudang.CreateProduct
{
    class CreateProductController : MyController
    {
        public CreateProductController(IMyView _myView) : base(_myView)
        {

        }

        public async void createProduct(string _productName, string _stock, string _price,
            string _description, MyList<MyFile> fileImage)
        {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(_productName), "name");
            formContent.Add(new StringContent(_stock), "stock");
            formContent.Add(new StringContent(_price), "price");
            formContent.Add(new StringContent(_description), "descripton");
            if (fileImage.Count > 0)
                formContent.Add(new StreamContent(new MemoryStream(fileImage[0].byteArray)), "picture", fileImage[0].fullFileName);

            var multiPartRequest = request
            .buildMultipartRequest(new MultiPartContent(formContent))
            .setEndpoint("api/product/create")
            .setRequestMethod(HttpMethod.Post);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }
    }
}


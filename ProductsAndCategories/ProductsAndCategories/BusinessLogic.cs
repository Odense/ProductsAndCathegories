using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using ProductsAndCategories.Models;

namespace ProductsAndCategories
{
    static class BusinessLogic
    {
        public static IEnumerable<ProductViewModel> GetViewModel(ResponseModel model)
        {
            try
            {
                var products = model.Products.Join(model.Categories,
                    product => product.CategoryId,
                    category => category.Id,
                    (product, category) =>
                        new ProductViewModel { ProductName = product.Name, CategoryName = category.Name });

                return products;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public static string GetData(string url)
        {
            try
            {
                var request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;

                var response = request.GetResponse();

                var dataStream = response.GetResponseStream();

                var reader = new StreamReader(dataStream);
                var responseString = reader.ReadToEnd();

                response.Close();

                return responseString;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static ResponseModel DeserializeResponse(string jsonStr)
        {
            return JsonConvert.DeserializeObject<ResponseModel>(jsonStr);
        }
    }
}
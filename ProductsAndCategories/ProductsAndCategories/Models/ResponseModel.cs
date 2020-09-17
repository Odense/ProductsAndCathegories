using System.Collections.Generic;

namespace ProductsAndCategories.Models
{
    class ResponseModel
    {
        public List<ProductModel> Products { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}
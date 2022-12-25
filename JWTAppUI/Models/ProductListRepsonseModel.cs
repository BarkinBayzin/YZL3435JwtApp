﻿namespace JWTAppUI.Models
{
    public class ProductListRepsonseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public CategoryListResponseModel Category { get; set; } = new CategoryListResponseModel();

    }
}

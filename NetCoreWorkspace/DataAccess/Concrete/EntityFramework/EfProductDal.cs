﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework {
	public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal {
		public List<ProductDetailDto> GetProductDetails() {
			using (NorthwindContext context = new NorthwindContext()) {
				var result = from p in context.Products
							 join c in context.Categories
							 on p.CategoryId equals c.CategoryId
							 select new ProductDetailDto { 
								 CategoryName = c.CategoryName, 
								 ProductId = p.ProductId, 
								 ProductName = p.ProductName, 
								 UnitsInStock = p.UnitsInStock 
							 };
				return result.ToList();
			}
		}
	}
}

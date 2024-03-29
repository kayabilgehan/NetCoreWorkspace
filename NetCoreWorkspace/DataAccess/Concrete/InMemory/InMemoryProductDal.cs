﻿using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory {
	public class InMemoryProductDal : IProductDal {
		List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product> {
				new Product {
					ProductId = 1,
					CategoryId = 1,
					ProductName = "Bardak",
					UnitPrice = 15,
					UnitsInStock = 15 },
				new Product {
					ProductId = 2,
					CategoryId = 1,
					ProductName = "Kamera",
					UnitPrice = 15,
					UnitsInStock = 15 },
				new Product {
					ProductId = 3,
					CategoryId = 1,
					ProductName = "Telefon",
					UnitPrice = 500,
					UnitsInStock = 3 },
				new Product {
					ProductId = 4,
					CategoryId = 2,
					ProductName = "Klavye",
					UnitPrice = 1500,
					UnitsInStock = 2 },
				new Product {
					ProductId = 4,
					CategoryId = 2,
					ProductName = "Fare",
					UnitPrice = 85,
					UnitsInStock = 1 },
			};
        }
        public void Add(Product product) {
			_products.Add(product);
		}

		public void Delete(Product product) {
			Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

			_products.Remove(productToDelete);
		}

		public Product Get(Expression<Func<Product, bool>> filter) {
			throw new NotImplementedException();
		}

		public List<Product> GetAll(Expression<Func<Product, bool>> filter = null) {
			return _products;
		}

		public List<Product> GetAllByCategory(int categoryId) {
			return _products.Where(p => p.CategoryId == categoryId).ToList();
		}

		public List<ProductDetailDto> GetProductDetails() {
			throw new NotImplementedException();
		}

		public void Update(Product product) {
			Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
			productToUpdate.ProductName = product.ProductName;
			productToUpdate.CategoryId = product.CategoryId;
			productToUpdate.UnitPrice = product.UnitPrice;
			productToUpdate.UnitsInStock = product.UnitsInStock;
		}
	}
}

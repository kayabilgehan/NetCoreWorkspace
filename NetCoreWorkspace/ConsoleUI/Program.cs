using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using DataAccess.DTOs;
using Entities.Concrete;

namespace ConsoleUI {
	internal class Program {
		static void Main(string[] args) {
			ProductTest();

			//CategoryTest();
		}

		private static void CategoryTest() {
			CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

			List<Category> categories = categoryManager.GetAll();
			foreach (Category category in categories) {
				Console.WriteLine(category.CategoryName);
			}
		}

		private static void ProductTest() {
			ProductManager productManager = new ProductManager(new EfProductDal());

			//List<Product> products = productManager.GetAll();
			//List<Product> products = productManager.GetAllByCategoryId(2);
			/*List<Product> products = productManager.GetByUnitPrice(50, 100);
			foreach (Product product in products) {
				Console.WriteLine(product.ProductName);
			}*/

			IDataResult<List<ProductDetailDto>> dataResult = productManager.GetProductDetails();
			if (dataResult.Success) {
				List<ProductDetailDto> productsDetailsList = dataResult.Data;
				foreach (ProductDetailDto productDetail in productsDetailsList) {
					Console.WriteLine(productDetail.ProductName + " / " + productDetail.CategoryName);
				}
			}
			else {
                Console.WriteLine(dataResult.Message);
            }

			return;
		}
	}
}
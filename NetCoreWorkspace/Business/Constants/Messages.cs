using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants {
	public static class Messages {
        public static string ProductAdded = "Product added.";
        public static string ProductNameInvalid = "Product name is invalid.";
		public static string ProductsListed = "Products listed.";
		public static string SystemMaintenanceError = "System maintenance time.";
		public static string UnitPriceInvalid = "Product price invalid.";
		public static string ProductCountOfCategoryError = "There can not be more than 10 products on same category.";
		public static string ProductNameAlreadyExists = "There is a product with same name.";
		public static string CategoryLimitExceded = "Category limit exceded. Can not add new product.";
	}
}

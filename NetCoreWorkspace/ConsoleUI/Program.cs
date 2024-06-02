using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;

namespace ConsoleUI {
	internal class Program {
		static void Main(string[] args) {
			//ProductTest();

			//CategoryTest();

			string User1Name = "Users";
			string User2Name = "Users2";
			string User3Name = "Users3";
			List<User> userList1 = new List<User>() {
				CreateAUser(User1Name + (Guid.NewGuid().ToString())),
				CreateAUser(User1Name + (Guid.NewGuid().ToString())),
				CreateAUser(User1Name + (Guid.NewGuid().ToString()))
			};
			List<User> userList2 = new List<User>() {
				CreateAUser(User2Name + (Guid.NewGuid().ToString())),
				CreateAUser(User2Name + (Guid.NewGuid().ToString())),
				CreateAUser(User2Name + (Guid.NewGuid().ToString()))
			};
			List<User> userList3 = new List<User>() {
				CreateAUser(User3Name + (Guid.NewGuid().ToString())),
				CreateAUser(User3Name + (Guid.NewGuid().ToString())),
				CreateAUser(User3Name + (Guid.NewGuid().ToString()))
			};
			var customUserList = new Dictionary<string, List<User>> { 
				{ User1Name, userList1 },
				{ User2Name, userList2 },
				{ User3Name, userList3 }
		};

			EfUserDal userService = new EfUserDal();
			userService.InsertCustomData<User>(customUserList);

			//User user1 = CreateAUser(User1Name);
			//User user2 = CreateAUser(User2Name);
			//User user3 = CreateAUser(User3Name);

			//using (NorthwindContext context = new NorthwindContext()) {
			//}
			//using (NorthwindContext context = new NorthwindContext(User1Name)){

			//	context.Add(user1);
			//	context.SaveChanges();
			//}
			//using (NorthwindContext context = new NorthwindContext(User2Name)) {

			//	context.Add(user2);
			//	context.SaveChanges();
			//}
			//using (NorthwindContext context = new NorthwindContext(User3Name)) {

			//	context.Add(user3);
			//	context.SaveChanges();
			//}


		}
		private static User CreateAUser(string name) {
			byte[] passwordHash, passwordSalt;
			HashingHelper.CreatePasswordHash(name, out passwordHash, out passwordSalt);
			return new User {
				Email = name + "@" + name + ".com",
				FirstName =  name,
				LastName = name + "-lastname",
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt,
				Status = true
			};
		}

		/*private static void CategoryTest() {
			CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

			List<Category> categories = categoryManager.GetAll();
			foreach (Category category in categories) {
				Console.WriteLine(category.CategoryName);
			}
		}*/

		/*private static void ProductTest() {
			ProductManager productManager = new ProductManager(new EfProductDal());

			//List<Product> products = productManager.GetAll();
			//List<Product> products = productManager.GetAllByCategoryId(2);
			/*List<Product> products = productManager.GetByUnitPrice(50, 100);
			foreach (Product product in products) {
				Console.WriteLine(product.ProductName);
			}*/

			/*IDataResult<List<ProductDetailDto>> dataResult = productManager.GetProductDetails();
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
		}*/
	}
}
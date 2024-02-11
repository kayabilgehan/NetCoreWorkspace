using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework {
	public class EfCategoryDal : EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal { 
	
	}
}

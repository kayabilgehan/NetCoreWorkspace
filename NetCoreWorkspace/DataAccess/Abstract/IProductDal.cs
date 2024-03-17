using Core.DataAccess;
using Entities.DTOs;
using Entities.Concrete;

namespace DataAccess.Abstract {
	public interface IProductDal : IEntityRepository<Product> {
		List<ProductDetailDto> GetProductDetails();
	}
}

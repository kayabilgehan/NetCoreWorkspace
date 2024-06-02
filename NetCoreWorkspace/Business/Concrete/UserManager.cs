using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
	public class UserManager : IUserService {
		IUserDal _userDal;

		public UserManager(IUserDal userDal) {
			_userDal = userDal;
		}

		public IDataResult<List<OperationClaim>> GetClaims(User user) {
			return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
		}

		public IResult Add(User user) {
			_userDal.Add(user);
			return new SuccessResult();
		}

		public IDataResult<User> GetByMail(string email) {
			User user = _userDal.Get(u => u.Email == email);
			return new DataResult<User>(user, user != null);
		}
	}
}

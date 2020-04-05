using Business.Abstract;
using Core.Entties.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        //BLL de hiçbir şekilde context kullanılmıyor.
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public void Add(User user)
        {
           _userDal.Add(user);
        }

        public User GetByMail(string email)
        {          
            return _userDal.Get(x=>x.Email==email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
           return _userDal.GetClaims(user);
        }
        public bool Exists(string email)
        {
            return _userDal.Exists(email);
        }

       
    }
}

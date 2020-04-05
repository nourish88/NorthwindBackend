using Core.DataAccess;
using Core.Entties.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
  public  interface IUserDal:IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        bool Exists(string email);
    }
}

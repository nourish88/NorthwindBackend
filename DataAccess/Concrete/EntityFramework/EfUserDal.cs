using Core.DataAccess.Concrete.EntityFramework;
using Core.Entties.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Collections.Generic;
using System.Linq;
namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {//joinleri burada yazdık
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new NorthwindContext())
            {
                var result = context.OperationClaims.Join(context.UserOperationClaims, a => a.Id, b => b.OperationClaimId, (a, b) => new { a, b }).Where(x => x.b.UserId == user.Id).Select(m => new OperationClaim
                {
                    Id = m.a.Id,
                    Name = m.a.Name
                }).ToList();
                return result;
            }
        }      
        public bool Exists(string email)
        {
            using (var context = new NorthwindContext())
            {
                return context.Users.Any(x => x.Email == email);
            }
        }
    }
}
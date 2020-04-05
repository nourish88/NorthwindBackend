using Core.Entties.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
  public  interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto,string password  );
        IResult UserExists(string email);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}

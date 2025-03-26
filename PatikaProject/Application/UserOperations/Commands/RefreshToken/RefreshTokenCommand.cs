using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PatikaProject.DbOperations;
using PatikaProject.TokenOperations;

namespace PatikaProject.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }

        private readonly IBookDbContext _context;

        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);

            if(user is not null)
            {
                TokenOperations.TokenHandler tokenHandler = new TokenOperations.TokenHandler(_configuration);

                Token token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.ExpireDate.AddMinutes(5);

                _context.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Geçerli Refresh Token Bulunamadı!");
        }

    }

}
    


using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PatikaProject.DbOperations;
using PatikaProject.TokenOperations;

namespace PatikaProject.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {

        public CreateTokenModel Model { get; set; }

        private readonly IBookDbContext _context;

        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;


        public CreateTokenCommand(IBookDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            
            if(user is not null)
            {
                TokenOperations.TokenHandler handler = new TokenOperations.TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.ExpireDate.AddMinutes(5);

                _context.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Kullanıcı adı ve şifre hatalı!");    
        }

    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

﻿using AutoMapper;
using PatikaProject.DbOperations;
using PatikaProject.Entity;

namespace PatikaProject.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }

        private readonly IMapper _mapper;

        private readonly IBookDbContext _dbContext;

        public CreateUserCommand(IBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {

            var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email);

            if (user != null)
                throw new InvalidOperationException("Kullanıcı zaten mevcut");

            user = _mapper.Map<User>(Model);


            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

        }
    }


    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}

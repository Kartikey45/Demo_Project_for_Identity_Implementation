using CommonLayer.Models;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new NullReferenceException("Register model is null");
                }

                if (model.Password != model.ConfirmPassword)
                {
                    return new UserManagerResponse
                    {
                        Message = "Doesn't match the password",
                        IsSuccess = false
                    };
                }

                var identityUser = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await userManager.CreateAsync(identityUser, model.Password);

                if (result.Succeeded)
                {
                    return new UserManagerResponse
                    {
                        Message = "User created successfully",
                        IsSuccess = true
                    };
                }

                return new UserManagerResponse
                {
                    Message = "User did not create",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}

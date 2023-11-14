using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;

namespace Users.Infrastructure
{
    public class CustomPasswordValidator : PasswordValidator<AppUser>
    { 
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            //получить базовые ошибки
            var result = await base.ValidateAsync(manager, user, password); 

            var errors = result.Succeeded? new List<IdentityError>():
                result.Errors.ToList();
            //добавить свою проверку
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError { 
                Code = "PasswordContainsUserName",
                Description = "Password cannot contain username"
                });
            }
            if (password.Contains("12345")) {
                errors.Add(new IdentityError { 
                Code="PasswordContainsSequence",
                 Description="Password cannot contain numeric sequence"
                });
            }
            //соединить и отправить
            return (errors.Count() == 0) ? IdentityResult.Success :
                            IdentityResult.Failed(errors.ToArray()); 
        }
    }
}

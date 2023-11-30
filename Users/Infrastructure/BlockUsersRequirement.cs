using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Infrastructure
{
    /// <summary>
    /// Предоставляет требование и применяется для указания данных, используемых при создании политики, где под данными подразумевается список пользователей, которые не будут авторизованы
    /// </summary>
    public class BlockUsersRequirement: IAuthorizationRequirement
    {
        public BlockUsersRequirement(params string[] users)
        {
            BlockedUsers = users;
        }

        public string[] BlockedUsers { get; set; }

    }
}

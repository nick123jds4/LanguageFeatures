using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Infrastructure
{
    /// <summary>
    /// Класс ответственен за оценку запроса на авторизацию с применением данных требования и унаследован от <see cref="AuthorizationHandler<T>"/>, где Т - тип класса требования <see cref="BlockUsersRequirement"/>.
    /// </summary>
    public class BlockUsersHandler : AuthorizationHandler<BlockUsersRequirement>
    {
        /// <summary>
        /// Принимает решение, разрешена ли авторизация, на основе конкретного требования.
        /// Вызывается, когда система авторизации нуждается в проверке доступа к ресурсу.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BlockUsersRequirement requirement)
        {
            if (context.User.Identity != null
                && context.User.Identity.Name != null
                && !requirement.BlockedUsers.Any(user => user.Equals(context.User.Identity.Name, StringComparison.OrdinalIgnoreCase)))
            {
                context.Succeed(requirement);
            }
            else {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}

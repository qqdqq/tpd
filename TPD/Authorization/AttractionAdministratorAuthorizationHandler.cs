using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;


namespace TPD.Authorization
{
    public class AttractionAdministratorAuthorizationHandler :
            AuthorizationHandler<OperationAuthorizationRequirement, Attraction>
    {
        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Attraction resource)
        {
            if (context.User == null || resource == null) { return Task.CompletedTask; }


            if (context.User.IsInRole(Constants.AttractionAdministratorsRole)) { context.Succeed(requirement); }

            return Task.CompletedTask;
        }
    }
}

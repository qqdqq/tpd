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
    public class AttractionManagerAuthorizationHandler : 
        AuthorizationHandler<OperationAuthorizationRequirement, Attraction>
    {
        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Attraction resource)
        {
            if(context.User == null || resource == null) { return Task.CompletedTask; }

            if (requirement.Name != Constants.ViewOperationName && 
                requirement.Name != Constants.EditOperationName &&
                requirement.Name != Constants.CreateOperationName)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Constants.AttractionManagerRole)) { context.Succeed(requirement); }

            return Task.CompletedTask;
        }
    }
}

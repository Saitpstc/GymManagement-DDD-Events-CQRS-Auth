﻿namespace Authorization_Authentication.Models;

using Microsoft.AspNetCore.Identity;

class User:IdentityUser<Guid>
{
    public ICollection<UserRoleMap> UserRoles { get; set; }
}
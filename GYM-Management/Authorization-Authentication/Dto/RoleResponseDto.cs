﻿namespace Authorization_Authentication.Dto;

using Application.Superadmin.Commands;
using Models;

public class RoleResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
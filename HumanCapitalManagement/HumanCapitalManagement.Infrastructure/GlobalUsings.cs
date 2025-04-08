global using System.Text;
global using System.Text.Json;
global using System.Security.Claims;
global using System.IdentityModel.Tokens.Jwt;

global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using HumanCapitalManagement.Domain.Entities;
global using HumanCapitalManagement.Application.DTOs;
global using HumanCapitalManagement.Application.Services;
global using HumanCapitalManagement.Infrastructure.Authentication;
global using HumanCapitalManagement.Application.Interfaces.Repositories;
global using HumanCapitalManagement.Infrastructure.EntityConfiguration;
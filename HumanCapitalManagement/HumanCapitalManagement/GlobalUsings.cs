global using System.Text.Json.Serialization;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.Options;

global using HumanCapitalManagement.API.Extensions;
global using HumanCapitalManagement.Infrastructure;
global using HumanCapitalManagement.Application.DTOs;
global using HumanCapitalManagement.Application.Interfaces.Services;
global using HumanCapitalManagement.Application.Services;
global using HumanCapitalManagement.API.OptionsSetup;
global using HumanCapitalManagement.Application.Interfaces.Repositories;
global using HumanCapitalManagement.Infrastructure.Repositories;
global using HumanCapitalManagement.Infrastructure.Services;
global using HumanCapitalManagement.Infrastructure.Authentication;
global using HumanCapitalManagement.API.Filters;
global using HumanCapitalManagement.Application.Exceptions;
global using HumanCapitalManagement.Application.Attributes;
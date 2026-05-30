global using System.Text;
global using System.Security.Claims;
global using System.Security.Cryptography;

global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.IdentityModel.JsonWebTokens;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Authentication.JwtBearer;

global using Constructix.Domain.Enums;
global using Constructix.Domain.Abstractions;
global using Constructix.Domain.Entities.Identity;

global using Constructix.Application.Models;
global using Constructix.Application.Abstractions.Services;

global using Constructix.Infrastructure.Services;
global using Constructix.Infrastructure.Models.Authentication;
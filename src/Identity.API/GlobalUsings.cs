global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text.RegularExpressions;

global using Duende.IdentityServer;
global using Duende.IdentityServer.Configuration;
global using Duende.IdentityServer.Events;
global using Duende.IdentityServer.Extensions;
global using Duende.IdentityServer.Models;
global using Duende.IdentityServer.Services;
global using Duende.IdentityServer.Stores;
global using Duende.IdentityServer.Validation;

global using Forum.Identity.API;
global using Forum.Identity.API.Configuration;
global using Forum.Identity.API.Data;
global using Forum.Identity.API.Models;
global using Forum.Identity.API.Services;
global using Forum.ServiceDefaults;

global using IdentityModel;

global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Microsoft.Extensions.Options;

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

global using Forum.ServiceDefaults;

global using Identity.API;
global using Identity.API.Configuration;
global using Identity.API.Data;
global using Identity.API.Identity.Account;
global using Identity.API.Identity.Consent;
global using Identity.API.Identity.Device;
global using Identity.API.Identity.Diagnostics;
global using Identity.API.Identity.Grants;
global using Identity.API.Identity.Home;
global using Identity.API.Models;
global using Identity.API.Services;

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

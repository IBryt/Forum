﻿namespace Forum.Identity.API.Services;

public interface IRedirectService
{
    string ExtractRedirectUriFromReturnUrl(string url);
}

using Duende.IdentityServer.Models;

namespace IdentifyServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(name: "catalog.fullaccess", displayName: "Catalog API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "catalog.fullaccess" }
            },
            new Client
            {
                ClientId = "catalogswaggerui",
                ClientName = "Catalog Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                RedirectUris = { "http://localhost:5106/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { "http://localhost:5106/swagger/" },

                AllowedScopes =
                {
                    "catalog.fullaccess"
                }
            }
        };
}
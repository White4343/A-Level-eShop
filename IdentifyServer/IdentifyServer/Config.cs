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
            new ApiScope(name: "catalog.fullaccess", displayName: "Catalog API"),
            new ApiScope(name: "basket.fullaccess", displayName: "Basket API"),
            new ApiScope(name: "basket.client", displayName: "Client Basket API"),
            new ApiScope(name: "order.fullaccess", displayName: "Order API"),
            new ApiScope(name: "order.client", displayName: "Client Order API"),
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
            },
            new Client
            {
                ClientId = "basketswaggerui",
                ClientName = "Basket Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                RedirectUris = { "http://localhost:5153/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { "http://localhost:5153/swagger/" },

                AllowedScopes =
                {
                    "basket.fullaccess"
                }
            },
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "basket.client", "order.client" }
            },
            new Client
            {
                ClientId = "reactui",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,

                RedirectUris = { "http://localhost:3000/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:3000" },
                
                AllowedScopes =
                {
                    "basket.client", "order.client"
                }
            }
        };
}
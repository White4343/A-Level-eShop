using Duende.IdentityServer.Models;
using IdentityServer;
using static System.Net.WebRequestMethods;

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

                RedirectUris = { WebApiLinks.CatalogApi + "/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { WebApiLinks.CatalogApi + "/swagger/" },

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

                RedirectUris = { WebApiLinks.BasketApi + "/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { WebApiLinks.BasketApi + "/swagger/" },

                AllowedScopes =
                {
                    "basket.fullaccess", "basket.client"
                }
            },
            new Client
            {
                ClientId = "orderswaggerui",
                ClientName = "Order Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = { WebApiLinks.OrderApi + "/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { WebApiLinks.OrderApi + "/swagger/" },

                AllowedScopes =
                {
                    "order.fullaccess", "order.client"
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

                RedirectUris = { WebApiLinks.ReactUI + "/signin-oidc" },
                PostLogoutRedirectUris  = { WebApiLinks.ReactUI },
                FrontChannelLogoutUri  = WebApiLinks.ReactUI,
                
                AllowedScopes =
                {
                    "basket.client", "order.client"
                }
            }
        };
}
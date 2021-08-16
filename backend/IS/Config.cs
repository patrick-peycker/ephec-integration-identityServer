// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IS
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> IdentityResources =>
			new IdentityResource[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Address(),
				new IdentityResources.Email()
			};

		public static IEnumerable<ApiScope> ApiScopes =>
			new ApiScope[]
			{
				new ApiScope("api1", "My API"),
			};

		public static IEnumerable<Client> Clients =>
			new Client[]
			{
                //mvc client
                new Client
				{
					ClientId = "mvc",
					ClientSecrets =  { new Secret("secret".Sha256()) },

					// Authorization Code Flow
					AllowedGrantTypes = GrantTypes.Code,
					RequirePkce = true,

					// Authorization Implicit Flow
					//AllowedGrantTypes = GrantTypes.Implicit,
					//RequirePkce = false,

					RequireConsent = false,

					// Where to redirect to after login
					RedirectUris = {"http://localhost:5002/signin-oidc"},

					// Where to redirect to after logout
					PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},

					AllowedScopes = new List<string> {
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Address,
						IdentityServerConstants.StandardScopes.Email,
						"api1"
					}
				},

				new Client {
					ClientId = "angular_spa",
					ClientName = "Angula SPA",

					AllowedGrantTypes = GrantTypes.Code,
					RequirePkce = true,

					RequireClientSecret = false,
					
					AllowedScopes = new List<string> {
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"api1"
					},

					RedirectUris = {"http://localhost:4200/auth-callback"},
					PostLogoutRedirectUris = {"http://localhost:4200"},
					AllowAccessTokensViaBrowser = true,
					AccessTokenLifetime = 3600
				}
			};
	}
}
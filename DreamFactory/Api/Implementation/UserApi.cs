﻿namespace DreamFactory.Api.Implementation
{
    using System;
    using System.Threading.Tasks;
    using DreamFactory.Http;
    using DreamFactory.Model.Database;
    using DreamFactory.Model.User;
    using DreamFactory.Serialization;

    internal partial class UserApi : BaseApi, IUserApi
    {
        public UserApi(IHttpAddress baseAddress, IHttpFacade httpFacade, IContentSerializer contentSerializer, HttpHeaders baseHeaders)
            : base(baseAddress, httpFacade, contentSerializer, baseHeaders, "user")
        {
        }

        public async Task<bool> RegisterAsync(Register register, bool login = false)
        {
            if (register == null)
            {
                throw new ArgumentNullException("register");
            }

            SqlQuery query = new SqlQuery();
            if (login)
            {
                query.CustomParameters.Add("login", true);
            }

            RegisterResponse response = await RequestSingleWithPayloadAsync<Register, RegisterResponse>(
                HttpMethod.Post, 
                new [] { "register " },
                query, 
                register);

            if ((response.Success ?? false) && login)
            {
                BaseHeaders.AddOrUpdate(HttpHeaders.DreamFactorySessionTokenHeader, response.SessionToken);
            }

            return response.Success ?? false;
        }

        public async Task<bool> UpdateProfileAsync(ProfileRequest profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }

            ProfileUpdateResponse response = await RequestSingleWithPayloadAsync<ProfileRequest, ProfileUpdateResponse>(HttpMethod.Post, new [] { "profile" }, new SqlQuery(), profile);
            return response.Success ?? false;
        }

        public async Task<ProfileResponse> GetProfileAsync()
        {
            return await RequestSingleAsync<ProfileResponse>(HttpMethod.Get, new[]{ "profile" }, new SqlQuery());
        }
    }
}

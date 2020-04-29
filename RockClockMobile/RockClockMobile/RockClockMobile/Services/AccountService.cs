using Newtonsoft.Json;
using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RockClockMobile.Services
{
    public class AccountService : IAccountService<ChangePasswordVM>
    {
        HttpClient client;
        Uri baseAddr;

        public AccountService()
        {
            baseAddr = new Uri("http://18.136.14.237:8282");
            client = new HttpClient { BaseAddress = baseAddr };
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<bool> AdminUserLogin(UserLogin userLogin)
        {
            if (userLogin == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(userLogin);

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = client.PostAsync($"api/Account/login", byteContent).Result;

            var readResponse = await result.Content.ReadAsStringAsync();

            var convertResponse = JsonConvert.DeserializeObject<UserLoginResponse>(readResponse);
            GlobalServices.AccessToken = convertResponse.token;

            return result.IsSuccessStatusCode;
        }

        public async Task<UserLoginM> UserLogin(UserLoginParam userLoginParam)
        {
            if (userLoginParam == null || !IsConnected)
                return null;

            var accessToken = GlobalServices.AccessToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var serializedItem = JsonConvert.SerializeObject(userLoginParam);

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = client.PostAsync($"api/Account/loginM", byteContent).Result;

            var readResponse = await result.Content.ReadAsStringAsync();

            var convertResponse = JsonConvert.DeserializeObject<UserLoginM>(readResponse);

            return convertResponse;
        }

        public async Task<bool> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (changePasswordVM != null && IsConnected)
            {
                var accessToken = GlobalServices.AccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var serializedItem = JsonConvert.SerializeObject(changePasswordVM);

                var response = await client.PostAsync($"api/Account/ChangePassword", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            return false;
        }
    }
}

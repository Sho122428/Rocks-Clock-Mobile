using Android.Content;
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
    public class UserLoginService : IUserLoginService<UserLogin>
    {
        HttpClient client;
        Uri baseAddr;

        public UserLoginService()
        {
            baseAddr = new Uri("http://18.136.14.237:8282");
            client = new HttpClient { BaseAddress = baseAddr };
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<bool> AddUserLogin(UserLogin userLogin)
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
    }
}

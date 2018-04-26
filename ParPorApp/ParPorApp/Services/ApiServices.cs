using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using ParPorApp.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParPorApp.Helpers;
using ParPorApp.Models;
using ParPorApp.Views;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Encoding = System.Text.Encoding;

namespace ParPorApp.Services

{
    internal class ApiServices
    {
        public async Task<bool> RegisterUserAsync(
            string email, string password, string confirmPassword)
        {
            var client = new HttpClient();

            var model = new RegisterBindingModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                Constants.BaseApiAddress + "api/Account/Register", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }


        // Login user
        public async Task<string> LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post, Constants.BaseApiAddress + "Token")
            {
                Content = new FormUrlEncodedContent(keyValues)
            };

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content);

            var accessTokenExpiration = jwtDynamic.Value<DateTime>(".expires");
            var accessToken = jwtDynamic.Value<string>("access_token");

            Settings.AccessTokenExpirationDate = accessTokenExpiration;

            Debug.WriteLine(accessTokenExpiration);

            Debug.WriteLine(content);

            return accessToken;
        }

        // get group list
        public async Task<List<Group>> GetGroupsAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/groups");

            var group = JsonConvert.DeserializeObject<List<Group>>(json);


            return group;
        }

		//Get user list
	    public async Task<List<User>> GetUsersAsync(string accessToken)
	    {
		    var client = new HttpClient();
	        client.MaxResponseContentBufferSize = 256000;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
			    "Bearer", accessToken);

		    var json = await client.GetAsync(Constants.BaseApiAddress + "api/Account/UserInfo");
	        string userJson = await json.Content.ReadAsStringAsync();

			var user = JsonConvert.DeserializeObject<List<User>>(userJson);
	        
            Debug.Write(userJson);
	        return user;
          
	    }

		// get events list
		public async Task<List<Event>> GetEventsAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/events?sort=desc");

            var events = JsonConvert.DeserializeObject<List<Event>>(json);

            return events;
        }
        
		// Put event
	    public async Task PutEventAsync(Event events, string accessToken)
	    {
		    var client = new HttpClient();
		    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

		    var json = JsonConvert.SerializeObject(events);
		    HttpContent content = new StringContent(json);
		    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

		    var response = await client.PutAsync(
			    Constants.BaseApiAddress + "api/events" + events.Id, content);
	    }

		//Post event
	    public async Task PostEventAsync(Event events, string accessToken)
	    {
		    var client = new HttpClient();
		    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var json = JsonConvert.SerializeObject(events);
			HttpContent content = new StringContent(json);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); 
			var response = await client.PostAsync(Constants.BaseApiAddress + "api/events", content);
		    if (response.IsSuccessStatusCode)
		    {
			    using (UserDialogs.Instance.Loading("Hang on...", null, null, true, MaskType.Black))
			    {
				    UserDialogs.Instance.Toast(string.Format("Event created! :)"));
				}
				
			}
		    else
		    {
				UserDialogs.Instance.Alert(string.Format("Uh oh :(", "something went wrong", "please try again"));
			    Debug.Write(response);
			}
			
	    }

		//public class AzureDataService
		//{
		//    public MobileServiceClient MobileService { get; set; }
		//    private IMobileServiceSyncTable eventTable;

		//    public async Task Initialize()
		//    {
		//        MobileService = new MobileServiceClient("https://parentportal.azurewebsites.net");
		//        const string path = "syncstore.db";
		//        //setup local sqlite store and init
		//        var store = new MobileServiceSQLiteStore(path);
		//        store.DefineTable();
		//        await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
		//        //get sync table that will call out azure
		//        eventTable = MobileService.GetSyncTable();

		//    }

		//    public async Task SyncEvent()
		//    {
		//        await eventTable.PullAsync("allEvents", eventTable.CreateQuery());
		//        await MobileService.SyncContext.PushAsync();
		//    }
		//}
	}
}

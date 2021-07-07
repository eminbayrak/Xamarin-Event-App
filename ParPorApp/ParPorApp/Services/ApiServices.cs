using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParPorApp.Helpers;
using ParPorApp.Models;
using Xamarin.Forms;
using System.Linq;
using Acr.UserDialogs;
using Plugin.Connectivity;
using MonkeyCache.SQLite;

namespace ParPorApp.Services

{
    internal class ApiServices
    {
        // Register account
        public async Task<bool> RegisterUserAsync(
            string email,
            string password,
            string confirmPassword,
            string firstName,
            string lastName,
            string teamName,
            string teamCode,
            DateTime accountDate)
        {
            var client = new HttpClient();
            var model = new Register
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                TeamName = teamName,
                ConfirmPassword = confirmPassword,
                TeamCode = teamCode,
                AccountDate = accountDate
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                Constants.BaseApiAddress + "api/Account/Register", httpContent);

            if (response.IsSuccessStatusCode)
            {
                using (UserDialogs.Instance.Loading("Hang on...", null, null, true, MaskType.Black))
                {
                    ToastConfig toastConfig = new ToastConfig("Your account has been successfully registered!");
                    toastConfig.SetDuration(4000);
                    toastConfig.SetBackgroundColor(Color.OrangeRed);
                    UserDialogs.Instance.Toast(toastConfig);
                }
                return true;
            }

            Console.WriteLine(response);

            await UserDialogs.Instance.AlertAsync("Something went wrong, please try again", "Uh oh!", "Ok");
            return false;
        }

        //Join member to team
        public async Task<bool> JoinTeamAsync(string teamName, string teamCode)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", Settings.AccessToken);
            var model = new Register
            {
                TeamName = teamName,
                TeamCode = teamCode
            };
            var json = JsonConvert.SerializeObject(model);
            
            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                Constants.BaseApiAddress + "api/Account/Update", httpContent);
            return true;
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

            try
            {
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
                return accessToken;
            }
            catch (Exception)
            {
                throw;
            }
            
            
        }

        // get group list
        public async Task<List<Group>> GetGroupsAsync(string accessToken)
        {
            Barrel.ApplicationId = "Api.Groups";
            var url = Constants.BaseApiAddress + "api/Groups";
            if (!CrossConnectivity.Current.IsConnected)
            {
                return Barrel.Current.Get<List<Group>>(key: url);
            }

            //Dev handles checking if cache is expired
            if (!Barrel.Current.IsExpired(key: url))
            {
                return Barrel.Current.Get<List<Group>>(key: url);
            }
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json = await client.GetStringAsync(url);            
            var group = JsonConvert.DeserializeObject<List<Group>>(json);
            Barrel.Current.Add(key: url, data: group, expireIn: TimeSpan.FromDays(1));
            return group;
        }
        
        // Get account groups
        public async Task<List<AccountGroups>> GetAccountGroupsAsync(string accessToken)
        {
            Barrel.ApplicationId = "Api.AccountGroups";
            var client = new HttpClient();
            var url = Constants.BaseApiAddress + "api/AccountGroups";
            if (!CrossConnectivity.Current.IsConnected)
            {
                return Barrel.Current.Get<List<AccountGroups>>(key: url);
            }

            //Dev handles checking if cache is expired
            if (!Barrel.Current.IsExpired(key: url))
            {
                return Barrel.Current.Get<List<AccountGroups>>(key: url);
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);
            var json = await client.GetStringAsync(url);

            var group = JsonConvert.DeserializeObject<List<AccountGroups>>(json);
            Barrel.Current.Add(key: url, data: group, expireIn: TimeSpan.FromDays(1));
            return group;
        }

        // Get team members
        public async Task<List<AccountGroups>> GetTeamMembersAsync(string accessToken)
        {
            Barrel.ApplicationId = "Api.AccountGroups.TeamMembers";
            var client = new HttpClient();
            var url = Constants.BaseApiAddress + "api/AccountGroups/TeamMembers";
            if (!CrossConnectivity.Current.IsConnected)
            {
                return Barrel.Current.Get<List<AccountGroups>>(key: url);
            }

            //Dev handles checking if cache is expired
            if (!Barrel.Current.IsExpired(key: url))
            {
                return Barrel.Current.Get<List<AccountGroups>>(key: url);
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);
            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/AccountGroups/TeamMembers");
            var group = JsonConvert.DeserializeObject<List<AccountGroups>>(json);
            Barrel.Current.Add(key: url, data: group, expireIn: TimeSpan.FromDays(1));
            return group;
        }

        //Get user list
        public async Task<List<User>> GetUsersAsync(string accessToken)
	    {
            Barrel.ApplicationId = "Api.Account.UserInfo";
            var client = new HttpClient();
            var url = Constants.BaseApiAddress + "api/Account/UserInfo";
            if (!CrossConnectivity.Current.IsConnected)
            {
                return Barrel.Current.Get<List<User>>(key: url);
            }

            //Dev handles checking if cache is expired
            if (!Barrel.Current.IsExpired(key: url))
            {
                return Barrel.Current.Get<List<User>>(key: url);
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
			    "Bearer", accessToken);

		    var json = await client.GetAsync(url);
	        string userJson = await json.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<List<User>>(userJson);
            Barrel.Current.Add(key: url, data: userJson, expireIn: TimeSpan.FromDays(1));
            return user;
	    }
        
        //Show all of the events
        public async Task<List<Event>> GetAllEventsAsync(string accessToken)
        {
            Barrel.ApplicationId = "Api.Events";
            var client = new HttpClient();
            var url = Constants.BaseApiAddress + "api/events";
            if (!CrossConnectivity.Current.IsConnected)
            {
                return Barrel.Current.Get<List<Event>>(key: url);
            }

            //Dev handles checking if cache is expired
            if (!Barrel.Current.IsExpired(key: url))
            {
                return Barrel.Current.Get<List<Event>>(key: url);
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(url);
            var events = JsonConvert.DeserializeObject<List<Event>>(json);
            //events = events.Where(x => x.EventType == "Game").ToList();
            //events = events.Where(x => x.EventDate >= DateTime.Now).ToList();
            events = events.OrderByDescending(x => x.EventDate).ToList();
            Barrel.Current.Add(key: url, data: events, expireIn: TimeSpan.FromDays(1));
            return events;
        }

        // Get list of Event && OrderBy EventDate && only show events that have todays and/or later date
        public async Task<List<Event>> GetEventsAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/events?sort=desc");
            var events = JsonConvert.DeserializeObject<List<Event>>(json);
            //events = events.Where(x => x.EventType == "Game").ToList();
            events = events.Where(x => x.EventDate >= DateTime.Now).ToList();
            events = events.OrderBy(x => x.EventDate).ToList();
            return events;
        }

        // Get list of Training Events && OrderBy EventDate
        public async Task<List<Event>> GetTrainingsAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/events?sort=desc");
            var events = JsonConvert.DeserializeObject<List<Event>>(json);
            events = events.Where(x => x.EventType == "Training").ToList();
            events = events.Where(x => x.EventDate >= DateTime.Now).ToList();
            events = events.OrderBy(x => x.EventDate).ToList();
            return events;
        }

        // Get list of Game Events && OrderBy EventDate
        public async Task<List<Event>> GetGamesAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/events?sort=desc");
            var events = JsonConvert.DeserializeObject<List<Event>>(json);
            //events = events.Where(x => x.TeamName == "A Team").ToList();
            events = events.Where(x => x.EventType == "Game").ToList();
            events = events.Where(x => x.EventDate >= DateTime.Now).ToList();
            events = events.OrderBy(x => x.EventDate).ToList();
            return events;
        }
        // Update User
        public async Task UpdateAsync(User user, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(
                Constants.BaseApiAddress + "api/Account/Update" + user.Id, content);
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
	        try
	        {
	            if (response.IsSuccessStatusCode)
	            {
	                using (UserDialogs.Instance.Loading("Hang on...", null, null, true, MaskType.Black))
	                {
	                    ToastConfig toastConfig = new ToastConfig("Event created :)");
	                    toastConfig.SetDuration(4000);
	                    toastConfig.SetBackgroundColor(Color.FromHex("#43b05c"));
	                    UserDialogs.Instance.Toast(toastConfig);
	                }

	            }
	            else
	            {
	                UserDialogs.Instance.Alert(string.Format("Uh oh :( something went wrong, please try again"));
	                Debug.Write(response);
	            }
            }
	        catch (Exception e)
	        {
	            Console.WriteLine(e);
	            throw;
	        }
	    }
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ParPorApp.Helpers;
using ParPorApp.Models;
using ParPorApp.Services;
using Xamarin.Forms;

namespace ParPorApp.ViewModels
{
    internal class AddEventViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();
        public string Note { get; set; }
        public string LocationAddress { get; set; }
        public int UserId { get; set; }
        public string Id { get; set; }
        public string PlaceId { get; set; }
        public DateTime EventDate { get; set; }
        public string EventType { get; set; }
        public string EventIcon { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public string TeamName { get; set; }
        public string OpponentTeamName { get; set; }
        public ICommand AddEventCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var events = new Event
                    {
                        EventType = EventType,
                        Note = Note,
                        EventDate = EventDate,
                        UserId = UserId,
                        PlaceId = PlaceId,
                        EventIcon = EventIcon,
                        LocationAddress = LocationAddress,
                        LocationLatitude = LocationLatitude,
                        LocationLongitude = LocationLongitude,
                        TeamName = TeamName,
                        OpponentTeamName = OpponentTeamName
                    };
                    await _apiServices.PostEventAsync(events, Settings.AccessToken);
                });
            }
        }
    }
}

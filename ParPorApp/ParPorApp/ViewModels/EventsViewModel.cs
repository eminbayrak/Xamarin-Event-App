using ParPorApp.Helpers;
using ParPorApp.ViewModels;
using ParPorApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
 using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ParPorApp.Models;
using Xamarin.Forms;

namespace ParPorApp.ViewModels
{
    public class EventsViewModel : INotifyPropertyChanged
    {
        private readonly ApiServices _apiServices = new ApiServices();
        private List<Event> _events;
        private Event item;

        public EventsViewModel()
        {
        }

        public EventsViewModel(Event item)
        {
            this.item = item;
        }


        //public string AccessToken { get; set; }

        public List<Event> Event
        {
            get => _events;
            set
            {
                _events = value;
                OnPropertyChanged();
            } 
        }

        //Fecthing all events
        public ICommand GetAllEventsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accessToken = Settings.AccessToken;
                    Event = await _apiServices.GetAllEventsAsync(accessToken);


                });
            }
        }
        //Fecthing upcoming events that have todays and/or later date
        public ICommand GetEventsCommand
        {
            get
            { return new Command(async () =>
              {
	              var accessToken = Settings.AccessToken;
				  Event = await _apiServices.GetEventsAsync(accessToken);
                  

              });
            }
        }

        //Fetching training events
        public ICommand GetTrainingsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accessToken = Settings.AccessToken;
                    Event = await _apiServices.GetTrainingsAsync(accessToken);


                });
            }
        }

        //Fetching game events
        public ICommand GetGamessCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accessToken = Settings.AccessToken;
                    Event = await _apiServices.GetGamesAsync(accessToken);


                });
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

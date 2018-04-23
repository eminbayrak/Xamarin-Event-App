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
    class AddEventViewModel
    {
		ApiServices _apiServices = new ApiServices();
	    public string Description { get; set; }
	    public string EndDateTime { get; set; }
	    public string GroupId { get; set; }
	    public string Id { get; set; }
	    public string LocationId { get; set; }
	    public string StartDateTime { get; set; }
	    public string Name { get; set; }
        public string LocationName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public ICommand AddEventCommand
	    {
            get
		    {
				return new Command(async () =>
				{
					
						var events = new Event()
						{

							Name = Name,
                            Description = Description,
							StartDateTime = StartDateTime,
							EndDateTime = EndDateTime,
                            GroupId = GroupId,
                            LocationId = LocationId,
                            Longitude = Longitude
						};
						await _apiServices.PostEventAsync(events, Settings.AccessToken);
					
				});
		    }
	    }

    }
}

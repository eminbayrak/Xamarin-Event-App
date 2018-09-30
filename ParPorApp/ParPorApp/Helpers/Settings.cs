// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using ParPorApp.ViewModels;
using Plugin.Calendars.Abstractions;

namespace ParPorApp.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;
        const string CalendarKey = "calendar_key";
        const bool CalendarDefault = false;
        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }


        private const string LongitudeKey = "Longitude_key";
        private static readonly double LangitudeDefaultKey = 0;
        public static double LongitudeKeySettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(LongitudeKey, LangitudeDefaultKey);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }
        public static string LastUsedEmail
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }
        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault("Email", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Email", value);
            }
        }
        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Password", value);
            }
        }

        public static string FirstName
        {
            get
            {
                return AppSettings.GetValueOrDefault("FirstName", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("FirstName", value);
            }
        }

        public static string LastName
        {
            get
            {
                return AppSettings.GetValueOrDefault("LastName", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("LastName", value);
            }
        }
        public static string TeamName
        {
            get
            {
                return AppSettings.GetValueOrDefault("TeamName", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("TeamName", value);
            }
        }
        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }

        public static DateTime AccessTokenExpirationDate
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessTokenExpirationDate", DateTime.UtcNow);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessTokenExpirationDate", value);
            }
        }

        public static bool AddedToCalendar
        {
            get => AppSettings.GetValueOrDefault(CalendarKey, CalendarDefault);
            set => AppSettings.AddOrUpdateValue(CalendarKey, value);
        }
        public static Calendar calendar { get; internal set; }
    }
}
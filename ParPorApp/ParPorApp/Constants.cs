namespace ParPorApp
{
    public static class Constants
    {
        //Azure
        public static string BaseApiAddress => "http://sportiveportal.azurewebsites.net/";

        //Home
        //public static string BaseApiAddress => "http://192.168.1.5:5000/";

        //Work
        //public static string BaseApiAddress => "http://10.65.234.100:5000/";
        //public static string BaseApiAddress => "http://10.65.233.20:5000/";

        //Localhost
        //public static string BaseApiAddress => "http://localhost:55601/";

        //public static int LogoIconHeight = 120;
    }
    public static class MessageKeys
    {
        public const string NavigateToEvent = "navigate_event";
        public const string NavigateToSession = "navigate_session";
        public const string NavigateToSpeaker = "navigate_speaker";
        public const string NavigateToSponsor = "navigate_sponsor";
        public const string NavigateToImage = "navigate_image";
        public const string NavigateLogin = "navigate_login";
        public const string Error = "error";
        public const string Connection = "connection";
        public const string LoggedIn = "loggedin";
        public const string Message = "message";
        public const string Question = "question";
        public const string Choice = "choice";
    }
}

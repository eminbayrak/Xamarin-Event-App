using System;
using ParPorApp.Models;

namespace ParPorApp.Services
{
    public interface IFacebookManager
    {
        void Login(Action<FacebookUser, string> onLoginComplete);

        void Logout();
    }
}
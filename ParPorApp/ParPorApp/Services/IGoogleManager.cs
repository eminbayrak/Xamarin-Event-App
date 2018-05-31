using System;
using ParPorApp.Models;

namespace ParPorApp.Services
{
    public interface IGoogleManager
    {
        void Login(Action<GoogleUser, string> OnLoginComplete);

        void Logout();
    }
}
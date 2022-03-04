using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using ShopApp.WebUI.Users;
using System;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Services
{
    public class MyHub :Hub
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ShopApp.WebUI.Models.DTO;
using ShopApp.WebUI.Services;
using ShopApp.WebUI.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IHubContext<MyHub> _hubContext;
             
        public UserController(UserManager<User> userManager, 
            SignInManager<User> signInManager,
            IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserDTO  userDTO)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.UserName = userDTO.UserName;
                user.Email = userDTO.Password;

                var result =await _userManager.CreateAsync(user, userDTO.Password);
                if (result.Succeeded){return Ok();}
                else
                {   result.Errors.ToList().ForEach(x => ModelState.AddModelError("", x.Description));}
            }
            return View(userDTO);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserDTO  userDTO)
        {
            var result =
                await _signInManager.PasswordSignInAsync(userDTO.UserName, userDTO.Password,false,false);
            if (result.Succeeded)
            {
                await _hubContext.Clients.Client("858789c2-9421-4afd-9cc7-468065facc12").SendAsync("sendToUser",userDTO.Password);
                //await _hubContext.Clients.All.SendAsync("sendToUser",userDTO.Password);
                return RedirectToAction("Index");
            }
            return View(userDTO);
        }

        public IActionResult SendNotificationToUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendNotificationToUser(string message)
        {
            //user id :858789c2-9421-4afd-9cc7-468065facc12
            await _hubContext.Clients.User("858789c2-9421-4afd-9cc7-468065facc12").SendAsync("send",message);
            return RedirectToAction("Index");
        }
        public ActionResult SendNotificationToEverybody()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> SendNotificationToEverybody(string message)
        {
            await _hubContext.Clients.All.SendAsync("send",message);
            return RedirectToAction("Index");
        }
    }
}

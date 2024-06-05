using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager ,IMapper mapper)
        {
			_userManager = userManager;
			_roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
                var users = await _userManager.Users.ToListAsync();
				var UserMapped = _mapper.Map<IReadOnlyList<ApplicationUser>,IReadOnlyList<UserViewModel>>(users);
				foreach (var User in UserMapped)
				{
					var user = await _userManager.FindByIdAsync(User.Id);
                    User.Roles = _userManager.GetRolesAsync(user).Result;
					User.FName = user.FNmae;

                }
				return View(UserMapped);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(email);
				if (user != null)
				{
					var mappedUser = new UserViewModel
					{
						Id = user.Id,
						FName = user.FNmae,
						LName = user.LName,
						PhoneNumber = user.PhoneNumber,
						Email = user.Email,
						Roles = _userManager.GetRolesAsync(user).Result
					};
					return View(new List<UserViewModel> {mappedUser});
				}
			}
			
			return View(Enumerable.Empty<UserViewModel>());
		}



        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id == null)
            {
                return BadRequest();//400
            }
			var user = await _userManager.FindByIdAsync(id);
			
            if (user == null)
            {
                return NotFound(); //404
            }
            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);
            return View(viewName, mappedUser);
        }
    }
}

using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EventMaker.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileManager _profileManager;
        private readonly IMapper _mapper;

            public ProfileController(IProfileManager profileManager,
                                 IMapper mapper)
        {
            _profileManager = profileManager ?? throw new ArgumentNullException(nameof(profileManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var profile = await _profileManager.GetProfile(User.Identity.Name);
            var profileView = _mapper.Map<ProfileDto, ProfileViewModel>(profile);
            return View(profileView);
        }


        [HttpGet]
        public async Task<IActionResult> EditProfileIndex()
       {
            var userProfile = await _profileManager.GetProfile(User.Identity.Name);
            var profileViewModel = _mapper.Map<ProfileEditViewModel>(userProfile);
            return View(profileViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userProfile = await _profileManager.GetProfile(User.Identity.Name);
                _mapper.Map<ProfileEditViewModel, ProfileDto>(model, userProfile);
                await _profileManager.EditProfileAsync(userProfile);
            }
            else
            {
                return NotFound("Profile not found"); /// TODO : rework this exceptions
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

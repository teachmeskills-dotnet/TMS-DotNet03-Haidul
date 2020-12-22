using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Common.Exceptions;
using EventMaker.Common.Resources;
using EventMaker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventMaker.Web.Controllers
{
    [Authorize]
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
                await _profileManager.UpdateProfileAsync(userProfile);
            }
            else
            {
                return NotFound(ModelErrorsResource.ProfileNotFound);
            }
            return RedirectToAction("Index", "Profile");
        }

        [HttpPost]
        public async Task<IActionResult> SetAvatar(ProfileViewModel model)
        {
            if (model != null)
            {
                if (model.Image != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Image.Length);
                    }

                    var userProfile = await _profileManager.GetProfile(User.Identity.Name);

                    _mapper.Map<ProfileViewModel, ProfileDto>(model, userProfile);
                    userProfile.Image = imageData;
                    await _profileManager.UpdateProfileAsync(userProfile);
                }
                return RedirectToAction("Index");
            }
            throw new NotFoundException(ExceptionResource.ProfileNotFound);
        }
    }
}

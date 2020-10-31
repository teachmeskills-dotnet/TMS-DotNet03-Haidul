using System;
using System.Threading.Tasks;
using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
    }
}

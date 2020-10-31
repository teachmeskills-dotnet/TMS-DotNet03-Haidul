using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Common.Exceptions;
using EventMaker.Common.Resources;
using System;
using System.Threading.Tasks;
using Profile = EventMaker.DAL.Entities.Profile;

namespace EventMaker.BLL.Managers
{
    public class ProfileManager : IProfileManager
    {
        private readonly IRepository<Profile> _repositoryProfile;
        private readonly IMapper _mapper;

        public ProfileManager(IRepository<Profile> repositoryProfile,
                              IMapper mapper)
        {
            _repositoryProfile = repositoryProfile ?? throw new ArgumentNullException(nameof(repositoryProfile));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateProfileAsync(string email, string userName , string userId)
        {
            var profile = new Profile
            {
                Email = email,
                Username = userName,
                Created = DateTime.Now,
                UserId = userId              
            };
            await _repositoryProfile.AddAsync(profile);

            await _repositoryProfile.SaveChangesAsync();
        }

        public async Task<ProfileDto> GetProfile(string userName)
        {
            var profile = await _repositoryProfile.GetEntityAsync(profile => profile.Username == userName);
            if (profile != null)
            {
                var profileDto = _mapper.Map<Profile, ProfileDto>(profile);
                return profileDto;
            }
            throw new ProfileNotFoundException(ExceptionResource.ProfileNotFound);
        }
    }
}
       

       

      
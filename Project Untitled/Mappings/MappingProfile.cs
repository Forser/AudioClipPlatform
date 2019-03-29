using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project_Untitled.Models;
using Project_Untitled.Models.ViewModels;

namespace Project_Untitled.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserSettingsViewModel, UserSettings>();
            CreateMap<UserSettings, UserSettingsViewModel>();

            CreateMap<NotificationsViewModel, Notifications>();
            CreateMap<Notifications, NotificationsViewModel>();

            CreateMap<IdentityUser, UserViewModel>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)).ForAllOtherMembers(opts => opts.Ignore());

            CreateMap<Message, MessageViewModel>();
            CreateMap<Message, NewMessageViewModel>();
            CreateMap<NewMessageViewModel, Message>();
        }
    }
}
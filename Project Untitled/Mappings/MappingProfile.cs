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
            CreateMap<UserSettings, UserProfileViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.NumberOfFollowers, opt => opt.Ignore())
                .ForMember(dest => dest.NumberOfPublishedClips, opt => opt.Ignore())
                .ForMember(dest => dest.NumOfMembersYouFollow, opt => opt.Ignore());

            CreateMap<NotificationsViewModel, Notifications>();
            CreateMap<Notifications, NotificationsViewModel>();

            CreateMap<Comment, CommentViewModel>();
            CreateMap<CommentViewModel, Comment>();

            CreateMap<Clips, ClipWithCommentsViewModel>();
            CreateMap<ClipWithCommentsViewModel, Clips>();

            CreateMap<IdentityUser, UserViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForAllOtherMembers(opts => opts.Ignore());

            CreateMap<UserSettings, UserViewModel>()
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImages))
                .ForAllOtherMembers(opts => opts.Ignore());

            CreateMap<FileUploadView, Clips>().ForMember(dest => dest.FileStatus, opt => opt.Ignore())
                                              .ForMember(dest => dest.Id, opt => opt.Ignore())
                                              .ForMember(dest => dest.Likes, opt => opt.Ignore())
                                              .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                                              .ForMember(dest => dest.UploadAt, opt => opt.Ignore())
                                              .ForMember(dest => dest.UserSettings, opt => opt.Ignore());

            CreateMap<Message, MessageViewModel>();
            CreateMap<Message, NewMessageViewModel>();
            CreateMap<NewMessageViewModel, Message>();
        }
    }
}
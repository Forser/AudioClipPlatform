using Microsoft.AspNetCore.Identity;
using Project_Untitled.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Untitled.Models
{
    public interface IClipsRepository
    {
        List<Clips> GetClipsForUser(IdentityUser user);

        bool ChangeClipStatus(int Id, IdentityUser user, FileStatus status);

        Task<bool> DeleteClip(int id, IdentityUser user);

        Clips GetClipWithComments(int Id);

        Task<bool> SendComment(Comment newComment);
    }
}
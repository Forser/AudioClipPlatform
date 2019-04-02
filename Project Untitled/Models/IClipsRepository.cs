using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectUntitled.Models
{
    public interface IClipsRepository
    {
        IQueryable<Clips> Clips { get; }

        List<Clips> GetClipsForUser(IdentityUser user);

        bool ChangeClipStatus(int Id, IdentityUser user, FileStatus status);

        Task<bool> DeleteClip(int id, IdentityUser user);

        Clips GetClipWithComments(int Id);

        Task<bool> SendComment(Comment newComment);

        List<Clips> GetClips();
    }
}
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Project_Untitled.Models
{
    public class ClipsRepository : IClipsRepository
    {
        private readonly AppIdentityDbContext Context;

        public ClipsRepository(AppIdentityDbContext ctx)
        {
            Context = ctx;
        }

        public bool ChangeClipStatus(int Id, IdentityUser user, FileStatus status)
        {
            bool changedStatus = false;
            var clip = Context.Clips.Where(s => s.Id == Id).FirstOrDefault();

            if(clip != null)
            {
                if(clip.FileStatus != status)
                {
                    clip.FileStatus = status;
                    Context.Clips.Update(clip);
                    Context.SaveChangesAsync();

                    changedStatus = true;
                }
            }

            return changedStatus;
        }

        public List<Clips> GetClipsForUser(IdentityUser user)
        {
            var UserClips = Context.Clips.Where(u => u.OwnerId == user.Id).ToList();

            return UserClips;
        }
    }
}

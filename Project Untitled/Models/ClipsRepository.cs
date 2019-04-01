﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Untitled.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var clip = Context.Clips.Where(s => s.Id == Id && s.OwnerId == user.Id).FirstOrDefault();

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

        public async Task<bool> DeleteClip(int id, IdentityUser user)
        {
            bool clipDeleted = false;
            var clip = Context.Clips.Where(s => s.Id == id && s.OwnerId == user.Id).FirstOrDefault();

            if(clip != null)
            {
                clip.FileStatus = FileStatus.Deleted;
                Context.Clips.Update(clip);
                int x = await Context.SaveChangesAsync();

                if(x > 0)
                {
                    clipDeleted = true;
                }
            }

            return clipDeleted;
        }

        public List<Clips> GetClipsForUser(IdentityUser user)
        {
            var UserClips = Context.Clips.Where(u => u.OwnerId == user.Id).ToList();

            return UserClips;
        }

        public Clips GetClipWithComments(int Id)
        {
            var clipWithComments = Context.Clips
                .Include(s => s.Comments)
                .Where(i => i.Id == Id && i.FileStatus == FileStatus.Listed)
                .FirstOrDefault();
            
            return clipWithComments;
        }

        public async Task<bool> SendComment(Comment newComment)
        {
            bool Succeeded = false;
            newComment.PostedAt = DateTime.Now;

            Context.Comments.Update(newComment);
            var result = await Context.SaveChangesAsync();
            if (result > 0) { Succeeded = true; }

            return Succeeded;
        }
    }
}

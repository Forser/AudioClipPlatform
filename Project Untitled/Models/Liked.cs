﻿namespace Project_Untitled.Models
{
    public class Liked
    {
        public int Id { get; set; }

        public int FileId { get; set; }

        public string LikeByUserId { get; set; }

        public int ClipId { get; set; }
        public Clips Clips { get; set; }
    }
}
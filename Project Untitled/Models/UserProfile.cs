using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Untitled.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        public int Followers { get; set; }

        public int UserId { get; set; }
        public UserHandler User { get; set; }

        public IList<Following> Following { get; set; }

        public IList<Clips> Clips { get; set; }

    }
}
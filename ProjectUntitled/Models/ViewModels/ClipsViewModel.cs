using System.Collections.Generic;

namespace ProjectUntitled.Models.ViewModels
{
    public class ClipsViewModel
    {
        public IEnumerable<Clips> Clips { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
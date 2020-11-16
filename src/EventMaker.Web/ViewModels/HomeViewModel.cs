using System.Collections.Generic;
using EventMaker.BLL.Models;

namespace EventMaker.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<EventDto> EventDtos { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}

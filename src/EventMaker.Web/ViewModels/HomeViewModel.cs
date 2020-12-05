using System.Collections.Generic;
using EventMaker.BLL.Models;
using EventMaker.Common.Enums;

namespace EventMaker.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<EventDto> EventDtos { get; set; }

        public FilterOptions? FilterOptions { get; set; }

        public EventFormats? EventFormats { get; set; }

        public PageViewModel PageViewModel { get; set; }
        //public FilterViewModel? FilterViewModel { get; set; }
    }
}

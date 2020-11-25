using System.Collections.Generic;
using EventMaker.BLL.Models;
using EventMaker.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventMaker.Web.ViewModels
{
    public class FilterViewModel
    {
        public FilterOptions? FilterOptions { get; }

        public EventFormats? EventFormats { get; }

        public string SelectedName { get; private set; }

        public FilterViewModel(FilterOptions filterOptions , EventFormats eventFormats, string name)
        {
            FilterOptions = filterOptions;
            EventFormats = eventFormats;
            SelectedName = name;
        }

    }
}


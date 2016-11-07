using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrokkAndOdin.Models;

namespace BrokkAndOdin.ViewModels
{
    public class RememberWhenViewModel
    {
        public RememberWhenViewModel()
        {
            TimePeriods = new List<RememberWhenTimePeriodViewModel>();
        }

        public IList<RememberWhenTimePeriodViewModel> TimePeriods { get; set; } 
    }

    public class RememberWhenTimePeriodViewModel
    {
        public IList<Photo> Photos { get; set; }
        public string Description { get; set; }
    }
}
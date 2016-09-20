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
            WeekAgo = new List<Photo>();
            MonthAgo = new List<Photo>();
            SixMonthsAgo = new List<Photo>();
            YearAgo = new List<Photo>();
        }
        public IList<Photo> WeekAgo { get; set; }
        public IList<Photo> MonthAgo { get; set; }
        public IList<Photo> SixMonthsAgo { get; set; }
        public IList<Photo> YearAgo { get; set; } 
    }
}
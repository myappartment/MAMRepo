using System;
using System.Collections.Generic;
using System.Text;

namespace MAM.Mobile.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Customers,
        JobMaster
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MAM.Mobile
{
    public static class Utility
    {
        public static string Role;
        public static string GetRole(string user)
        {
            if (user == "prageeth")
            {
                return "Consumer";
            }
            return "Producer";
        }
    }
}

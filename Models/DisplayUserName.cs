using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlteraBarberShop.Models
{
    public class DisplayUserName
    {
        public static string passUserName;
        public static void getUserName(string UserName)
        {
            passUserName = UserName;
        }
    }
}
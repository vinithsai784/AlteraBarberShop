using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlteraBarberShop.Models
{
    public class AppointmentClass
    {
        internal DateTime? createdDateTime;

        public int UserID { get; set; }
        public string DateTime { get; set; }
        public int StyleId { get; set; }
        public int StatusId { get; set; }
        public int FacialId { get; set; }
        public string Address { get; set; }
    }
}
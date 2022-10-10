using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlteraBarberShop.Models
{
    public class AppointmentView
    {


        public int UserID { get; set; } 
        public string DateTime { get; set; }
        public int StyleId { get; set; }
        public int StatusId { get; set; }
        public int FacialId { get; set; }
        public string Address { get; set; }
        
        IEnumerable<StylesView> ListOfStyles { get; set; }
        
    }
}
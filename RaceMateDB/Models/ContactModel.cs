using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RaceMateDB.Models
{
    public class ContactModel
    {

        public string Message { get; set; }
       
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PhoneNumber { get; set; }
        public string SupportEmail { get; set; }
        public string MarketingEMail { get; set; }
    }
}
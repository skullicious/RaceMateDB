using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceMateDB.Models
{
    public class EventReview
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public int EventId { get; set; }
        public string ReviewerName { get; set; }

        public virtual int EventModelId { get; set; }

    }
}
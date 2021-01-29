using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DWA_Assignment1.Models
{
    public class Event
    {
        [Key, Column(Order = 0)]
        public int EventId { get; set; }
        public int? MeetId { get; set; }
        public string AgeRange { get; set; }
        public string Gender { get; set; }
        public string Distance { get; set; }
        public string Stroke { get; set; }
        public int Round { get; set; }
        public virtual List<Lane> Lanes { get; set; }
        //public Lane Lane1 { get; set; }
        //public Lane Lane2 { get; set; }
        //public Lane Lane3 { get; set; }
        //public Lane Lane4 { get; set; }
        //public Lane Lane5 { get; set; }
        //public virtual Meet Meet { get; set; }
    }
}
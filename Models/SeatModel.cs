using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BookingApi.Models
{
    public class SeatItem
    {
        [Key]
        public string SeatNumber { get; set;}
        public string EmailAddress { get; set; }
        public string UniqueName { get; set; }
        public bool Booked { get; set; }

    }

    public class SeatTransactions
    {
        [JsonProperty("seats")]
        public List<SeatItem> seats {get; set;}

    }

}
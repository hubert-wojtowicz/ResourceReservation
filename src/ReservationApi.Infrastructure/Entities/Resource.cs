﻿namespace ReservationApi.Infrastructure
{
    public class Resource
    {
        public Guid ResourceId { get; set; }
        public List<string> Tags { get; set; }
        public Reservation? Reservation { get; set; }
    }
}
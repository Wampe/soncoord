﻿using System;

namespace Soncoord.Infrastructure.Models
{
    public class Queue
    {
        public string Id { get; set; }
        public string Note { get; set; }
        public string BotRequestBy { get; set; }
        public string NonlistSong { get; set; }
        public double DonationAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ReadOnly { get; set; }
        public SongRequest Song { get; set; }
        public SongRequester[] Requests { get; set; }
        public string SongId { get; set; }
        public string StreamerId { get; set; }
        public int Position { get; set; }
    }
}

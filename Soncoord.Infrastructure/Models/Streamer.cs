using Soncoord.Infrastructure.Interfaces;
using System;

namespace Soncoord.Infrastructure.Models
{
    public class Streamer : IStreamer
    {
        public bool AllowDuplicates { get; set; }
        public bool AllowLiveLearns { get; set; }
        public bool BotActive { get; set; }
        public bool CanAnonymousEnterName { get; set; }
        public bool CanAnonymousRequest { get; set; }
        public bool CanFollowerRequest { get; set; }
        public bool CanSubscriberRequest { get; set; }
        public bool CanSubscriberT2Request { get; set; }
        public bool CanSubscriberT3Request { get; set; }
        public bool CanUserRequest { get; set; }
        public bool ConcurrentRequestsPerAnonymous { get; set; }
        public bool ConcurrentRequestsPerFollower { get; set; }
        public bool ConcurrentRequestsPerSub { get; set; }
        public bool ConcurrentRequestsPerSubTier2 { get; set; }
        public bool ConcurrentRequestsPerSubTier3 { get; set; }
        public bool ConcurrentRequestsPerUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DonationPage { get; set; }
        public bool DonationsIgnoreLimits { get; set; }
        public string Id { get; set; }
        public bool LimitAnonymousRequests { get; set; }
        public bool LimitFollowerRequests { get; set; }
        public bool LimitSubscriberRequests { get; set; }
        public bool LimitSubscriberT2Requests { get; set; }
        public bool LimitSubscriberT3Requests { get; set; }
        public bool LimitUserRequests { get; set; }
        public bool LiveLearnsNoSongFound { get; set; }
        public int MaxRequests { get; set; }
        public int MinAmount { get; set; }
        public int MinLiveLearnAmount { get; set; }
        public int MinutesBetweenRequests { get; set; }
        public string Name { get; set; }
        public int NewDays { get; set; }
        public string QueueMethod { get; set; }
        public string RequestMode { get; set; }
        public string RequestText { get; set; }
        public bool RequestsActive { get; set; }
        public int RequestsPerAnonymous { get; set; }
        public int RequestsPerFollower { get; set; }
        public int RequestsPerSub { get; set; }
        public int RequestsPerSubTier2 { get; set; }
        public int RequestsPerSubTier3 { get; set; }
        public int RequestsPerUser { get; set; }
        public int SessionLength { get; set; }
        public bool ShowDonationLink { get; set; }
        public string SubPage { get; set; }
        public string Theme { get; set; }
        public string UserId { get; set; }
    }
}

namespace Soncoord.Infrastructure.Interfaces
{
    public interface IStreamerQueueSettings
    {
        bool AllowDuplicates { get; set; } 
        bool AllowLiveLearns { get; set; } 
        bool CanAnonymousEnterName { get; set; } 
        bool CanAnonymousRequest { get; set; } 
        bool CanFollowerRequest { get; set; } 
        bool CanSubscriberRequest { get; set; } 
        bool CanSubscriberT2Request { get; set; } 
        bool CanSubscriberT3Request { get; set; } 
        bool CanUserRequest { get; set; } 
        int ConcurrentRequestsPerAnonymous { get; set; } 
        int ConcurrentRequestsPerFollower { get; set; } 
        int ConcurrentRequestsPerSub { get; set; } 
        int ConcurrentRequestsPerSubTier2 { get; set; } 
        int ConcurrentRequestsPerSubTier3 { get; set; } 
        int ConcurrentRequestsPerUser { get; set; } 
        bool DonationsIgnoreLimits { get; set; } 
        bool LimitAnonymousRequests { get; set; } 
        bool LimitFollowerRequests { get; set; } 
        bool LimitSubscriberRequests { get; set; } 
        bool LimitSubscriberT2Requests { get; set; } 
        bool LimitSubscriberT3Requests { get; set; } 
        bool LimitUserRequests { get; set; } 
        bool LiveLearnsNoSongFound { get; set; } 
        int MaxRequests { get; set; } 
        int MinAmount { get; set; } 
        int MinLiveLearnAmount { get; set; } 
        int MinutesBetweenRequests { get; set; } 
        string QueueMethod { get; set; } 
        bool RequestsActive { get; set; } 
        int RequestsPerAnonymous { get; set; } 
        int RequestsPerFollower { get; set; } 
        int RequestsPerSub { get; set; } 
        int RequestsPerSubTier2 { get; set; } 
        int RequestsPerSubTier3 { get; set; } 
        int RequestsPerUser { get; set; } 
        int SessionLength { get; set; } 
    }
}

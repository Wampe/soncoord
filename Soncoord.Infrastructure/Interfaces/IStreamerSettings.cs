using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soncoord.Infrastructure.Interfaces
{
    public interface IStreamerQueueSettings
    {
        //allowDuplicates: false
        //allowLiveLearns: false
        //canAnonymousEnterName: false
        //canAnonymousRequest: false
        //canFollowerRequest: true
        //canSubscriberRequest: true
        //canSubscriberT2Request: true
        //canSubscriberT3Request: true
        //canUserRequest: false
        //concurrentRequestsPerAnonymous: 1
        //concurrentRequestsPerFollower: 3
        //concurrentRequestsPerSub: 10
        //concurrentRequestsPerSubTier2: 4
        //concurrentRequestsPerSubTier3: 5
        //concurrentRequestsPerUser: 1
        //donationsIgnoreLimits: true
        //limitAnonymousRequests: true
        //limitFollowerRequests: true
        //limitSubscriberRequests: true
        //limitSubscriberT2Requests: true
        //limitSubscriberT3Requests: true
        //limitUserRequests: true
        //liveLearnsNoSongFound: false
        //maxRequests: 0
        //minAmount: 0
        //minLiveLearnAmount: 0
        //minutesBetweenRequests: 60
        //queueMethod: "fifo"
        //requestsActive: true
        //requestsPerAnonymous: 1
        //requestsPerFollower: 50
        //requestsPerSub: 100
        //requestsPerSubTier2: 6
        //requestsPerSubTier3: 7
        //requestsPerUser: 10
        //sessionLength: 8

        bool AllowDuplicates { get; set; } //allowDuplicates: false
        bool AllowLiveLearns { get; set; } //allowLiveLearns: false
        bool CanAnonymousEnterName { get; set; } //canAnonymousEnterName: false
        bool CanAnonymousRequest { get; set; } //canAnonymousRequest: false
        bool CanFollowerRequest { get; set; } //canFollowerRequest: true
        bool CanSubscriberRequest { get; set; } //canSubscriberRequest: true
        bool CanSubscriberT2Request { get; set; } //canSubscriberT2Request: true
        bool CanSubscriberT3Request { get; set; } //canSubscriberT3Request: true
        bool CanUserRequest { get; set; } //canUserRequest: false
        int ConcurrentRequestsPerAnonymous { get; set; } //concurrentRequestsPerAnonymous: 1
        int ConcurrentRequestsPerFollower { get; set; } //concurrentRequestsPerFollower: 3
        int ConcurrentRequestsPerSub { get; set; } //concurrentRequestsPerSub: 10
        int ConcurrentRequestsPerSubTier2 { get; set; } //concurrentRequestsPerSubTier2: 4
        int ConcurrentRequestsPerSubTier3 { get; set; }  //concurrentRequestsPerSubTier3: 5
        int ConcurrentRequestsPerUser { get; set; } //concurrentRequestsPerUser: 1
        bool DonationsIgnoreLimits { get; set; } //donationsIgnoreLimits: true
        bool LimitAnonymousRequests { get; set; } //limitAnonymousRequests: true
        bool LimitFollowerRequests { get; set; } //limitFollowerRequests: true
        bool LimitSubscriberRequests { get; set; } //limitSubscriberRequests: true
        bool LimitSubscriberT2Requests { get; set; } //limitSubscriberT2Requests: true
        bool LimitSubscriberT3Requests { get; set; } //limitSubscriberT3Requests: true
        bool LimitUserRequests { get; set; } //limitUserRequests: true
        bool LiveLearnsNoSongFound { get; set; } //liveLearnsNoSongFound: false
        int MaxRequests { get; set; } //maxRequests: 0
        int MinAmount { get; set; } //minAmount: 0
        int MinLiveLearnAmount { get; set; } // minLiveLearnAmount: 0
        int MinutesBetweenRequests { get; set; } //minutesBetweenRequests: 60
        string QueueMethod { get; set; } //queueMethod: "fifo"
        bool RequestsActive { get; set; } //requestsActive: false
        int RequestsPerAnonymous { get; set; } //requestsPerAnonymous: 1
        int RequestsPerFollower { get; set; } //requestsPerFollower: 50
        int RequestsPerSub { get; set; } // requestsPerSub: 100
        int RequestsPerSubTier2 { get; set; } //requestsPerSubTier2: 6
        int RequestsPerSubTier3 { get; set; } //requestsPerSubTier3: 7
        int RequestsPerUser { get; set; } //requestsPerUser: 10
        int SessionLength { get; set; } //sessionLength:8
    }
}

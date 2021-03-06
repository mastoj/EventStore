﻿// Copyright (c) 2012, Event Store LLP
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
// 
// Redistributions of source code must retain the above copyright notice,
// this list of conditions and the following disclaimer.
// Redistributions in binary form must reproduce the above copyright
// notice, this list of conditions and the following disclaimer in the
// documentation and/or other materials provided with the distribution.
// Neither the name of the Event Store LLP nor the names of its
// contributors may be used to endorse or promote products derived from
// this software without specific prior written permission
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 
using System;
using System.Collections.Generic;
using EventStore.Core.Messaging;

namespace EventStore.Core.Messages
{
    public static class MonitoringMessage
    {
        public class GetFreshStats : Message
        {
            public readonly IEnvelope Envelope;
            public readonly Func<Dictionary<string, object>, Dictionary<string, object>> StatsSelector;
            public readonly bool UseMetadata;
            public readonly bool UseGrouping;

            public GetFreshStats(IEnvelope envelope, 
                Func<Dictionary<string, object>, Dictionary<string, object>> statsSelector, 
                bool useMetadata,
                bool useGrouping)
            {
                Envelope = envelope;
                StatsSelector = statsSelector;
                UseMetadata = useMetadata;
                UseGrouping = useGrouping;
            }
        }

        public class GetFreshStatsCompleted : Message
        {
            public readonly bool Success;
            public readonly Dictionary<string,object> Stats;

            public GetFreshStatsCompleted(bool success, Dictionary<string,object> stats)
            {
                Success = success;
                Stats = stats;
            }
        }

        public class InternalStatsRequest : Message
        {
            public readonly IEnvelope Envelope;

            public InternalStatsRequest(IEnvelope envelope)
            {
                Envelope = envelope;
            }
        }

        public class InternalStatsRequestResponse : Message
        {
            public readonly Dictionary<string, object> Stats;

            public InternalStatsRequestResponse(Dictionary<string,object> stats)
            {
                Stats = stats;
            }
        }
    }
}

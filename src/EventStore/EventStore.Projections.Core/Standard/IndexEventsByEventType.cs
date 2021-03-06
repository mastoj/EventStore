// Copyright (c) 2012, Event Store LLP
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
using EventStore.Projections.Core.Services;
using EventStore.Projections.Core.Services.Processing;

namespace EventStore.Projections.Core.Standard
{
    public class IndexEventsByEventType : IProjectionStateHandler
    {
        private readonly string _indexStreamPrefix;

        public IndexEventsByEventType(string source, Action<string> logger)
        {
            if (!string.IsNullOrWhiteSpace(source))
                throw new InvalidOperationException("Empty source expected");
            if (logger != null)
            {
                logger("Index events by event type projection handler has been initialized");
            }
            // we will need to declare event types we are interested in
            _indexStreamPrefix = "$et-";
        }

        public void ConfigureSourceProcessingStrategy(QuerySourceProcessingStrategyBuilder builder)
        {
            builder.FromAll();
            builder.AllEvents();
        }

        public void Load(string state)
        {
        }

        public void Initialize()
        {
        }

        public bool ProcessEvent(
            EventPosition position, CheckpointTag eventPosition, string streamId, string eventType, string category1,
            Guid eventId, int sequenceNumber, string metadata, string data, out string newState,
            out EmittedEvent[] emittedEvents)
        {
            emittedEvents = null;
            newState = null;
            if (streamId.StartsWith(_indexStreamPrefix))
                return false;
            if (eventType == "$>")
                return false;

            emittedEvents = new[]
                {
                    new EmittedEvent(
                        _indexStreamPrefix + eventType, Guid.NewGuid(), "$>", sequenceNumber + "@" + streamId,
                        eventPosition, expectedTag: null)
                };

            return true;
        }

        public void Dispose()
        {
        }
    }
}

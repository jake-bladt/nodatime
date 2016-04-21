// Copyright 2011 The Noda Time Authors. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE.txt file.

using System.Globalization;
using BenchmarkDotNet.Attributes;

namespace NodaTime.Benchmarks.NodaTimeTests
{
    [Config(typeof(BenchmarkConfig))]
    public class InstantBenchmarks
    {
        private static readonly Instant Sample = Instant.FromUtc(2011, 8, 24, 12, 29, 30);
        private static readonly Offset SmallOffset = Offset.FromHours(1);
        private static readonly Offset LargePositiveOffset = Offset.FromHours(12);
        private static readonly Offset LargeNegativeOffset = Offset.FromHours(-13);
        private static readonly DateTimeZone London = DateTimeZoneProviders.Tzdb["Europe/London"];

        [Benchmark]
        public string ToStringIso() => Sample.ToString("g", CultureInfo.InvariantCulture);

        [Benchmark]
        public Instant PlusDuration() => Sample.Plus(Duration.Epsilon);

        [Benchmark]
        public ZonedDateTime InUtc() => Sample.InUtc();

        [Benchmark]
        public ZonedDateTime InZoneLondon() => Sample.InZone(London);

#if !V1_0 && !V1_1
        [Benchmark]
        public OffsetDateTime WithOffset_SameUtcDay() => Sample.WithOffset(SmallOffset);

        [Benchmark]
        public OffsetDateTime WithOffset_NextUtcDay() => Sample.WithOffset(LargePositiveOffset);

        [Benchmark]
        public OffsetDateTime WithOffset_PreviousUtcDay() => Sample.WithOffset(LargeNegativeOffset);
#endif
    }
}

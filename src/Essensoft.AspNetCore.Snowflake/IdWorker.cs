using System;
using Microsoft.Extensions.Options;

namespace Essensoft.AspNetCore.Snowflake
{
    /// <summary>
    /// Id Worker
    /// </summary>
    public class IdWorker : IIdWorker
    {
        private readonly SnowflakeOptions _options;
        private readonly object _lock = new object();
        private long _lastTimestamp = -1L;
        private long _sequence = 0L;

        public IdWorker(IOptions<SnowflakeOptions> optionsAccessor)
        {
            _options = optionsAccessor?.Value;
            _sequence = _options.Sequence;

            if (_options.WorkerId > _options.MaxWorkerId || _options.WorkerId < 0)
            {
                throw new SnowflakeException($"worker Id can't be greater than { _options.MaxWorkerId} or less than 0");
            }

            if (_options.DatacenterId > _options.MaxDatacenterId || _options.DatacenterId < 0)
            {
                throw new SnowflakeException($"datacenter Id can't be greater than { _options.MaxDatacenterId} or less than 0");
            }
        }

        public long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();

                if (timestamp < _lastTimestamp)
                {
                    throw new SnowflakeException($"Clock moved backwards or wrapped around. Refusing to generate id for {_lastTimestamp - timestamp} ticks");
                }

                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & _options.SequenceMask;
                    if (_sequence == 0)
                    {
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }

                _lastTimestamp = timestamp;
                var id = ((timestamp - _options.Epoch) << _options.TimestampLeftShift) |
                         (_options.DatacenterId << _options.DatacenterIdShift) |
                         (_options.WorkerId << _options.WorkerIdShift) | _sequence;

                return id;
            }
        }

        private long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }

            return timestamp;
        }

        private long TimeGen()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}

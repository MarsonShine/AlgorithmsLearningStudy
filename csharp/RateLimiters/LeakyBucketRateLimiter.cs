using Microsoft.Extensions.Logging;

namespace RateLimiters
{
	public class LeakyBucketRateLimiter : IRateLimiter
	{
		private readonly long _capacity;
		private readonly double _leakRate;
		private long _water;
		private long _lastLeakTicks;
		private readonly object _lock = new(); // 也可以使用无锁编程 Interlocked，详见 TokenBucketRateLimiter.cs
		private readonly ILogger<LeakyBucketRateLimiter> _logger;

		public LeakyBucketRateLimiter(int capacity, double leakRate, ILogger<LeakyBucketRateLimiter> logger)
		{
			if (capacity <= 0)
				throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive.");
			if (leakRate <= 0)
				throw new ArgumentOutOfRangeException(nameof(leakRate), "Leak rate must be positive.");

			_capacity = capacity;
			_leakRate = leakRate;
			_water = 0;
			_lastLeakTicks = DateTime.UtcNow.Ticks;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public bool AllowRequest()
		{
			try
			{
				lock (_lock)
				{
					LeakWater();

					if (_water < _capacity)
					{
						_water++;
						_logger.LogDebug("Request allowed. Current water level: {WaterLevel}", _water);
						return true;
					}

					_logger.LogDebug("Request denied. Bucket full. Water level: {WaterLevel}", _water);
					return false;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while processing the request.");
				return false;
			}
		}

		private void LeakWater()
		{
			long now = DateTime.UtcNow.Ticks;
			long elapsedTicks = now - _lastLeakTicks;
			double seconds = elapsedTicks / (double)TimeSpan.TicksPerSecond;
			long waterToLeak = (long)(seconds * _leakRate);

			if (waterToLeak > 0)
			{
				_water = Math.Max(0, _water - waterToLeak);
				_lastLeakTicks = now;
			}
		}

	}
}

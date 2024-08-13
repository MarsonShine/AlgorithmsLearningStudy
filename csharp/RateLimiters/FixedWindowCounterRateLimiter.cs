using System.Collections.Concurrent;

namespace RateLimiters
{
	public class FixedWindowCounterRateLimiter(int limit, TimeSpan windowDuration) : IRateLimiter
	{
		private readonly int _limit = limit;
		private readonly TimeSpan _windowDuration = windowDuration;
		private readonly ConcurrentDictionary<long, int> _counters = new ConcurrentDictionary<long, int>();

		public bool AllowRequest()
		{
			long windowKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / (long)_windowDuration.TotalSeconds;

			int count = _counters.AddOrUpdate(
				windowKey,
				1,
				(_, currentCount) => currentCount + 1);

			if (count > _limit)
			{
				return false;
			}

			// 清理旧的计数器
			if (windowKey % 10 == 0) // 每10个窗口清理一次，避免频繁清理
			{
				CleanupOldCounters(windowKey);
			}

			return true;
		}

		private void CleanupOldCounters(long currentWindowKey)
		{
			foreach (var key in _counters.Keys)
			{
				if (key < currentWindowKey - 1)
				{
					_counters.TryRemove(key, out _);
				}
			}
		}
	}
}

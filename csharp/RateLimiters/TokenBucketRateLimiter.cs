using Microsoft.Extensions.Logging;

namespace RateLimiters
{
	public class TokenBucketRateLimiter : IRateLimiter
	{
		private readonly long _capacity;
		private readonly double _refillRate;
		private long _tokens; // 精度要求更高的话，可以设置为double，下面的 Interlocked.Increment 可以用 Interlocked.Exchange/CompareExchange 替代
		private long _lastRefillTicks;
		private readonly ILogger<TokenBucketRateLimiter> _logger;
		/// <summary>
		/// Initializes a new instance of the <see cref="TokenBucketRateLimiter"/> class.
		/// </summary>
		/// <param name="capacity">容量，如总共100个令牌</param>
		/// <param name="refillRate">访问速率，如每秒10个令牌</param>
		/// <param name="logger"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public TokenBucketRateLimiter(long capacity, double refillRate, ILogger<TokenBucketRateLimiter> logger)
		{
			_capacity = capacity;
			_refillRate = refillRate;
			_tokens = capacity;
			_lastRefillTicks = DateTime.UtcNow.Ticks;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
		}
		public bool AllowRequest()
		{
			try
			{
				RefillTokens();
				if (Interlocked.Decrement(ref _tokens) >= 0)
				{
					_logger.LogDebug("Request allowed. Remaining tokens: {RemainingTokens}", _tokens);
					return true;
				}

				Interlocked.Increment(ref _tokens); // 恢复令牌
				_logger.LogDebug("Request denied. No tokens available.");
				return false;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while processing the request.");
				return false;
			}
		}

		private void RefillTokens()
		{
			long now = DateTime.UtcNow.Ticks;
			long elapsedTicks = now - Interlocked.Read(ref _lastRefillTicks);
			double seconds = elapsedTicks / (double)TimeSpan.TicksPerSecond;
			int tokensToAdd = (int)(seconds * _refillRate);

			if (tokensToAdd > 0)
			{
				long newTokens = Math.Min(_capacity, Interlocked.Add(ref _tokens, tokensToAdd));
				Interlocked.Exchange(ref _tokens, newTokens);
				Interlocked.Exchange(ref _lastRefillTicks, now);
			}
		}
	}
}

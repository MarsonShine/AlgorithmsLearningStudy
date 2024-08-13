namespace RateLimiters
{
	public interface IRateLimiter
	{
		bool AllowRequest();
	}
}

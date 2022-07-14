namespace RPGWebAPI.Helpers
{
	public struct TokenResult<Result>
	{
		public bool Success { get; set; }

		public Result? Value { get; set; }
	}
}

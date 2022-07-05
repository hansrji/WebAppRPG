namespace WebApp.DataTransferObjects
{
	public record ItemDto
	{
		public Guid Id { get; init; }
		public string? Name { get; init; }
		public decimal Price { get; init; }
	}
}

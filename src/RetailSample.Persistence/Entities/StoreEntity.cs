namespace RetailSample.Persistence.Entities;

public sealed class StoreEntity : EntityBase<Guid>
{
	public string Name { get; set; } = string.Empty;

	public string Address { get; set; } = string.Empty;

	public string City { get; set; } = string.Empty;

	public string Region { get; set; } = string.Empty;

	public string Country { get; set; } = string.Empty;
}

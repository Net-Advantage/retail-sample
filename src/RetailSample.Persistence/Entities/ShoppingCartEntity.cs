namespace RetailSample.Persistence.Entities;

public sealed class ShoppingCartEntity : EntityBase<Guid>
{
	public DateTime CreatedOn { get; set; }

	public decimal TotalPrice { get; set; }
}

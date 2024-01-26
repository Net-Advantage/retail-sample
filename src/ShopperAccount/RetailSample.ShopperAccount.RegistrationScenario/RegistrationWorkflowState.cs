﻿namespace RetailSample.ShopperAccount.RegistrationScenario;

public sealed class RegistrationWorkflowState : WorkflowState
{
	public string Username { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public ShopperType ShopperType { get; set; }
	public string? Name { get; set; } = string.Empty;
	public string? FirstName { get; set; } = string.Empty;
	public string? LastName { get; set; } = string.Empty;
	public string Telephone { get; set; } = string.Empty;
	public DateTime VerifiedOn { get; set; }

}

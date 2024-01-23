namespace RetailSample.Workflows.UserManagementScenarios;

public sealed class NewUserWorkflowParameters : IWorkflowParameters
{
	public string Username { get; init; } = default!;
	public string FirstName { get; init; } = default!;
	public string LastName { get; init; } = default!;

}

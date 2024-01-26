namespace RetailSample.Test.ShopperAccount.RegistrationUnitTests;

public sealed class RegistrationWorkflowUnitTests
{
	[Fact]
	public async Task RunWorkflow_ShouldCreateState()
	{
		// Arrange
		var workflowParameters = new RegistrationWorkflowParameters();
		var workflow = new RegistrationWorkflow(workflowParameters);

		// Act
		await workflow.RunAsync();

		// Assert
		workflow.Should().NotBeNull();
		workflow.WorkflowState.Should().NotBeNull();
	}
}
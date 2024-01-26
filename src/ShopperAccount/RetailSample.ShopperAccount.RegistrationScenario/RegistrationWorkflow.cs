namespace RetailSample.ShopperAccount.RegistrationScenario;

public sealed class RegistrationWorkflow : Workflow<RegistrationWorkflowState>
{
	private readonly RegistrationWorkflowParameters _parameters;

	public RegistrationWorkflow(
		RegistrationWorkflowParameters parameters)
	{
		AddActivity<InitiateAccountActivity>();
		_parameters = parameters;
	}

	protected override Task OnDataLoadAsync()
	{
		WorkflowState = new RegistrationWorkflowState();
		return Task.CompletedTask;
	}
}

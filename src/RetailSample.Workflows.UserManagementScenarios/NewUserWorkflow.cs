namespace RetailSample.Workflows.UserManagementScenarios;

public sealed class NewUserWorkflow : Workflow<NewUserWorkflowState>
{
	private readonly NewUserWorkflowParameters _workflowParameters;
	private readonly NewUserWorkflowRepository _workflowRepository;

	public NewUserWorkflow(
		NewUserWorkflowParameters workflowParameters,
		NewUserWorkflowRepository workflowRepository)
	{
		_workflowParameters = workflowParameters;
		_workflowRepository = workflowRepository;

		AddActivity<RegistrationActivity>(RegistrationActivityPostProcessor);
	}

	protected override async Task OnDataLoadAsync()
	{
		var result = await _workflowRepository.LoadAsync(_workflowParameters);

		result.IfSucc(workflowState => WorkflowState = workflowState);
		result.IfFail(exception =>
		{
			var validationFailure = new ValidationFailure("", exception.Message);
			ValidationResult.Errors.Add(new ValidationFailure("", exception.Message));
		});
	}

	protected override async Task OnDataPersistAsync()
	{
		if (WorkflowState == null)
		{
			return;
		}

		await _workflowRepository.PersistAsync(WorkflowState);
	}

	private void RegistrationActivityPostProcessor(RegistrationActivity activity)
	{
		
	}
}



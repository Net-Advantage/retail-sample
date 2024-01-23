namespace RetailSample.Workflows.UserManagementScenarios;

public sealed class NewUserWorkflowRepository(
	IApplicationContext applicationContext,
	ITenantableDbContextFactory<RetailSampleDbContext> dbContextFactory)
	: IWorkflowRepository<NewUserWorkflowParameters, NewUserWorkflowState>
{
	private readonly IApplicationContext _applicationContext = applicationContext;
	private readonly ITenantableDbContextFactory<RetailSampleDbContext> _dbContextFactory = dbContextFactory;

	public async Task<Result<NewUserWorkflowState>> LoadAsync(NewUserWorkflowParameters parameters)
	{
		var dbContext = _dbContextFactory.CreateDbContext(_applicationContext);

		var user = await dbContext.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(row => row.Username == parameters.Username);

		if(user != null)
		{
			return new Result<NewUserWorkflowState>(
				new Exception($"User with username '{parameters.Username}' already exists."));
		}

		user = new UserEntity()
		{
			Id = Guid.NewGuid(),
			Username = parameters.Username,
			FirstName = parameters.FirstName,
			LastName = parameters.LastName
		};

		return new NewUserWorkflowState()
		{
			User = user
		};
	}

	public async Task<Result<bool>> PersistAsync(NewUserWorkflowState workflowState)
	{
		var dbContext = _dbContextFactory.CreateDbContext(_applicationContext);
		dbContext.Users.Add(workflowState.User!);
		await dbContext.SaveChangesAsync();
		return true;
	}
}
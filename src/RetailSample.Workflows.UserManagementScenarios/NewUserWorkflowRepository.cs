using RetailSample.Persistence.Entities;

namespace RetailSample.Workflows.UserManagementScenarios;

public sealed class NewUserWorkflowRepository(
	IApplicationContext applicationContext,
	ITenantableDbContextFactory<RetailSampleDbContext> dbContextFactory) 
	: RepositoryBase<UserEntity>(dbContextFactory.CreateDbContext(applicationContext)), 
		IWorkflowRepository<NewUserWorkflowParameters, NewUserWorkflowState>
{
	public async Task<Result<NewUserWorkflowState>> LoadAsync(NewUserWorkflowParameters parameters)
	{
		var specification = new GetUserEntitySpecification(parameters);
		var userEntity = await FirstOrDefaultAsync(specification);

		if (userEntity != null)
		{
			return new Result<NewUserWorkflowState>(
				new Exception($"User with username '{parameters.Username}' already exists."));
		}

		userEntity = new UserEntity()
		{
			Id = Guid.NewGuid(),
			Username = parameters.Username,
			FirstName = parameters.FirstName,
			LastName = parameters.LastName
		};

		return new NewUserWorkflowState()
		{
			User = userEntity
		};
	}

	public async Task<Result<bool>> PersistAsync(NewUserWorkflowState workflowState)
	{
		var userEntity = await AddAsync(workflowState.User!);
		return userEntity != null;
	}
}


//public sealed class NewUserWorkflowRepository(
//	IApplicationContext applicationContext,
//	ITenantableDbContextFactory<RetailSampleDbContext> dbContextFactory)
//	: IWorkflowRepository<NewUserWorkflowParameters, NewUserWorkflowState>
//{
//	private readonly IApplicationContext _applicationContext = applicationContext;
//	private readonly ITenantableDbContextFactory<RetailSampleDbContext> _dbContextFactory = dbContextFactory;

//	public async Task<Result<NewUserWorkflowState>> LoadAsync(NewUserWorkflowParameters parameters)
//	{
//		var dbContext = _dbContextFactory.CreateDbContext(_applicationContext);
//		var repository = new EntityRepository<UserEntity>(dbContext);
//		var specification = new GetUserEntitySpecification(parameters);

//		var userEntity = await repository.FirstOrDefaultAsync(specification);
//		var userEntities = await repository.ListAsync(specification);

//		if(userEntity != null)
//		{
//			return new Result<NewUserWorkflowState>(
//				new Exception($"User with username '{parameters.Username}' already exists."));
//		}

//		userEntity = new UserEntity()
//		{
//			Id = Guid.NewGuid(),
//			Username = parameters.Username,
//			FirstName = parameters.FirstName,
//			LastName = parameters.LastName
//		};

//		return new NewUserWorkflowState()
//		{
//			User = userEntity
//		};
//	}

//	public async Task<Result<bool>> PersistAsync(NewUserWorkflowState workflowState)
//	{
//		var dbContext = _dbContextFactory.CreateDbContext(_applicationContext);
//		dbContext.Users.Add(workflowState.User!);
//		await dbContext.SaveChangesAsync();
//		return true;
//	}
//}
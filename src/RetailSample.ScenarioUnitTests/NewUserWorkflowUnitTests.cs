namespace RetailSample.ScenarioUnitTests;

public sealed class NewUserWorkflowUnitTests(
	ITestOutputHelper testOutputHelper,
	NewUserWorkflowTestFixture testFixture)
	: DatabaseTestBase<NewUserWorkflowTestFixture>(testOutputHelper, testFixture)
{
	[Theory]
	[InlineData("731724a1-9b57-46ce-baaf-7325bc8711c0", TenantIsolationStrategy.DedicatedDedicated)]
	public async Task CreateNew_ReturnsIsValidTrue(Guid tenantId, TenantIsolationStrategy tenantIsolationStrategy)
	{
		// Arrange
		TestFixture.ApplicationContextFactory = () => new ApplicationContext()
		{
			TenantIsolationStrategy = tenantIsolationStrategy,
			TenantContext = new TenantContext()
			{
				TenantId = tenantId
			}
		};

		var dbContextFactory = TestFixture.ServiceScope.ServiceProvider.GetRequiredService<ITenantableDbContextFactory<RetailSampleDbContext>>();
		var applicationContext = TestFixture.ServiceScope.ServiceProvider.GetRequiredService<IApplicationContext>();
		var dbContext = dbContextFactory.CreateDbContext(applicationContext);
		await dbContext.Database.EnsureCreatedAsync();

		var workflowParameters = new NewUserWorkflowParameters
		{
			Username = "jsoap",
			FirstName = "Joe",
			LastName = "Soap"
		};

		var workflowRepository = TestFixture.ServiceProvider.GetRequiredService<NewUserWorkflowRepository>();

		var workflow = new NewUserWorkflow(workflowParameters, workflowRepository);

		var activity = workflow.Activities.Keys.OfType<RegistrationActivity>()!.Single();
		
		// Assert Arranged
		var changedActivityStates = workflow.Activities.Where(a => a.Key.HasStateChanged);
		changedActivityStates.Should().BeEmpty();

		workflow.Activities.Should().HaveCount(1);
		workflow.WorkflowState.Should().BeNull();
		workflow.ValidationResult.Should().BeNull();

		activity.ActivityState.Should().BeNull();
		activity.ValidationResult.Should().BeNull();
		activity.HasStateChanged.Should().BeFalse();
		activity.InitialActivityState.Should().BeNull();

		// Act
		await workflow.RunAsync();

		// Assert
		changedActivityStates = workflow.Activities.Where(a => a.Key.HasStateChanged);
		changedActivityStates.Should().HaveCount(1);

		workflow.Activities.Should().HaveCount(1);
		workflow.WorkflowState.Should().NotBeNull();
		workflow.ValidationResult.IsValid.Should().BeFalse();

		activity.ActivityState.Should().NotBeNull();
		activity.HasStateChanged.Should().BeTrue();
		activity.ValidationResult.IsValid.Should().BeFalse();
	}
}

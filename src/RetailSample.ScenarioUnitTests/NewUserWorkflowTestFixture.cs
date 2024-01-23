namespace RetailSample.ScenarioUnitTests;

public sealed class NewUserWorkflowTestFixture(
	IMessageSink diagnosticMessageSink)
		: DatabaseFixtureBase(diagnosticMessageSink)
{
	protected override void ConfigureServices(IServiceCollection services)
	{
		services.TryAddTransient<IApplicationContext>((sp) =>
			ApplicationContextFactory?.Invoke() ?? new ApplicationContext()
			{
				TenantContext = new TenantContext()
				{
					TenantId = Guid.Empty
				}
			});

		services.AddTenantablePersistence<RetailSampleDbContext>("RetailSampleDb_", ConfigurationRoot);

		services.AddUserUserManagementWorkflows();
	}
}


[assembly: TestFramework("RetailSample.ScenarioUnitTests.RunOnce", "RetailSample.ScenarioUnitTests")]

namespace RetailSample.ScenarioUnitTests;

public class RunOnce : XunitTestFramework, IDisposable
{
	private readonly MsSqlContainer _container;
	public RunOnce(IMessageSink messageSink)
		: base(messageSink)
	{
		_container = new MsSqlBuilder()
			.WithImage("mcr.microsoft.com/mssql/server:2019-latest")
			.WithName("retail-sample-test-db")
			.WithPortBinding(14331, 1433)
			.WithPassword("Password123")
			.Build();

		_container.StartAsync().GetAwaiter().GetResult();
	}

	public new void Dispose()
	{
		_container.StopAsync().GetAwaiter().GetResult();
		GC.SuppressFinalize(this);
		base.Dispose();
	}
}
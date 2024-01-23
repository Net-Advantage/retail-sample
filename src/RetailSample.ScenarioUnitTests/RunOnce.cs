[assembly: Xunit.TestFramework(
	"RetailSample.ScenarioUnitTests.RunOnce",
	"RetailSample.ScenarioUnitTests")]

namespace RetailSample.ScenarioUnitTests;

public class RunOnce : XunitTestFramework, IDisposable
{
	private readonly MsSqlContainer _container;

	public RunOnce(IMessageSink messageSink)
		: base(messageSink)
	{
		DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Container starting ..."));

		_container = new MsSqlBuilder()
			.WithImage("mcr.microsoft.com/mssql/server:2019-latest")
			.WithName("retail-sample-test-db")
			.WithPortBinding(14331, 1433)
			.WithPassword("Password123")
			.WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
			.Build();

		StartContainerAndEnsureReady().GetAwaiter().GetResult();
		
	}

	public new void Dispose()
	{
		DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Container stopping ..."));
		_container.StopAsync().GetAwaiter().GetResult();
		GC.SuppressFinalize(this);
		base.Dispose();
		DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Container stopped!"));
	}

	private async Task StartContainerAndEnsureReady()
	{
		await _container.StartAsync();

		var retryCount = 1;
		var maxRetries = 5;
		var delay = TimeSpan.FromSeconds(10);
		var isReady = false;

		while (retryCount <= maxRetries && !isReady)
		{
			try
			{
				DiagnosticMessageSink.OnMessage(new DiagnosticMessage($"Attempt ({retryCount}) to open a connection to the container."));
				using var connection = new SqlConnection(_container.GetConnectionString());
				await connection.OpenAsync();
				isReady = true;
			}
			catch
			{
				// If connection fails, wait for a while and then retry
				await Task.Delay(delay);
				retryCount++;
			}
		}

		if (!isReady)
		{
			var failureMessage = $"Failed to establish a connection to the SQL Server container after {maxRetries} retries.";
			DiagnosticMessageSink.OnMessage(new DiagnosticMessage(failureMessage));
			throw new InvalidOperationException(failureMessage);
		}

		DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Container is ready!"));
	}
}

namespace RetailSample.ShopperAccount.RegistrationScenario.InitiateAccount;

public sealed class InitiateAccountActivity : Activity<InitiateAccountState>
{
	public InitiateAccountActivity()
	{
		InitialActivityState = new InitiateAccountState();
	}
}

using Innkeep.Core.Env;

namespace Innkeep.Api.Fiskaly.Tests.Repositories;

public abstract class AbstractFiskalyRepositoryTest
{
	protected string FiskalyTestClientId = string.Empty;
	protected string FiskalyTestTssId = string.Empty;

	public virtual void Initialize()
	{
		var result = Env.Load("../../../../Env/tests.env");

		FiskalyTestTssId = Environment.GetEnvironmentVariable("FISKALY_TEST_TSS_ID") ?? string.Empty;
		FiskalyTestClientId = Environment.GetEnvironmentVariable("FISKALY_TEST_CLIENT_ID") ?? string.Empty;
	}
}
using RetailSample.Persistence.Entities;

namespace RetailSample.Workflows.UserManagementScenarios.Specifications;

public sealed class GetUserEntitySpecification : Specification<UserEntity>, ISingleResultSpecification<UserEntity>
{
	public GetUserEntitySpecification(NewUserWorkflowParameters parameters)
	{
		Query
			.AsNoTracking()
			.Where(row => row.Username == parameters.Username);
	}
}

using RetailSample.Persistence.Entities;

namespace RetailSample.Workflows.UserManagementScenarios;

public sealed class NewUserWorkflowState : WorkflowState
{
    public UserEntity? User { get; set; }
    public StoreEntity? Store { get; set; }
}
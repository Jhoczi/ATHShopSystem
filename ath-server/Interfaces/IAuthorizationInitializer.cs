namespace ath_server.Interfaces;

public interface IAuthorizationInitializer
{
    Task GenerateAdminAndRoles();
}
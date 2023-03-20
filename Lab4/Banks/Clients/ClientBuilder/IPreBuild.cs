namespace Banks.Clients.ClientBuilder;

public interface IPreBuild
{
    IClient Build(int personId, int idFromBank);
}
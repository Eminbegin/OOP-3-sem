namespace Banks.Clients.ClientBuilder;

public interface ISubjectBuilder
{
    ISubjectBuilder WithAddress(string address);
    ISubjectBuilder WithRussianPassport(string passport);
    ISubjectBuilder WithEmail(string email);
    IPreBuild PreBuild();
}
namespace DataAccess.Models;

public class Account
{
    public Account(Guid login, Guid password, Guid workerId)
    {
        Login = login;
        Password = password;
        WorkerId = workerId;
    }

    protected Account() { }

    public Guid Login { get; set; }
    public Guid Password { get; set; }
    public Guid WorkerId { get; set; }
}
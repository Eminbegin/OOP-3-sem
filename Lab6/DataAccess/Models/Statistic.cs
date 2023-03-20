namespace DataAccess.Models;

public class Statistic
{
    public Statistic(Guid id, int messagesCount, int handledMessagesCount, DateTime date, ICollection<SendMethodMessages> methodMessages = null)
    {
        MessagesCount = messagesCount;
        HandledMessagesCount = handledMessagesCount;
        MethodMessages = methodMessages ?? new List<SendMethodMessages>();
        Id = id;
        Date = date;
    }

    protected Statistic() { }

    public int MessagesCount { get; set; }
    public Guid Id { get; set; }
    public int HandledMessagesCount { get; set; }
    public virtual ICollection<SendMethodMessages> MethodMessages { get; set; }
    public DateTime Date { get; set; }
}
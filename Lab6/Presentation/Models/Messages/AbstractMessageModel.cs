namespace Presentation.Models.Messages;

public record AbstractMessageModel(Guid Recipient, string Text);
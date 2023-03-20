namespace Presentation.Models.Messages;

public record MessengerMessageModel(Guid Recipient, string Text, string UserTag)
    : AbstractMessageModel(Recipient, Text);

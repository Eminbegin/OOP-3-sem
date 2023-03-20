namespace Presentation.Models.Messages;

public record EmailMessageModel(Guid Recipient, string Text, string Theme)
        : AbstractMessageModel(Recipient, Text);
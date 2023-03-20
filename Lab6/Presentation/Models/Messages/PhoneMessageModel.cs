namespace Presentation.Models.Messages;

public record PhoneMessageModel (Guid Recipient, string Text, string PhoneNumber)
        :AbstractMessageModel( Recipient, Text);
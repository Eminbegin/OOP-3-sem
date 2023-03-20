namespace Business.Dto.SendingMethods;

public record MessengerSenderDto(Guid Id, string Name)
    :SendingMethodDto(Id, Name);
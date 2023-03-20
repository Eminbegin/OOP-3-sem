namespace Business.Dto.SendingMethods;

public record PhoneSenderDto(Guid Id, string Name)
    :SendingMethodDto(Id, Name);
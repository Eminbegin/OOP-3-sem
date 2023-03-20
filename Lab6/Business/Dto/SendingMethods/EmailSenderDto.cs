namespace Business.Dto.SendingMethods;

public record EmailSenderDto(Guid Id, string Name)
    :SendingMethodDto(Id, Name);
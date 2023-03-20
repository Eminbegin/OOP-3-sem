namespace Business.Dto.SendingMethods;

public record GroupMethodsDto(Guid GroupId, string Name, List<SendingMethodDto> SendingMethods);
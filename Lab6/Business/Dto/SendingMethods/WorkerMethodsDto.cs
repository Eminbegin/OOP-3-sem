namespace Business.Dto.SendingMethods;

public record WorkerMethodsDto(Guid WorkerId, string Name, List<SendingMethodDto> SendingMethods);

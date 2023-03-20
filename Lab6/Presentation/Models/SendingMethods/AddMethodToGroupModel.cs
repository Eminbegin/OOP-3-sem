namespace Presentation.Models.SendingMethods;

public record AddMethodToGroupModel(Guid SessionId, Guid GroupId, Guid MethodId);
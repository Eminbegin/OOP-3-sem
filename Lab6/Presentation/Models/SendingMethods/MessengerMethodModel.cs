namespace Presentation.Models.SendingMethods;

public record MessengerMethodModel(string Name)
    : SendingMethodModel(Name);
    
    
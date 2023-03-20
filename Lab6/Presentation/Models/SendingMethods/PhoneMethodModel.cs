namespace Presentation.Models.SendingMethods;

public record PhoneMethodModel(string Name)
    : SendingMethodModel(Name);
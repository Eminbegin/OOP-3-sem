namespace Presentation.Models.SendingMethods;

public record EmailMethodModel(string Name)
    : SendingMethodModel(Name);
using Business.Dto.SendingMethods;
using DataAccess.Models.SendingMethods;

namespace Business.AbstractFactories;

public interface ISenderFactory
{
    SendingMethod CreateSender(SendingMethodDto sendingMethodDto);
    ISenderFactory AddSpareFactory(ISenderFactory senderFactory);
}
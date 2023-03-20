using Business.Dto.SendingMethods;
using DataAccess.Models.SendingMethods;

namespace Business.AbstractFactories.ImplementationSenderFactory;

public class EmailSenderFactory : ISenderFactory
{
    private ISenderFactory? _spareFactory;
    
    public SendingMethod CreateSender(SendingMethodDto sendingMethodDto)
    {
        if (sendingMethodDto is EmailSenderDto emailSenderDto)
        {
            return new EmailSender(Guid.NewGuid(), emailSenderDto.Name);
        }
        
        if (_spareFactory is not null)
        {
            return _spareFactory.CreateSender(sendingMethodDto);
        }

        throw new Exception();
    }

    public ISenderFactory AddSpareFactory(ISenderFactory senderFactory)
    {
        ArgumentNullException.ThrowIfNull(senderFactory);
        _spareFactory = senderFactory;
        return _spareFactory;
    }
}
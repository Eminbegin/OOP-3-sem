using Business.Dto.SendingMethods;
using DataAccess.Models.Workers;

namespace Business.Mapping;

public static class WorkerMethodMapper
{
    public static WorkerMethodsDto AsDtoWithMethods(this Worker worker)
    {
        return new WorkerMethodsDto(worker.Id, worker.Name, worker.SendingMethods.Select(s => s.AsDto()).ToList());
    }
}
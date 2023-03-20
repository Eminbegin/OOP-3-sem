using Business.Dto;
using DataAccess.Models;

namespace Business.Mapping;

public static class SessionMapper
{
    public static SessionDto AsDto(this Session session)
        => new SessionDto(session.Login, session.WorkerId);
}
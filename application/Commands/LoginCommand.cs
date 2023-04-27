using apibanca.application.DTOs;
using apibanca.application.Infrastructure.Data;
using AutoMapper;
using MediatR;

namespace apibanca.application.Command;

public class LoginCommandResponse : UserDto { }

public class LoginCommand : IRequest<LoginCommandResponse>
{
    public string username { get; set; }
    public string password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly ApiContext _db;
    private readonly IMapper _mapper;
    public LoginCommandHandler(ApiContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var reg = _db.Users.FirstOrDefault(p => p.UserName.Equals(request.username));
        if (reg == null) throw new KeyNotFoundException();
        return _mapper.Map<LoginCommandResponse>(reg);
    }
}
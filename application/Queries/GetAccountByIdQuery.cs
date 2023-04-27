using apibanca.application.DTOs;
using apibanca.application.Infrastructure.Data;
using AutoMapper;
using MediatR;

namespace apibanca.application.Queries;

public class GetAccountByIdQueryResponse : AccountDto { }

public class GetAccountByIdQuery : IRequest<GetAccountByIdQueryResponse>
{
    public int idAccount { get; set; }
}

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, GetAccountByIdQueryResponse>
{
    private readonly ApiContext _db;
    private readonly IMapper _mapper;
    public GetAccountByIdQueryHandler(ApiContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<GetAccountByIdQueryResponse> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var reg = await _db.Accounts.FindAsync(request.idAccount);
        if (reg == null) throw new KeyNotFoundException();
        return _mapper.Map<GetAccountByIdQueryResponse>(reg);
    }
}
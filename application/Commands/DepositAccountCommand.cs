using apibanca.application.DTOs;
using apibanca.application.Infrastructure.Data;
using apibanca.application.Exceptions;
using AutoMapper;
using MediatR;

namespace apibanca.application.Commands;

public class DepositAccountCommandResponse : AccountDto { }

public class DepositAccountCommand : IRequest<DepositAccountCommandResponse>
{
    
    public int idAccount { get; set; }
    public decimal amount { get; set; }   
}

public class DepositAccountCommandHandler : IRequestHandler<DepositAccountCommand, DepositAccountCommandResponse>
{
    private readonly ApiContext _db;
    private readonly IMapper _mapper;
    public DepositAccountCommandHandler(ApiContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<DepositAccountCommandResponse> Handle(DepositAccountCommand request, CancellationToken cancellationToken)
    {
        var account = _db.Accounts.Find(request.idAccount);
        //using var transaction = _db.Database.BeginTransaction();
        try
        {
            account.Deposit(request.amount);
            await _db.SaveChangesAsync();
            //await transaction.CommitAsync();
        }
        catch (DatabaseException)
        {
            //await transaction.RollbackAsync();
            throw;
        }
        return _mapper.Map<DepositAccountCommandResponse>(account);
    }
}

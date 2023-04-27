using apibanca.application.DTOs;
using apibanca.application.Infrastructure.Data;
using apibanca.application.Exceptions;
using AutoMapper;
using MediatR;

namespace apibanca.application.Commands;

public class WithdrawAccountCommandResponse : AccountDto { }

public class WithdrawAccountCommand : IRequest<WithdrawAccountCommandResponse>
{
    public int idAccount { get; set; }
    public decimal amount { get; set; }   
}

public class WithdrawAccountCommandHandler : IRequestHandler<WithdrawAccountCommand, WithdrawAccountCommandResponse>
{
    private readonly ApiContext _db;
    private readonly IMapper _mapper;
    public WithdrawAccountCommandHandler(ApiContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<WithdrawAccountCommandResponse> Handle(WithdrawAccountCommand request, CancellationToken cancellationToken)
    {
        var account = _db.Accounts.Find(request.idAccount);
        //using var transaction = _db.Database.BeginTransaction();
        try
        {
            account.Withdraw(request.amount);
            await _db.SaveChangesAsync();
            //await transaction.CommitAsync();
        }
        catch (DatabaseException)
        {
            //await transaction.RollbackAsync();
            throw;
        }
        return _mapper.Map<WithdrawAccountCommandResponse>(account);
    }
}

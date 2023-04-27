using apibanca.application.DTOs;
using apibanca.application.Infrastructure.Data;
using apibanca.application.Entities;
using apibanca.application.Exceptions;
using AutoMapper;
using MediatR;

namespace apibanca.application.Commands;

public class CreateAccountCommandResponse : AccountDto { }

public class CreateAccountCommand : IRequest<CreateAccountCommandResponse>
{
    public int idUser { get; set; }
    public decimal initialAmount { get; set; }   
}

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountCommandResponse>
{
    private readonly ApiContext _db;
    private readonly IMapper _mapper;
    public CreateAccountCommandHandler(ApiContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<CreateAccountCommandResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = Account.Create(request.idUser);
        account.Deposit(request.initialAmount);
        //using var transaction = _db.Database.BeginTransaction();
        try
        {
            _db.Accounts.Add(account);
            await _db.SaveChangesAsync();
            //await transaction.CommitAsync();
        }
        catch (DatabaseException)
        {
            //await transaction.RollbackAsync();
            throw;
        }
        return _mapper.Map<CreateAccountCommandResponse>(account);
    }
}

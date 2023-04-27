using apibanca.application.Infrastructure.Data;
using apibanca.application.Exceptions;
using AutoMapper;
using MediatR;

namespace apibanca.application.Commands;

public class DeleteAccountCommandResponse { }

public class DeleteAccountCommand : IRequest<DeleteAccountCommandResponse>
{
    public int idAccount { get; set; }
}

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, DeleteAccountCommandResponse>
{
    private readonly ApiContext _db;
    private readonly IMapper _mapper;
    public DeleteAccountCommandHandler(ApiContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<DeleteAccountCommandResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _db.Accounts.FindAsync(request.idAccount);
        if (account == null) throw new KeyNotFoundException();
        try
        {
            account.IsActive = false;
            await _db.SaveChangesAsync();
        }
        catch (DatabaseException)
        {
            throw;
        }
        return new DeleteAccountCommandResponse();
    }
}

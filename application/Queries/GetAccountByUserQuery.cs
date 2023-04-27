using apibanca.application.DTOs;
using apibanca.application.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace apibanca.application.Queries;

    public class GetAccountsByUserQueryResponse : List<AccountDto>
    {
        public GetAccountsByUserQueryResponse(IEnumerable<AccountDto> collection) 
            : base(collection)  {  }
    }
    public class GetAccountsByUserQuery: IRequest<GetAccountsByUserQueryResponse>
    {
        public int idUser { get; set; }
    }

    public class GetAccountsByUserQueryHandler : IRequestHandler<GetAccountsByUserQuery, GetAccountsByUserQueryResponse>
    {
        private readonly ApiContext _db;
        private readonly IMapper _mapper;

        public GetAccountsByUserQueryHandler(ApiContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GetAccountsByUserQueryResponse> Handle(GetAccountsByUserQuery request, CancellationToken cancellationToken)
        {
            var curr = await _db.Accounts.Where(w => w.IDUser.Equals(request.idUser) && w.IsActive).ToListAsync();
            var list = _mapper.Map<IEnumerable<AccountDto>>(curr);
            return new GetAccountsByUserQueryResponse(list);
        }
    }

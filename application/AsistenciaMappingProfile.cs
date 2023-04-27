using apibanca.application.Commands;
using apibanca.application.DTOs;
using apibanca.application.Entities;
using AutoMapper;

namespace apibanca.application;

public class BankMappingProfile : Profile
{
    public BankMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Account, AccountDto>();

        CreateMap<CreateAccountCommand, Account>();
        CreateMap<DeleteAccountCommand, Account>();
        CreateMap<DepositAccountCommand, Account>();
        CreateMap<WithdrawAccountCommand, Account>();

        // CreateMap<AsistenciaDetalle, AsistenciaDetalleDto>();
        CreateMap<Account, CreateAccountCommandResponse>();
        // CreateMap<AsistenciaDetalle, AsistenciaDetalleEliminarCommandResponse>();
        // CreateMap<AsistenciaDetalle, AsistenciaDetalleActualizarCommandResponse>();
        // CreateMap<AsistenciaDetalleCrearCommand, AsistenciaDetalle>();
        // CreateMap<AsistenciaDetalleEliminarCommand, AsistenciaDetalle>();
        // CreateMap<AsistenciaDetalleActualizarCommand, AsistenciaDetalle>();

    }
}

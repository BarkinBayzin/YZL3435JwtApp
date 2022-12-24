using JWTApp.BackOffice.Core.Application.Interfaces;
using JWTApp.BackOffice.Core.Domain;
using JWTApp.BackOffice.Core.DTOs;
using JWTApp.BackOffice.Core.Features.CQRS.Queries;
using MediatR;
using System.Runtime.CompilerServices;
using System.Xml;

namespace JWTApp.BackOffice.Core.Features.CQRS.Handlers
{
    public class CheckUserQueryRequestHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDTO>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IRepository<AppRole> _appRoleRepository;

        public CheckUserQueryRequestHandler(IRepository<AppUser> appUserRepository, IRepository<AppRole> appRoleRepository)
        {
            _appUserRepository = appUserRepository;
            _appRoleRepository = appRoleRepository;
        }

        public async Task<CheckUserResponseDTO> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {
            var dto =  new CheckUserResponseDTO();

            var user = await _appUserRepository.GetByFilterAsync(x => x.Username == request.Username && x.Password == request.Password);

            if(user == null) { dto.IsExist= false; }
            else
            {
                dto.IsExist= true;
                dto.Username = request.Username;
                dto.Id = user.Id;
                var role = await _appRoleRepository.GetByFilterAsync(x => x.Id == user.AppRoleId);
                dto.Role = role.Definition;
            }

            return dto;
        }
    }
}

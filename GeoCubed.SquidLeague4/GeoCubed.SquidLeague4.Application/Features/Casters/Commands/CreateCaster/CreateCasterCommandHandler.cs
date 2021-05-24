using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Commands.CreateCaster
{
    public class CreateCasterCommandHandler : IRequestHandler<CreateCasterCommand, CreateCasterCommandResponse>
    {
        private readonly IAsyncRepository<CasterProfile> _casterRepository;
        private readonly IMapper _mapper;

        public CreateCasterCommandHandler(IMapper mapper, IAsyncRepository<CasterProfile> casterRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._casterRepository = casterRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(casterRepository.GetType(), this.GetType()));
        }

        public async Task<CreateCasterCommandResponse> Handle(CreateCasterCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateCasterCommandResponse();

            var validator = new CreateCasterCommandValidator();
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count() > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (response.Success)
            {
                var casterToAdd = this._mapper.Map<CasterProfile>(request);
                var caster = await this._casterRepository.AddAsync(casterToAdd);
                response.Caster = this._mapper.Map<CasterCommandDto>(caster);
            }

            return response;
        }
    }
}

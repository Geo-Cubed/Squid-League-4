using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Features.Casters.Commands.CreateCaster;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Commands.UpdateCaster
{
    public class UpdateCasterCommandHandler : IRequestHandler<UpdateCasterCommand, UpdateCasterCommandResponse>
    {
        private readonly ICasterRepository _casterRepository;
        private readonly IMapper _mapper;

        public UpdateCasterCommandHandler(IMapper mapper, ICasterRepository casterRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._casterRepository = casterRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(casterRepository.GetType(), this.GetType()));
        }

        public async Task<UpdateCasterCommandResponse> Handle(UpdateCasterCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateCasterCommandResponse();

            var validator = new UpdateCasterCommandValidator(this._casterRepository);
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
                var caster = this._mapper.Map<CasterProfile>(request);
                response.Success = await this._casterRepository.UpdateAsync(caster);
                if (!response.Success)
                {
                    response.Caster = this._mapper.Map<CasterCommandDto>(caster);
                    response.Message = "There was an issue while trying to update the caster";
                }
            }

            return response;
        }
    }
}

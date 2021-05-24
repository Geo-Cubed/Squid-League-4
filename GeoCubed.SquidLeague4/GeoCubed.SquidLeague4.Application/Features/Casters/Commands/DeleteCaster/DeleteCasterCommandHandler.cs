using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Commands.DeleteCaster
{
    public class DeleteCasterCommandHandler : IRequestHandler<DeleteCasterCommand, DeleteCasterCommandResponse>
    {
        private readonly ICasterRepository _casterRepository;
        private readonly IMapper _mapper;

        public DeleteCasterCommandHandler(IMapper mapper, ICasterRepository casterRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._casterRepository = casterRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(casterRepository.GetType(), this.GetType()));
        }

        public async Task<DeleteCasterCommandResponse> Handle(DeleteCasterCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteCasterCommandResponse();

            var validator = new DeleteCasterCommandValidator(this._casterRepository);
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
                var casterToDelete = await this._casterRepository.GetByIdAsync(request.Id);
                response.Success = await this._casterRepository.DeleteAsync(casterToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue while trying to delete the caster";
                    response.CasterId = request.Id;
                }
            }

            return response;
        }
    }
}

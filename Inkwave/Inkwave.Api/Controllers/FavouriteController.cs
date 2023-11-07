﻿using Inkwave.Application.Features.Favourites.Commands.AddFavourite;
using Inkwave.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inkwave.WebAPI.Controllers
{
    public class FavouriteController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public FavouriteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost()]
        public async Task<ActionResult<Result<Guid>>> AddFavourite(AddFavouriteCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}

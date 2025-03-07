using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries;

public record GetUserQuery(Guid Id) : IRequest<Result<User>>;

using Application.Abstractions.Messaging;

namespace Application.Users.Queries;

public sealed record GetUserQuery(Guid Id) : IQuery<UserResponse>;

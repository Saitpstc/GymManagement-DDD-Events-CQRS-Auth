namespace Shared.Infrastructure.Email;

using EmailConfirmation;

public delegate IEmailConfirmationService? ConfirmationServiceResolver(string identifier);
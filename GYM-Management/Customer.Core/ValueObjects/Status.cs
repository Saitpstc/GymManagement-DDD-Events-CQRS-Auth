namespace Customer.Core;

using Enums;
using Shared.Core.Domain;

public record Status(MembershipStatus CurrentStatus, string StatusReason):ValueObject;
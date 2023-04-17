namespace Customer.Core;

using Enums;
using Shared.Core.Domain;

internal record Status(MembershipStatus CurrentStatus, string StatusReason):ValueObject;
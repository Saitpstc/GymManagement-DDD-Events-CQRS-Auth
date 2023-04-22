namespace Customer.Core;

using Enums;
using Shared.Core.Domain;

record Status(MembershipStatus CurrentStatus, string StatusReason):ValueObject;
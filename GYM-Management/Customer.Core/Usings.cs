global using Shared.Core;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Customer.Application")]
[assembly: InternalsVisibleTo("Customer.Infrastructure")]
[assembly: InternalsVisibleTo("Customer.Test")]
[assembly: InternalsVisibleTo("Customer.Host")]
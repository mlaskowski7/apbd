using System.Net;

namespace Tutorial7.Utils;
public record ErrorWrapper(string Message, int ResponseStatusCode)
{
}
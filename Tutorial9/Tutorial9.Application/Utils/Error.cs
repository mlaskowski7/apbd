using System.Net;

namespace Tutorial9.Application.Utils;

public record Error(string Message, HttpStatusCode StatusCode);
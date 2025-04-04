using System;

namespace LegacyApp.DateTimeProviders;

public class StdDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}
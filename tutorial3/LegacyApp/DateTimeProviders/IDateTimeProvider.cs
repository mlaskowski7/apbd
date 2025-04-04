using System;

namespace LegacyApp.DateTimeProviders;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}
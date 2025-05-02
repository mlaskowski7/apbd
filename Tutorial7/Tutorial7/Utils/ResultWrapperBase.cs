namespace Tutorial7.Utils;

public abstract class ResultWrapperBase
{
    public bool IsOk { get; protected init; }
    public ErrorWrapper? Error { get; protected init; }
}
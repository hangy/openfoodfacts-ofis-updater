namespace OfisUpdater
{
    using System.Collections.Generic;

    public interface IMultiLineParser<TResult>
    {
        bool TryParse(IReadOnlyList<string> lines, out TResult result);
    }
}

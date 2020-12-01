using Xunit;

namespace Library.Tests.Extensions
{
    public class FactSkipWhenMockApiAttribute : FactAttribute
    {
        public FactSkipWhenMockApiAttribute()
        {
            Skip = "Skip when using mock API. Only works on proxy API.";
        }
    }
}

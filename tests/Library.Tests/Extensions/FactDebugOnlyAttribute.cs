using System.Diagnostics;
using Xunit;

namespace Library.Tests.Extensions
{
    public class FactDebugOnlyAttribute : FactAttribute
    {
        public FactDebugOnlyAttribute()
        {
            if (!Debugger.IsAttached)
            {
                Skip = "Runs in debug mode only";
            }
        }
    }
}

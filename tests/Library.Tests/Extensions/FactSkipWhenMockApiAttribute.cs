using DangEasy.Configuration;
using Xunit;

namespace Library.Tests.Extensions
{
    public class FactSkipWhenMockApiAttribute : FactAttribute
    {
        // Note: Due to seemingly a bug in xunit, we cannot use this attribute to skip tests for the mock endpoint
        // See example here: https://youtu.be/kktHEB_9b7g

        public FactSkipWhenMockApiAttribute()
        {
            var config = new ConfigurationLoader().Load("appsettings.json", Directory.Bin);

            if (config["Values:Eventbrite_ApiUrl"].Contains("mock"))
            {
                Skip = "Skip when using mock API. Only works on proxy API.";
            }
        }
    }
}

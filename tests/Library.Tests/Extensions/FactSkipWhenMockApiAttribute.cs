using DangEasy.Configuration;
using Xunit;

namespace Library.Tests.Extensions
{
    public class FactSkipWhenMockApiAttribute : FactAttribute
    {
        public FactSkipWhenMockApiAttribute()
        {
            var config = new ConfigurationLoader().Load("appsettings.json", Directory.Bin);

            if (config["Eventbrite_ApiUrl"].Contains("mock"))
            {
                Skip = "Skip when using mock API. Only works on proxy API.";
            }
        }
    }
}

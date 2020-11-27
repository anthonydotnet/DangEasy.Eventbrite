using System;
using Xunit;
using System.Threading.Tasks;

namespace Eventbrite.Test.Integration
{
    public class When_Copying_Event : BaseEventSetup, IDisposable
    {
        [Fact]
        public async Task Event_Is_Copied()
        {
            var res = await _service.DuplicateEvent(Event.Id);

            Assert.True(res.Id > 0);
        }
    }
}

using System;
using Xunit;
using System.Threading.Tasks;

namespace Library.Tests
{
    public class When_Copying : BaseEventSetup, IDisposable
    {
        [Fact]
        public async Task Event_Is_Copied()
        {
            var res = await Service.CopyEvent(Event.Id);

            Assert.True(res.Id > 0);
        }
    }
}

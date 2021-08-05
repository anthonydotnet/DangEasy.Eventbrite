using System;
using Xunit;
using Library.Tests.Extensions;

namespace Library.Tests
{
    public class When_Copying : BaseEventSetup, IDisposable
    {
        [FactSkipWhenMockApi] // organization_id is null :(
        public void Event_Is_Copied()
        {
            var res = Service.CopyEvent(Event.Id).Result;

            Assert.True(res.Id > 0);
        }
    }
}

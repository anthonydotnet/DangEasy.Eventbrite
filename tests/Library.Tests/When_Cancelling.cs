using Library.Tests.Extensions;
using Xunit;

namespace Library.Tests
{
    public class When_Cancelling : BaseEventSetup
    {
        [Fact]
        public void Event_Is_Cancelled()
        {
            Service.CancelEvent(Event.Id);
        }
    }
}

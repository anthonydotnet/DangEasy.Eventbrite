using Xunit;

namespace Library.Tests
{
    public class When_Cancelling : BaseEventSetup
    {
        [Fact]
        public void Event_Is_Cancelled()
        {
            var res = Service.CancelEvent(Event.Id).Result;

            Assert.True(res.Canceled);
        }
    }
}

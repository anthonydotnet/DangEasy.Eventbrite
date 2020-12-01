using System;
using Library.Tests.Extensions;
using Xunit;

namespace Library.Tests
{
    public class When_Deleting : BaseEventSetup, IDisposable
    {
        [Fact]
        public void Events_Is_Deleted()
        {
            Service.DeleteEvent(Event.Id);
        }


       // [FactDebugOnly]
        //public void DebugOnly_Events_Are_Deleted()
        //{
        //    var res = Service.GetEvents(Service.OrganizationId).Result;

        //    foreach (var e in res.Events)
        //    {
        //        Service.DeleteEvent(e.Id);
        //    }
        //}

        //public override void Dispose()
        //{
        //    // override deletion in base class - do nothing 
        //}
    }

}

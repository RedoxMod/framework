using NUnit.Framework;
using RedoxMod.API.Eventing;
using RedoxMod.Architecture;
using RedoxMod.Core.Eventing;
using System.Threading.Tasks;

namespace RedoxMod.Tests.Core
{
    [TestFixture]
    public class EventBusContainerTests
    {
        private IContainer _container;

        [SetUp]
        public void Setup()
        {
            _container = new Container();
            _container.Instance<IEventBus>(new EventBus()); // Manually registering EventBus
        }

        [Test]
        public void EventBus_Should_Be_Resolvable_From_Container()
        {
            var eventBus = _container.Resolve<IEventBus>();

            Assert.NotNull(eventBus, "EventBus was not resolved from container.");
            Assert.IsInstanceOf<EventBus>(eventBus, "Resolved EventBus is not of the correct type.");
        }

        [Test]
        public async Task Resolved_EventBus_Should_Work_Correctly()
        {
            var eventBus = _container.Resolve<IEventBus>();
            bool wasCalled = false;

            await eventBus.SubscribeAsync("testEvent", (evt) => wasCalled = true);
            await eventBus.EmitAsync("testEvent", new TestEvent());

            Assert.IsTrue(wasCalled, "EventBus resolved from the container did not function correctly.");
        }

        private class TestEvent : IEvent { }
    }
}

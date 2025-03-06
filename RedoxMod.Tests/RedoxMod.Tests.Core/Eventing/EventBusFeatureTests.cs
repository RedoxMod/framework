using NUnit.Framework;
using RedoxMod.API.Eventing;
using RedoxMod.Core.Eventing;
using System.Threading.Tasks;
using System;

namespace RedoxMod.Tests.Core
{
    [TestFixture]
    public class EventBusFeatureTests
    {
        private IEventBus _eventBus;

        [SetUp]
        public void Setup()
        {
            _eventBus = new EventBus();
        }

        [Test]
        public async Task MultipleHandlers_Should_All_Be_Called()
        {
            int callCount = 0;

            await _eventBus.SubscribeAsync("testEvent", (evt) => callCount++);
            await _eventBus.SubscribeAsync("testEvent", (evt) => callCount++);

            await _eventBus.EmitAsync("testEvent", new TestEvent());

            Assert.AreEqual(2, callCount, "Not all handlers were called.");
        }

        [Test]
        public async Task Event_Should_Pass_Correct_Data()
        {
            TestEvent receivedEvent = null;

            await _eventBus.SubscribeAsync<TestEvent>("testEvent", (evt) => receivedEvent = evt);

            var expectedEvent = new TestEvent();
            await _eventBus.EmitAsync("testEvent", expectedEvent);

            Assert.AreEqual(expectedEvent, receivedEvent, "Event data did not match.");
        }

        [Test]
        public async Task Unsubscribing_After_Emit_Should_Not_Remove_Previously_Called_Handlers()
        {
            bool wasCalled = false;
            Action<IEvent> handler = (evt) => wasCalled = true;

            await _eventBus.SubscribeAsync("testEvent", handler);
            await _eventBus.EmitAsync("testEvent", new TestEvent());
            await _eventBus.UnsubscribeAsync("testEvent", handler);

            Assert.IsTrue(wasCalled, "Handler was not called before removal.");
        }

        private class TestEvent : IEvent { }
    }
}

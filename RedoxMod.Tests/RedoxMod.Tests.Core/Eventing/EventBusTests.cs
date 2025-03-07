using NUnit.Framework;
using RedoxMod.API.Eventing;
using RedoxMod.Core.Eventing;
using System.Threading.Tasks;
using System;

namespace RedoxMod.Tests.Core
{
    [TestFixture]
    public class EventBusTests
    {
        private IEventBus _eventBus;

        [SetUp]
        public void Setup()
        {
            _eventBus = new EventBus();
        }

        [Test]
        public async Task SubscribeAsync_Should_AddHandler()
        {
            bool wasCalled = false;

            await _eventBus.SubscribeAsync("testEvent", (evt) => wasCalled = true);

            await _eventBus.EmitAsync("testEvent", new TestEvent());

            Assert.IsTrue(wasCalled, "Handler was not called.");
        }

        [Test]
        public async Task SubscribeAsync_Generic_Should_AddTypedHandler()
        {
            bool wasCalled = false;

            await _eventBus.SubscribeAsync<TestEvent>("testEvent", (evt) => wasCalled = true);

            await _eventBus.EmitAsync("testEvent", new TestEvent());

            Assert.IsTrue(wasCalled, "Typed handler was not called.");
        }

        [Test]
        public async Task UnsubscribeAsync_Should_RemoveHandler()
        {
            bool wasCalled = false;
            Action<IEvent> handler = (evt) => wasCalled = true;

            await _eventBus.SubscribeAsync("testEvent", handler);
            await _eventBus.UnsubscribeAsync("testEvent", handler);
            await _eventBus.EmitAsync("testEvent", new TestEvent());

            Assert.IsFalse(wasCalled, "Handler was called after being removed.");
        }

        [Test]
        public void EmitAsync_Should_Not_Throw_If_No_Subscribers()
        {
            Assert.DoesNotThrowAsync(async () => await _eventBus.EmitAsync("nonexistentEvent", new TestEvent()));
        }

        // Test Event for simulation
        private class TestEvent : IEvent { }
    }
}

using System;
using Core.Actions;
using Core.Queue;
using NUnit.Framework;
using NSubstitute;
using Features.StationLogic;

namespace Editor.Tests
{
    [TestFixture]
    public class station
    {
        private Station _station;
        private InstanceCounter<ICharacterAction> _counter;
        private ResettableQueue<ICharacterAction> _queue;
        private ICharacterAction _action1;
        private ICharacterAction _action2;
        

        [SetUp]
        public void Init()
        {
            _action1 = Substitute.For<ICharacterAction>();
            _action2 = Substitute.For<ICharacterAction>();
            _queue = Substitute.For<ResettableQueue<ICharacterAction>>();

            _counter = new InstanceCounter<ICharacterAction>(new [] {_action1, _action2}, new[] {1, 2});

            _station = new Station(_queue, _counter);
        }
        
        [Test]
        public void current_station_is_null_after_instantiation()
        {
            Assert.AreEqual(null, Station.CurrentStation);
        }

        [Test]
        public void counter_is_set_correctly_after_instantiation()
        {
            Assert.AreEqual(_counter.CurrentCount, _station.CurrentCount);
            Assert.AreEqual(_counter.AvailableCount, _station.AvailableCount);
        }
        
        [Test]
        public void assignable_count_is_set_correctly_after_instantiation()
        {
            Assert.AreEqual(0, _station.AssignableCount);
            
            _station = new Station(_queue, _counter, 4);
            
            Assert.AreEqual(4, _station.AssignableCount);
        }
        
        [Test]
        public void has_assignable_count_returns_correct_value_after_instantiation()
        {
            Assert.AreEqual(false, _station.HasAssignableCount);
            
            ResettableQueue<ICharacterAction> queue = Substitute.For<ResettableQueue<ICharacterAction>>();
            _station = new Station(queue, _counter, 4);
            
            Assert.AreEqual(true, _station.HasAssignableCount);
        }
        
        [Test]
        public void raises_station_entered_event_on_trigger_enter()
        {
            bool received = false;

            Station.StationEntered += (sender, args) => { received = true; };
            
            _station.HandleOnTriggerEnter();
            
            Assert.IsTrue(received);
        }
        
        [Test]
        public void is_sender_in_station_entered_on_trigger_enter()
        {
            object mySender = null;
            
            Station.StationEntered += (sender, args) => { mySender = sender; };
            _station.HandleOnTriggerEnter();
            
            Assert.AreEqual(mySender, _station);
        }
        
        [Test]
        public void does_not_reset_queue_if_not_current_station_on_trigger_enter()
        {
            _station.HandleOnTriggerEnter();

            _queue.DidNotReceive().ResetQueue();
        }
        
        [Test]
        public void does_not_change_current_station_on_trigger_enter()
        {
            _station.HandleOnTriggerEnter();

            Assert.AreEqual(null, Station.CurrentStation);
        }
        
        [Test]
        public void resets_queue_if_current_station_on_trigger_enter()
        {
            _station.Open();
            
            _station.HandleOnTriggerEnter();

            _queue.Received().ResetQueue();
        }
        
        [Test]
        public void raises_station_opened_event()
        {
            bool received = false;

            Station.StationOpened += (sender, args) => { received = true; };
            
            _station.Open();
            
            Assert.IsTrue(received);
        }
        
        [Test]
        public void is_sender_in_station_opened()
        {
            object mySender = null;
            
            Station.StationOpened += (sender, args) => { mySender = sender; };
            _station.Open();
            
            Assert.AreEqual(mySender, _station);
        }
        
        [Test]
        public void sets_current_station_correctly_when_calling_open()
        {
            _station.Open();
            
            Assert.AreEqual(_station, Station.CurrentStation);
        }
        
        [Test]
        public void enqueues_on_enqueue()
        {
            _station.EnqueueAction(_action1);
            
            _queue.Received().Enqueue(_action1);
        }
        
        [Test]
        public void decrements_assignable_count_on_enqueue()
        {
            _station = new Station(_queue, _counter, 4);
            
            _station.EnqueueAction(_action1);
            
            Assert.AreEqual(3, _station.AssignableCount);
        }
        
        [Test]
        public void raises_station_changed_on_enqueue()
        {
            bool received = false;

            Station.StationChanged += (sender, args) => { received = true; };
            
            _station.EnqueueAction(_action1);
            
            Assert.IsTrue(received);
        }
        
        [Test]
        public void is_sender_in_station_changed()
        {
            object mySender = null;
            
            Station.StationChanged += (sender, args) => { mySender = sender; };
            _station.EnqueueAction(_action1);
            
            Assert.AreEqual(mySender, _station);
        }

        [Test]
        public void does_not_enqueue_when_no_action_left()
        {
            _station.EnqueueAction(_action1);
            _queue.ClearReceivedCalls();
            _station.EnqueueAction(_action1);
            
            _queue.Received().Enqueue(_action1);
        }
        
        [Test]
        public void does_not_raise_station_changed_when_no_action_left()
        {
            _station.EnqueueAction(_action1);

            bool received = false;

            Station.StationChanged += (sender, args) => { received = true; };
            
            _station.EnqueueAction(_action1);
            
            Assert.IsTrue(!received);
        }
        
        [Test]
        public void does_not_decrement_assignable_count_when_no_action_left()
        {
            _station = new Station(_queue, _counter, 4);
            _station.EnqueueAction(_action1);

            _station.EnqueueAction(_action1);
            
            Assert.AreEqual(3, _station.AssignableCount);
        }
        
        [Test]
        public void does_not_enqueue_when_assignable_count_is_0()
        {
            _station = new Station(_queue, _counter, 1);
            _station.EnqueueAction(_action1);
            _queue.ClearReceivedCalls();
            _station.EnqueueAction(_action2);
            
            _queue.Received().Enqueue(_action2);
        }
        
        [Test]
        public void does_not_raise_station_changed_when_assignable_count_is_0()
        {
            _station = new Station(_queue, _counter, 1);
            _station.EnqueueAction(_action1);
            
            bool received = false;

            Station.StationChanged += (sender, args) => { received = true; };
            
            _station.EnqueueAction(_action1);
            
            Assert.IsTrue(!received);
        }
        
        [Test]
        public void does_not_decrement_assignable_count_when_assignable_count_is_0()
        {
            _station = new Station(_queue, _counter, 1);
            
            _station.EnqueueAction(_action1);
            _station.EnqueueAction(_action2);
            
            Assert.AreEqual(0, _station.AssignableCount);
        }
        
        [Test]
        public void does_not_decrement_assignable_count_if_has_no_assignable_count()
        {
            _station.EnqueueAction(_action1);
            
            Assert.AreEqual(0, _station.AssignableCount);
        }

        // TODO: I don't like this test but i don't know what to do about it
        [Test]
        public void resets_station_when_calling_open_while_not_current_station()
        {
            _counter = Substitute.For<InstanceCounter<ICharacterAction>>(new [] {_action1, _action2}, new[] {1, 2});
            
            _station = new Station(_queue, _counter, 3);
            
            _station.EnqueueAction(_action1);
            _station.EnqueueAction(_action2);
            
            _station.Open();
            
            _queue.Received().Clear();
            _counter.Received().Reset();

            Assert.AreEqual(3, _station.AssignableCount);
        }
        
        [Test]
        public void clears_queue_on_reset()
        {
            _station.Reset();

            _queue.Received().Clear();
        }
        
        [Test]
        public void resets_action_counter_on_reset()
        {
            _counter = Substitute.For<InstanceCounter<ICharacterAction>>(new [] {_action1, _action2}, new[] {1, 2});

            _station = new Station(_queue, _counter);
            
            _station.Reset();

            _counter.Received().Reset();
        }
        
        [Test]
        public void resets_assignable_count_on_reset()
        {
            _station = new Station(_queue, _counter, 3);
            
            _station.EnqueueAction(_action1);
            _station.EnqueueAction(_action2);
            
            _station.Reset();

            Assert.AreEqual(3, _station.AssignableCount);
        }
        
        [Test]
        public void raises_station_changed_on_reset()
        {
            bool received = false;

            Station.StationChanged += (sender, args) => { received = true; };
            
            _station.Reset();
            
            Assert.IsTrue(received);
        }
        
        [Test]
        public void raises_station_closed_on_close()
        {
            bool received = false;

            Station.StationClosed += (sender, args) => { received = true; };
            
            _station.Close();
            
            Assert.IsTrue(received);
        }
        
        [Test]
        public void is_sender_in_station_closed()
        {
            object mySender = null;
            
            Station.StationClosed += (sender, args) => { mySender = sender; };
            _station.Close();
            
            Assert.AreEqual(mySender, _station);
        }
        
        [Test]
        public void raises_station_exited_on_trigger_exit()
        {
            bool received = false;

            Station.StationExited += (sender, args) => { received = true; };
            
            _station.HandleOnTriggerExit();
            
            Assert.IsTrue(received);
        }
        
        [Test]
        public void is_sender_in_station_exited_on_trigger_exit()
        {
            object mySender = null;
            
            Station.StationExited += (sender, args) => { mySender = sender; };
            _station.HandleOnTriggerExit();
            
            Assert.AreEqual(mySender, _station);
        }
    }
}

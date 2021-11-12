using Features.Actions;
using Features.Queue;
using Features.Station;
using NUnit.Framework;

namespace Tests
{
    public class station
    {
        // A Test behaves as an ordinary method
        [Test]
        public void does_not_reset_queue_when_not_current_station()
        {
            // Arrange
            
            // ResettableQueue<CharacterAction> queue = new ResettableQueue<CharacterAction>();
            // Station station1 = new Station(queue);
            // Station station2 = new Station(queue);
            //
            // station1.OpenStation();
            // //queue.Enqueue(Action.Dash);
            // queue.Dequeue();
            //
            // // Act
            // station2.HandleOnTriggerEnter();
            //
            // // Assert
            // Assert.AreEqual(true, queue.IsEmpty());
        }

    
    }
}

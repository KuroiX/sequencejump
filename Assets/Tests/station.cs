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
            Station station1 = new Station();
            Station station2 = new Station();
            ResettableQueue<int> queue = new ResettableQueue<int>();
            
            station1.OpenStation();
            queue.Enqueue(1);
            queue.Dequeue();

            // Act
            station2.HandleOnTriggerEnter(queue);
            
            // Assert
            Assert.AreEqual(true, queue.IsEmpty());
        }

    
    }
}

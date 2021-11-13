using Features.Actions;
using NUnit.Framework;

namespace Editor.Tests
{
    [TestFixture]
    public class station
    {
        private CharacterAction[] _actions;
        
        [SetUp]
        public void Init()
        {
            int length = CharacterAction.CharacterActions.Count;

            _actions = new CharacterAction[length];
            int i = 0;
            
            foreach (var value in CharacterAction.CharacterActions.Values)
            {
                _actions[i++] = value;
            }
        }
        
        // [Test]
        // public void public_properties_are_set_correctly_after_instantiation()
        // {
        //     // Arrange
        //     StationSettings settings = new StationSettings
        //     {
        //         maxAssignableActions = 3,
        //         //actionCounts = new []{}
        //     };
        // }
        
        
        // [Test]
        // public void does_not_reset_queue_when_not_current_station()
        // {
        //     // Arrange
        //     
        //     ResettableQueue<CharacterAction> queue = new ResettableQueue<CharacterAction>();
        //     Station station1 = new Station(queue);
        //     Station station2 = new Station(queue);
        //     
        //     station1.OpenStation();
        //     //queue.Enqueue(Action.Dash);
        //     queue.Dequeue();
        //     
        //     // Act
        //     station2.HandleOnTriggerEnter();
        //     
        //     // Assert
        //     Assert.AreEqual(true, queue.IsEmpty());
        // }

    
    }
}

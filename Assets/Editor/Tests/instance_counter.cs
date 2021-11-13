using Features.Station;
using NUnit.Framework;

namespace Editor.Tests
{
    [TestFixture]
    public class instance_counter
    {
        private readonly int[] _t = {0, 1, 2, 3, 4};
        private readonly int[] _counts = {1, 2, 3, 4, 5};

        private InstanceCounter<int> _myCounter;
        
        [SetUp]
        public void Init()
        {
           _myCounter = new InstanceCounter<int>(_t, _counts);
        }

        [Test]
        public void has_correct_item_counts_after_instantiation()
        {
            for (int i = 0; i < _counts.Length; i++)
            {
                Assert.AreEqual(i + 1, _myCounter.CurrentAvailableInstances[i]);
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void returns_correct_boolean_on_remove_item(int value)
        {
            for (int i = 0; i < 10; i++)
            {
                var before = _myCounter.CurrentAvailableInstances[value];
                Assert.AreEqual(before > 0, _myCounter.RemoveInstance(value));
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void has_correct_item_count_after_remove(int value)
        {
            int before = _myCounter.CurrentAvailableInstances[value];
            _myCounter.RemoveInstance(value);
            Assert.AreEqual(before-1, _myCounter.CurrentAvailableInstances[value]);
        }

        [Test]
        public void has_correct_item_count_after_reset()
        {
            for (int i = 0; i < _t.Length; i++)
            {
                for (int j = 0; _myCounter.RemoveInstance(i) || j < _t.Length * 2; j++);
            }
            
            _myCounter.ResetCurrentAvailableInstances();
            
            for (int i = 0; i < _counts.Length; i++)
            {
                Assert.AreEqual(i + 1, _myCounter.CurrentAvailableInstances[i]);
            }
        }
    }
}
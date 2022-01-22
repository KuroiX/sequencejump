using System;
using Features.StationLogic;
using NUnit.Framework;

namespace Editor.Tests
{
    [TestFixture]
    public class instance_counter
    {
        private readonly int[] _instances = {0, 1, 2, 3, 4};
        private readonly int[] _counts = {1, 2, 3, 4, 5};

        private InstanceCounter<int> _myCounter;
        
        [SetUp]
        public void Init()
        {
           _myCounter = new InstanceCounter<int>(_instances, _counts);
        }
        
        [Test]
        public void throws_argument_exception_with_negative_count_on_instantiation()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                InstanceCounter<int> myCounter = new InstanceCounter<int>(new[] {0}, new[] {-1});
            });
        }
        
        [Test]
        public void throws_format_exception_with_longer_instance_array_size_on_instantiation()
        {
            Assert.Throws<FormatException>(() =>
            {
                InstanceCounter<int> myCounter = new InstanceCounter<int>(new[] {0, 0}, new[] {0});
            });
        }
        
        [Test]
        public void throws_format_exception_with_longer_count_array_size_on_instantiation()
        {
            Assert.Throws<FormatException>(() =>
            {
                InstanceCounter<int> myCounter = new InstanceCounter<int>(new[] {0}, new[] {0, 0});
            });
        }

        [Test]
        public void has_correct_instance_counts_after_instantiation()
        {
            for (int i = 0; i < _instances.Length; i++)
            {
                Assert.AreEqual(_counts[i], _myCounter.CurrentCount[_instances[i]]);
            }
        }

        [Test]
        public void returns_correct_boolean_on_remove()
        {
            for (int i = 0; i < _instances.Length; i++)
            {
                int before = _counts[i];
                Assert.AreEqual(before > 0, _myCounter.Remove(i));
            }
        }
        
        [Test]
        public void returns_correct_boolean_on_has_left()
        {
            for (int i = 0; i < _instances.Length; i++)
            {
                int before = _counts[i];
                Assert.AreEqual(before > 0, _myCounter.HasLeft(i));
            }
        }

        [Test]
        public void has_correct_item_count_after_remove()
        {
            for (int i = 0; i < _instances.Length; i++)
            {
                int before = _counts[i];
                _myCounter.Remove(_instances[i]);
                Assert.AreEqual(before - 1, _myCounter.CurrentCount[_instances[i]]);
            }
        }

        [Test]
        public void has_correct_item_count_after_reset()
        {
            for (int i = 0; i < _instances.Length; i++)
            {
                _myCounter.Remove(_instances[i]);
            }
            
            _myCounter.Reset();
            
            for (int i = 0; i < _instances.Length; i++)
            {
                Assert.AreEqual(_counts[i], _myCounter.CurrentCount[_instances[i]]);
            }
        }
    }
}
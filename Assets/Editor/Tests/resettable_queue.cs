using Features.Queue;
using NUnit.Framework;

namespace Editor.Tests
{
    [TestFixture]
    public class resettable_queue
    {
        private enum E
        {
            TRUE,
            FALSE
        };
        private struct DefaultStruct
        { 
            public int i;
            public float f;
            public bool b;
            public char c;
            public E e;
            public object o;
        }
        
        private ResettableQueue<int> _queue;
        
        [SetUp]
        public void Init()
        {
            _queue = new ResettableQueue<int>();
        }
        
        [Test]
        public void is_empty_on_creation()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsTrue(_queue.IsEmpty());
            
        }

        [Test]
        public void is_not_empty_after_add()
        {
            // Arrange
            
            // Act
            _queue.Enqueue(1);
            
            // Assert
            Assert.IsFalse(_queue.IsEmpty());
        }

        [Test]
        public void is_empty_after_enqueue_and_dequeue()
        {
            // Arrange
            
            // Act
            _queue.Enqueue(1);
            _queue.Dequeue();
            
            // Assert
            Assert.IsTrue(_queue.IsEmpty());

        }

        [Test]
        public void dequeues_and_deletes_one_enqueued_item()
        {
            _queue.Enqueue(1);

            Assert.AreEqual(1, _queue.Count);
            Assert.AreEqual(1, _queue.Dequeue());
            Assert.AreEqual(0, _queue.Count);
            Assert.IsTrue(_queue.IsEmpty());
        }

        [Test]
        public void dequeues_and_deletes_multiple_enqueued_items_as_fifo()
        {
            _queue.Enqueue(1);
            _queue.Enqueue(2);
            _queue.Enqueue(3);
            _queue.Enqueue(4);

            Assert.AreEqual(4, _queue.Count);
            Assert.AreEqual(1, _queue.Dequeue());
            Assert.AreEqual(3, _queue.Count);
            Assert.AreEqual(2, _queue.Dequeue());
            Assert.AreEqual(3, _queue.Dequeue());
            Assert.AreEqual(4, _queue.Dequeue());
            Assert.IsTrue(_queue.IsEmpty());
        }

        [Test]
        public void returns_default_when_dequeuing_or_peeking_empty_queue()
        {
            ResettableQueue<DefaultStruct> objectQueue = new ResettableQueue<DefaultStruct>();
            
            DefaultStruct defaultStruct = objectQueue.Peek();
            
            Assert.AreEqual(default(int), defaultStruct.i);
            Assert.AreEqual(default(float), defaultStruct.f);
            Assert.AreEqual(default(bool), defaultStruct.b);
            Assert.AreEqual(default(char), defaultStruct.c);
            Assert.AreEqual(default(E), defaultStruct.e);
            Assert.AreEqual(default(object), defaultStruct.o);
            
            defaultStruct = objectQueue.Dequeue();
            
            Assert.AreEqual(default(int), defaultStruct.i);
            Assert.AreEqual(default(float), defaultStruct.f);
            Assert.AreEqual(default(bool), defaultStruct.b);
            Assert.AreEqual(default(char), defaultStruct.c);
            Assert.AreEqual(default(E), defaultStruct.e);
            Assert.AreEqual(default(object), defaultStruct.o);
        }
        
        [Test]
        public void returns_default_when_peeking_over_queue_size()
        {
            ResettableQueue<DefaultStruct> objectQueue = new ResettableQueue<DefaultStruct>();

            DefaultStruct defaultStruct = objectQueue.Peek(1);
            
            Assert.AreEqual(default(int), defaultStruct.i);
            Assert.AreEqual(default(float), defaultStruct.f);
            Assert.AreEqual(default(bool), defaultStruct.b);
            Assert.AreEqual(default(char), defaultStruct.c);
            Assert.AreEqual(default(E), defaultStruct.e);
            Assert.AreEqual(default(object), defaultStruct.o);
            
            defaultStruct = objectQueue.Dequeue();
            
            Assert.AreEqual(default(int), defaultStruct.i);
            Assert.AreEqual(default(float), defaultStruct.f);
            Assert.AreEqual(default(bool), defaultStruct.b);
            Assert.AreEqual(default(char), defaultStruct.c);
            Assert.AreEqual(default(E), defaultStruct.e);
            Assert.AreEqual(default(object), defaultStruct.o);
        }

        [Test]
        public void returns_next_item_upon_calling_peek()
        {
            _queue.Enqueue(1);
            
            Assert.AreEqual(1, _queue.Count);
            Assert.AreEqual(1, _queue.Peek());
            Assert.AreEqual(1, _queue.Count);
            
            _queue.Enqueue(2);
            
            Assert.AreEqual(1, _queue.Peek());

            _queue.Dequeue();
            
            Assert.AreEqual(2, _queue.Peek());
        }
        
        [Test]
        public void returns_item_at_index_upon_calling_peek_with_index()
        {
            _queue.Enqueue(1);
            _queue.Enqueue(2);
            _queue.Enqueue(3);
            
            Assert.AreEqual(1, _queue.Peek(0));
            Assert.AreEqual(2, _queue.Peek(1));
            Assert.AreEqual(3, _queue.Peek(2));
        }

        [Test]
        public void is_empty_after_calling_clear()
        {
            _queue.Enqueue(1);
            _queue.Enqueue(2);
            _queue.Enqueue(3);
            
            _queue.Clear();

            Assert.AreEqual(0, _queue.Size);
            Assert.AreEqual(0, _queue.Count);
            
            Assert.IsTrue(_queue.IsEmpty());
        }

        [Test]
        public void resets_queue_when_calling_reset_queue()
        {
            _queue.Enqueue(1);
            _queue.Enqueue(2);
            _queue.Enqueue(3);

            _queue.Dequeue();
            _queue.Dequeue();
            _queue.Dequeue();
            _queue.ResetQueue();
            
            
            Assert.AreEqual(3, _queue.Size);
            Assert.AreEqual(3, _queue.Count);
            
            Assert.AreEqual(1, _queue.Dequeue());
            Assert.AreEqual(2, _queue.Dequeue());
            Assert.AreEqual(3, _queue.Dequeue());
            
            Assert.AreEqual(3, _queue.Size);
            Assert.AreEqual(0, _queue.Count);
        }

        [Test]
        public void has_correct_size()
        {
            Assert.AreEqual(0, _queue.Size);
            
            _queue.Enqueue(1);
            _queue.Enqueue(2);
            _queue.Enqueue(3);
            
            Assert.AreEqual(3, _queue.Size);
            
            _queue.Dequeue();
            _queue.Dequeue();
            
            Assert.AreEqual(3, _queue.Size);
        }
        
        [Test]
        public void has_correct_count()
        {
            Assert.AreEqual(0, _queue.Size);
            
            _queue.Enqueue(1);
            _queue.Enqueue(2);
            _queue.Enqueue(3);
            
            Assert.AreEqual(3, _queue.Count);
            
            _queue.Dequeue();
            _queue.Dequeue();
            
            Assert.AreEqual(1, _queue.Count);
        }
    }
}
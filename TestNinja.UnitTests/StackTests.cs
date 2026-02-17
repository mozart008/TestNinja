using NUnit.Framework;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Fundamentals.Stack<int?> _stack;

        [SetUp]
        public void SetUp()
        {
            _stack = new Fundamentals.Stack<int?>();
        }

        [Test]
        public void Push_ArgIsNull_ThrowArgumentNullException()
        {
            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidArgument_AddObjectToStack()
        {
            _stack.Push(1);

            Assert.That(_stack.Count, Is.EqualTo(1));
            Assert.That(_stack.Peek(), Is.EqualTo(1));
        }

        [Test]
        public void Count_StackIsEmpty_ReturnZero()
        {
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Count_StackHasValue_ReturnCount()
        {
            _stack.Push(1);

            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Peek_EmptyStack_ThrowInvalidOperatonException()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackWithObjects_ReturnsLastObject()
        {
            _stack.Push(1);
            _stack.Push(2);

            var result = _stack.Peek();

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Peek_StackWithObjects_DoesNotRemoveObjectOnTop()
        {
            _stack.Push(1);
            _stack.Push(2);

            var result = _stack.Peek();

            Assert.That(result, Is.EqualTo(2));
            Assert.That(_stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Pop_EmptyStack_ThrowsInvalidOperatonException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackWithObject_RemovesLastValue()
        {
            _stack.Push(1);
            _stack.Push(2);
            _stack.Push(3);

            var result = _stack.Pop();

            Assert.That(result, Is.EqualTo(3));
            Assert.That(_stack.Count, Is.EqualTo(2));
            Assert.That(_stack.Peek, Is.EqualTo(2));
        }
    }
}

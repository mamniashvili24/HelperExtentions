using NSubstitute;
using System.Diagnostics;

namespace HelperExtentions.Test
{
    public class TryCatchExceptions_Test
    {
        [Fact]
        public void TryCatchFinally_WhenExceptionOfType_ShouldCallTryActionAndCatchActionAndFinallyAction()
        {
            // Arrange
            var finallyActionCalled = false;
            var expectedException = new ArgumentException("ArgumentException");
            ArgumentException returnedExeption = null;

            // Act
            TryCatchExceptions.TryCatchFinally<ArgumentException>(
                () => throw expectedException,
                ex => returnedExeption = ex,
                () => finallyActionCalled = true
            );

            // Assert
            finallyActionCalled.Should().BeTrue();
            returnedExeption.Should().Be(expectedException);
        }

        [Fact]
        public void TryCatch_WhenExceptionOfType_ShouldCallTryActionAndCatchAction()
        {
            // Arrange
            var expectedException = new ArgumentException("ArgumentException");
            ArgumentException returnedExeption = null;

            // Act
            TryCatchExceptions.TryCatch<ArgumentException>(
                () => throw expectedException,
                ex => returnedExeption = ex
            );

            // Assert
            returnedExeption.Should().Be(expectedException);
        }


        [Fact]
        public void TryCatchThrow_WhenExceptionOfType_ShouldCallTryActionAndCatchActionAndRethrow()
        {
            // Arrange
            var expectedException = new ArgumentException("ArgumentException");
            var catchActionCalled = false;
            ArgumentException returnedExeption = null;

            // Act
            var act = () => TryCatchExceptions.TryCatchThrow<ArgumentException>(
                () => throw expectedException,
                () => catchActionCalled = true
            );

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage(expectedException.Message);
            catchActionCalled.Should().BeTrue();
        }

        [Fact]
        public void TryFinally_WhenNoException_ShouldCallTryActionAndFinallyAction()
        {
            // Arrange
            var tryActionCalled = false;
            var finallyActionCalled = false;

            // Act
            TryCatchExceptions.TryFinally(
                () => tryActionCalled = true,
                () => finallyActionCalled = true
            );

            // Assert
            tryActionCalled.Should().BeTrue();
            finallyActionCalled.Should().BeTrue();
        }

        [Fact]
        public void TryFinally_WhenException_ShouldCallTryActionAndFinallyAction()
        {
            // Arrange
            var finallyActionCalled = false;

            // Act
            Action act = () => TryCatchExceptions.TryFinally(
                () => throw new ArgumentException(),
                () => finallyActionCalled = true
            );

            // Assert
            act.Should().Throw<ArgumentException>();
            finallyActionCalled.Should().BeTrue();
        }

        [Fact]
        public void TryCatch_ReturnsResult_WhenNoExceptionThrown()
        {
            // Arrange
            var expected = 42;
            Func<int> tryAction = () => expected;
            Func<Exception, int> catchAction = _ => throw new Exception("Catch action should not be called.");

            // Act
            var result = tryAction.TryCatch(catchAction);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void TryCatch_ReturnsResult_WhenExceptionOfTypeTExeptionTypeIsThrown()
        {
            // Arrange
            var expected = 42;
            Func<int> tryAction = () => throw new ArgumentException("Test exception");
            Func<ArgumentException, int> catchAction = ex => expected;

            // Act
            var result = tryAction.TryCatch(catchAction);

            // Assert
            result.Should().Be(expected);
        }
        
        [Fact]
        public void TryCatch_RethrowsException_WhenExceptionOfDifferentTypeIsThrown()
        {
            // Arrange
            var catchException = new NullReferenceException("Catch action should not be called.");
            Func<int> tryAction = () => throw new InvalidOperationException("Test exception");
            Func<InvalidOperationException, int> catchAction = (error) => throw catchException;

            // Act
            Action act = () => tryAction.TryCatch(catchAction);

            // Assert
            act.Should().Throw<NullReferenceException>().WithMessage(catchException.Message);
        }
    }
}
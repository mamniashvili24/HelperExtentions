namespace HelperExtentions.Test
{
    public class TryCatchExceptions_Test
    {
        [Fact]
        public void TryCatchFinally_WhenNoException_ShouldCallTryActionCatchActionAndFinallyAction()
        {
            // Arrange
            var tryActionCalled = false;
            var catchActionCalled = false;
            var finallyActionCalled = false;

            // Act
            TryCatchExceptions.TryCatchFinally(
                () => tryActionCalled = true,
                ex => catchActionCalled = true,
                () => finallyActionCalled = true
            );

            // Assert
            tryActionCalled.Should().BeTrue();
            catchActionCalled.Should().BeFalse();
            finallyActionCalled.Should().BeTrue();
        }

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
        public void TryCatch_WhenNoException_ShouldCallTryActionAndNotCallCatchAction()
        {
            // Arrange
            var tryActionCalled = false;
            var catchActionCalled = false;

            // Act
            TryCatchExceptions.TryCatch(
                () => tryActionCalled = true,
                ex => catchActionCalled = true
            );

            // Assert
            tryActionCalled.Should().BeTrue();
            catchActionCalled.Should().BeFalse();
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
    }
}
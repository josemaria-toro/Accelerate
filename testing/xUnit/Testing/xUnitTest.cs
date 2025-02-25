using Xunit;

namespace Accelerate.Testing
{
    /// <summary>
    /// Base class for tests based on xUnit.
    /// </summary>
    /// <typeparam name="TContext">
    /// Type of context use on the test.
    /// </typeparam>
    public abstract class xUnitTest<TContext> : Test<TContext>, IClassFixture<TContext> where TContext : xUnitTestContext
    {
        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="context">
        /// Test context
        /// </param>
        protected xUnitTest(TContext context) : base(context)
        {
        }
    }
}
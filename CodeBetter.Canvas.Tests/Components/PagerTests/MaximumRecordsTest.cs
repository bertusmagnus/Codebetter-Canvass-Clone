namespace CodeBetter.Canvas.Tests.PagerTests
{
    using Xunit;

    public class MaximumRecordsTest
    {
        [Fact]
        public void OverwritesRecordsPerPageIfExistingIsGreater()
        {
            var pager = new Pager(1, 50);
            pager.MaximumRecordsPerPage(10);
            Assert.Equal(10, pager.RecordsPerPage);
        }

        [Fact]
        public void OverwritesRecordsPerPageIfExistingIsZero()
        {
            var pager = new Pager(1, 0);
            pager.MaximumRecordsPerPage(10);
            Assert.Equal(10, pager.RecordsPerPage);
        }

        [Fact]
        public void DoesNothingIfExistingIsSmaller()
        {
            var pager = new Pager(1, 4);
            pager.MaximumRecordsPerPage(10);
            Assert.Equal(4, pager.RecordsPerPage);
        }
    }
}
namespace CodeBetter.Canvas.Tests.PagedListTests
{
    using Xunit;

    public class TotalPagesTests
    {
        [Fact]
        public void ReturnsZeroIfNoRecords()
        {
            var paged = new PagedInfo(new Pager(), 0);
            Assert.Equal(0, paged.TotalPages);
        }
        [Fact]
        public void ReturnsZeroIfNoRecordsPerPage()
        {
            var paged = new PagedInfo(new Pager(0, 0), 0);
            Assert.Equal(0, paged.TotalPages);
        }
        [Fact]
        public void ReturnsNumberOfPages()
        {
            var paged = new PagedInfo(new Pager(0, 5), 10);
            Assert.Equal(2, paged.TotalPages);
        }
        [Fact]
        public void ReturnsNumberOfPagesRoundedUp()
        {
            var paged = new PagedInfo(new Pager(0, 4), 10);
            Assert.Equal(3, paged.TotalPages);
        }
    }
}
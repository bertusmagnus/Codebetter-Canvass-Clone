namespace CodeBetter.Canvas.Tests.PagedListTests
{
    using Xunit;

    public class HasRecordsTests
    {
        [Fact]
        public void ReturnsTrueIfThereAreRecords()
        {
            Assert.True(new PagedList<int>(new Pager(), null, 10).HasRecords);
        }
        [Fact]
        public void ReturnsFalseWhenThereAreNoRecords()
        {
            Assert.False(new PagedList<int>(new Pager(), null, 0).HasRecords);
        }
    }
}
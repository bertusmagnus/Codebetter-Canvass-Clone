namespace CodeBetter.Canvas.Tests.PagerTests
{
    using Xunit;

    public class FirstRecordTests
    {
        [Fact]
        public void FirstPageIsZero()
        {
            Assert.Equal(0, new Pager(1).FirstRecord);
            Assert.Equal(0, new Pager(1, 10).FirstRecord);
            Assert.Equal(0, new Pager(1, 10).FirstRecord);
        }
        [Fact]
        public void OtherPagesIsOffsetByRecordSize()
        {
            Assert.Equal(10, new Pager(2, 10).FirstRecord);
            Assert.Equal(15, new Pager(4, 5).FirstRecord);            
        }
        [Fact]
        public void FirstPageIsZeroIfNoRecordSize()
        {
            Assert.Equal(0, new Pager(2, 0).FirstRecord);            
        }

    }
}
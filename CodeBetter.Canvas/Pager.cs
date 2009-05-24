namespace CodeBetter.Canvas
{
    public class Pager
    {
        public int CurrentPage { get; set; }
        public int RecordsPerPage { get; set; }
        public int FirstRecord
        {
            get
            {
                return (CurrentPage - 1) * RecordsPerPage;
            }
        }

        public void MaximumRecordsPerPage(int maximum)
        {
            if (RecordsPerPage == 0 || RecordsPerPage > maximum)
            {
                RecordsPerPage = maximum;
            }
        }

        public Pager(){}
        public Pager(int currentPage)
        {
            CurrentPage = currentPage;
        }
        public Pager(int currentPage, int recordsPerPage)
        {
            CurrentPage = currentPage;
            RecordsPerPage = recordsPerPage;
        }
    }
}
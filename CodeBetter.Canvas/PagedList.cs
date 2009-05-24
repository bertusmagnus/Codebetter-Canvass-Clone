namespace CodeBetter.Canvas
{
    using System;

    public class PagedList<T>
    {
        public T[] Data { get; set; }
        public PagedInfo PagedInfo { get; private set; }
        public bool HasRecords
        {
            get { return PagedInfo.TotalRecords > 0; }
        }

        public PagedList(){}
        public PagedList(Pager pager, T[] data, int totalRecords)
        {
            PagedInfo = new PagedInfo(pager, totalRecords);
            Data = data;
        }
    }

    public class PagedInfo
    {
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int RecordsPerPage { get; set; }
        public int TotalPages
        {
            get
            {
                if (TotalRecords == 0 || RecordsPerPage == 0)
                {
                    return 0;
                }
                return (int)Math.Ceiling((float)TotalRecords / RecordsPerPage);
            }
        }

        public PagedInfo(Pager pager, int totalRecords)
        {
            TotalRecords = totalRecords;
            CurrentPage = pager.CurrentPage;
            RecordsPerPage = pager.RecordsPerPage;
        }
    }
}
namespace FastServices.Web.ViewModels.PaginationList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PaginationList<T> : List<T>
    {
        public int PageIndex { get; private set; }

        public int TotalPages { get; set; }

        public PaginationList(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get { return this.PageIndex > 1; }
        }

        public bool HasNextPage
        {
            get { return this.PageIndex < this.TotalPages; }
        }

        public static PaginationList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginationList<T>(items, count, pageIndex, pageSize);
        }
    }
}

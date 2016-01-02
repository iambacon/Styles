namespace IAmBacon.Presentation.Extensions
{
    using PagedList;

    /// <summary>
    /// Extensions of <see cref="IPagedList"/>.
    /// </summary>
    public static class PagedListExtensions
    {
        /// <summary>
        /// Returns a <see cref="bool"/> indicating whether to display pagination.
        /// </summary>
        /// <param name="pagedList">The paged list.</param>
        /// <returns>The <see cref="bool"/> indicating whether to display pagination.</returns>
        public static bool DisplayPagination(this IPagedList pagedList)
        {
            if (pagedList.HasNextPage && !pagedList.HasPreviousPage)
            {
                return true;
            }

            if (!pagedList.HasNextPage && pagedList.HasPreviousPage)
            {
                return true;
            }

            if (pagedList.HasNextPage && pagedList.HasPreviousPage)
            {
                return true;
            }

            return false;
        }
    }
}

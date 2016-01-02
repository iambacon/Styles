namespace IAmBacon.ViewModels.Shared
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// View model representing pagination.
    /// </summary>
    public class PaginationViewModel
    {
        /// <summary>
        /// Gets the next page.
        /// </summary>
        public PaginationItemViewModel NextPage
        {
            get
            {
                PaginationItemViewModel currentPage = this.Pages.FirstOrDefault(x => x.IsCurrentPage);

                if (currentPage == null)
                {
                    return null;
                }

                int nextPageNumber = currentPage.PageNumber + 1;
                return this.Pages.FirstOrDefault(x => x.PageNumber == nextPageNumber);
            }
        }

        /// <summary>
        /// Gets or sets the pages.
        /// </summary>
        public List<PaginationItemViewModel> Pages { get; set; }
    }
}

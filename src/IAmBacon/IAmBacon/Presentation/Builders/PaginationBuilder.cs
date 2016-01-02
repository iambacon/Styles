namespace IAmBacon.Presentation.Builders
{
    using System.Collections.Generic;

    using IAmBacon.ViewModels;
    using PagedList;

    using Framework.Mvc;

    using IAmBacon.Presentation.Enumerations;
    using IAmBacon.Presentation.Extensions;
    using IAmBacon.ViewModels.Shared;

    /// <summary>
    /// Builder class for pagination.
    /// </summary>
    public class PaginationBuilder
    {
        /// <summary>
        /// The maximum page numbers to display
        /// </summary>
        private readonly int maxPageNumbersToDisplay;

        /// <summary>
        /// The paged list.
        /// </summary>
        private readonly IPagedList<PostViewModel> pagedList;

        /// <summary>
        /// The URL.
        /// </summary>
        private readonly IUrlHelper url;

        /// <summary>
        /// The pagination list.
        /// </summary>
        private readonly PaginationViewModel pagination;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginationBuilder"/> class.
        /// </summary>
        /// <param name="pagedList">The paged list.</param>
        /// <param name="maxPageNumbersToDisplay">The maximum page numbers to display.</param>
        /// <param name="url">The URL.</param>
        public PaginationBuilder(IPagedList<PostViewModel> pagedList, int maxPageNumbersToDisplay, IUrlHelper url)
        {
            this.pagedList = pagedList;
            this.maxPageNumbersToDisplay = maxPageNumbersToDisplay;
            this.url = url;
            this.pagination = new PaginationViewModel { Pages = new List<PaginationItemViewModel>() };
        }

        /// <summary>
        /// Returns the pagination object.
        /// </summary>
        /// <returns>The pagination object.</returns>
        public PaginationViewModel GetResult()
        {
            return this.pagination;
        }

        /// <summary>
        /// Creates the list of <see cref="PaginationItemViewModel"/>.
        /// </summary>
        public void Build()
        {
            if (this.pagedList.PageNumber >= this.maxPageNumbersToDisplay)
            {
                this.pagination.Pages.Add(this.PaginationLink(1));
                this.pagination.Pages.Add(PaginationEllipsis());

                int previousPageNo = this.pagedList.PageNumber - 1;
                this.pagination.Pages.Add(this.PaginationLink(previousPageNo));
                this.pagination.Pages.Add(this.CurrentPaginationLink(this.pagedList.PageNumber));

                if (this.pagedList.PageNumber < this.pagedList.PageCount)
                {
                    int nextPageNo = this.pagedList.PageNumber + 1;
                    this.pagination.Pages.Add(this.PaginationLink(nextPageNo));
                }
            }
            else
            {
                int pageNumbersToDisplay = this.pagedList.PageCount < this.maxPageNumbersToDisplay
                                               ? this.pagedList.PageCount
                                               : this.maxPageNumbersToDisplay;

                for (int i = 1; i <= pageNumbersToDisplay; i++)
                {
                    this.pagination.Pages.Add(
                        this.pagedList.PageNumber == i ? this.CurrentPaginationLink(i) : this.PaginationLink(i));
                }
            }
        }

        /// <summary>
        /// Creates a <see cref="PaginationItemViewModel"/> of type Ellipsis.
        /// </summary>
        /// <returns>The <see cref="PaginationItemViewModel"/> of type Ellipsis.</returns>
        private static PaginationItemViewModel PaginationEllipsis()
        {
            return new PaginationItemViewModel
            {
                Text = "&#8230;",
                Type = PaginationItemType.Ellipsis
            };
        }

        /// <summary>
        /// Creates a <see cref="PaginationItemViewModel"/> of type Link.
        /// </summary>
        /// <param name="pageNo">The page no.</param>
        /// <returns>The <see cref="PaginationItemViewModel"/> of type Link.</returns>
        private PaginationItemViewModel PaginationLink(int pageNo)
        {
            return new PaginationItemViewModel
            {
                PageNumber = pageNo,
                Text = pageNo.ToString(),
                Href = this.url.Blog(pageNo)
            };
        }

        /// <summary>
        /// Creates a <see cref="PaginationItemViewModel"/> of type Link and current page set to true.
        /// </summary>
        /// <param name="pageNo">The page no.</param>
        /// <returns>The <see cref="PaginationItemViewModel"/> of type Link and current page set to true.</returns>
        private PaginationItemViewModel CurrentPaginationLink(int pageNo)
        {
            PaginationItemViewModel link = this.PaginationLink(pageNo);
            link.IsCurrentPage = true;

            return link;
        }
    }
}

using System;

namespace SportsStore.Models.Data
{
    public class PagingInfo
    {
        private int totalPages = 5;

        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => GetTotalPages();
        private int GetTotalPages()
        {
            var result = Math.Ceiling((decimal)TotalItems / ItemsPerPage);

            return Convert.ToInt32(result);
        }
    }
}

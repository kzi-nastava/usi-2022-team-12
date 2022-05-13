namespace HealthInstitution.Pagination.Requests
{
    public class PageRequest
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public static PageRequest Of(int page, int size)
        {
            return new PageRequest { Page = page, Size = size };
        }
    }
}
}

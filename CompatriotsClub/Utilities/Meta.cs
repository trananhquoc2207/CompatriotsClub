namespace CompatriotsClub.Utilities
{
    public class Meta
    {
        public const int MAX_PAGE_SIZE = 500;

        public int total { get; set; }
        public int page_size { get; set; }
        public int page_number { get; set; }
        public int TotalPages { get; private set; }

        private Meta(int total, int? page_size, int? page_number)
        {
            this.total = total;
            if (page_size != null && page_number != null
                && page_size > 0 && page_number > 0 && page_size <= MAX_PAGE_SIZE)
            {
                this.page_size = (int)page_size;
                this.page_number = (int)page_number;
            }
            else
            {
                this.page_size = MAX_PAGE_SIZE;
                this.page_number = 1;
            }
            TotalPages = (int)Math.Ceiling(this.total / (double)this.page_size);
        }

        public static Meta ProcessAndCreate(int total, int? page_size, int? page_number)
        {
            return new Meta(total, page_size, page_number);
        }
    }
}

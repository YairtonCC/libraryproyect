
using Library.Domain.Enum.Library.Domain.Entities;

namespace Library.Domain.Enities
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public LoanStatus Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Enities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; }

        // Relación 1:N → Un miembro puede tener varios préstamos
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}

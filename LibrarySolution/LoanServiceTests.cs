using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using LibraryProyect.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceTests
    {
        private readonly Mock<ILoanRepository> _loanRepoMock;
        private readonly Mock<IBookRepository> _bookRepoMock;
        private readonly Mock<IMemberRepository> _memberRepoMock;
        private readonly LoanService _loanService;

        public LoanServiceTests()
        {
            _loanRepoMock = new Mock<ILoanRepository>();
            _bookRepoMock = new Mock<IBookRepository>();
            _memberRepoMock = new Mock<IMemberRepository>();

            _loanService = new LoanService(
                _loanRepoMock.Object,
                _bookRepoMock.Object,
                _memberRepoMock.Object
            );
        }

        [Fact]
        public async Task AddLoan_ShouldBeActive_WhenDatesAreValid()
        {
            var loan = new Loan
            {
                BookId = 1,
                MemberId = 1,
                LoanDate = DateTime.Now.AddDays(-1),
                ReturnDate = DateTime.Now.AddDays(5),
                Status = LoanStatus.Active
            };

            _bookRepoMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new Book { Id = 1, Title = "Cien años de soledad" });
            _memberRepoMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new Member { Id = 1, Name = "Juan Pérez" });
            _loanRepoMock.Setup(r => r.AddAsync(It.IsAny<Loan>()))
                .ReturnsAsync(loan);

            var result = await _loanService.AddAsync(loan);

            Assert.Equal(LoanStatus.Active, result.Status);
        }

        [Fact]
        public async Task ReturnLoan_ShouldChangeStatusToReturned()
        {
            var loan = new Loan
            {
                Id = 1,
                BookId = 1,
                MemberId = 1,
                LoanDate = DateTime.Now.AddDays(-10),
                ReturnDate = DateTime.Now.AddDays(5),
                Status = LoanStatus.Active
            };

            _loanRepoMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(loan);
            _loanRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Loan>()))
                .ReturnsAsync(true);

            var result = await _loanService.ReturnLoanAsync(1);

            Assert.True(result);
            Assert.Equal(LoanStatus.Returned, loan.Status);
        }

        [Fact]
        public async Task OverdueLoan_ShouldBlockNewLoan()
        {
            var overdueLoan = new Loan
            {
                Id = 2,
                BookId = 2,
                MemberId = 1,
                LoanDate = DateTime.Now.AddDays(-20),
                ReturnDate = DateTime.Now.AddDays(-5),
                Status = LoanStatus.Overdue
            };

            _loanRepoMock.Setup(r => r.GetOverdueLoansByMemberIdAsync(1))
                .ReturnsAsync(new List<Loan> { overdueLoan });

            var newLoan = new Loan
            {
                BookId = 3,
                MemberId = 1,
                LoanDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(7),
                Status = LoanStatus.Active
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _loanService.AddAsync(newLoan));
        }
    }
}

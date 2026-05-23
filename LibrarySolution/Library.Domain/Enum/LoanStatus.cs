namespace Library.Domain.Entities
{
    public enum LoanStatus
    {
        Active = 0,     // El préstamo está vigente
        Returned = 1,   // El libro fue devuelto
        Overdue = 2     // El préstamo está vencido
    }
}

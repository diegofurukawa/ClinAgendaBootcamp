using System;

namespace ClinAgendaAPI
{
    public class Patient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string DocumentNumber { get; set; }
        public required int StatusId { get; set; }
        public required string BirthDate { get; set; } // Changed from BirthDateday to BirthDate
    }
}
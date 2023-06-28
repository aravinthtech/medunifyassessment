using System.Reflection.Metadata;

namespace ManagePatientServices.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public List<PatientVisit> PatientVisits { get; } = new();
    }
}

using Microsoft.Extensions.Hosting;

namespace ManagePatientServices.Models
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public List<Patient> Patients { get; } = new();
    }
}

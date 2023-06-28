namespace ManagePatientServices.Models.Request
{
    public class PatientRequest
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}

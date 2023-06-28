namespace ManagePatientServices.Models
{
    public class PatientVisit
    {
        public int PatientVisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public List<PatientProgressNotes> ProgressNotes { get; } = new();
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}

namespace ManagePatientServices.Models.Request
{
    public class PatientVisitRequest
    {
        public int PatientId { get; set; }
        public string VisitDate { get; set; }
        public List<PatientProgressNotesRequest> ProgressNotes { get; set; }
    }
}

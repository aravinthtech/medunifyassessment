namespace ManagePatientServices.Models
{
    public class PatientProgressNotes
    {
        public int PatientProgressNotesId { get; set; }
        public string SectionName { get; set; }
        public string SectionText { get; set; }
        public int PatientVisitId { get; set; }
        public PatientVisit PatientVisit { get; set; }
    }
}

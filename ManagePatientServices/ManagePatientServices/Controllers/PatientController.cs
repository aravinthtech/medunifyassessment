using ManagePatientServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagePatientServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        [HttpGet]
        [ActionName("Search")]
        public IActionResult Search()
        {
            using(var db=new PatientContext())
            {
                var patientList= db.Patients.ToList();
                return Ok(patientList);
            }
        }

        [HttpPost]
        [ActionName("Insert")]
        public IActionResult Insert(Patient patient)
        {
            using (var db = new PatientContext())
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return Ok();
            }
        }


        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update(Patient patient)
        {
            using (var db = new PatientContext())
            {
                var existingPatient = db.Patients.Where(m => m.PatientId == patient.PatientId).FirstOrDefault();
                if (existingPatient == null) return NotFound();
                db.Patients.Update(existingPatient);
                db.SaveChanges();
                return Ok();
            }
        }


        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete(int patientId)
        {
            using (var db = new PatientContext())
            {
                var existingPatient = db.Patients.Where(m => m.PatientId == patientId).FirstOrDefault();
                if (existingPatient == null) return NotFound();
                db.Patients.Remove(existingPatient);
                db.SaveChanges();
                return Ok();
            }
        }
    }
}

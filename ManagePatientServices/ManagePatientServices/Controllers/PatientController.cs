using ManagePatientServices.Models;
using ManagePatientServices.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ManagePatientServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        [HttpGet]
        [Route("Search")]
        public IActionResult Search([FromQuery] PatientSearchRequest patientSearchCriteria)
        {
            using(var db=new PatientContext())
            {
                IQueryable<Patient> patientListQ = db.Patients.Where(pt=>pt.IsDeleted==false);

                if (!string.IsNullOrEmpty(patientSearchCriteria.FirstName))
                    patientListQ = patientListQ.Where(pt => pt.FirstName.Contains(patientSearchCriteria.FirstName));

                if (!string.IsNullOrEmpty(patientSearchCriteria.LastName))
                    patientListQ = patientListQ.Where(pt => pt.LastName.Contains(patientSearchCriteria.LastName));

                if (!string.IsNullOrEmpty(patientSearchCriteria.Address))
                    patientListQ = patientListQ.Where(pt => pt.Address.Contains(patientSearchCriteria.Address));

                if (!string.IsNullOrEmpty(patientSearchCriteria.State))
                    patientListQ = patientListQ.Where(pt => pt.State.Contains(patientSearchCriteria.State));

                if (!string.IsNullOrEmpty(patientSearchCriteria.City))
                    patientListQ = patientListQ.Where(pt => pt.City.Contains(patientSearchCriteria.City));

                if (patientSearchCriteria.OrganizationId!=0)
                    patientListQ = patientListQ.Where(pt => pt.OrganizationId==patientSearchCriteria.OrganizationId);

                var patientList= patientListQ.ToList();
                return Ok(patientList);
            }
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(PatientRequest patientRequest)
        {
            using (var db = new PatientContext())
            {
                var patient = new Patient()
                {
                    FirstName=patientRequest.FirstName,
                    LastName=patientRequest.LastName,
                    Address=patientRequest.Address,
                    City=patientRequest.City,
                    State=patientRequest.State,
                    CreatedAt=DateTime.Now,
                    UpdatedAt=DateTime.Now,
                    OrganizationId=1
                };
                db.Patients.Add(patient);
                db.SaveChanges();
                return Ok();
            }
        }


        [HttpPost]
        [Route("Update")]
        public IActionResult Update(PatientRequest patientRequest)
        {
            using (var db = new PatientContext())
            {
                var existingPatient = db.Patients.Where(m => m.PatientId == patientRequest.PatientId).FirstOrDefault();
                if (existingPatient == null) return NotFound();

                existingPatient.FirstName = patientRequest.FirstName;
                existingPatient.LastName = patientRequest.LastName;
                existingPatient.Address = patientRequest.Address;
                existingPatient.City = patientRequest.City;
                existingPatient.State = patientRequest.State;
                existingPatient.UpdatedAt = DateTime.Now;
                db.Patients.Update(existingPatient);
                db.SaveChanges();
                return Ok();
            }
        }


        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(PatientRequest patientRequest)
        {
            using (var db = new PatientContext())
            {
                var existingPatient = db.Patients.Where(m => m.PatientId == patientRequest.PatientId).FirstOrDefault();
                if (existingPatient == null) return NotFound();
                existingPatient.IsDeleted=true;
                existingPatient.UpdatedAt = DateTime.Now;
                db.Patients.Update(existingPatient);
                db.SaveChanges();
                return Ok();
            }
        }

        [HttpGet]
        [Route("Detail/{patientId}")]
        public IActionResult Detail(int patientId)
        {
            using (var db = new PatientContext())
            {
                var existingPatient = db.Patients.Include(m=>m.PatientVisits).Where(m => m.PatientId == patientId).FirstOrDefault();
                if (existingPatient == null) return NotFound();               
                return Ok(existingPatient);
            }
        }

        [HttpGet]
        [Route("VisitDetail/{patientVisitId}")]
        public IActionResult VisitDetail(int patientVisitId)
        {
            using (var db = new PatientContext())
            {
                var existingPatient = db.PatientVisits.Include(m => m.ProgressNotes).Where(m => m.PatientVisitId == patientVisitId).FirstOrDefault();
                if (existingPatient == null) return NotFound();
                return Ok(existingPatient);
            }
        }

        [HttpPost]
        [Route("AddVisit")]
        public IActionResult AddVisit(PatientVisitRequest patientVisitRequest)
        {
            using (var db = new PatientContext())
            {
                PatientVisit patientVisit = new PatientVisit();
                patientVisit.PatientId= patientVisitRequest.PatientId;

                if(DateTime.TryParse(patientVisitRequest.VisitDate,out var dtVisit))
                {
                    patientVisit.VisitDate = dtVisit;
                }
                else
                {
                    return BadRequest();
                }

                foreach (var patientProgressNotes in patientVisitRequest.ProgressNotes)
                {
                    PatientProgressNotes patientProgress=new PatientProgressNotes();
                    patientProgress.SectionText = patientProgressNotes.SectionText;
                    patientProgress.SectionName = patientProgressNotes.SectionName;
                    patientProgress.PatientVisit = patientVisit;
                    db.PatientProgressNotes.Add(patientProgress);
                    patientVisit.ProgressNotes.Add(patientProgress);
                }

                db.PatientVisits.Add(patientVisit);
                db.SaveChanges();
                return Ok();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentSystem.Data.Entities
{
    public class DoctorAvailability
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}

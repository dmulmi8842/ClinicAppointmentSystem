using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentSystem.Data.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public string Notes { get; set; }
    }
}

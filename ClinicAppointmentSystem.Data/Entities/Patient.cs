using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentSystem.Data.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
    }
}

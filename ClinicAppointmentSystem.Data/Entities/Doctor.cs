﻿namespace ClinicAppointmentSystem.Data.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public bool status { get; set; }
        public virtual User User { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}

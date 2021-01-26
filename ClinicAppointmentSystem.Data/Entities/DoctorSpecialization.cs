namespace ClinicAppointmentSystem.Data.Entities
{
    public class DoctorSpecialization
    {
        public int Id { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}

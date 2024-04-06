namespace StudentRegistration.DTO
{
    public class Address
    {
        public Guid Id { get; set; }
        public string PhysicalAddress { get; set; }
        public Guid StudentId { get; set; }
    }
}

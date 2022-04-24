namespace CompatriotsClub.Data
{
    public class Ward
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int DistrictId { get; set; }

        public virtual District Districts { get; set; }
    }
}

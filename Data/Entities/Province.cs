﻿namespace CompatriotsClub.Data
{
    public class Province
    {
        public Province()
        {
            Districts = new HashSet<District>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}

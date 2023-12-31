﻿using Employee.Model;

namespace Employee.Frontend.Models
{
    public class State
    {
        public int Id { get; set; }
        public string? StateName { get; set; }
        public int CountryId { get; set; }
        public Countries? Countries { get; set; }
        public string? CountryName { get; set; }
        public ICollection<Employees> Employees { get; set; } =new HashSet<Employees>();

      
    }
}

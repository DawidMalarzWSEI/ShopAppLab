using System;
using System.Collections.Generic;

namespace ShopApp.DB
{
    public partial class Shop
    {
        public Shop()
        {
            Employees = new HashSet<Employee>();
            Positions = new HashSet<Position>();
        }

        public int Id { get; set; }
        public string? ShopName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}

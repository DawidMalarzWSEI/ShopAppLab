using System;
using System.Collections.Generic;

namespace ShopApp.DB
{
    public partial class Position
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string PositionName { get; set; } = null!;
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}

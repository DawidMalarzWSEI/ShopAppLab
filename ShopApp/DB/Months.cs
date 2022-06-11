using System;
using System.Collections.Generic;

#nullable disable

namespace ShopApp.DB
{
    public partial class Months
    {
        public Months()
        {
            Salaries = new HashSet<Salary>();
        }

        public int Id { get; set; }
        public string MonthName { get; set; }

        public virtual ICollection<Salary> Salaries { get; set; }
    }
}

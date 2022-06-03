using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.ViewModels
{
    public class EmployeeDetailModel
    {
        public int Id { get; set; }
        public int UserNo { get; set;  }
        public string Name { get; set; }

        public string Surename { get; set; }

        public int PositionId { get; set; }
        public int ShopId { get; set; }

        public int Salary { get; set; }
        public bool isAdmin { get; set; }

        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}

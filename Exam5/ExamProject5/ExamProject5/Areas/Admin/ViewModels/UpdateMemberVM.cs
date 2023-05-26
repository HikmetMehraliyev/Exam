using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject5.Areas.Admin.ViewModels
{
    public class UpdateMemberVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public string Profession { get; set; }
        public IFormFile Photo { get; set; }
    }
}

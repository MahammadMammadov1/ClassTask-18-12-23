using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace Agency.Core.Models
{
    public class Portfolio : BaseEntity
    {
        public int CatagoryId { get; set; } 
        public Catagory? Catagory { get; set; }

        public string Title { get; set; }

        public string Description{ get; set; }
        public string LittleDescription{ get; set; }
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}

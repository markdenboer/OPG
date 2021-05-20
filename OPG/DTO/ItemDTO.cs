using OPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPG.DTO
{
    public class ItemDTO
    {
        public string Name { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public int Amount { get; set; }
    }
}

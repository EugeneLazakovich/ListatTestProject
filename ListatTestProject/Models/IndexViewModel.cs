using ListatTestProject_BL.DTOs;
using System.Collections.Generic;

namespace ListatTestProject.Models
{
    public class IndexViewModel
    {
        public IEnumerable<SaleDto> Sales { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Dtos
{
    public class GetBrandDto
    {
        public Guid Id { get; set; }
        public string BrandCode { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? BrandImageUrl { get; set; }
    }
}

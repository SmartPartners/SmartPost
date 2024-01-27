using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.Brands;
using SmartPost.Service.DTOs.Categories;

namespace SmartPost.Service.Interfaces.Brands
{
    public interface IBrandService
    {
        Task<bool> ReamoveAsync(long id);
        Task<BrandForResultDto> RetrieveByIdAsync(long id);
        Task<BrandForResultDto> CreateAsync(BrandForCreationDto dto);
        Task<BrandForResultDto> ModifyAsync(long id, BrandForUpdateDto dto);
        Task<IEnumerable<BrandForResultDto>> RetrieveAllAsync(PaginationParams @params);
    }
}

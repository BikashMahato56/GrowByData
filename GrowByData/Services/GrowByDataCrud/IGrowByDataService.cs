using GrowByData.Data;
using GrowByData.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GrowByData.Services
{
    public interface IGrowByDataService
    {
        Task<ProductList> ECommerceApi();
        Task<int?> Insert(ProductDto model);
        Task<bool> Update(ProductDto model);
        Task<int> Delete(ProductDto model);
        Task<ProductDto> GetById(int id);
        Task<IEnumerable<ProductDto>> GetAll();
        void RecordApiDataToDatabase();
    }
}

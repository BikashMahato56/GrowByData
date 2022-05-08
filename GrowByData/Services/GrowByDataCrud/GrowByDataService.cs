using Dapper;
using GrowByData.Data;
using GrowByData.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Text;

namespace GrowByData.Services
{
    public class GrowByDataService : IGrowByDataService
    {
        public static string sp_growbydata = "grow_sp_GrowByDataCrud";
        private readonly IConfiguration _config;
        private string connectionString = "DefaultConnection";
        public GrowByDataService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<int> Delete(ProductDto model)
        {
            DynamicParameters prams = new DynamicParameters();
            prams.Add("@ProductID", model.ProductID);
            prams.Add("@Mode", "D");
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(connectionString));
            return await db.ExecuteAsync(sp_growbydata, prams, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> BulkInsert(IEnumerable<Product> models)
        {
            try
            {
                foreach (var item in models)
                {
                    await Insert(new ProductDto
                    {
                        Name = item.Name,
                        Discount = item.Discount,
                        TotalPrice = item.TotalPrice,
                        Rating = item.Rating,
                        Link = item.Href.Link,
                        AddedOn = DateTime.Now
                    });
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }

        public async void RecordApiDataToDatabase()
        {
            ProductList list = await ECommerceApi();
            await BulkInsert(list.Data);
        }
        public async Task<ProductList> ECommerceApi()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var Uri = new Uri("https://ecommerce-api-with-jwt.herokuapp.com/api/products");
                    httpClient.DefaultRequestHeaders.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue("api-key", "$2y$10$tAajJXlhdqDfGi8CppFN3.KWnofLUVE03gknOyEDv9OBAcypda9MO&#39");
                    var response = await httpClient.GetAsync(Uri);
                    string apiResonse = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<ProductList>(apiResonse);
                    return model;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            DynamicParameters prams = new DynamicParameters();
            prams.Add("@Mode", "V");
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(connectionString));
            return await db.QueryAsync<ProductDto>(sp_growbydata, prams, commandType: CommandType.StoredProcedure);
        }

        public async Task<ProductDto> GetById(int id)
        {
            DynamicParameters prams = new DynamicParameters();
            prams.Add("@ProductID", id);
            prams.Add("@Mode", "V");
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(connectionString));
            return await db.QueryFirstOrDefaultAsync<ProductDto>(sp_growbydata, prams, commandType: CommandType.StoredProcedure);

        }

        public async Task<int?> Insert(ProductDto model)
        {
            DynamicParameters prams = new DynamicParameters();
            prams.Add("@ProductID", model.ProductID);
            prams.Add("@Name", model.Name);
            prams.Add("@Discount", model.Discount);
            prams.Add("@TotalPrice", model.TotalPrice);
            prams.Add("@Rating", model.Rating);
            prams.Add("@Link", model.Link);
            prams.Add("@AddedOn", model.AddedOn);
            prams.Add("@Mode", "I");
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(connectionString));
            return await db.ExecuteAsync(sp_growbydata, prams, commandType: CommandType.StoredProcedure);

        }

        public async Task<bool> Update(ProductDto model)
        {
            DynamicParameters prams = new DynamicParameters();
            prams.Add("@ProductID", model.ProductID);
            prams.Add("@Name", model.Name);
            prams.Add("@Discount", model.Discount);
            prams.Add("@TotalPrice", model.TotalPrice);
            prams.Add("@Rating", model.Rating);
            prams.Add("@Link", model.Link);
            prams.Add("@Mode", "U");
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(connectionString));
            var status = await db.ExecuteAsync(sp_growbydata, prams, commandType: CommandType.StoredProcedure);
            return status > 0 ? true : false;
        }

    }
}

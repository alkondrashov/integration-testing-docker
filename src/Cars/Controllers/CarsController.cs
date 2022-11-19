using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Dapper;

namespace Cars.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController
    {
        private readonly ILogger<CarsController> _logger;

        private string _connectionString;

        public CarsController(ILogger<CarsController> logger)
        {
            _logger = logger;
            _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        }

        [HttpPost]
        public async Task<CarModel> Create(CarModel model)
        {

            using (var connection = new MySqlConnection(_connectionString))
            {
                _logger.LogInformation("connection string: " + _connectionString);
                _logger.LogInformation("model: " + model);
                _logger.LogInformation("Checked=" + model.Available + " Text" + model.Name);
                var result = await connection.QueryAsync<string>("INSERT INTO cars (name, available) values (@Name, @Available); SELECT LAST_INSERT_ID();", model);
                var id = result.Single();
                return await Get(id);
            }
        }

        [HttpGet("{id}")]
        public async Task<CarModel> Get(string id)
        {
            _logger.LogInformation("Connection: " + _connectionString);
            using(var connection = new MySqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<CarModel>("SELECT id,available,name FROM cars WHERE id=@Id", new { Id = id });
                var model = result.FirstOrDefault();
                _logger.LogInformation("Model with id: " + model.Id);
                return model;
            }
        }
    }
}
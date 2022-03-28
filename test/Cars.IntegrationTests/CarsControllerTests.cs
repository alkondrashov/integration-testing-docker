using System;
using System.Net.Http;
using Newtonsoft.Json;
using Xunit;
using MySql.Data.MySqlClient;
using Dapper;
using System.Text;
using Xunit.Abstractions;
using Cars.Controllers;

namespace Cars.IntegrationTests
{
    public class CarsControllerTests : IDisposable
    {
        private string _endpoint = "/api/cars";
        private string _url;
        private string _connectionString;

        private ITestOutputHelper _outputHelper { get; }

        public CarsControllerTests(ITestOutputHelper outputHelper)
        {
            _url = Environment.GetEnvironmentVariable("API_URL") + _endpoint;
            _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            _outputHelper = outputHelper;
        }
        
        [Fact]
        public async void shouldAddToDatabaseOnPost()
        {
            var client = new HttpClient();
            var car = new CarModel { Available = false, Name = "Test Text" };
            
            _outputHelper.WriteLine("Connection: " + _url);

            var result = await client.PostAsync(_url, new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json"));
            var expectedModel = JsonConvert.DeserializeObject<CarModel>(await result.Content.ReadAsStringAsync());
            
            var response = await client.GetAsync($"{_url}/{expectedModel.Id}");
            var actualModel = JsonConvert.DeserializeObject<CarModel>(await result.Content.ReadAsStringAsync());
            
            Assert.Equal(expectedModel.Id, actualModel.Id);
            Assert.Equal(expectedModel.Name, actualModel.Name);
        }

        public void Dispose()
        {
            using(var connection = new MySqlConnection(_connectionString))
            {
                connection.Execute("truncate cars");
            }
        }
    }
}

using SimpleWebAPI.Messages;
using SimpleWebAPI.Model;
using System.Xml.Linq;

namespace SimpleWebAPI.Controller
{
    public class VehicleController
    {
        private List<Vehicle> _data;

        public VehicleController()
        {
            _data = new();
            GetDataFromSource();
        }

        private void GetDataFromSource()
        {
            _data.Add(new Vehicle() { Id = Guid.NewGuid().ToString(), Brand = "Бренд 1", Model = "Модель 1", ReleaseYear = 2025 });
            _data.Add(new Vehicle() { Id = Guid.NewGuid().ToString(), Brand = "Бренд 2", Model = "Модель 2", ReleaseYear = 2025 });
            _data.Add(new Vehicle() { Id = Guid.NewGuid().ToString(), Brand = "Бренд 3", Model = "Модель 3", ReleaseYear = 2025 });
        }

        public async Task GetAllAsync(HttpResponse response)
        {
            await response.WriteAsJsonAsync(_data);
        }

        public async Task GetAsync(string? id, HttpResponse response)
        {
            var found = _data.FirstOrDefault(x => x.Id == id);

            if (found == null)
            {
                response.StatusCode = 404;
                await response.WriteAsJsonAsync(new NotFoundMessage());
                return;
            }

            await response.WriteAsJsonAsync(found);
        }

        public async Task CreateAsync(HttpRequest request, HttpResponse response)
        {
            try
            {
                var new_data = await request.ReadFromJsonAsync<Vehicle>();

                if (new_data == null)
                {
                    throw new Exception($"VehicleController.CreateAsync(Exception): Некорректные данные!");
                }

                new_data.Id = Guid.NewGuid().ToString();
                _data.Add(new_data);
                await response.WriteAsJsonAsync(new_data);
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = ex.Message });
            }
        }

        public async Task UpdateAsync(HttpRequest request, HttpResponse response)
        {
            try
            {
                var request_data = await request.ReadFromJsonAsync<Vehicle>();

                if (request_data == null)
                {
                    throw new Exception("VehicleController.UpdateAsync: Некорректный ввод данных!");
                }

                var found_data = _data.FirstOrDefault(x => x.Id == request_data.Id);

                if (found_data == null)
                {
                    throw new Exception("VehicleController.UpdateAsync: Данные не найдены!");
                }

                found_data.Brand = request_data.Brand;
                found_data.Model = request_data.Model;
                found_data.ReleaseYear = request_data.ReleaseYear;

                await response.WriteAsJsonAsync<Vehicle>(found_data);
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = ex.Message });
            }
        }

        public async Task DeleteAsync(string? id, HttpResponse response)
        {
            try
            {
                var found_data = _data.FirstOrDefault(x => x.Id == id);

                if (found_data == null)
                    throw new Exception("VehicleController.DeleteAsync: Данные не найден!");

                _data.Remove(found_data);
                await response.WriteAsJsonAsync(found_data);
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = ex.Message });
            }
        }
    }
}

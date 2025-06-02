using SimpleWebAPI.Model;

namespace SimpleWebAPI.Controller
{
    public class UsersController
    {
        private List<User> _users;

        public UsersController() 
        {
            _users = new();
            GetDataFromSource();
        }

        private void GetDataFromSource()
        {
            _users.Add(new() { Id = Guid.NewGuid().ToString(), Login = "log1", Password = "1111"});
            _users.Add(new() { Id = Guid.NewGuid().ToString(), Login = "log2", Password = "2222"});
            _users.Add(new() { Id = Guid.NewGuid().ToString(), Login = "log3", Password = "3333"});
        }

        public async Task GetAllAsync(HttpResponse response)
        {
            await response.WriteAsJsonAsync(_users);
        }

        public async Task GetAsync(string? id, HttpResponse response)
        {
            User? found_user = _users.FirstOrDefault(x => x.Id == id);

            if(found_user == null)
            {
                response.StatusCode = 404;
                await response.WriteAsJsonAsync(new { message = "Пользователь не найден!" });
                return;
            }

            await response.WriteAsJsonAsync(found_user);
        }

        public async Task CreateAsync(HttpRequest request, HttpResponse response)
        {
            try
            {
                var new_user = await request.ReadFromJsonAsync<User>();

                if (new_user == null)
                {
                    throw new Exception("UsersController.Create(Exception): Некорректные данные!");
                }

                new_user.Id = Guid.NewGuid().ToString();
                _users.Add(new_user);
                await response.WriteAsJsonAsync(new_user);
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
                User? request_user = await request.ReadFromJsonAsync<User>();

                if(request_user == null)
                {
                    throw new Exception("UsersController.Update: Некорректный ввод данных!");
                }

                User? found_user = _users.FirstOrDefault(x => x.Id == request_user.Id);
                   
                if(found_user == null)
                {
                    throw new Exception("UsersController.Update: Пользователь не найден!");
                }

                found_user.Login = request_user.Login;
                found_user.Password = request_user.Password;

                await response.WriteAsJsonAsync<User>(found_user);
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
                User? found_user = _users.FirstOrDefault(x => x.Id == id);

                if (found_user == null)
                    throw new Exception("UsersController.Delete: Пользователь не найден!");

                _users.Remove(found_user);
                await response.WriteAsJsonAsync(found_user);
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new {message = ex.Message});
            }
        }
    }
}

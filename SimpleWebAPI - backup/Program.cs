using System.Text.RegularExpressions;
using SimpleWebAPI.Controller;

UsersController usersController = new();
VehicleController vehiclesController = new();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseStaticFiles();

// Терминальный middleware
app.Run(
    async context =>
    {
        var response = context.Response;
        var request = context.Request;
        var path = request.Path;

        // Для сопоставления регулярного выражения 
        string expressionForNumber = "^/api/users/([0-9]+)$";                                   // если Id - int 
        string expressionForGuid_user = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";         // если Id - Guid 
        string expressionForGuid_vehicle = @"^/api/vehicles/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";   // если Id - Guid 

        #region === MAIN ===

        if(path == "/")
        {
            response.ContentType = "text/html; charset=utf-8";
            await response.SendFileAsync("html/index.html");
        }
        #endregion

        #region === USERS ===

        if (path == "/api/users" && request.Method == "GET")
        {
            // Отдать все данные (сервер -> клиент)
            await usersController.GetAllAsync(response);
        }
        else if (Regex.IsMatch(path, expressionForGuid_user) && request.Method == "GET")
        {
            // Отдать одного пользователя (сервер -> клиент)
            string? id = path.Value?.Split('/')[3];
            await usersController.GetAsync(id, response);
        }
        else if (path == "/api/users" && request.Method == "POST")
        {
            // Получить данные для добавления (клиент -> сервер)
            await usersController.CreateAsync(request, response);
        }
        else if (path == "/api/users" && request.Method == "PUT")
        {
            // Получить данные для обновления (клиент -> сервер)
            await usersController.UpdateAsync(request, response);
        }
        else if (Regex.IsMatch(path, expressionForGuid_user) && request.Method == "DELETE")
        {
            // Получить данные для удаления (клиент -> сервер)
            string? id = path.Value?.Split('/')[3];
            await usersController.DeleteAsync(id, response);
        }
        else if(path == "/users")
        {
            response.ContentType = "text/html; charset=utf-8";
            await response.SendFileAsync("html/users.html");
        }
        #endregion

        #region === VEHICLES ===

        if (path == "/api/vehicles" && request.Method == "GET")
        {
            // Отдать все данные (сервер -> клиент)
            await vehiclesController.GetAllAsync(response);
        }
        else if (Regex.IsMatch(path, expressionForGuid_vehicle) && request.Method == "GET")
        {
            // Отдать одного (сервер -> клиент)
            string? id = path.Value?.Split('/')[3];
            await vehiclesController.GetAsync(id, response);
        }
        else if (path == "/api/vehicles" && request.Method == "POST")
        {
            // Получить данные для добавления (клиент -> сервер)
            await vehiclesController.CreateAsync(request, response);
        }
        else if (path == "/api/vehicles" && request.Method == "PUT")
        {
            // Получить данные для обновления (клиент -> сервер)
            await vehiclesController.UpdateAsync(request, response);
        }
        else if (Regex.IsMatch(path, expressionForGuid_vehicle) && request.Method == "DELETE")
        {
            // Получить данные для удаления (клиент -> сервер)
            string? id = path.Value?.Split('/')[3];
            await vehiclesController.DeleteAsync(id, response);
        }
        else if (path == "/vehicles")
        {
            response.ContentType = "text/html; charset=utf-8";
            await response.SendFileAsync("html/vehicles.html");
        }
        #endregion
    }
);

app.Run();

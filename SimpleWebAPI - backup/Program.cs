using System.Text.RegularExpressions;
using SimpleWebAPI.Controller;

UsersController usersController = new();
VehicleController vehiclesController = new();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseStaticFiles();

// ������������ middleware
app.Run(
    async context =>
    {
        var response = context.Response;
        var request = context.Request;
        var path = request.Path;

        // ��� ������������� ����������� ��������� 
        string expressionForNumber = "^/api/users/([0-9]+)$";                                   // ���� Id - int 
        string expressionForGuid_user = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";         // ���� Id - Guid 
        string expressionForGuid_vehicle = @"^/api/vehicles/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";   // ���� Id - Guid 

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
            // ������ ��� ������ (������ -> ������)
            await usersController.GetAllAsync(response);
        }
        else if (Regex.IsMatch(path, expressionForGuid_user) && request.Method == "GET")
        {
            // ������ ������ ������������ (������ -> ������)
            string? id = path.Value?.Split('/')[3];
            await usersController.GetAsync(id, response);
        }
        else if (path == "/api/users" && request.Method == "POST")
        {
            // �������� ������ ��� ���������� (������ -> ������)
            await usersController.CreateAsync(request, response);
        }
        else if (path == "/api/users" && request.Method == "PUT")
        {
            // �������� ������ ��� ���������� (������ -> ������)
            await usersController.UpdateAsync(request, response);
        }
        else if (Regex.IsMatch(path, expressionForGuid_user) && request.Method == "DELETE")
        {
            // �������� ������ ��� �������� (������ -> ������)
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
            // ������ ��� ������ (������ -> ������)
            await vehiclesController.GetAllAsync(response);
        }
        else if (Regex.IsMatch(path, expressionForGuid_vehicle) && request.Method == "GET")
        {
            // ������ ������ (������ -> ������)
            string? id = path.Value?.Split('/')[3];
            await vehiclesController.GetAsync(id, response);
        }
        else if (path == "/api/vehicles" && request.Method == "POST")
        {
            // �������� ������ ��� ���������� (������ -> ������)
            await vehiclesController.CreateAsync(request, response);
        }
        else if (path == "/api/vehicles" && request.Method == "PUT")
        {
            // �������� ������ ��� ���������� (������ -> ������)
            await vehiclesController.UpdateAsync(request, response);
        }
        else if (Regex.IsMatch(path, expressionForGuid_vehicle) && request.Method == "DELETE")
        {
            // �������� ������ ��� �������� (������ -> ������)
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

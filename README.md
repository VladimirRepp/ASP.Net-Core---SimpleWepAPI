# ASP.Net-Core---SimpleWepAPI
ASP.NET Core Simple WebAPI
===

🔹 Проект: Чистый RESTful веб-API на ASP.NET Core для работы с пользователями
🔹 Цель: Демонстрация создания минимального WebAPI с CRUD-операциями, готового к интеграции с фронтендом или мобильными приложениями
🔹 Ключевые технологии:
* Платформа: ASP.NET Core 6+
* Архитектура: REST API
* База данных: не используется, легко интегрируется 
* Сериализация: JSON
* Документация: Swagger/OpenAPI (автогенерация)

🔹 Дополнительно:
- Entity Framework Core (ORM)
- Встроенная валидация моделей
- Чистая архитектура контроллеров

🔹 Основные возможности API: <br />
✅ CRUD операции для сущности User:  <br />
GET /api/users - получить всех пользователей <br />
GET /api/users/{id} - получить конкретного пользователя  <br />
POST /api/users - создать нового пользователя <br />
PUT /api/users/{id} - обновить пользователя <br />
DELETE /api/users/{id} - удалить пользователя <br />
✅ Автодокументация через Swagger UI <br />
✅ Валидация входных данных <br />
✅ Логирование работы API <br />

🔹 Структура проекта: <br />
SimpleWebAPI/ <br />
├── Controllers/ <br />
│   └── UsersController.cs    # API Endpoints <br />
├── Models/ <br />
│   ├── User.cs               # Data model <br />
│   └── AppDbContext.cs       # Database context <br />
├── Properties/ <br />
│   └── launchSettings.json   # Runtime config <br />
├── appsettings.json          # DB configuration <br />
└── Program.cs                # Startup logic <br />

🔹 Возможные улучшения:
- Добавить JWT аутентификацию
- Реализовать пагинацию для GET /users
- Добавить кэширование ответов
- Настроить CORS для фронтенда

🔹 Преимущества этого API:
- Чистая REST-архитектура
- Готовая документация через Swagger
- Простота интеграции с любым клиентом

🔹 Скриншот интерфейса:
![image](https://github.com/user-attachments/assets/e30ded3d-f750-4048-ba3f-a8d2771eccef)

![image](https://github.com/user-attachments/assets/19d00f78-dcf8-4f30-887b-bffe6374becb)

![image](https://github.com/user-attachments/assets/fa08f707-36e1-4931-bbf1-04bb01758430)





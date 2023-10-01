Перед запуском приложения требуется указать строку подключения к БД в файле `Web/appsettings.json`. Для ключа `DefaultConnection` нужно указать адрес, пароль и название бд, которуе будет использовать приложение:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost; User ID=root; Password=your_password_here; Database=your_database_name_here"
},
...
```
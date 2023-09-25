Перед запуском приложения требуется указать строку подключения к БД в файле `Web/appsettings.json`. Для ключа `DefaultConnection` нужно указать адрес, пароль и название бд, которуе будет использовать приложение:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server_address; User ID=root; Password=your_password_here; Database=your_database_name_here"
},
...
```

При желании изменить порт или адрес приложения требуется также изменить параметр `HostUrl` в `Web/appsettings.json` для корректного функционирования коротких ссылок.

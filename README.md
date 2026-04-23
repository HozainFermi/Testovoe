ASP.NET Core Razor Pages приложение для учёта сотрудников с возможностью фильтрации, редактирования и формирования отчётов.

- .NET 10
- ASP.NET Core Razor Pages
- Entity Framework Core
- MSSQL Server
- Docker
- EPPlus (Excel экспорт)

## Функционал

- CRUD операции с сотрудниками
- Фильтрация по ФИО, подразделению, образованию
- Пагинация
- Справочники подразделений и образований
- Формирование списка на повышение ЗП
- Выгрузка отчётов в Excel

## Запуск через Docker

```bash
git clone https://github.com/HozainFermi/Testovoe.git
cd WebApplication1
docker-compose up --build

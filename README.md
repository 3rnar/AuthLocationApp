# AuthLocationApp

## 📌 Overview
**AuthLocationApp** – тестовый веб-проект, реализующий двухэтапную регистрацию пользователя. Использует **.NET** на бэкенде и **Angular** на фронтенде.

## 🛠 Tech Stack
- **Backend:** .NET, Entity Framework Core
- **Frontend:** Angular, TypeScript
- **Database:** SQLite
- **ORM:** Entity Framework Core
- **Design:** Material UI
- **Logging:** Serilog
- **Security:** BCrypt.Net-Next (хеширование паролей)
- **Validation:** FluentValidation
- **Testing:** xUnit

## 🚀 Features
-  **Two-step Registration Wizard**

## 🛠 Architecture
- **OOP (Объектно-Ориентированное Программирование)**: применение принципов **SOLID** для чистой архитектуры.
- **Domain-Driven Design (DDD)**: разделение бизнес-логики на **Entities, Aggregates, Value Objects**.
- **CQRS (Command Query Responsibility Segregation)**: разделение команд (изменение данных) и запросов (чтение данных) для повышения производительности.
- **MediatR**: внедрение паттерна **Mediator** для управления обработчиками команд и запросов.


## 📦 Installation & Setup
Запуск проекта возможен нажатием **F5** в Visual Studio.
### Requirements
- **Node.js**: v18+
- **.NET SDK**: 7.0+
- **SQLite**: Latest
- **Angular CLI**: 15+
- **Docker**: Latest (optional)

## 🔗 API Endpoints
### 🔐 Authentication
- `GET /api/countries` – получение списка стран
- `GET /api/provinces/by-country/{id:int}` – получение списка провиниции по идентификатору страны
- `POST /api/users` – создание пользователя
- `GET /api/users/{id:int}` – получение пользователя по идентификатору

## 📜 License
Этот проект создан в рамках тестового задания и не предназначен для коммерческого использования.
# 🏆 Dance Studio Backend

Учебный бекэнд проект для условной студии танцев. Позволяет регистрировать пользователей, управлять занятиями и проводить аутентификацию.

## 🚀 Функционал

- ✅ Регистрация пользователей  
- ✅ Авторизация через **JWT + Refresh Token**  
- ✅ Запись пользователей на занятия  
- ✅ Создание новых занятий  
- ✅ Получение списка всех занятий  
- ✅ Получение списка занятий определенного тренера  

## 🏗 Архитектура  

Проект построен на **микросервисной архитектуре** и состоит из двух сервисов:  

1. **Auth Service** – отвечает за аутентификацию и управление токенами  
2. **BackendAPI** – регистрация пользователей и управление занятиями (создание, запись, получение)  

## 🛠 Технологии  

Проект использует:  

- **C# + ASP.NET Core** – основной фреймворк  
- **PostgreSQL** – основная база данных  
- **Redis** – хранение Refresh токенов  
- **Kafka** – обмен сообщениями между сервисами  
- **Entity Framework Core** – работа с БД  
- **JWT** – аутентификация  

## 📂 Структура проекта  

/Controllers # Контроллеры обработки запросов 
/Services # Бизнес-логика сервиса 
/Interfaces # Интерфейсы сервисов 
/Models # Модели пользователей и токенов 
/DTOs # Data Transfer Objects (DTO) 
/Data # Контекст базы данных (PostgreSQL, Redis)

## 🔑 Аутентификация  

Система аутентификации реализована с помощью **JWT + Refresh Token**.  

1. При входе пользователь получает **Access Token (JWT)** и **Refresh Token**.  
2. Access Token используется для авторизации в API.  
3. Refresh Token хранится в **Redis** и позволяет обновлять Access Token.  
4. Если Access Token истек, пользователь получает новый, отправляя Refresh Token.  

## 📡 API  

На данный момент **документация API отсутствует**

## 📦 Установка и запуск  

1. **Склонируйте репозиторий:**  
   ```
   git clone https://github.com/your-repo/dance-studio-backend.git
   cd dance-studio-backend
2. **Настройте PostgreSQL и Redis (укажите подключения в appsettings.json)**
3. **Запустите каждый микросервис**

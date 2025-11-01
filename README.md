# Hooli library

**Hooli library** is a university project that demonstrates the principles of clean architecture, design patterns, using **ASP.NET Core**, **Entity Framework** , and **PostgreSQL**. This service provides a comprehensive API for building personalized music libraries and managing associated metadata. 

## 🎯 Project Overview

- **Clean Architecture** implementation with separated layers (Core, Infrastructure, Services, API)
- **Repository Pattern** for data access abstraction
- **Dependency Injection** for loose coupling
- **RESTful API** design principles
- **Database-first** approach with Entity Framework Core migrations
- **Containerization** using Docker for simplified deployment


## 🚀 Technologies

- **.NET 8**
- **ASP.NET Core**
- **Entity Framework**
- **PostgreSQL**
- **Docker**

---

## ▶️ Installation

**Initial start:**
1. Clone repository: `git clone https://github.com/Nikolas321654/Hooli_library.git`
2. `cd HomeLib`
3. `docker-compose up --build`
4. `dotnet ef database update --startup-project HomeLib.API --project HomeLib.Infrastructure`

**Start:** 
1. `cd HomeLib`
2. `docker-compose up --build`

**Stop:**
`docker-compose down`

**(http://localhost:8080)**

## 📚 Literature

- **Clean Code** — Robert C. Martin

- **Clean Architecture** — Robert C. Martin

- **Pro C# 10 with .NET 6: Foundational Principles and Practices in Programming** —  Andrew Troelsen, Phil Japikse

- **Design Patterns: Elements of Reusable Object-Oriented Software** — Gang of Four (GoF)
- **Learning PostgreSQL** - Salahaldin Juba, Achim Vannahme, Andrey Volkov

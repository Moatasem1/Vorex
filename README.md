# 🦊 Vorex – Smart Money Tracking App with Crypto Risk Analysis

Vorex is a powerful personal finance tracking system built with modern web technologies and best architectural practices. It helps users manage daily expenses, set budgets, and get real-time risk analysis for their cryptocurrency investments. Vorex goes beyond basic tracking by offering a smart, scalable, and extensible architecture using advanced backend design patterns.

---

## 🔥 Features

- ✅ **User Registration & Login** with JWT Authentication
- 📊 **Expense & Budget Tracking**
- 🧠 **Smart Crypto Risk Analysis Engine**
- ❤️ **Favorites & Compare List** Support
- 🏷️ **Category-based Expense Filtering**
- 🔐 **Secure Password Management**
- 🖼️ **Profile Image Upload and Update**

---

## 🧱 Domain-Driven Design (DDD)

The entire backend is structured using **DDD** principles:
- Domain models encapsulate core business logic.
- Clean separation between **Domain**, **Application**, **Infrastructure**, and **Presentation** layers.
- Reduces tight coupling and improves scalability and testability.

---

## 🧰 Architecture & Design Patterns Used

| Pattern | Description |
|--------|-------------|
| **CQRS (Command Query Responsibility Segregation)** | Separates read and write logic for better scalability and cleaner code. |
| **Mediator Pattern** | Used via MediatR to decouple request handling from business logic. |
| **Specification Pattern** | Encapsulates complex filtering logic in reusable, composable specifications. |
| **Generic Repository Pattern** | Provides reusable data access methods for multiple entities. |
| **Unit of Work Pattern** | Coordinates and commits changes across multiple repositories in one transaction. |
| **Factory Pattern** | Used to create instances of domain services and components with flexibility. |

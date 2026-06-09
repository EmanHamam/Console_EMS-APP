
# EMS (Examination Management System)

A comprehensive .NET console-based Examination Management System built in C# with clean architecture principles. This application allows students to take practice and final exams with interactive question management.

https://github.com/user-attachments/assets/c88c4b64-7d3c-482b-8295-77b9bd8e98d4


---

## 📋 Table of Contents

- [Overview](#overview)
- [Project Structure](#project-structure)
- [Projects](#projects)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Architecture](#architecture)
- [Class Diagram](#class-diagram)

---

## 🎯 Overview

The Examination Management System (EMS) is a console-based application designed to manage examinations and student assessments. It implements clean architecture principles with separation of concerns across multiple layers (Domain, Infrastructure, and Presentation).

### Key Capabilities:
- **Multiple Exam Types**: Practice Exams and Final Exams
- **Question Variety**: True/False, Choose One, and Choose All question types
- **Student Management**: Track student responses and event notifications
- **Answer Management**: Capture and manage student answers
- **Interactive UI**: Console-based menu-driven interface
- **Event System**: Observer pattern for exam events

---

## 📁 Project Structure

```
EMS/
├── EMS/                          # Main Console Application (Presentation Layer)
├── EMS.Domain/                   # Business Logic & Entities (Domain Layer)
└── Ems.Infrastructure/           # Data Access & Utilities (Infrastructure Layer)
```

---

## 🏗️ Projects

### 1. **EMS (Main Application)**
**Path**: `EMS/EMS.csproj`

The console application entry point that coordinates the user interface and orchestrates the examination flow.

**Key Files:**
- `Program.cs` - Main entry point with menu system and exam coordination

**Responsibilities:**
- User interaction through interactive menus
- Exam selection and execution
- Student exam participation
- Question randomization and selection

**Target Framework**: `.NET 10.0`

---

### 2. **EMS.Domain (Domain Layer)**
**Path**: `EMS.Domain/EMS.Domain.csproj`

Contains all business logic, entities, and interfaces following Domain-Driven Design principles.

**Key Components:**

#### Models
- **Exams**
  - `Exam.cs` - Abstract base class for all exam types
  - `PracticeExam.cs` - Practice exam implementation with review mode
  - `FinalExam.cs` - Final exam implementation with strict grading

- **Questions**
  - `Question.cs` - Abstract base class for questions
  - `ChooseOneQuestion.cs` - Single-choice questions
  - `ChooseAllQuestion.cs` - Multiple-choice questions
  - `TrueFalseQuestion.cs` - True/False questions
  - `QuestionList.cs` - Collection management for questions

- **Answers**
  - `Answer.cs` - Student answer entity
  - `AnswerList.cs` - Collection management for answers

- **Students**
  - `Student.cs` - Student entity with event subscription capabilities

- **Subjects**
  - `Subject.cs` - Subject entity for exam organization

#### Interfaces & Contracts
- `IExamBehavior.cs` - Interface defining exam behavior contract

#### Events & Delegates
- `ExamEventArgs.cs` - Custom event arguments for exam events
- `ExamStartedHandler.cs` - Delegate for exam started events

#### Enumerations
- `ExamMode.cs` - Exam state modes (e.g., Started, Ended, Reviewing)

**Responsibilities:**
- Define core business entities
- Implement business logic and rules
- Define contracts and interfaces
- Manage state and behavior

---

### 3. **Ems.Infrastructure (Infrastructure Layer)**
**Path**: `Ems.Infrastructure/Ems.Infrastructure.csproj`

Provides infrastructure services including data access, repositories, and UI helpers.

**Key Components:**

#### Repositories
- `IRepository.cs` - Generic repository interface defining CRUD operations
- `Repository.cs` - Generic repository implementation

#### Helpers
- `ConsoleHelper.cs` - Console UI utilities for menus and display formatting
- `DataSeeder.cs` - Sample data initialization for demonstration and testing

**Responsibilities:**
- Data access and persistence
- Cross-cutting concerns (helpers, utilities)
- External service integration
- Console display formatting

---

## ✨ Features

### Exam Management
- ✅ **Practice Exams** - Low-stakes practice with immediate feedback
- ✅ **Final Exams** - High-stakes exams with stricter rules
- ✅ **Multiple Question Types** - True/False, Single Choice, Multiple Choice
- ✅ **Timed Exams** - Built-in timer support for exam duration
- ✅ **Question Randomization** - Secure random question ordering

### Student Features
- ✅ **Event Notifications** - Students notified when exams start
- ✅ **Answer Tracking** - Complete answer history captured
- ✅ **Score Calculation** - Automatic grading and scoring
- ✅ **Review Mode** - Post-exam review (for practice exams)

### User Interface
- ✅ **Interactive Menus** - Console-based menu system
- ✅ **Easy Navigation** - Intuitive menu options
- ✅ **Clear Formatting** - Well-formatted console output
- ✅ **Real-time Feedback** - Immediate response to user actions

---

## 🛠️ Technology Stack

- **Language**: C# (.NET 10.0)
- **Framework**: .NET Console Application
- **Architecture Pattern**: Clean Architecture / Layered Architecture
- **Design Patterns**:
  - Observer Pattern (for exam events)
  - Repository Pattern (for data access)
  - Strategy Pattern (for different exam types)
  - Factory Pattern (for question creation)
  - Template Method Pattern (for exam flow)

---

## 🚀 Getting Started

### Prerequisites
- .NET 10.0 SDK or later
- Visual Studio 2026 or Visual Studio Code

### Installation

1. **Clone the repository**
   ```powershell
   git clone https://github.com/EmanHamam/Console_EMS_C-APP.git
   cd Console_EMS_C-APP
   ```

2. **Restore dependencies**
   ```powershell
   dotnet restore
   ```

3. **Build the solution**
   ```powershell
   dotnet build
   ```

4. **Run the application**
   ```powershell
   dotnet run --project EMS/EMS.csproj
   ```

---

## 📖 Usage

### Main Menu
Once the application starts, you'll see the main menu with the following options:

```
Main Menu
─────────
1. Practice Exam
2. Final Exam
3. Exit
```

### Taking an Exam

1. **Select Exam Type** - Choose Practice Exam or Final Exam
2. **Answer Questions** - Answer the displayed questions based on their type
3. **Submit Answers** - Confirm your answers
4. **View Results** - See your score and performance

### Question Types

#### True/False Question
- Select between True or False options
- Clear and straightforward format

#### Choose One Question
- Select exactly one correct answer from multiple options
- Only one answer is correct

#### Choose All Question
- Select all correct answers from multiple options
- Multiple answers may be correct

---

## 🏛️ Architecture

### Layered Architecture Design

```
┌─────────────────────────────────────┐
│   EMS (Presentation Layer)          │
│  - Console Application              │
│  - User Interaction                 │
└──────────────┬──────────────────────┘
			   │
	   ┌───────┴────────┐
	   │                │
	   ▼                ▼
┌──────────────┐  ┌────────────────────┐
│ EMS.Domain   │  │ Ems.Infrastructure │
│  (Business   │  │   (Data Access &   │
│    Logic)    │  │     Utilities)     │
└──────────────┘  └────────────────────┘
```

### Key Architectural Principles

1. **Separation of Concerns** - Each layer has a specific responsibility
2. **Dependency Injection** - Loose coupling between components
3. **SOLID Principles** - Applied throughout the codebase
4. **Event-Driven** - Observer pattern for exam notifications
5. **Generic Repository** - Flexible data access abstraction

### Data Flow

```
User Input (Console)
	↓
Program.cs (Orchestration)
	↓
Exam Models (Business Logic)
	↓
Question & Answer Models
	↓
Event System (Notifications)
	↓
Repository (Data Access)
	↓
Console Output (UI Display)
```

---

## 📊 Class Diagram

The project includes a detailed class diagram located at:
```
EMS\Class Diagram\ClassDiagram1.cd
```

This diagram illustrates the relationships between all domain models, interfaces, and dependencies.

---

## 🔑 Core Interfaces

### IExamBehavior
Defines the contract for exam behavior:
- Start exam
- End exam
- Get results
- Check completion

### IRepository
Generic repository interface for data operations:
- Create
- Read
- Update
- Delete
- GetAll

---

## 📝 Event System

The application uses the Observer pattern for event management:

- **ExamStartedHandler** - Delegate for exam started events
- **ExamEventArgs** - Custom event arguments carrying exam information
- **Students** - Observers that listen to exam events

When an exam starts, all subscribed students are notified via the `OnExamStarted` event handler.

---

## 🗂️ Project Dependencies

```
EMS (Console App)
├── → EMS.Domain
└── → Ems.Infrastructure
	  └── → EMS.Domain
```

---

## 📌 Future Enhancements

- [ ] Database persistence (SQL Server/SQLite)
- [ ] Web API layer for remote access
- [ ] Admin dashboard for exam management
- [ ] Student performance analytics
- [ ] Question bank management
- [ ] Automatic scoring and grading system
- [ ] Export exam results to PDF
- [ ] Multi-language support


---

## 🔗 Resources

- [.NET 10.0 Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
- [Design Patterns in C#](https://refactoring.guru/design-patterns/csharp)


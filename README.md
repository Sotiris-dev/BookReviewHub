
# BookReviewHub

**Αρχιτεκτονική**: Microsoft Clean Architecture (Domain, Application, Infrastructure, API, Identity, Tests)

---

## 📁 Δομή Projects

```
/BookReviewHub
│
├── BookReviewHub.Api              → ASP.NET Core 9 MVC + REST  
├── BookReviewHub.Application      → Use cases, services, interfaces, DTOs  
├── BookReviewHub.Domain           → Entities, Enums, Domain rules  
├── BookReviewHub.Infrastructure   → EF Core, DbContext, Repos & Services  
├── BookReviewHub.Identity         → (προαιρετική) custom IdentityUser  
└── BookReviewHub.Tests            → Unit tests (xUnit + InMemory EF)
```

---

## 🚀 Setup Projects via CLI

```bash
dotnet new webapi -n BookReviewHub.Api
dotnet new classlib -n BookReviewHub.Application
dotnet new classlib -n BookReviewHub.Domain
dotnet new classlib -n BookReviewHub.Infrastructure
dotnet new classlib -n BookReviewHub.Identity
dotnet new xunit -n BookReviewHub.Tests

dotnet sln add */*.csproj
```

Commit message:  
`chore: initial project structure`

---

## ✅ Προσθήκες & Dependencies

### 📦 Infrastructure

```bash
dotnet add BookReviewHub.Infrastructure package Microsoft.EntityFrameworkCore.Tools
dotnet add BookReviewHub.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
```

### 📦 Identity

```bash
dotnet add BookReviewHub.Identity package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

### 📦 API

```bash
dotnet add BookReviewHub.Api package Swashbuckle.AspNetCore
dotnet add BookReviewHub.Api package FluentValidation.AspNetCore
dotnet add BookReviewHub.Api package Serilog.AspNetCore
dotnet add BookReviewHub.Api package Serilog.Sinks.Console
dotnet add BookReviewHub.Api package Serilog.Sinks.File
```

### 📦 Tests

```bash
dotnet add BookReviewHub.Tests package xunit
dotnet add BookReviewHub.Tests package xunit.runner.visualstudio
dotnet add BookReviewHub.Tests package Microsoft.EntityFrameworkCore.InMemory
```

---

## 🧱 Domain Layer

Οντότητες:
- `User`, `Book`, `Review`, `ReviewVote`
- Domain validation (π.χ. rating 1–5) εντός constructors / methods

---

## 🎯 Application Layer

- Interfaces: `IBookService`, `IReviewService`, `IReviewVoteService`, `IUserService`  
- DTOs (immutable records):  
  `BookDto`, `CreateBookDto`, `ReviewDto`, `CreateReviewDto`, `ReviewVoteDto`, `UserDto`

---

## 🏗 Infrastructure Layer

- `ApplicationDbContext` (EF Core + Identity)  
- Υλοποιήσεις services (`BookService`, `ReviewService`, `ReviewVoteService`, κ.ά.)  
- Migrations & Database setup (SQL Server / InMemory)

---

## ✅ API Layer

- Controllers: `BooksController`, `ReviewsController`, `ReviewVotesController`, `AccountController`  
- DI setup:
  ```csharp
  builder.Services.AddIdentity<IdentityUser, IdentityRole>()
      .AddEntityFrameworkStores<ApplicationDbContext>()
      .AddDefaultTokenProviders();
  ```
- Dev password policy (για ανάπτυξη) στο `Program.cs`

---

## 🔄 Migrations & Database

- EF Core tools: `dotnet-ef`  
- Clean initial migration:
  - Διέγραψε παλιά migration & snapshot  
  - `dotnet ef migrations add InitialCreate`
  - `dotnet ef database update`
- Στη συνέχεια, Identity migrations:
  - `dotnet ef migrations add SyncApplicationModel`
  - `dotnet ef database update`

---

## 🛡 Tests (xUnit + InMemory DB)

- `TestDbContextFactory.Create()` δημιουργεί καθαρή in-memory DB ανά test

### 🔍 Test Cases

**ReviewServiceTests**  
- `AddReviewStoresReview()` → επιβεβαιώνει δημιουργία review  
- `AddReview_InvalidRating_Throws()` → ρίχνει exception για rating εκτός ορίων  

**ReviewVotesServiceTests**  
- `VoteAsync_CreatesVote()` → upvote δημιουργείται σωστά  
- `VoteAsync_SameUserTwice_UpdatesVote()` → αλλαγή vote χωρίς διπλή εγγραφή

---

## 🧪 Τι έχει δοκιμαστεί

- ✅ **POST /api/account/register** → εγγραφή χρήστη μέσω Identity
- ✅ **GET /api/books** → επιστροφή λίστας βιβλίων με filters
- ✅ **POST /api/books** → δημιουργία βιβλίου με valid δεδομένα
- ✅ **GET /api/reviews** → ανάκτηση όλων των reviews
- ✅ **POST /api/reviews** → δημιουργία review συνδεδεμένου με user & book
- ✅ **GET /api/reviewvotes** → επιστροφή όλων των ψήφων
- ✅ **POST /api/reviewvotes/reviews/{id}/vote** → ψήφος up/down με έλεγχο μοναδικότητας χρήστη ανά review

## 📦 Τι Έπεται

> ℹ️ Το έργο παραμένει επεκτάσιμο για μελλοντική υποστήριξη frontend (π.χ. Blazor/React) ή JWT authentication, αν ζητηθεί.

---

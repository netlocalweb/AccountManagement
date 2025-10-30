# Sistemi i Menaxhimit të Llogarive

Ky është një aplikacion Web API për menaxhimin e llogarive të klientëve, transaksioneve bankare dhe operacioneve financiare. Sistemi përfshin autentifikimin e përdoruesve, mbështetjen e shumë monedhave dhe ndjek parimet e arkitekturës së pastër.

## 📋 Përmbajtja

- [Përmbledhje e Projektit](#përmbledhje-e-projektit)
- [Arkitektura dhe Modelet e Dizajnit](#arkitektura-dhe-modelet-e-dizajnit)
- [Struktura e Projektit](#struktura-e-projektit)
- [Marrëdhëniet e Entiteteve](#marrëdhëniet-e-entiteteve)
- [Bibliotekat e Klasave](#bibliotekat-e-klasave)
- [Kontrollerët](#kontrollerët)
- [Repository-t](#repository-t)
- [Karakteristikat](#karakteristikat)
- [Teknologjitë](#teknologjitë)
- [Udhëzimet për Instalim](#udhëzimet-për-instalim)

## 🎯 Përmbledhje e Projektit

Sistemi ofron funksionalitetin bazë bankar duke përfshirë:
- Regjistrimin dhe autentifikimin e përdoruesve
- Menaxhimin e profilit të klientit
- Krijimin dhe menaxhimin e llogarive bankare
- Përpunimin e transaksioneve (depozita/tërheqje)
- Raportimin financiar
- Mbështetjen e shumë monedhave

Ndërtuar si Web API për të fokusuar në logjikën e biznesit dhe për të mundësuar testimin e lehtë përmes dokumentimit Swagger.

## 🏗️ Arkitektura dhe Modelet e Dizajnit

### Implementimi i Arkitekturës së Pastër

Projekti përdor arkitekturën e pastër me katër shtresa kryesore:
- **Kontrollerët (Prezantim)**: Endpoint-et e API-së dhe menaxhimi i kërkesave
- **Kontratat (Aplikacion)**: Përkufizimet e ndërfaqeve dhe kontratat e shërbimeve
- **Entitetet (Domain)**: Modelet e biznesit, DTO-të dhe logjika kryesore e domainit
- **Repository (Infrastrukturë)**: Aksesi në të dhëna dhe implementimi i Entity Framework

### Modelet e Dizajnit të Përdorura

**1. Repository Pattern**
- Ndan logjikën e aksesit në të dhëna nga logjika e biznesit
- Mundëson ndërrimin e lehtë të ofruesit të bazës së të dhënave
- Përmirëson testueshmërinë përmes abstraksionit të ndërfaqes

**2. Unit of Work Pattern**
- Koordinon operacionet në repository të shumta
- Siguron konsistencën e transaksioneve
- Pikë e vetme ruajtjeje për të gjitha ndryshimet në bazën e të dhënave

**3. Dependency Injection**
- Redukton lidhjen midis komponentëve
- Përmirëson testueshmërinë dhe fleksibilitetin
- Kontejner i integruar DI i ASP.NET Core

**4. DTO Pattern**
- Kontrollon ekspozimin e të dhënave përmes API-së
- Menaxhimi i versioneve për ndryshimet e API-së
- Ndarje midis modeleve të domainit dhe kontratave të API-së

**5. AutoMapper**
- Hartim automatik objekt-në-objekt
- Redukton kodin e përsëritur
- Konfigurimi i centralizuar i hartimit

### Përfitimet
- **Mirëmbajtja**: Ndarje e qartë e përgjegjësive
- **Testueshmëria**: Varësi të testueshmë dhe shtresa të izoluara
- **Shkallëzueshmëria**: Strukturë modulare për shtimin e lehtë të karakteristikave
- **Fleksibiliteti**: Dizajni bazuar në ndërfaqe lejon ndryshimet e implementimit

## 📁 Struktura e Projektit

```
AccountManagement/
├── AccountManagement.sln                 # Skedari i zgjidhjes
├── README.md                            # Dokumentacioni i projektit
│
├── AccountManagement/                   # Projekti kryesor Web API
│   ├── Controllers/                     # Kontrollerët e API-së
│   ├── Extensions/                      # Zgjerimet e konfigurimit të shërbimit
│   ├── Migrations/                      # Migracionet e Entity Framework
│   ├── wwwroot/                        # Skedarët statik
│   ├── Program.cs                      # Pika e hyrjes së aplikacionit
│   ├── MappingProfile.cs               # Konfigurimi i AutoMapper
│   ├── appsettings.json                # Konfigurimi i aplikacionit
│   └── nlog.config                     # Konfigurimi i regjistrimit
│
├── Contracts/                          # Përkufizimet e ndërfaqeve
├── Entities/                           # Modelet e domainit dhe DTO-të
├── LoggerService/                      # Implementimi i regjistrimit
└── Repository/                         # Shtresa e aksesit në të dhëna
```

## 🔗 Marrëdhëniet e Entiteteve

### Entitetet Kryesore dhe Marrëdhëniet e Tyre

```
User (Identity)
    ↓ (1:1)
Client
    ↓ (1:N)
BankAccount
    ↓ (1:N)
BankTransaction

Currency (1:N) → BankAccount
Category (1:N) → Products
```

### Detajet e Entiteteve

#### **User** (Identity Framework)
- **Qëllimi**: Autentifikimi dhe autorizimi
- **Veti Kryesore**: Email, Password, Roles
- **Marrëdhëniet**: Një-me-një me Client

#### **Client**
- **Qëllimi**: Menaxhimi i informacionit të klientit
- **Veti Kryesore**: FirstName, LastName, Email, Phone, Birthdate
- **Marrëdhëniet**: 
  - Një-me-një me User
  - Një-me-shumë me BankAccount

#### **BankAccount**
- **Qëllimi**: Menaxhimi i llogarisë financiare
- **Veti Kryesore**: Code, Name, Balance, IsActive
- **Marrëdhëniet**: 
  - Shumë-me-një me Client
  - Shumë-me-një me Currency
  - Një-me-shumë me BankTransaction

#### **BankTransaction**
- **Qëllimi**: Gjurmimi i transaksioneve financiare
- **Veti Kryesore**: Amount, Action (Debit/Credit), TransactionDate
- **Marrëdhëniet**: Shumë-me-një me BankAccount

#### **Currency**
- **Qëllimi**: Mbështetja e shumë monedhave
- **Veti Kryesore**: Code, Name, Symbol
- **Marrëdhëniet**: Një-me-shumë me BankAccount

#### **Category & Products**
- **Qëllimi**: Kategorizimi dhe menaxhimi i produkteve
- **Marrëdhëniet**: Category ka Një-me-shumë me Products

## 📚 Bibliotekat e Klasave

### 1. **Biblioteca Contracts**
**Qëllimi**: Përkufizimet e ndërfaqeve dhe kontratat e shërbimeve

**Ndërfaqet Kryesore**:
- `IRepositoryManager`: Koordinon të gjitha operacionet e repository
- `IAuthService`: Shërbimet e autentifikimit dhe autorizimit
- `IClientRepository`, `IBankAccountRepository`, `IBankTransactionRepository`: Kontratat e aksesit në të dhëna
- `ILoggerManager`: Abstraksioni i regjistrimit

**Përfitimet**:
- Mundëson parimin e inversimit të varësisë
- Përmirëson testueshmërinë përmes imitimit të ndërfaqes
- Mbështet lidhjen e lirë midis shtresave

### 2. **Biblioteca Entities**
**Qëllimi**: Modelet e domainit, DTO-të dhe strukturat kryesore të të dhënave

**Komponentët**:
- `Models/`: Entitetet kryesore të biznesit (Client, BankAccount, BankTransaction, etj.)
- `DTO/`: Objektet e Transferimit të të Dhënave për komunikimin e API-së
- `Enums/`: Enumerimet e sistemit (TransactionAction, etj.)
- `ErrorModel/`: Modelet e menaxhimit të gabimeve të personalizuara

**Përfitimet**:
- Vendndodhje qendrore për të gjitha strukturat e të dhënave
- E ndarë në projekte të shumta
- Modelim i pastër i domainit me veti navigimi

### 3. **Biblioteca LoggerService**
**Qëllimi**: Regjistrimi i centralizuar duke përdorur NLog

**Karakteristikat**:
- Regjistrimi i strukturuar me nivele të shumta
- Dalje në skedar dhe konsol
- I konfigurueshëm për mjedise të ndryshme
- Regjistrimi i kërkesës/përgjigjes

**Përfitimet**:
- I ripërdorshëm në projekte
- Depurim dhe monitorim i lehtë
- Konfigurimi i centralizuar i regjistrimit

### 4. **Biblioteca Repository**
**Qëllimi**: Implementimi i shtresës së aksesit në të dhëna

**Komponentët**:
- `RepositoryContext`: Entity Framework DbContext
- `RepositoryBase<T>`: Operacionet generike CRUD
- Repository specifike për pyetjet komplekse të biznesit
- `RepositoryManager`: Implementimi i Unit of Work

**Përfitimet**:
- Ndarje e shqetësimeve të aksesit në të dhëna
- Pavarësia e ofruesit të bazës së të dhënave
- Shtresë e dhënash e testueshmë me mbështetje imitimi

## 🎮 Kontrollerët

### **AuthenticationController**
**Qëllimi**: Regjistrimi dhe autentifikimi i përdoruesit
- `POST /api/authentication/register` - Regjistrimi i përdoruesit
- `POST /api/authentication/login` - Hyrja e përdoruesit me gjenerimin e token JWT
- Përdor ASP.NET Core Identity për menaxhimin e sigurt të fjalëkalimit

### **ClientController**
**Qëllimi**: Menaxhimi i profilit të klientit
- Operacionet e plota CRUD për informacionin e klientit
- Lidh përdoruesit me profilet e tyre të klientit
- Menaxhon marrëdhënien një-me-një User-Client

### **BankAccountController**
**Qëllimi**: Menaxhimi i llogarisë bankare
- Krijimi dhe përditësimi i llogarive
- Kërkesat për gjendje dhe detajet e llogarisë
- Mbështetja e shumë monedhave
- Autorizimi siguron që përdoruesit aksesojnë vetëm llogaritë e tyre

### **BankTransactionController**
**Qëllimi**: Përpunimi i transaksioneve
- Operacionet e depozitës dhe tërheqjes
- Marrja e historikut të transaksioneve
- Operacione atomike për konsistencën e të dhënave
- Përdor precizionin decimal për llogaritjet financiare

### **CurrencyController**
**Qëllimi**: Menaxhimi i monedhave
- Operacionet CRUD për monedhat e mbështetura
- Validimi dhe konfigurimi i monedhave
- Mbështet operacionet bankare me shumë monedha

### **ReportsController**
**Qëllimi**: Raportimi dhe analitika financiare
- Përmbledhjet e llogarive dhe raportet e gjendjes
- Analiza e historikut të transaksioneve
- Përdor Dapper për pyetjet komplekse të optimizuara

### **CategoryController & ProductsController**
**Qëllimi**: Menaxhimi i katalogut të produkteve
- Menaxhimi i hierarkisë së kategorive
- Operacionet CRUD të produkteve
- Demonstron marrëdhëniet shumë-me-shumë

## 🗃️ Implementimi i Repository

### **Modeli Generic Repository**

#### **RepositoryBase<T>**
**Qëllimi**: Ofron operacionet bazë CRUD për të gjitha llojet e entiteteve
- `GetAllAsync()` - Merr të gjitha regjistrimet
- `GetByIdAsync(id)` - Gjen sipas çelësit primar
- `CreateAsync(entity)` - Shton regjistrim të ri
- `UpdateAsync(entity)` - Modifikon regjistrim ekzistues
- `DeleteAsync(entity)` - Heq regjistrim
- `FindByConditionAsync(expression)` - Pyetje të personalizuara LINQ

**Përfitimet**: Ripërdorimi i kodit, ndërfaqe konsistente, operacione asinkrone

### **Repository Specifike**

#### **ClientRepository**
**Qëllimi**: Operacionet specifike të të dhënave të klientit
- `GetClientByUserIdAsync()` - Lidh përdoruesit me profilet e klientit
- `GetClientsWithAccountsAsync()` - Përfshin të dhënat e llogarive të lidhura

#### **BankAccountRepository**
**Qëllimi**: Operacionet e të dhënave të llogarisë
- `GetAccountsByClientAsync()` - Llogaritë e klientit
- `GetAccountWithTransactionsAsync()` - Llogaria me historikun e transaksioneve
- `UpdateBalanceAsync()` - Përditësimet e sigurta të gjendjes me validim

#### **BankTransactionRepository**
**Qëllimi**: Operacionet e të dhënave të transaksionit
- `GetTransactionsByAccountAsync()` - Historiku i transaksioneve të llogarisë
- `GetTransactionHistoryAsync()` - Rezultate të faqëzuara
- `CreateTransactionWithBalanceUpdateAsync()` - Krijimi atomik i transaksionit

#### **CurrencyRepository**
**Qëllimi**: Operacionet e të dhënave të monedhave
- `GetActiveCurrenciesAsync()` - Monedhat e disponueshme
- `GetCurrencyByCodeAsync()` - Gjen sipas kodit ISO

#### **ReportsRepository (Dapper)**
**Qëllimi**: Pyetjet komplekse të raportimit
- Përdor Dapper për performancë të optimizuar SQL
- Bashkime dhe agregime komplekse
- Performancë më e mirë për operacionet vetëm leximi të raportimit

### **RepositoryManager (Unit of Work)**
**Qëllimi**: Koordinon operacionet në repository

**Karakteristikat**:
- Inicializimi i dembel i repository-ve
- Metoda e vetme `SaveAsync()` për të gjitha ndryshimet
- Koordinimi i transaksioneve
- Siguron konsistencën e të dhënave

**Modeli i Përdorimit**:
```csharp
var account = await _repositoryManager.BankAccount.GetByIdAsync(accountId);
var transaction = new BankTransaction { ... };

await _repositoryManager.BankTransaction.CreateAsync(transaction);
await _repositoryManager.BankAccount.UpdateAsync(account);
await _repositoryManager.SaveAsync(); // Operacion atomik
```

## ✨ Karakteristikat Kryesore

### **Autentifikimi JWT**
- Sistem autentifikimi pa gjendje
- Siguria bazuar në token pa sesione server-side
- Autorizimi bazuar në pretendime
- Menaxhimi i sigurt i fjalëkalimit me ASP.NET Core Identity

### **Mbështetja e Shumë Monedhave**
- Llogaritë bankare mbështesin monedha të ndryshme (USD, EUR, GBP, etj.)
- Validimi dhe menaxhimi i monedhave
- Precizioni decimal për llogaritjet financiare
- Konfigurimi i zgjerueshmë i monedhave

### **Menaxhimi i Transaksioneve**
- Operacionet Debit/Credit
- Përpunimi atomik i transaksioneve
- Validimi dhe përditësimi i gjendjes
- Gjurmimi i historikut të transaksioneve
- Konsistenca e të dhënave përmes modelit Unit of Work

### **Regjistrimi Gjithëpërfshirës**
- Integrimi i NLog me regjistrimin e strukturuar
- Nivele të shumta regjistrimi (Debug, Info, Warning, Error)
- Dalje në skedar dhe konsol
- Konfigurimi specifik për mjedis

### **Dokumentacioni i API-së**
- Integrimi Swagger/OpenAPI
- Ndërfaqe testimi interaktiv API
- Gjenerimi automatik i dokumentacionit
- Mbështetja e testimit të autentifikimit

### **Hartimi i Objekteve**
- AutoMapper për konvertimet DTO
- Elimon hartimin manual të vetive
- Konfigurimi i centralizuar i hartimit
- Redukton kodin e përsëritur

### **Menaxhimi i Bazës së të Dhënave**
- Entity Framework Core me qasjen Code-First
- Migracionet e bazës së të dhënave për evolucionin e skemës
- Mundësitë e kthimit pas
- Kontrolli i versionit për strukturën e bazës së të dhënave

### **Menaxhimi i Gabimeve**
- Middleware e menaxhimit global të përjashtimeve
- Format konsistent i përgjigjes së gabimit
- Regjistrimi i duhur i gabimeve
- Mesazhet e gabimeve të fokusuara në siguri

### **Validimi i Input-it**
- Anotacione të dhënash për validimin deklarativ
- Validimi server-side për siguri
- Atribute validimi të personalizuara
- Zbatimi i rregullave të biznesit

## 🛠️ Teknologjitë

- **Framework**: ASP.NET Core 6.0+
- **Baza e të Dhënave**: SQL Server me Entity Framework Core
- **Autentifikimi**: ASP.NET Core Identity + JWT
- **ORM**: Entity Framework Core + Dapper (për raporte)
- **Regjistrimi**: NLog
- **Hartimi**: AutoMapper
- **Dokumentacioni**: Swagger/OpenAPI
- **Arkitektura**: Arkitektura e Pastër me Repository Pattern

## 🚀 Udhëzimet për Instalim

### Parakushtet
- .NET 6.0 SDK ose më i ri
- SQL Server (LocalDB e mbështetur)
- Visual Studio 2022 ose VS Code

### Instalimi

1. **Klono Repository-n**
   ```bash
   git clone [repository-url]
   cd AccountManagement
   ```

2. **Konfiguro Bazën e të Dhënave**
   - Përditëso connection string në `AccountManagement/appsettings.json`
   - Shembull LocalDB:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AccountManagementDB;Trusted_Connection=true"
   }
   ```

3. **Instalo Varësitë**
   ```bash
   dotnet restore
   ```

4. **Krijo Bazën e të Dhënave**
   ```bash
   dotnet ef database update --project AccountManagement
   ```

5. **Ekzekuto Aplikacionin**
   ```bash
   dotnet run --project AccountManagement
   ```

6. **Aksesu API-në**
   - API: `https://localhost:7xxx`
   - Swagger UI: `https://localhost:7xxx/swagger`

### Konfigurimi
- **Cilësimet JWT**: Konfiguro skadimin e token-it dhe çelësin sekret në `appsettings.json`
- **Regjistrimi**: Konfigurimi i NLog në `nlog.config`
- **CORS**: Modifiko politikën CORS në `ServiceExtensions.cs`

### Testimi i API-së
Përdor Swagger UI për të:
1. Regjistruar përdorues të ri
2. Hyrë për të marrë token JWT
3. Krijuar profil klienti
4. Menaxhuar llogaritë bankare
5. Përpunuar transaksionet
6. Gjeneruar raporte

## 📝 Dokumentacioni i API-së

Pasi të jetë duke ekzekutuar aplikacioni, vizitoni `/swagger` për dokumentacion interaktiv API me të gjitha endpoint-et e disponueshme, modelet e kërkesës/përgjigjes dhe kërkesat e autentifikimit.

## 🔒 Karakteristikat e Sigurisë

- Autentifikimi bazuar në token JWT
- Autorizimi bazuar në role
- Hash-imi i fjalëkalimit me framework Identity
- Validimi dhe pastrimi i input-it
- Konfigurimi i politikës CORS
- Endpoint-e të sigurta API

## 📈 Rezultatet e Projektit

### Arritjet Teknike
- Implementoi arkitekturën e pastër me ndarje të duhur të shqetësimeve
- Aplikoi modele të shumta dizajni (Repository, Unit of Work, Dependency Injection)
- Arriti autentifikimin e sigurt me token JWT
- Integroi Entity Framework me migracionet e bazës së të dhënave
- Implementoi regjistrimin gjithëpërfshirës dhe menaxhimin e gabimeve

### Zonat Kryesore të të Mësuarit
- Parimet dhe përfitimet e arkitekturës së pastër
- Modeli Repository për abstraksionin e aksesit në të dhëna
- Autentifikimi JWT dhe dizajni i API-së pa gjendje
- Qasja Code-First e Entity Framework
- AutoMapper për efikasitetin e hartimit të objekteve

### Përmirësimet e Ardhshme
- Implementimi i testimit të njësisë dhe integrimit
- Optimizimi i performancës dhe cache-imi
- Kontejnerizimi Docker
- Konfigurimi i pipeline CI/CD
- Versionimi i API-së dhe kufizimi i normës
- Karakteristikat e përmirësuara të sigurisë

---

*Projekt i zhvilluar si pjesë e programit të praktikës duke demonstruar praktikat e zhvillimit .NET të nivelit të ndërmarrjes.*

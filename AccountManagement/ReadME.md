dotnet ef migrations add AddClientTable --startup-project AccountManagement
dotnet ef database update --startup-project AccountManagement 
dotnet ef migrations add AddCurrencyTable --startup-project AccountManagement
dotnet ef migrations remove --startup-project AccountManagement

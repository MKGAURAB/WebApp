# WebApp
This is a very basic ASP.NET Core 2.1Web Application (MVC) project (using .Net Core 2.1, Microsoft.EntityFrameworkCore, NSwag).
The App has these following functionalities:
* It maintains a Controller named HomeController, which process CSV/XML file and store transaction data into database.
* It has an another Controller named TransactionController, which is a ApiController can get transactions from database according to following criteria:
	1. Using currency code
	2. Using date range
	3. Using status
# Project Setup
To run the App, you have to do following things first:
* Execute the sql script named as `CreateTransactionDb.sql` in the Solution.
* Change the DbConnection string from appsettings.json as per your DB server configuration.

Accessing the UI and the WebApi's
* https://localhost:5001/swagger for WebApi
* https://localhost:5001 for the File Upload UI
# PhotoProjectAPI-master

How to run a project

Step1. Please create a database in MsSQL.

Step2. Please open Visual Studio and go to "appsettings.json" file.

Step3. "DefaultConnectionString": "Data Source=SERVERNAME;Initial Catalog=DATABASENAME ;Integrated Security=True;Pooling=False"

 In line "DefaultConnectionString" please replace SERVERNAME with your SQL Server name and DATABASENAME with the database name that you created
 
Step4. Please open "NuGet Package Manager Console" and and execute the "update-database" command. (If for some reason it doesn't work please execute the "add-migration migration_name" command first, then "update-database" command)

Step5. Please run a project

Login credentials
Standard account:
Username: user 
Password: Haslo56!
Administrator account:
Username: admin
Password: Haslo56!
(If for some reason those don't work, please create a new user in "POST/api/Authentication/Registration" section)


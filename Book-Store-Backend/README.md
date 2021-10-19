# Book Store Backend

## Setup

- Update the database

default db

Database name: BookStore
Microsoft SQL Server localhost

create database manually in microsoft SQL

```powershell

Enable-Migrations
Update-Database

```

## Notes

- Any changes to database should be followed by

```powershell
Add-Migrations <migration name>
```

- Default users

|username|password|
|--------|--------|
|Admin   |secret  |

- Routes

	- Authentication
	
		- Testing route
			GET api/values/1
		- Registration
			POST api/Account/Register (FORM URL ENCODED)
			{Email: "",
			Password: "",
			ConfirmPassword: "",}
		- Login
			POST api/Token (FORM URL ENCODED)
			{
				grant_type:"password",
				username: "",
				password: "",
			}

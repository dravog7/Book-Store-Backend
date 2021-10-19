# BookStore Solution

## Setup

- clone the repository

- open .sln file in visual studio

- Tools > NuGet package manager > Manage NuGet packages for solution
	after opening this, you will see a restore button on a pop up top of window, click to restore

- Tools > NuGet package manager > Package manager console
	
	Incase of rosllyn compiler error, run the following
	```powershell
	Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
	```

- Further project specific instructions in BookStore-Backend folder
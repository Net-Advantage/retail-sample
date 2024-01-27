<a href="https://net-advantage.github.io/retail-sample/coverage/" target="_blank"><img src="https://img.shields.io/badge/Code-Coverage-brightgreen" alt="Coverage"></a>

# Nabs Retail Sample

This is the Nabs Retail Sample that uses Nabs.ActivityFramework

Add the NUGET_AUTH_TOKEN variable to your environment variables with the value of your PAT token.

Run this command in powershell:
    
```powershell
# add NUGET_AUTH_TOKEN to environment variables
[System.Environment]::SetEnvironmentVariable('NUGET_AUTH_TOKEN', 'your_token_here', [System.EnvironmentVariableTarget]::User)
```

To check that it has been added correctly, run this command in powershell:

```powershell
# check that NUGET_AUTH_TOKEN has been added to environment variables
Get-Item env:NUGET_AUTH_TOKEN
```
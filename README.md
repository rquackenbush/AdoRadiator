# ADO Radiator

Simple application that let's you know how far through an iteration the team is.

```powershell
 [System.Environment]::SetEnvironmentVariable('ADO_ORGANIZATION', '<YOUR ORG>', 'User')
 [System.Environment]::SetEnvironmentVariable('ADO_PROJECT', '<YOUR PROJECT>', 'User')
 [System.Environment]::SetEnvironmentVariable('ADO_TEAM', '<YOUR TEAM>', 'User')
 [System.Environment]::SetEnvironmentVariable('ADO_PERSONAL_ACCESS_TOKEN', '<A PAT WITH READ ACCESS TO WORK ITEMS>', 'User')
 [System.Environment]::SetEnvironmentVariable('TEAMS_WEBHOOK_URL', '<YOUR TEAMS WEBHOOK>', 'User')
 ```

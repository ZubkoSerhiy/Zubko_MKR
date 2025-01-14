Set-ExecutionPolicy Unrestricted -Scope Process -Force

if (-not (Get-Command choco -ErrorAction SilentlyContinue)) {
    Set-ExecutionPolicy Bypass -Scope Process -Force; 
    [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12; 
    iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
}

choco install git -y
choco install dotnetcore-sdk -y

dotnet nuget add source --name "S_Zubko" --source "http://localhost:5000/v3/index.json"
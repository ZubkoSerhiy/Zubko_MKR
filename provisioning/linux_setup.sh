#!/bin/bash

sudo apt-get update
sudo apt-get upgrade -y

wget https://packages.microsoft.com/keys/microsoft.asc
sudo apt-key add microsoft.asc

sudo add-apt-repository https://packages.microsoft.com/ubuntu/20.04/prod
sudo apt-get update

sudo apt-get install -y git
sudo apt-get install -y dotnet-sdk-6.0

dotnet nuget add source --name "S_Zubko" --source "http://localhost:5000/v3/index.json"
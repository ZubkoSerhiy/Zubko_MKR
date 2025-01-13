#!/bin/bash

/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

brew update
brew install git
brew install --cask dotnet-sdk

dotnet nuget add source --name "S_Zubko" --source "http://localhost:5000/v3/index.json"
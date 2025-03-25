Agent Pool:
    Organization Settings > Agent pools
    

    Windows:
        Add pool > Self hosted > windows
        New Agent
        Windows
        Download

        PS C:\> mkdir c:\agent ; cd c:\agent
        PS C:\agent> Add-Type -AssemblyName System.IO.Compression.FileSystem ; [System.IO.Compression.ZipFile]::ExtractToDirectory("$HOME\Downloads\vsts-agent-win-x64-3.246.0.zip", "$PWD")
        PS C:\agent> .\config.cmd
        PS C:\agent> .\run.cmd


    Linux:
        Add pool > Self hosted > linux

        PS C:\> mkdir agent ; cd agent
        PS C:\agent> Add-Type -AssemblyName System.IO.Compression.FileSystem ; [System.IO.Compression.ZipFile]::ExtractToDirectory("$HOME\Downloads\vsts-agent-win-x64-3.246.0.zip", "$PWD")
        PS C:\agent> .\config.cmd
        PS C:\agent> .\run.cmd

        -------

        wsl --install

        wsl --set-default-version 2

        wsl --list --verbose

        Linux Terminal:
            sudo apt update
            sudo apt upgrade -y

            sudo apt install -y curl jq unzip

            mkdir myagent && cd myagent
            curl -O https://vstsagentpackage.azureedge.net/agent/3.246.0/vsts-agent-linux-x64-3.246.0.tar.gz
            tar zxvf vsts-agent-linux-x64-3.246.0.tar.gz


            sudo apt-get update && sudo apt-get install -y apt-transport-https ca-certificates curl software-properties-common && curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add - && sudo add-apt-repository "deb [arch=amd64] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" && sudo apt-get update && sudo apt-get install -y docker-ce && sudo service docker start && docker --version && sudo usermod -aG docker $USER && newgrp docker


            ./config.sh

            ./run.sh

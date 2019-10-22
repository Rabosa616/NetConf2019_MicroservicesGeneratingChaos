# write-host "cleaning images and containers"
# Use latest for Master branch 
# Use latest for your own build
param([string]$CloudServerRepository = '',[string]$user = '',[string]$password = '')

write-output $CloudServerRepository
write-output $user
write-output $password

$errorBackup = $ErrorActionPreference
$prefBackup = $WarningPreference

$WarningPreference = 'SilentlyContinue'
$ErrorActionPreference = 'SilentlyContinue'

Write-Host "---------------Cleaning Images and containers ---------------"
docker rm -f $(docker ps -qa)
docker rmi -f $(docker images --filter=reference='microservices.generatingchaos*' -qa)
docker system prune -f
docker image prune -f 
 
Write-Host " --------------- Connecting to cloud ---------------"
docker login --username=$user --password=$password $CloudServerRepository

Write-Host " ---------------Build images ---------------"
docker build -t microservices.generatingchaos.services.api -f src/1.Services/Microservices.GeneratingChaos.Services.Api/Dockerfile .
docker build -t microservices.generatingchaos.services.weather -f src/1.Services/Microservices.GeneratingChaos.Services.Weather/Dockerfile .
docker build -t microservices.generatingchaos.ui -f src/2.UI/Microservices.GeneratingChaos.UI/Dockerfile .

Write-Host " ---------------Tag images ---------------"

docker tag  microservices.generatingchaos.services.api $CloudServerRepository/microservices.generatingchaos.services.api
docker tag  microservices.generatingchaos.services.weather $CloudServerRepository/microservices.generatingchaos.services.weather
docker tag  microservices.generatingchaos.ui $CloudServerRepository/microservices.generatingchaos.ui

Write-Host " --------------- Push images ---------------"

docker push $CloudServerRepository/microservices.generatingchaos.services.api
docker push $CloudServerRepository/microservices.generatingchaos.services.weather
docker push $CloudServerRepository/microservices.generatingchaos.ui

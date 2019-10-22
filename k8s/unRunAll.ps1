Write-Host "Uninstall cim namespace"
kubectl delete -f NameSpace.yaml

Write-Host "Uninstall secrets"
kubectl delete -f docker-secret.yaml

Write-Host "Uninstall cluster role binding"
kubectl delete -f ClusterRoleBinding.yaml

Write-Host "Redis"
kubectl delete -f Redis.yaml
kubectl delete -f RedisService.yaml

#Write-Host "Uninstall Mongo"
#kubectl delete -f Mongo.yaml

Write-Host "Uninstall API"
kubectl delete -f .\ApiService.yaml
kubectl delete -f .\WeatherService.yaml

Write-Host "Uninstall Kube-monkey"
kubectl delete -f .\kube-monkey\cluster-role-binding.yaml
kubectl delete -f .\kube-monkey\configmap.yaml
kubectl delete -f .\kube-monkey\deployment.yaml
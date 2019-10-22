Write-Host "Install cim namespace"
kubectl create -f NameSpace.yaml

Write-Host "Install secrets"
kubectl create -f docker-secret.yaml

Write-Host "Install cluster role binding"
kubectl create -f ClusterRoleBinding.yaml

Write-Host "Install Redis"
linkerd inject RedisApi.yaml | kubectl apply -f -
linkerd inject RedisService.yaml | kubectl apply -f -

#Write-Host "Install Mongo"
#kubectl create -f Mongo.yaml | kubectl apply -f -

Write-Host "Install API"
linkerd inject .\ApiService.yaml | kubectl apply -f -
linkerd inject .\WeatherService.yaml | kubectl apply -f -

# Write-Host "Heapster"
# kubectl create -f .\heapster\heapster.yaml   

# Write-Host "Kube-monkey"
# kubectl create -f .\kube-monkey\cluster-role-binding.yaml
# kubectl create -f .\kube-monkey\configmap.yaml
# kubectl create -f .\kube-monkey\deployment.yaml

# Write-Host "Install InfluxDB"
# kubectl create configmap influxdb-conf --from-file=influxdb/influxdb.conf -n=cim
# kubectl create -f influxdb/influxdb-service.yaml
# kubectl create -f influxdb/influxdb-deployment.yaml

# Docker Gatling image for CIM

> Gatling Docker image for easy initialization of gatling.

This image needs to be build, it includes all necessary stuff to make Gatling works for the load tests and kubernetes. This image should be on the repo cloud available to be used with the k8s receipts.



## Generate image and push it to the cloud

In order to push the image to the cloud do this steps (you must be logged in into the docker cloud repository):

```
docker build -t gatling:latest .

docker tag gatling:latest cimdockercontainers.azurecr.io/cim.services.gatling:latest

docker push cimdockercontainers.azurecr.io/cim.services.gatling:latest
```

## Gatling conf		

Into the folder conf/ there is a file gatling.conf it includes all the configuration available for the running instance. 

We just modify it to write over the influx db. All others are commented. Feel free to uncomment some of them if necessary.

## k8s receipt

There are already prepared 2 receipt for k8s to deploy Gatling on cim namespace. every one of them runs a different simulation.

The simulations should be stored on the folder simulationscripts/simulations to le them be copied into the image.

Use the following files to create them.

[gatling-job-ApiBehavior1.yaml]: gatling-job-ApiBehavior1.yaml	"gatling-job-ApiBehavior1.yaml"
[gatling-job-ApiBehavior1_PoC]: gatling-job-ApiBehavior1_PoC	"gatling-job-ApiBehavior1_PoC"

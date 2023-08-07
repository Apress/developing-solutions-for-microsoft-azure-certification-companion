# Containerized Web Applications / Azure Container Instances and Azure Container Registry

This document outlines how to work with the code.  The main choice you want to make is do you want to do the .NET 6 version, the .NET 7 version, or both (easily done with Docker)!.  

Which brings up a good point -> You can't do this without Docker.  So, you'll likely want to get WSL2 on your Windows Machine, and ensure you get a Linux distribution (such as Ubuntu).  Once you have that, install Docker Desktop or a similar tool like Rancher.io to work with containers easily from WSL on a Windows w/Linux distribution.  Make sure you use Linux containers unless you specifically need Windows containers.

>**NOTE:** It is assumed if you are going to run past this point you have Docker running and are able to create images.  If Docker is not working, then you can't do the rest of this activity from your local machine.

## Create an Azure Container Registry (ACR)

1. Create the registry
1. Follow instructions in the book to ensure you can log in to the ACR

## Open the project of your choice (you can do both if you want)

Either utilize DN6 or DN7, or both in separate images and container instances, your choice.

1. Build the image using the provided Docker file and the build command

```bash
docker build -t simplednxweb .
```  

1. Run the image locally and make sure it works

```bash
docker run -dp 8080:80 simplednxweb
```  

1. Browse to the running container

```https
http://localhost:8080/
```  

[http://localhost:8080/](http://localhost:8080)   

## Push the image to the ACR

1. Log in to Azure

```bash
az login
```  

1. Log in to your ACR (remember to enable admin, note that the password won't ever show in the terminal)

```bash
docker login yourregistry.azurecr.io
```

1. Tag the image for your ACR

```bash
docker tag simplednxweb yourregistry.azurecr.io/simplednxweb
docker tag simplednxweb yourregistry.azurecr.io/simplednxweb:v1
```  

1. Push the image

```bash
docker push yourregistry.azurecr.io/simplednxweb
docker push yourregistry.azurecr.io/simplednxweb:v1
```  
## Publish to Azure Container Instances

To complete the exercise, leverage the image from your ACR and publish to an Azure ACI instance

1. Deploy using the portal, select the container registry and image

    Remember: If you want to use environment variables, you must set them during deployment as the container will be immutable. 

    >**Note:** Don't forget to set your Fully-Qualified-Domain-Name on deployment if you want to browse to the container instance without using the Public IP.

1. Deploy using the Azure CLI

    The commands to do this are in the book, so make sure to reference them there.  The thing that is most important is setting the environment variables.  

    ```bash
    az container create --resource-group $rg --name $name --image $img --dns-name-label $dns --ports 80 --environment-variables 'SimpleWebShared__MySimpleValue'='CLI: Shared Value' 'SimpleWebShared__MySecretValue'='CLI:Secret Value'  
    ```  

## Deploy to an Azure App Service

Choose the Linux free plan, then deploy your container

1. Utilize the ACR image and publish during deployment of the Web Application.

## Conclusion

Use this project to help you learn how to interact with the Azure Blob Storage

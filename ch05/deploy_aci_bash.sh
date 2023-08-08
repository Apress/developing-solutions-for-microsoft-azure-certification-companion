#rg:
rg=az204-exam-ref-containers-ecosystem
#aci name:
name=mysimpleweb-clideploy
#image to deploy:
img=az204examref20251231.azurecr.io/my-simplewebsite:v1
#public DNS
dns=mysimplewebCLIDeploy
#create the ACI
az container create --resource-group $rg --name $name --image $img --dns-name-label $dns --ports 80 --environment-variables 'SimpleWebShared__MySimpleValue'='CLI: Shared Value' 'SimpleWebShared__MySecretValue'='CLI:Secret Value'

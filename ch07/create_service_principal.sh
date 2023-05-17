rg=az204-exam-ref-authnauthz
subId=$(az account show --query 'id' --output tsv)
appName=deployment-sp-20251231

az ad sp create-for-rbac \
--name $appName --role contributor 
--scopes /subscriptions/$subId/resourceGroups/$rg

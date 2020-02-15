[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [String] $ACR_NAME,

    [Parameter(Mandatory = $true)]
    [String] $AKS_NAME,

    [Parameter(Mandatory = $true)]
    [String] $IMAGE_NAME = "AsrAks",

    [Parameter(Mandatory = $false)]
    [String] $IMAGE_VERSION = "latest",

    [String] $ACR_RESOURCE_GROUP,
    [String] $AKS_RESOURCE_GROUP
)

$IMAGE_FULLTAG = "$($IMAGE_NAME):$($IMAGE_VERSION)"

## Get the service principal ID of your AKS cluster
# $AKS_SP_ID = az aks show --resource-group $AKS_RESOURCE_GROUP --name $AKS_NAME --query "servicePrincipalProfile.clientId" -o tsv

## Get the resource ID of your ACR instance
# $ACR_RESOURCE_ID = az acr show --name $ACR_NAME --resource-group $ACR_RESOURCE_GROUP --query "id" -o tsv

## Create a role assignment for your AKS cluster to access the ACR instance
# az role assignment create --assignee $AKS_SP_ID --scope $ACR_RESOURCE_ID --role contributor

# Build the image using ACR, and push it at the same time
az acr build --registry $ACR_NAME --image $IMAGE_FULLTAG .

kubectl config use-context $AKS_NAME
kubectl apply -f ./kube/AsrAks.yaml

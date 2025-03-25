# Create an AppService Plan:
az appservice plan create --name demo-service-plan --resource-group demo-resource-group --sku B1 --is-linux

# List all existing AppService Plans:
az appservice plan list --resource-group demo-resource-group

# Create an AppService (WebApp):
az webapp create --resource-group demo-resource-group --plan demo-service-plan --name harshademowebapiapp --runtime "DOTNETCORE:8.0"

# Get list of supported runtimes for AppService:
az webapp list-runtimes --os-type linux

# Get list of all existing AppServices:
az webapp list --resource-group demo-resource-group

# Login to the container registry (in order to pull existing container image):
az acr login --name harshacontainerregistry --resource-group demo-resource-group

# ---------

# Set the container image for the AppService:
az webapp config container set --name harshademowebapiapp --resource-group demo-resource-group --container-image-name harshacontainerregistry.azurecr.io/demowebapp:latest

# [Optional] - Set the environment variables:
az webapp config appsettings set --resource-group demo-resource-group --name harshademowebapiapp --settings ASPNETCORE_ENVIRONMENT=Development

# Now, in Azure Portal:
# [Select "Managed Identity" in AppService - Deployment Center - Authentication in Portal]

# Restart the AppService [to take affect of the updated container]:
az webapp restart --name harshademowebapiapp --resource-group demo-resource-group


# ----------

# ** Updating source code **:

# Login to the container registry (in order to pull existing container image):
az acr login --name harshacontainerregistry --resource-group demo-resource-group

# After updating your source code, reset the container image for the AppService:
az webapp config container set --name harshademowebapiapp --resource-group demo-resource-group --container-image-name harshacontainerregistry.azurecr.io/demowebapp:latest

# Restart the AppService [to take affect of the updated container]:
az webapp restart --name harshademowebapiapp --resource-group demo-resource-group

# ---------

# ** Downloading logs **:

# If something goes wrong in deployment, download & check logs of the AppService:
az webapp log download --name harshademowebapiapp --resource-group demo-resource-group --log-file logs.zip


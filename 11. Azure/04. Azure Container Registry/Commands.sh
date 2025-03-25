# Create azure container registry:
az acr create --resource-group demo-resource-group --name harshacontainerregistry --sku Basic

# List existing container registries:
az acr list --output table

# Login to the newly created container registry:
az acr login --name harshacontainerregistry

# Build the local docker image:
docker build -t demowebapp:latest -f ./DemoWebApplication/Dockerfile .

# Tag the docker image:
docker tag demowebapp:latest harshacontainerregistry.azurecr.io/demowebapp:latest

# Push docker image to Azure Container Registry:
docker push harshacontainerregistry.azurecr.io/demowebapp:latest

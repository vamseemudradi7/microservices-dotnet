# Register OperationalInsights provider to your Azure subscription:
az provider register -n Microsoft.OperationalInsights --wait

# Create azure container app environment:
az containerapp env create --name demo-container-appl-env --resource-group demo-resource-group --location "East US"

# Create azure container app:
az containerapp create --name harsha-demo-container-app --resource-group demo-resource-group --environment demo-container-appl-env --image harshacontainerregistry.azurecr.io/demowebapp:latest --registry-server harshacontainerregistry.azurecr.io --cpu 0.5 --memory 1.0Gi --min-replicas 1 --max-replicas 1 --ingress 'external' --target-port 8080 --registry-identity system


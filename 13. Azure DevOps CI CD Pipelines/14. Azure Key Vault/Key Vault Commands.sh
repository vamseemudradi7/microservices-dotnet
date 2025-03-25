# Create the Key Vault
    az keyvault create --name products-pipeline-kv --resource-group ecommerce-resource-group --location "South India"

# ---------------------

# Optional:
# To know ObjectID of the user, Portal > Microsoft Entra ID > Users > Your username > Object Id

# ---------------------


# Set KeyVault admin access to the current user
# Portal > Key Vaults > Select the keyvault > Access control (IAM) > Add role assignment > Key Vault Administrator > User, group or service principal > Harsha Vardhan

# Add secrets to the Key Vault
    az keyvault secret set --vault-name products-pipeline-kv --name imageRepository --value "products-microservice"


# Verify the secrets:
    az keyvault secret list --vault-name products-pipeline-kv

# Get specific secret value:
    az keyvault secret show --vault-name products-pipeline-kv --name imageRepository


-----------

# DevOps > Service Connections > New Service Connection > Azure Resource Manager > Service Principal (Automatic) > Resource Group: ecommerce-resource-group > Name: ecommerce-resource-group-connection > Grant access permission to all pipelines.

# To know the service principal id of the service connection, click on the service connection > Manage Service Principal > Note the service principal id in the title

# Add the Resource Manager service connection to "Key Vault Reader" role.
# Portal > Key Vaults > Select the keyvault > Access control (IAM) > Add role assignment > Key Vault User > User, group or service principal > service principal id of the service connection [Select all]

# Login to Azure Subscription
    az login --tenant c1483e56-375f-447a-a1f6-72e8a652a43d

# Create Service Bus Namespace
    az servicebus namespace create --resource-group ecommerce-resource-group --name ecommerce-servicebus-namespace --location "South India" --sku Standard

# Create Service Bus Topic
    az servicebus topic create --resource-group ecommerce-resource-group --namespace-name ecommerce-servicebus-namespace --name products.updates --max-size 1024 --default-message-time-to-live 14dT00h00m00s
    # --max-size: 1 MB to 5 GB
    # --time-to-live: ##dT##h##m##s

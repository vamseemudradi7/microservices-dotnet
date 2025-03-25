# Login to Azure Subscription
    az login --tenant c1483e56-375f-447a-a1f6-72e8a652a43d

# Create Service Bus Topic
    az servicebus topic create --resource-group ecommerce-resource-group --namespace-name ecommerce-servicebus-namespace --name products.deletions --max-size 1024 --default-message-time-to-live 14dT00h00m00s

# Create a topic subscription
    az servicebus topic subscription create --resource-group ecommerce-resource-group --namespace-name ecommerce-servicebus-namespace --topic-name products.deletions --name products.deletions.orders

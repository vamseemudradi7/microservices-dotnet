# Create a topic subscription
    az servicebus topic subscription create --resource-group ecommerce-resource-group --namespace-name ecommerce-servicebus-namespace --topic-name products.updates --name products.updates.orders

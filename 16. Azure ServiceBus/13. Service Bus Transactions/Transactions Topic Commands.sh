# Create Service Bus Topic for Transactions
    az servicebus topic create --resource-group ecommerce-resource-group --namespace-name ecommerce-servicebus-namespace --name orders.placed.reprocess --max-size 1024 --default-message-time-to-live 14dT00h00m00s

# Update a topic subscription - remove filter
    az servicebus topic subscription rule delete --resource-group ecommerce-resource-group --namespace-name ecommerce-servicebus-namespace --topic-name orders.placed --subscription-name orders.placed.products --name '$Default'


# Update a topic subscription - add filter
    az servicebus topic subscription rule create --resource-group ecommerce-resource-group --namespace-name ecommerce-servicebus-namespace --topic-name orders.placed --subscription-name orders.placed.products --name OrderItemsCountRule --filter-sql-expression 'OrderItemsCount = 1'


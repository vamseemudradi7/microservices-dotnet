# Namespace:
    kubectl create namespace ecommerce-namespace

# --------------

# Apply all in the current folder:
    kubectl apply -f .
    kubectl delete -f .


# Apply the deployment yaml file:
    kubectl apply -f mysql-deployment.yaml
    kubectl apply -f mongodb-deployment.yaml
    kubectl apply -f postgres-deployment.yaml
    kubectl apply -f users-microservice-deployment.yaml
    kubectl apply -f products-microservice-deployment.yaml
    kubectl apply -f redis-deployment.yaml
    kubectl apply -f rabbitmq-deployment.yaml
    kubectl apply -f apigateway-deployment.yaml
    kubectl apply -f orders-microservice-deployment.yaml


# -----------

    kubectl delete -f mysql-deployment.yaml
    kubectl delete -f mongodb-deployment.yaml
    kubectl delete -f postgres-deployment.yaml
    kubectl delete -f users-microservice-deployment.yaml
    kubectl delete -f products-microservice-deployment.yaml
    kubectl delete -f redis-deployment.yaml
    kubectl delete -f rabbitmq-deployment.yaml
    kubectl delete -f apigateway-deployment.yaml
    kubectl delete -f orders-microservice-deployment.yaml


# ---------------

# Get namespaces:
    kubectl get namespaces

# Get deployments in a namespace:
    kubectl get deployments --namespace ecommerce-namespace

# Get all in a namespace:
    kubectl get all --namespace ecommerce-namespace


# --------------


# Get deployment details:
    kubectl describe deployment apigateway-deployment --namespace ecommerce-namespace


# Delete all deployments:
    kubectl delete deployments --all --namespace=ecommerce-namespace

# ---------------

# Delete a deployment:
    kubectl delete deployment mysql-deployment --namespace ecommerce-namespace


# -----------------

export IMAGE_NAME := 90dy/pancake-token-sniper:latest

.PHONY: help
help:
	@echo "Usage: make [target]"
	@echo ""
	@echo "Targets:"
	@echo "  dev		  Run the application in development mode"
	@echo "  build Build the Docker image"
	@echo "  run   Run the Docker image"
	@echo "  push  Push the Docker image to the registry"
	@echo "  deploy   Deploy the application to Kubernetes"
	@echo "  delete   Delete the application from Kubernetes"
	@echo "  logs	 Show the logs of the application in Kubernetes"
	@echo "  shell	Open a shell in the application container in Kubernetes"

.PHONY: dev
dev:
	dotnet watch run

.PHONY: build
build:
	# It doesn't work on M1 Mac because of the amd64 architecture needed for the deployment
	docker build --force-rm -t $(IMAGE_NAME) .

.PHONY: run
run:
	docker run -p 8080:8080 --env-file .env -it --rm $(IMAGE_NAME)

.PHONY: push
push:
	docker push $(IMAGE_NAME)

.PHONY: deploy
deploy:
	kubectl config use-context admin@k8s-default
	set -a && source .env && set +a && \
        envsubst < k8s/deployment.yaml | kubectl apply -f -

.PHONY: delete
delete:
	kubectl config use-context admin@k8s-default
	set -a && source .env && set +a && \
	    envsubst < k8s/deployment.yaml | kubectl delete -f -

.PHONY: logs
logs:
	kubectl logs -l app=pancake-token-sniper -f

.PHONY: shell
shell:
	kubectl exec -it $(shell kubectl get pods -l app=pancake-token-sniper -o jsonpath='{.items[0].metadata.name}') -- /bin/sh

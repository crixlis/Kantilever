cd particulier
@echo Build Angular project particulier
call ng build --prod
@echo Build succesfull!
@echo Starting docker image build
call docker build --tag="kantilever/webshopfrontend:1.1" .
@echo Image build succesfull!
@echo Run docker image
call docker run -d -p 8080:80 -d kantilever/webshopfrontend:1.1
@echo Docker image running!
cd ..
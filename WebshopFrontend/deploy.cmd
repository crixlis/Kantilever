cd particulier
@echo Build Angular project particulier
call ng build
@echo Build succesfull!
@echo Starting docker image build
call docker build --tag="kantilever/webshopfrontend:1.1" .
@echo Image build succesfull!
@echo Run docker image
call docker run -p 1024:4200 kantilever/webshopfrontend:1.1
@echo Docker image running!
cd ..
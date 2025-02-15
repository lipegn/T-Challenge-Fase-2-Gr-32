# T-Challenge-Fase-2-Gr-32

Entrega do Tech Challenge da fase 4 do grupo 32 

Mayara Alves da Silva - RM 357738
Felipe Gonçalves - RM 357046

Esse projeto utiliza:
    - .Net8
    - Sql Server
    - RabbitMq

## Rodando o Projeto

- Buildar as imagens da API e do consumidor pois o projeto está apontando para uma imagem local

    docker build -f .\dockerfile-contato-api -t fiap-contato-api:v1 .
    docker build -f .\dockerfile-consumidor -t fiap-consumidor:v1 .

- Subir os pods, que é onde roda as aplicações. Nesse projetos criamos um arquivo para da pod: Menssageria (RabbtMq), Banco (SQL), API e Consumidor

    kubectl apply -f fiap-sql.yaml
    kubectl apply -f fiap-menssageria.yaml
    kubectl apply -f fiap-api.yaml
	kubectl apply -f fiap-consumidor.yaml
	
- Subir os serviços, que é onde expoem as portas. Foi criado quatro services um para cara pod

    kubectl apply -f svc-fiap-sql.yaml
    kubectl apply -f svc-fiap-menssageria.yaml
    kubectl apply -f svc-fiap-api.yaml
	kubectl apply -f svc-fiap-consumidor.yaml

Para acessar a aplicação utilizar: http://localhost:32100/swagger/index.html

Para conectar ao banco de dados: Server: localhost,32200 | User: sa | Password: Passw0rd

Para acessar o RabbitMq utilizar: http://localhost:31672 | User: guest | Password: guest
	

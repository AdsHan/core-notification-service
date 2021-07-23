# core-notification-service
Aplicação .Net Core 5.0 Web API desenvolvida com o objetivo didático de demonstrar um serviço de notificação chamado por mensageria que envia email de boas-vindas. Quando um usuário for criado será enviado o email de boas-vindas. 

# Este projeto contém:
- Arquitetura Microsserviços;
- RabbitMQ como messaging broker;
- MongoDB fornecendo o template de email;
- SMTP do Gmail para envio de email; 
- Message Bus;
- Pattern CQRS com MediatR;
- Fluent Validation;
- Versionamento da API;
- Swagger/Swagger UI;

# Como executar:
- Clonar / baixar o repositório em seu workplace.
- Baixar o .Net Core SDK e o Visual Studio / Code mais recentes.
- Instalar o RabbitMQ local ou em container.
- Instalar o MongoDB local ou em container.
- Nas configurações da API, ajustar o PATH do XML do Swagger.

# Sobre
Este projeto foi desenvolvido por Anderson Hansen sob [MIT license](LICENSE).

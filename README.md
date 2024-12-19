#  SchollApi
É uma aplicação para gerenciamento de aluno.

## Tecnologias

### Para o back-end:
* .NET 9
* JWT
* Entity Framework Core
* Scalar
* Docker
* Docker compose
* Postgresql
* git


### Para o Front-end
* ReactJs
* Axios
* NodeJs

## Ajuda
### docker
Executar o bash no container desejado
```bash
docker exec -it <nome_do_container> bash
````

### Postgresql

Acesse o PostgreSQL informando o _host_ e a _porta_:
*-p* - PORTA 
*-U* - Usuário
*-d* - Database
```bash
psql -h localhost -p 5432 -U user -d mydatabase
````

Acesse o PostgreSQL no mesmo _host_:
```bash
psql -U user -d mydatabase
````

Liste todas as tabelas: No prompt do psql, execute:
```bash
\dt
````

Com um _schema_ específico:
```bash
\dt schema_name.*
````

Execute o SELECT: No prompt do psql, execute:
```bash
SELECT * FROM nome_da_tabela;
```


### Referências
Scalar - https://github.com/scalar/scalar/blob/main/packages/scalar.aspnetcore/README.md
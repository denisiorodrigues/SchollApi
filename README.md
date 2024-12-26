#  SchollApi
É uma aplicação para gerenciamento de aluno.

## Tecnologias

### Para o back-end:
* .NET 9
* devContainer
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

### .NET

Comando para configurar os certificados digital para funcionar o HTTPS

```bash
dotnet dev-certs https
```

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
```

Acesse o PostgreSQL no mesmo _host_:
```bash
psql -U postgres -d schoolapi
```

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

---

Usuário registrado

```json
{
    "email": "denisio@ymail.com",
  "password": "Numsey@20241@#",
  "confirmPassword": "Numsey@20241@#"
}
```


### Referências
Scalar - https://github.com/scalar/scalar/blob/main/packages/scalar.aspnetcore/README.md

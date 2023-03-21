# TempusCrud
Sistema desenvolvido em ASP.NET MVC utilizando Migrations e SQL Server

Para conexão com o banco estou utilizando a ConnectionStrings que faz o login diretamente com o windows authentication.
"ConnectionStrings": {
    "DataBase": "Server=DESKTOP-H00N7RI\\SQLEXPRESS;Database=DB_Tempus; Integrated Security=SSPI"
},

Caso deseje alterar para o login manual do banco de dados, altere o "Integrated Security=SSPI" para "User Id=(login);Password=(senha)"

Para a geração automatica do banco de dados utilize este comando no console: Update-Database -Context BancoContext

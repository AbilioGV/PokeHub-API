# 🚀 PokeHub API

API desenvolvida em **.NET 8** para gerenciamento de hunts de Pokémon, com integração à PokeAPI para validação e enriquecimento de dados.

---

## 🧠 Sobre o projeto

O **PokeHub** é uma API REST que permite gerenciar caçadas (hunts) de Pokémon, acompanhando tentativas, status e histórico.

O sistema foi projetado com foco em **boas práticas de backend**, incluindo separação de responsabilidades, validações de negócio e integração com API externa.

---

## ⚙️ Tecnologias utilizadas

- .NET 8
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- HttpClient (integração com API externa)
- PokeAPI

---

## 🏗️ Arquitetura

O projeto segue uma estrutura baseada em camadas:

- **Controllers** → Recebem requisições HTTP  
- **Services** → Contêm a lógica de negócio  
- **DTOs** → Modelos de resposta  
- **Data (DbContext)** → Acesso ao banco de dados  

---

## 📌 Funcionalidades

- ✅ Criar hunts com validação de Pokémon via API externa  
- ✅ Impedir múltiplas hunts ativas do mesmo Pokémon  
- ✅ Incrementar tentativas automaticamente  
- ✅ Atualizar status dinamicamente (`active` ↔ `completed`)  
- ✅ Buscar hunts com sprite (integração com PokeAPI)  
- ✅ Filtrar hunts por status  
- ✅ Buscar hunt por ID  
- ✅ Registro de datas (início e conclusão)  

---

## 🧠 Regras de negócio

- ❌ Não é permitido ter mais de uma hunt **ativa** para o mesmo Pokémon  
- ❌ Não é possível incrementar tentativas de hunts concluídas  
- ✅ Hunts podem ser concluídas e novas podem ser criadas após conclusão  
- ✅ Pokémon é validado antes de ser salvo no banco  
- ✅ Dados são normalizados para evitar inconsistências  

---

## 🔗 Integração com API externa

A API consome dados da **PokeAPI** para:

- Validar se o Pokémon existe  
- Obter sprites (imagens)  
- Enriquecer a resposta da API  

---

## 📥 Exemplos de endpoints

### 🔹 Criar Hunt

POST /hunts

```json
{
  "pokemonName": "pikachu",
  "method": "Masuda"
}
```

---

### 🔹 Incrementar Tentativas

PATCH /hunts/{id}/attempt

---

### 🔹 Atualizar Status

PATCH /hunts/{id}/status

```json
{
  "status": "completed"
}
```

---

### 🔹 Listar Hunts

GET /hunts

---

### 🔹 Buscar por Status

GET /hunts?status=active

---

### 🔹 Buscar por ID

GET /hunts/{id}

---

## 🗄️ Banco de Dados

Utiliza **PostgreSQL** com Entity Framework Core.

---

## ⚠️ Configuração

Antes de rodar o projeto, configure sua string de conexão:

```json
"ConnectionStrings": {
  "DefaultConnection": "SUA_STRING_AQUI"
}
```

---

## ▶️ Como rodar o projeto

```bash
dotnet restore
dotnet build
dotnet run
```

---

## 📌 Melhorias futuras

- Autenticação de usuários  
- Cache de dados da PokeAPI  
- Paginação  
- Frontend em React  
- Sistema de favoritos / coleções  

---

## 👨‍💻 Autor

Desenvolvido por Abílio Gonçalves

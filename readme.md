# TaskFlow API

Une API REST de gestion de projets et de tâches construite avec .NET 10 et ASP.NET Core. Elle permet aux utilisateurs de créer des projets, de gérer des tâches et des commentaires avec une authentification JWT sécurisée.

## Caractéristiques

- Authentification et autorisation JWT
- Gestion des projets (CRUD)
- Gestion des tâches (CRUD)
- Gestion des commentaires sur les tâches
- Gestion des rôles utilisateurs
- Base de données SQL Server avec Entity Framework Core
- Documentation Swagger/OpenAPI interactive
- Gestion centralisée des exceptions
- Validation des données avec DTOs

## Prérequis

- .NET 10 SDK
- Microsoft SQL  
- Docker et Docker Compose

## Installation

### 1. Cloner le projet

```bash
git clone <repository-url>
cd TaskFlow.Api
```

### 2. Restaurer les dépendances

```bash
dotnet restore
```

### 3. Configurer la connexion à la base de données

Utilisez `dotnet user-secrets` pour stocker les informations sensibles en développement :

```bash
# Définir la chaîne de connexion
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=TaskFlowDb;User Id=sa;Password=Admin@123;TrustServerCertificate=True;"

# Définir la clé JWT
dotnet user-secrets set "Jwt:Key" "ThisIsASecretKeyForTaskFlowApiMustBeLongEnough123"
```

### 4. Appliquer les migrations

```bash
dotnet ef database update
```

Cela créera les tables et schémas nécessaires dans votre base de données.

## Utilisation avec Docker Compose

L'application inclut un fichier `compose.yaml` pour déployer facilement une instance SQL Server :

```bash
docker-compose up -d
```

Cela démarrera :
- **SQL Server** sur le port 1433
- **Credentials** : SA / votre_mot_de_passe

Mettez à jour `appsettings.json` pour utiliser `localhost:1433` comme serveur.

## Démarrage de l'application

### Mode développement

```bash
dotnet run --launch-profile https
```

L'API sera accessible à `https://localhost:7087` ou `http://localhost:5195`

### Swagger UI

Une fois l'application démarrée, accédez à la documentation interactive Swagger :

```
https://localhost:7087/swagger
http://localhost:5195/swagger
```

Vous pouvez tester tous les endpoints directement depuis l'interface.

## Authentification

L'API utilise les tokens JWT (JSON Web Tokens) pour l'authentification.

### Utilisation du token

Après connexion, vous recevrez un token JWT. Incluez-le dans l'en-tête `Authorization` de vos requêtes :

```bash
curl -H "Authorization: Bearer YOUR_JWT_TOKEN" https://localhost:7087/api/projects
```

### Configuration JWT

Les paramètres JWT sont définis dans `appsettings.json` :

```json
{
  "Jwt": {
    "Key": "votre-clé-secrète",
    "Issuer": "TaskFlow.Api",
    "Audience": "TaskFlow.Client"
  }
}
```

- **Key** : Clé secrète pour signer les tokens (minimum 32 caractères)
- **Issuer** : Entité qui émet le token
- **Audience** : Entité destinataire du token

## Structure du projet

```
TaskFlow.Api/
├── Controllers/           # Points de terminaison REST
│   ├── UsersController.cs
│   ├── ProjectsController.cs
│   ├── TasksController.cs
│   └── CommentsController.cs
├── Models/               # Modèles de données
│   ├── User.cs
│   ├── Project.cs
│   ├── TaskItem.cs
│   ├── TaskComment.cs
│   └── Enums/
├── DTOs/                 # Data Transfer Objects
│   ├── Auth/
│   ├── Projects/
│   ├── Tasks/
│   └── Comments/
├── Services/             # Logique métier
│   ├── Interfaces/
│   ├── UserService.cs
│   ├── ProjectService.cs
│   ├── TaskService.cs
│   ├── CommentService.cs
│   └── JwtService.cs
├── Data/                 # Contexte Entity Framework
│   └── TaskFlowDbContext.cs
├── Middleware/           # Middlewares personnalisés
│   └── ExceptionMiddleware.cs
├── Migrations/           # Migrations Entity Framework
└── appsettings.json      # Configuration
```

## Modèles de données

### User
- `Id` : Identifiant unique
- `Email` : Adresse email (unique)
- `PasswordHash` : Hash du mot de passe
- `FirstName` : Prénom
- `LastName` : Nom
- `Role` : Rôle utilisateur (Admin, User)

### Project
- `Id` : Identifiant unique
- `Name` : Nom du projet
- `Description` : Description optionnelle
- `CreationDate` : Date de création
- `UserId` : Identifiant du propriétaire

### TaskItem
- `Id` : Identifiant unique
- `Title` : Titre de la tâche
- `Description` : Description optionnelle
- `Status` : État (Todo, InProgress, Done)
- `Priority` : Priorité (1-5)
- `DueDate` : Date d'échéance
- `ProjectId` : Projet associé

### TaskComment
- `Id` : Identifiant unique
- `Content` : Contenu du commentaire
- `CreationDate` : Date de création
- `TaskId` : Tâche associée
- `UserId` : Auteur du commentaire

## Gestion des erreurs

L'API retourne des codes HTTP standards :

- `200 OK` : Requête réussie
- `201 Created` : Ressource créée
- `204 No Content` : Succès sans contenu
- `400 Bad Request` : Requête invalide
- `401 Unauthorized` : Authentification requise
- `403 Forbidden` : Accès refusé
- `404 Not Found` : Ressource non trouvée
- `500 Internal Server Error` : Erreur serveur

## Développement

### Ajouter une migration

```bash
dotnet ef migrations add NomDeLaMigration
```

### Mettre à jour la base de données

```bash
dotnet ef database update
```

### Annuler une migration

```bash
dotnet ef migrations remove
```

## Notes importantes

1. **Clé JWT** : Changez la clé par défaut en production avec une clé secrète forte
2. **HTTPS** : L'API force HTTPS en production
3. **CORS** : À configurer selon vos besoins pour les requêtes cross-origin
4. **Validation** : Les données sont validées via les DTOs et les services
5. **Permissions** : Les utilisateurs ne peuvent accéder qu'à leurs propres données

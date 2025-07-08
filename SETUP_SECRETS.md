# Configuration des Secrets

## Développement Local

### 1. User Secrets (Recommandé pour le développement)

Les secrets sont stockés localement et ne sont jamais commités dans Git.

```bash
# Initialiser les secrets utilisateur (déjà fait)
dotnet user-secrets init

# Ajouter la clé API OpenRouter
dotnet user-secrets set "OpenRouter:ApiKey" "votre-clé-api-ici"

# Vérifier les secrets
dotnet user-secrets list
```

### 2. Variables d'environnement

Alternativement, vous pouvez utiliser des variables d'environnement :

```bash
# macOS/Linux
export OpenRouter__ApiKey="votre-clé-api-ici"

# Windows
set OpenRouter__ApiKey=votre-clé-api-ici
```

## Production

### 1. Variables d'environnement

```bash
export OpenRouter__ApiKey="votre-clé-api-de-production"
```

### 2. Azure App Service

Dans les paramètres de l'application Azure :
- Nom : `OpenRouter__ApiKey`
- Valeur : `votre-clé-api-de-production`

### 3. Docker

```dockerfile
ENV OpenRouter__ApiKey=votre-clé-api-de-production
```

## Ordre de priorité de la configuration

1. Variables d'environnement
2. User Secrets (développement uniquement)
3. appsettings.Development.json
4. appsettings.json

## Sécurité

- ✅ Les secrets utilisateur sont stockés localement
- ✅ Les variables d'environnement ne sont pas dans le code
- ✅ Le fichier appsettings.json ne contient plus de clés sensibles
- ✅ Le fichier .gitignore exclut les fichiers sensibles 

# Configuration des secrets OpenRouter

Pour utiliser l'IA, il te faut une clé API OpenRouter. Ne la commite jamais dans le dépôt !

## Étapes pour le développement local

1. Crée un fichier `.env` à la racine du projet (s'il n'existe pas).
2. Ajoute la ligne suivante dans `.env` :

   OpenRouter__ApiKey=sk-or-v1-xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

3. Ne partage jamais ce fichier. Il est ignoré par git.

4. La clé sera automatiquement chargée au démarrage de l'application si tu as bien suivi la configuration dans `Program.cs`. 
# 🚀 Guide de Configuration Rapide - NadsTech

## Configuration Initiale

### 1. Cloner et Configurer
```bash
git clone <votre-repo>
cd NadsTech
dotnet restore
```

### 2. Configuration OpenRouter
1. Créez un compte sur [OpenRouter](https://openrouter.ai)
2. Obtenez votre clé API
3. Modifiez `appsettings.json` :
```json
{
  "OpenRouter": {
    "ApiKey": "votre-clé-api-ici"
  }
}
```

### 3. Lancer l'Application
```bash
dotnet run
```
Accédez à : `https://localhost:5001`

## 🎯 Fonctionnalités Principales

### Interface Utilisateur
- ✅ **Mode sombre/clair** - Toggle en haut à droite
- ✅ **Design responsive** - Compatible mobile/desktop
- ✅ **Navigation intuitive** - Menu principal et footer

### Pages Disponibles
- 🏠 **Accueil** (`/`) - Présentation et articles en vedette
- 📰 **Articles** (`/articles`) - Liste avec filtres et recherche
- ℹ️ **À propos** (`/about`) - Informations sur le projet

### Fonctionnalités IA (à implémenter)
- 🤖 **Analyse de contenu** - Insights automatiques
- 📊 **Statistiques** - Graphiques et tendances
- ⏰ **Timing optimal** - Meilleur moment pour publier
- 🔗 **Articles similaires** - Suggestions personnalisées

## 🛠️ Structure du Projet

```
NadsTech/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor      # Layout principal
│   │   └── ThemeToggle.razor     # Toggle thème
│   ├── Pages/
│   │   ├── Home.razor            # Page d'accueil
│   │   ├── Articles.razor        # Liste des articles
│   │   └── About.razor           # Page à propos
│   └── App.razor                 # Configuration app
├── Models/
│   ├── Article.cs                # Modèle article
│   ├── Comment.cs                # Modèle commentaire
│   └── Reaction.cs               # Modèle réaction
├── Data/
│   └── ApplicationDbContext.cs   # Contexte EF
├── Services/
│   ├── IAnalysisService.cs       # Interface IA
│   └── OpenRouterAnalysisService.cs # Service IA
├── wwwroot/
│   ├── js/
│   │   └── theme.js             # Script thème
│   └── app.css                  # Styles CSS
└── Program.cs                   # Configuration
```

## 🔧 Développement

### Ajouter une Nouvelle Page
1. Créez un fichier dans `Components/Pages/`
2. Ajoutez `@page "/votre-route"`
3. Ajoutez le lien dans `MainLayout.razor`

### Ajouter un Nouveau Service
1. Créez l'interface dans `Services/`
2. Implémentez le service
3. Enregistrez dans `Program.cs`

### Modifier le Design
- **Couleurs** : Modifiez `tailwind.config.js`
- **Styles** : Éditez `wwwroot/app.css`
- **Thème** : Ajustez `ThemeToggle.razor`

## 📊 Base de Données

### Modèles Principaux
- **Article** : Titre, contenu, catégorie, tags, statistiques
- **Comment** : Contenu, auteur, réponses imbriquées
- **Reaction** : Types (like, share, bookmark), utilisateur

### Migration
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 🚀 Déploiement

### Production
```bash
dotnet publish -c Release
```

### Docker
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0
COPY bin/Release/net9.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "NadsTech.dll"]
```

## 🔐 Sécurité

### Variables d'Environnement
```bash
export OpenRouter__ApiKey="votre-clé"
export ConnectionStrings__DefaultConnection="votre-connection-string"
```

### Configuration Production
- Utilisez HTTPS
- Configurez les secrets
- Activez la compression
- Optimisez les performances

## 📈 Prochaines Étapes

### Fonctionnalités à Implémenter
- [ ] Interface d'administration
- [ ] Système d'authentification
- [ ] Création/édition d'articles
- [ ] Système de commentaires
- [ ] Intégration IA complète
- [ ] API REST
- [ ] Tests unitaires

### Optimisations
- [ ] Cache Redis
- [ ] CDN pour les assets
- [ ] Compression des images
- [ ] SEO optimisé
- [ ] Analytics

## 🆘 Support

### Problèmes Courants
1. **Erreur de base de données** : Vérifiez la connection string
2. **IA ne fonctionne pas** : Vérifiez la clé OpenRouter
3. **Styles cassés** : Vérifiez Tailwind CSS

### Debug
```bash
dotnet run --environment Development
```

### Logs
Les logs sont dans la console et peuvent être configurés dans `appsettings.json`

---

**NadsTech** - Blog tech avec IA 🤖✨ 
# NadsTech - Blog Tech avec IA

Un blog technologique moderne alimenté par l'intelligence artificielle, développé avec Blazor Server et intégrant OpenRouter pour l'analyse de contenu.

## 🚀 Fonctionnalités

### ✨ Interface Moderne
- **Design responsive** avec Tailwind CSS
- **Mode sombre/clair** avec persistance des préférences
- **Navigation intuitive** et accessible
- **Animations fluides** et transitions

### 🤖 Intelligence Artificielle
- **Analyse de contenu** avec OpenRouter (GPT-4, Claude, Mistral)
- **Insights automatiques** sur chaque article
- **Statistiques et graphiques** générés par IA
- **Timing optimal** pour la publication
- **Articles similaires** suggérés automatiquement

### 📊 Fonctionnalités Sociales
- **Système de commentaires** en temps réel
- **Réactions** (like, partage, bookmark)
- **Partage social** intégré
- **Statistiques de vues** et engagement

### 🔍 Recherche et Filtres
- **Recherche avancée** dans le contenu
- **Filtrage par catégorie**
- **Tri multiple** (date, vues, likes)
- **Pagination** optimisée

## 🛠️ Technologies

- **.NET 9** - Framework principal
- **Blazor Server** - Interface utilisateur interactive
- **Entity Framework Core** - ORM pour la base de données
- **SQLite** - Base de données légère
- **Tailwind CSS** - Framework CSS utilitaire
- **OpenRouter API** - Accès aux modèles de langage
- **SignalR** - Communication en temps réel

## 📦 Installation

### Prérequis
- .NET 9 SDK
- Compte OpenRouter (pour l'API IA)

### Étapes d'installation

1. **Cloner le repository**
   ```bash
   git clone https://github.com/votre-username/nadstech.git
   cd nadstech
   ```

2. **Installer les dépendances**
   ```bash
   dotnet restore
   ```

3. **Configurer l'API OpenRouter**
   - Créez un compte sur [OpenRouter](https://openrouter.ai)
   - Obtenez votre clé API
   - Modifiez `appsettings.json` :
   ```json
   {
     "OpenRouter": {
       "ApiKey": "votre-clé-api-ici"
     }
   }
   ```

4. **Lancer l'application**
   ```bash
   dotnet run
   ```

5. **Accéder à l'application**
   - Ouvrez votre navigateur
   - Allez sur `https://localhost:5001`

## 🗄️ Structure de la Base de Données

### Articles
- Titre, contenu, résumé
- Catégorie et tags
- Statistiques (vues, likes, partages)
- Analyses IA (insights, sentiment, timing)

### Commentaires
- Contenu et auteur
- Système de réponses imbriquées
- Modération automatique

### Réactions
- Types : Like, Love, Share, Bookmark
- Traçage par utilisateur (IP/session)

## 🤖 Configuration IA

### Modèles Supportés
- **GPT-4** - Analyse générale et insights
- **Claude 3.5 Sonnet** - Analyse de sentiment
- **Mistral** - Suggestions d'articles

### Fonctionnalités IA
1. **Analyse de contenu** - Extraction des points clés
2. **Sentiment analysis** - Évaluation du ton de l'article
3. **Timing optimal** - Meilleur moment pour publier
4. **Articles similaires** - Suggestions personnalisées
5. **Statistiques** - Graphiques et visualisations

## 🎨 Personnalisation

### Thème
- Modifiez les couleurs dans `tailwind.config.js`
- Ajoutez vos propres styles dans `wwwroot/app.css`

### Contenu
- Créez des articles via l'interface d'administration
- Personnalisez les catégories et tags
- Configurez les analyses IA

## 📈 Déploiement

### Production
1. **Build de production**
   ```bash
   dotnet publish -c Release
   ```

2. **Configuration serveur**
   - IIS ou Nginx
   - Base de données SQL Server/PostgreSQL
   - Variables d'environnement pour les secrets

### Docker (optionnel)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0
COPY bin/Release/net9.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "NadsTech.dll"]
```

## 🔧 Développement

### Structure du Projet
```
NadsTech/
├── Components/
│   ├── Layout/          # Layouts et navigation
│   ├── Pages/           # Pages principales
│   └── App.razor        # Configuration de l'app
├── Models/              # Modèles de données
├── Data/                # Contexte Entity Framework
├── Services/            # Services IA et métier
├── wwwroot/            # Assets statiques
└── Program.cs          # Configuration de l'app
```

### Ajout de Fonctionnalités
1. **Nouvelle page** : Créez un fichier dans `Components/Pages/`
2. **Nouveau service** : Ajoutez dans `Services/`
3. **Nouveau modèle** : Définissez dans `Models/`

## 🤝 Contribution

1. Fork le projet
2. Créez une branche feature (`git checkout -b feature/AmazingFeature`)
3. Commit vos changements (`git commit -m 'Add AmazingFeature'`)
4. Push vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrez une Pull Request

## 📄 Licence

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.

## 🆘 Support

- **Issues** : [GitHub Issues](https://github.com/votre-username/nadstech/issues)
- **Documentation** : [Wiki](https://github.com/votre-username/nadstech/wiki)
- **Email** : contact@nadstech.com

## 🚀 Roadmap

- [ ] Interface d'administration
- [ ] Système d'authentification
- [ ] Newsletter automatisée
- [ ] API REST publique
- [ ] Application mobile
- [ ] Intégration réseaux sociaux
- [ ] Analytics avancés
- [ ] Multilingue

---

**NadsTech** - L'actualité tech analysée par l'IA 🤖✨


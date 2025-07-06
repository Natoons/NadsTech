# Migration vers Tailwind CSS

Ce projet a été migré de Bootstrap vers Tailwind CSS pour une meilleure flexibilité et personnalisation.

## Changements effectués

### 1. Remplacement de Bootstrap par Tailwind CSS
- Suppression de la référence Bootstrap dans `Components/App.razor`
- Ajout de Tailwind CSS via CDN
- Configuration personnalisée de Tailwind avec des couleurs primaires

### 2. Mise à jour des composants

#### Layout principal (`Components/Layout/MainLayout.razor`)
- Remplacement des classes Bootstrap par des classes Tailwind
- Nouveau design avec sidebar fixe et contenu principal
- Interface moderne avec ombres et bordures arrondies

#### Navigation (`Components/Layout/NavMenu.razor`)
- Menu de navigation redessiné avec Tailwind
- Icônes SVG intégrées
- Effets de survol et transitions

#### Pages
- **Home.razor** : Design moderne avec cartes et typographie améliorée
- **Counter.razor** : Interface épurée avec bouton stylisé
- **Weather.razor** : Tableau moderne avec hover effects

### 3. Styles CSS (`wwwroot/app.css`)
- Remplacement des styles Bootstrap par des directives Tailwind `@apply`
- Conservation des styles spécifiques à Blazor (validation, erreurs)
- Amélioration de la typographie avec la police Inter

### 4. Configuration
- Fichier `tailwind.config.js` pour la personnalisation
- Configuration inline dans `App.razor` pour le CDN
- Couleurs primaires personnalisées

## Avantages de Tailwind CSS

1. **Utilitaires-first** : Classes CSS prêtes à l'emploi
2. **Personnalisation** : Facile de modifier les couleurs et styles
3. **Performance** : Taille de bundle optimisée
4. **Responsive** : Classes responsive intégrées
5. **Accessibilité** : Focus states et contrastes améliorés

## Utilisation

Pour ajouter de nouveaux composants avec Tailwind :

```html
<!-- Exemple de carte -->
<div class="bg-white rounded-lg shadow-md p-6">
    <h2 class="text-2xl font-semibold text-gray-800 mb-4">Titre</h2>
    <p class="text-gray-600">Contenu...</p>
    <button class="bg-primary-600 hover:bg-primary-700 text-white px-4 py-2 rounded-lg">
        Action
    </button>
</div>
```

## Classes Tailwind principales utilisées

- **Layout** : `flex`, `grid`, `container`, `max-w-*`
- **Spacing** : `p-*`, `m-*`, `px-*`, `py-*`
- **Colors** : `bg-*`, `text-*`, `border-*`
- **Typography** : `text-*`, `font-*`, `leading-*`
- **Effects** : `shadow-*`, `rounded-*`, `opacity-*`
- **Responsive** : `sm:`, `md:`, `lg:`, `xl:`

## Prochaines étapes

1. Personnaliser davantage les couleurs selon votre charte graphique
2. Ajouter des animations avec les classes `transition-*`
3. Créer des composants réutilisables avec des classes Tailwind
4. Optimiser pour la production avec un build Tailwind personnalisé 
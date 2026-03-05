# Guide de Configuration de la Scène Unity "Ninja Moléculaire"

Ce guide présente les étapes à suivre dans l'Éditeur Unity pour assembler le projet à l'aide des scripts générés dans `Assets/Projet/Scripts/`.

## 1. Préparation de la Scène de Base
1. Ouvrez ou créez une scène dans `Assets/Scenes/`.
2. Assurez-vous d'avoir un **XR Origin (XR Rig)** dans votre scène. Vous pouvez le créer via un clic droit dans la Hiérarchie : `XR > XR Origin (VR)`.
3. Créez un objet 3D simple pour servir de table ou de paillasse de laboratoire (ex: un Cube aplati : `GameObject > 3D Object > Cube` et ajustez son échelle : `X: 2, Y: 0.1, Z: 1`). Placez-le devant le joueur (ex: Position `Z: 0.6, Y: 0.8`).

## 2. Configuration du "Game Manager"
1. Créez un GameObject vide dans la Hiérarchie et nommez-le **`GameManager`**.
2. Glissez-déposez le script `GameManager.cs` sur cet objet.
3. Laissez les paramètres par défaut ou ajustez la durée du jeu (ex: `300` secondes pour 5 minutes).

## 3. Configuration de l'Interface (Canvas & RecipeUI)
1. Créez un Canvas en mode **World Space** : `GameObject > UI > Canvas`.
2. Modifiez le *Render Mode* du Canvas en **World Space**. Ajustez sa position et son échelle pour qu'il soit visible au-dessus de la paillasse (Échelle : `0.002, 0.002, 0.002`).
3. Ajoutez des textes TextMeshPro : `UI > Text - TextMeshPro` pour le **Score**, le **Chronomètre** et la **Commande/Recette**.
4. Attachez le script `RecipeUI.cs` au Canvas.
5. Dans l'inspecteur du `RecipeUI`, liez les champs *Score Text*, *Timer Text* et *Recipe Text* avec les éléments TextMeshPro que vous venez de créer.

## 4. Configuration du Sabre (Slicer)
1. Créez ou importez un modèle 3D de sabre / couteau.
2. Ajoutez un composant `Box Collider` (ou un collider approprié à la forme de la lame) et cochez impérativement la case **Is Trigger**.
3. Ajoutez un script `Slicer.cs` sur cet objet.
4. Assurez-vous que le sabre possède un composant `XR Grab Interactable` et un `Rigidbody` pour pouvoir être attrapé en VR.

## 5. Configuration des Cibles (Fruits cibles & Bombes)
***Important : Faites ces étapes pour un objet, puis glissez l'objet depuis la hiérarchie vers `Assets/Projet/Prefabs` pour en faire un Prefab, puis supprimez-le de la scène.***

### Le Fruit
1. Créez un modèle 3D représentant un fruit entier.
2. Ajoutez un `Rigidbody` et un `Box Collider` (ou `Sphere Collider`). (Assurez-vous que `Is Trigger` est **décoché**).
3. Ajoutez le script `FruitTarget.cs`.
4. Créez un autre Prefab qui contient **les deux moitiés** du fruit (chacune ayant son propre `Rigidbody` et `Collider`).
5. Dans l'inspecteur du `FruitTarget` du *fruit entier*, glissez ce Prefab des moitiés dans le champ *Sliced Prefab*.

### La Bombe
1. Créez un modèle 3D (ex: Sphère rouge ou noire).
2. Ajoutez un `Rigidbody` et un `Collider` (ou `Sphere Collider`).
3. Ajoutez le script `Bomb.cs`.

## 6. Configuration du Spawner
1. Créez un GameObject vide dans la hiérarchie et nommez-le **`Spawner`**.
2. Placez-le sous votre table de travail ou légèrement en dessous pour que les objets aillent vers le haut.
3. Ajoutez le script `Spawner.cs` sur cet objet.
4. Dans l'inspecteur du Spawner, remplissez la liste *Prefabs To Spawn* avec les Prefabs de Fruits et de Bombes créés à l'étape 5.
5. Ajustez la force (`Spawn Force Min/Max`) pour que les objets montent à la bonne hauteur devant le joueur.

## 7. Configuration des Événements du GameManager (Optionnel mais recommandé)
1. Sélectionnez le `GameManager` dans la hiérarchie.
2. Dans la section *On Game Start ()*, vous pouvez par exemple ajouter des événements pour afficher/masquer certains textes, activer le spawner, etc. (Le Spawner s'inscrit déjà automatiquement grâce au code).
3. De même pour *On Game Over ()* pour afficher un menu de fin.

---
**Astuce Lancement :** N'oubliez pas d'ajouter un bouton (UI ou un bouton physique VR) qui appelle la méthode `GameManager.Instance.StartGame()` pour lancer la partie !

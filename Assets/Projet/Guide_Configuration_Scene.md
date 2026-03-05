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

## 7. Configuration du Menu Principal (Étape par Étape)
Le menu permet au joueur de choisir son mode de jeu au lancement de l'application.

### A. Créer l'objet MenuManager
1. Dans la hiérarchie à gauche, faites un **clic droit > Create Empty**.
2. Renommez cet objet vide en **`MenuManager`**.
3. Glissez-y le script `MenuManager.cs`.

### B. Créer l'écran du Menu Principal (Le Canvas)
1. Allez dans : `GameObject > UI > Canvas`.
2. Renommez-le en **`MainMenu_Canvas`**. L'Éditeur va aussi créer un objet nommé `EventSystem`, gardez-le précieusement.
3. Dans l'inspecteur à droite, trouvez le composant "Canvas". Changez le paramètre *Render Mode* (probablement sur Screen Space) pour le mettre sur **`World Space`**.
4. La taille de base est gigantesque. Modifiez son *Scale* (Échelle) pour la mettre à **`X: 0.002, Y: 0.002, Z: 0.002`**.
5. Déplacez ce Canvas devant les yeux du joueur (par exemple `Position Z: 1`, `Position Y: 1.5`).

### C. Ajouter et configurer les Boutons
1. Faites un clic droit sur votre `MainMenu_Canvas` > `UI > Button - TextMeshPro`.
2. Renommez-le en `Bouton_Defouloir`. En dépliant ce bouton, vous verrez un objet `Text (TMP)`, cliquez dessus et écrivez "Jouer (Défouloir)" dans la case texte.
3. Faites un autre clic droit sur `MainMenu_Canvas` et créez un deuxième bouton nommé `Bouton_Recette` avec le texte "Jouer (Recette)". Positionnez-les correctement.
4. **Relier le bouton au code :**
   - Cliquez sur `Bouton_Defouloir`.
   - Dans l'inspecteur à droite, descendez tout en bas jusqu'à voir **`On Click ()`**.
   - Cliquez sur le petit bouton **`+`**.
   - Vous verrez une case avec écrit *None (Object)*. Prenez l'objet **`MenuManager`** (créé en A) depuis la hiérarchie et glissez-le dans cette case.
   - À droite, cliquez sur le menu déroulant *"No Function"* > Sélectionnez **`MenuManager`**, puis choisissez **`StartModeDefouloir ()`**.
   - Répétez l'opération C.4 pour le `Bouton_Recette`, mais en choisissant la fonction **`StartModeRecette ()`**.

### D. Lier les éléments dans le MenuManager
1. Cliquez sur votre objet **`MenuManager`** dans la hiérarchie.
2. Dans le script affiché à droite, vous verrez 3 cases vides.
3. Glissez votre `MainMenu_Canvas` dans la case **Main Menu Panel**.
4. Glissez le Canvas que vous aviez créé à l'étape 3 (celui avec le score et le chrono) dans la case **Game UI Panel**. *(Note: Créez plus tard un GameOver_Canvas pour la 3ème case, ou laissez vide pour l'instant).*

### E. Indispensable pour cliquer en VR ! (EventSystem)
Si vous lancez le jeu tel quel, vos manettes ne pourront pas interagir avec le menu.
1. Cliquez sur l'objet **`EventSystem`** (qui a été créé tout seul avec votre premier Canvas).
2. S'il a un composant appelé "Standalone Input Module" ou "Input System UI Input Module", vous pouvez repérer en bas de l'écran un autre bouton "Replace with XR UI Input Module" ou cliquez sur **Add Component** et cherchez **`XR UI Input Module`**.
3. Cet ajout convertit la souris de l'ordinateur en pointeur VR !
4. Allez sur le contrôleur de votre manette PICO (`XR Origin > ... > Right Controller`) et assurez-vous qu'elle a bien un composant **`XR Ray Interactor`** et un **`Line Renderer`** pour pouvoir viser le bouton avec un rayon visuel.

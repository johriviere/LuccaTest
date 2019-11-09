# Consigne du projet
> Vous souhaitez créer un petit programme permettant de convertir automatiquement des montants dans une devise souhaitée.
* https://www.lucca.fr/societe/recrutement/test-technique-back-end

# Cas d'utilisation
Le projet gère les cas d'utilisation suivants : 

1. Cas standard
	* L'utilisateur utilise le programme avec un fichier d'entrée cohérent.  
De plus la conversion demandée est possible (existence d'au moins un chemin possible de proche en proche entre la devise source et la devise cible).

2. Cas anormaux
	* L'utilisateur utilise le programme sans argument.
	* L'utilisateur utilise un fichier d'entrée non cohérent (cohérence du format ou cohérence logique).
	* L'utilisateur utilise un fichier d'entrée cohérent, cependant la conversion ne peut pas être effectuée car il n'y a pas de chemin possible entre la devise source et la devise cible.

# Théorie des graphes
## Contexte
Une partie de la consigne est : 
> Si plusieurs chemins de conversion vous permettent d'atteindre la devise cible, vous devez utiliser le chemin le plus court.

La résolution de cette problématique est assimilable à **une recherche du plus court chemin** pour aller d'une *devise source*
à une *devise cible* dans un **graphe** *(liste de taux de change)*.

## Généralités sur les graphes
* [Introduction to graphs](https://www.youtube.com/watch?v=gXgEDyodOJU&list=PL2_aWCzGMAwI3W_JlcBbtYTwiQSsOTa6P&index=39&t=0s)
* [Properties of graphs](https://www.youtube.com/watch?v=AfYqN3fGapc&list=PL2_aWCzGMAwI3W_JlcBbtYTwiQSsOTa6P&index=39)

Les propriétés du graphe représentant ce problème sont :
* **Graphe simple** (pas de *self-loop*, pas de *multi-edge*)
	* **non orienté (*undirected*)**  
	* **sans notion de poids (*unweighted*)**

## Représentation d'un graphe
Un graphe peut être représenté de différentes façons.
* [Edge list](https://www.youtube.com/watch?v=ZdY1Fp9dKzs&list=PL2_aWCzGMAwI3W_JlcBbtYTwiQSsOTa6P&index=40)
* [Adjacency matrix](https://www.youtube.com/watch?v=9C2cpQZVRBA&list=PL2_aWCzGMAwI3W_JlcBbtYTwiQSsOTa6P&index=41)
* [Adjacency list](https://www.youtube.com/watch?v=k1wraWzqtvQ)

La manière de représenter un graphe aura une influence sur 2 choses :
* **Complexité spatiale**  
Il s'agit de la quantité de mémoire utilisée pour représenter les données du graphe.
* **Complexité temporelle**  
Il s'agit de la performance des algorithmes qui vont pouvoir s'appliquer sur le graphe.  
Par exemple : 
	* trouver si deux sommets sont connectés
	* trouver tous les sommets adjacents à un sommet donné

Dans ce projet, j'ai choisi de représenter les données en __Edge list__.

## Algorithme du plus court chemin
Plusieurs algorithmes existent pour trouver le plus court chemin entre 2 sommets dans un graphe : 
* Dijkstra
* Bellman-Ford
* Floyd-Warshall

Dans ce projet j'ai choisi d'implémenter l'algorithme de **Dijkstra**.
* [Video : mise en application pas à pas de l'algorithme de Dijkstra](https://www.youtube.com/watch?v=4gvV7X1vcws)
* [Finding The Shortest Path, With A Little Help From Dijkstra](https://medium.com/basecs/finding-the-shortest-path-with-a-little-help-from-dijkstra-613149fbdc8e)

# Décisions d'architecture
## Utilisation d'une architecture hexagonale (port/adapter)
### Ressources
* https://softwarecampament.wordpress.com/portsadapters/
* https://blog.xebia.fr/2016/03/16/perennisez-votre-metier-avec-larchitecture-hexagonale/
* https://www.freecodecamp.org/news/implementing-a-hexagonal-architecture/
* https://blog.octo.com/architecture-hexagonale-trois-principes-et-un-exemple-dimplementation/

### Description rapide
**Trois** grandes zones :
* _**Application**_ (Driver side)
* _**Domain**_
* _**Infrastructure**_ (Driven side)

Le code et les concepts métier sont isolés dans la zone _**Domain**_. La zone _**Domain**_ ne dépend de rien.
Il ne doit exister aucun concept technique dans dans la zone _**Domain**_.  
La zone _**Domain**_ contient des ports : 
* ports primaires : ports à brancher sur la zone _**Application**_
* ports secondaires : ports à brancher sur la zone _**Infrastructure**_
Le branchement aux ports se fait via des _Adapter_.

L'avantage est d'isoler dans le _**Domain**_ toute la valeur métier du programme.


## Fonctionnalité de validation du fichier d'entrée de la responsabilité de la zone _**Application**_
Pour moi, la responsabilité de _validation_ du fichier d'entrée est à faire par la zone _**Domain**_.  
C'est similaire par exemple au travail qui est fait par les validateurs dans un site web MVC.


## Fonctionnalité de recherche du plus court chemin de la responsabilité de la zone _**Infrastructure**_
Une partie du problème posé dans la consigne est assimilable à une recherche du plus court chemin pour aller d'un point A (devise source)
à un point B (devise cible) dans un graphe (liste de taux de change).  
Pour moi, toute la logique de recherche est purement technique et ne doit donc pas se trouver dans la zone _**Domain**_.  
C'est juste une fonctionnalite technique dont le _**Domain**_ a besoin. Cela semble logique de le placer dans la zone _**Infrastructure**_.  
On peut imaginer implémenter des algorithmes alternatifs pour trouver le plus court chemin  :
* Algorithme de Bellman-Ford
* Algorithme de Floyd-Warshall


# Tests
## Tests unitaires
Les projets de tests utilisent le framework de tests unitaires [MSTest](https://docs.microsoft.com/en-us/visualstudio/test/using-microsoft-visualstudio-testtools-unittesting-members-in-unit-tests?view=vs-2019).  
Le framework de simulacre est [Moq](https://github.com/moq/moq4).

# Inversion Of Control (IoC)
Dans ce projet, j'ai utilise l'IoC. J'utilise notamment ce principe pour rendre indépendante la zone _**Domain**_ de l'architecture hexagonale.  
L'IoC est l'un des 5 principes [SOLID](https://essential-dev-skills.com/principe-solid/).
* https://www.tutorialsteacher.com/ioc
* https://stackify.com/dependency-inversion-principle/

Pour faire l'injection de dépendance, j'ai utilisé le container IoC _Unity_.
* https://www.tutorialsteacher.com/ioc/unity-container

J'ai choisi d'injecter les dépendances par constructeur.
* https://www.tutorialsteacher.com/ioc/dependency-injection
* https://www.tutorialsteacher.com/ioc/constructor-injection-using-unity-container




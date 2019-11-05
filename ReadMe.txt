Consigne du projet
------------------
https://www.lucca.fr/societe/recrutement/test-technique-back-end


Décisions d'architectures
-----------------------
I. Utilisation d'une architecture hexagonale (port/adapter)
https://softwarecampament.wordpress.com/portsadapters/
https://blog.xebia.fr/2016/03/16/perennisez-votre-metier-avec-larchitecture-hexagonale/
https://www.freecodecamp.org/news/implementing-a-hexagonal-architecture/
https://blog.octo.com/architecture-hexagonale-trois-principes-et-un-exemple-dimplementation/

3 grandes zones :
- Application (Driver side)
- Domain
- Infrastructure (Driven side)

Le code et les concepts métier sont isolés dans la zone "Domain". La zone "Domain" ne dépend de rien.
Aucun concept technique dans la zone "Domain".

La zone "Domain" contient des ports : 
- ports primaires => ports à brancher avec la zone "Application"
- ports secondaires => ports à brancher sur la zone "Infrastructure"
Le branchement aux ports se fait via des "Adapter".

L'avantage est d'isoler dans le "Domain" toute la valeur métier du programme.


II. Decision de mettre le code de validation du fichier d'entrée dans la zone "Application"
Pour moi, la responsabilité de "validation" du fichier d'entrée est à faire par la zone "Domain".
C'est similaire par exemple au travail qui est fait par les validateurs dans un site web MVC.


III. Decision de mettre tout le code de recherche du plus court chemin dans la zone "Infrastructure"
Une partie du problème posé dans la consigne est assimilable à une recherche du plus court chemin pour aller d'un point A (devise source)
à un point B (devise cible) dans un graph (liste de taux de change)
Pour moi, toute la logique de recherche est purement technique et ne doit donc pas se trouver dans la zone "Domain".
C'est juste une fonctionnalité technique dont le "Domain" a besoin. Cela semble logique de le placer dans la zone "Infrastructure".
On peut imaginer qu'il peut y avoir plusieurs techniques pour récupérer le plus court chemin.


Tests
-----
les projets de tests utilisent le framework 'MSTest'.
Le framework de simulacre est Moq.

IoC
-----
Dans ce projet, j'ai utilisé l'IoC. C'est obligatoire lorsque qu'on fait une architecture hexagonale.
L'IoC est notamment utilisée pour rendre indpendante la zone "Domain".
https://www.tutorialsteacher.com/ioc

Pour faire l'njection de dépendance, j'ai utilisé le container IoC Unity.
https://www.tutorialsteacher.com/ioc/unity-container
J'ai choisi d'injecter les dépendances par constructeur.
https://www.tutorialsteacher.com/ioc/dependency-injection
https://www.tutorialsteacher.com/ioc/constructor-injection-using-unity-container


Cas d'utilisation
-----------------
Le projet gère les cas d'utilisation suivants : 

1. Cas standards
1.a. l'utilisateur utilise le programme avec un fichier d'entrée cohérent.
		De plus la conversion demandée est possible (existence d'au moins un lien de proche en proche entre la devise source et la devise cible).

2. Cas anormaux
2.a. L'utilisateur utilise le programme sans argument.
2.b. L'utilisateur utilise un fichier d'entrée non cohérent (cohérence du format ou cohérence logique).
2.c. L'utilisateur utilise un fichier d'entrée cohérent, cependant la conversion ne peut pas être effectuée car il n'y a pas de lien possible entre 
	la devise source et la devise cible.


Ressource concernant la théorie des graphs
------------------------------------------
Une partie du problème posé dans la consigne est assimilable à une recherche du plus court chemin pour aller d'un point A (devise source)
à un point B (devise cible) dans un graph (liste de taux de change).

1. Generalités sur les graphs
https://www.youtube.com/watch?v=gXgEDyodOJU&list=PL2_aWCzGMAwI3W_JlcBbtYTwiQSsOTa6P&index=39&t=0s
https://www.youtube.com/watch?v=AfYqN3fGapc&list=PL2_aWCzGMAwI3W_JlcBbtYTwiQSsOTa6P&index=39
https://www.youtube.com/watch?v=ZdY1Fp9dKzs&list=PL2_aWCzGMAwI3W_JlcBbtYTwiQSsOTa6P&index=40

2. Algorithme du plus court chemin
https://www.youtube.com/watch?v=4gvV7X1vcws


Controle de code source
-----------------------
J'utilise Git et mon projet est heberge dans un repository prive sur mon espace GitHub.
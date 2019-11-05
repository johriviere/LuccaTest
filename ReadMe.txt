Consigne du projet
------------------
https://www.lucca.fr/societe/recrutement/test-technique-back-end


D�cisions d'architectures
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

Le code et les concepts m�tier sont isol�s dans la zone "Domain". La zone "Domain" ne d�pend de rien.
Aucun concept technique dans la zone "Domain".

La zone "Domain" contient des ports : 
- ports primaires => ports � brancher avec la zone "Application"
- ports secondaires => ports � brancher sur la zone "Infrastructure"
Le branchement aux ports se fait via des "Adapter".

L'avantage est d'isoler dans le "Domain" toute la valeur m�tier du programme.


II. Decision de mettre le code de validation du fichier d'entr�e dans la zone "Application"
Pour moi, la responsabilit� de "validation" du fichier d'entr�e est � faire par la zone "Domain".
C'est similaire par exemple au travail qui est fait par les validateurs dans un site web MVC.


III. Decision de mettre tout le code de recherche du plus court chemin dans la zone "Infrastructure"
Une partie du probl�me pos� dans la consigne est assimilable � une recherche du plus court chemin pour aller d'un point A (devise source)
� un point B (devise cible) dans un graph (liste de taux de change)
Pour moi, toute la logique de recherche est purement technique et ne doit donc pas se trouver dans la zone "Domain".
C'est juste une fonctionnalit� technique dont le "Domain" a besoin. Cela semble logique de le placer dans la zone "Infrastructure".
On peut imaginer qu'il peut y avoir plusieurs techniques pour r�cup�rer le plus court chemin.


Tests
-----
les projets de tests utilisent le framework 'MSTest'.
Le framework de simulacre est Moq.

IoC
-----
Dans ce projet, j'ai utilis� l'IoC. C'est obligatoire lorsque qu'on fait une architecture hexagonale.
L'IoC est notamment utilis�e pour rendre indpendante la zone "Domain".
https://www.tutorialsteacher.com/ioc

Pour faire l'njection de d�pendance, j'ai utilis� le container IoC Unity.
https://www.tutorialsteacher.com/ioc/unity-container
J'ai choisi d'injecter les d�pendances par constructeur.
https://www.tutorialsteacher.com/ioc/dependency-injection
https://www.tutorialsteacher.com/ioc/constructor-injection-using-unity-container


Cas d'utilisation
-----------------
Le projet g�re les cas d'utilisation suivants : 

1. Cas standards
1.a. l'utilisateur utilise le programme avec un fichier d'entr�e coh�rent.
		De plus la conversion demand�e est possible (existence d'au moins un lien de proche en proche entre la devise source et la devise cible).

2. Cas anormaux
2.a. L'utilisateur utilise le programme sans argument.
2.b. L'utilisateur utilise un fichier d'entr�e non coh�rent (coh�rence du format ou coh�rence logique).
2.c. L'utilisateur utilise un fichier d'entr�e coh�rent, cependant la conversion ne peut pas �tre effectu�e car il n'y a pas de lien possible entre 
	la devise source et la devise cible.


Ressource concernant la th�orie des graphs
------------------------------------------
Une partie du probl�me pos� dans la consigne est assimilable � une recherche du plus court chemin pour aller d'un point A (devise source)
� un point B (devise cible) dans un graph (liste de taux de change).

1. Generalit�s sur les graphs
https://www.youtube.com/watch?v=gXgEDyodOJU&list=PL2_aWCzGMAwI3W_JlcBbtYTwiQSsOTa6P&index=39&t=0s
https://www.youtube.com/watch?v=AfYqN3fGapc&list=PL2_aWCzGMAwI3W_JlcBbtYTwiQSsOTa6P&index=39
https://www.youtube.com/watch?v=ZdY1Fp9dKzs&list=PL2_aWCzGMAwI3W_JlcBbtYTwiQSsOTa6P&index=40

2. Algorithme du plus court chemin
https://www.youtube.com/watch?v=4gvV7X1vcws


Controle de code source
-----------------------
J'utilise Git et mon projet est heberge dans un repository prive sur mon espace GitHub.
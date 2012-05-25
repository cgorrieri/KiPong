#Description générale 

KiPong est un jeu de pong classique dans lequel l'utilisateur interagit avec le jeu de manière intuitive en contrôlant la raquette au moyen de sa main droite.

#Description détaillée 

##Commencer le jeu 
Dans un premier menu, l'utilisateur à la possibilité de choisir de jouer au jeu avec le clavier ou grace à la Kinect.

##But du jeu 
Le jeu de Pong est le plus vieux et le premier inventé sur les ordinateurs.
Le but est de manipuler une balle à l'aide d'une raquette.
Cette balle rebondit dans tout l'ecran, mais les parties gauche et droites ne sont pas "emmurées", de ce fait des raquette sont disposées pour empecher la balle de passer.
Les raquettes se déplacent de haut en bas uniquement.

##Les modes de jeu 
L'utilisateur à le choix entre deux modes de jeux :
  - Jouer contre l'intelligence artificiel.
  - Jouer contre un joueur physique.

###Jouer contre l'ordinateur 
Dans ce cas, un seul joueur est nécessaire pour faire fonctionner le jeu, c'est la requette de gauche qui est alors manipulée.

###Jouer contre un autre joueur 
Pour ce mode, deux joueurs sont nécessaires.
Attention, ce n'est pas forcement le joueur de droite qui controle la raquette de droite. En fait c'est le premier joueur arrivé qui controle la palette de gauche, et le second controle celle de droite. 

##Demander de l'aide 
Afin de guider l'utilisateur au sein de l'application, il à la possibilité de consulter l'aide à tout moment, en levant simplement la main gauche comme pour demander de l'aide dans une salle de cours.

##Gangner le jeu 
il n'est pas vraiment possible pour l'utilisateur de gagner le jeu, les parties se font l'un contre l'autre, dans ce cas, un des deux gagnent.
Chacun des deux dispose de 6 points, il pert alors un point lorsque la balle passe dans son camp et n'est pas recupere par la raquette.
Un fois qu'un des deux joueurs se trouve à 6 points, alors il gagne la partie et peut alors en recommencer une nouvelle.

##Mettre le jeu en pause 
Pour mettre le jeu en pause, l'utilisateur doit effectuer un swip (deplacement de la main) horizontalement vers la gauche comme pour faire un retour arriere.

##Difficultées 

Pour augmenter la difficulté, l’intelligence artificiel devient pratiquement imbattable, la balle se déplace plus vite et la taille des raquette est diminuée.


#Utilisation de la Kinect 

##Navigation dans les menus 
Pour naviguer dans les menus, il faut utiliser la Kinect car il faudrait alors faire deplacer constamment l'utilisateur entre l'ordinateur et l'ere de jeu.

Pour naviguer dans les menus, l'utilisateur utilise sa main droite et se deplace de bas en haut.
Pour selectionner une option, il effectue un swip (deplacement de la main horizontalement de gauche à droite)
Pour retourner au menu il effectue un swip de la main droite vers la gauche

##Probleme de fonctionnement 
De nombreux éléments sont necessaires au fonctionnement de la Kinect. 
Dans un primier temps il faut installer les drivers de connexion au PC de la Kinect.
Il faut aussi installer le Kinect SDK v1.
Pour le fonctionnement du jeu, de nombreux autres outils sont necessaires.

###La Kinect de clignote pas 
Assurez vous que la prise secteur est branché et que la Kinect à bien été reconnue par l'ordinateur.

  La Kinect est branché mais le jeu ne réagit pas 
Dans ce cas, il faut arreter le jeu, debrancher puis rebrancher la Kinect. Relancer le jeu pour verifier la configuration.

###Les mouvements de la raquette sont tremblants 
Cela peut etre dut au fait que l'utilisateur ne se trouve pas en face de la Kinect, il suffit alors de la deplacer.
Attention, la distance minimal est d'au moins 2 metres, la distance maximale est de 4 metres.

###Les bugs connus 
Les personnes de petite taille ne sont pas facilement identifiées par la Kinect. Il peut alors être necessaire de repositionner la Kinect.

Pour une reconnaissance optimal, l'utilisateur ne doit pas être vetu de veste ou tout élément susceptible de modifier sa silouette.
Par exemple les vestes ouvertes qui augmentent la taille du bassin lors de l’écartement des bras.
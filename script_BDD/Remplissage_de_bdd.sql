
USE [Ecommerce]
GO

INSERT INTO [dbo].[Categorie]
           ([nom_categorie]
           ,[image]
           ,[description])
     VALUES
           ('Mobile','/Content/Image/Categorie/mobile.jpeg','Choisissez votre mobile parmi notre collection') , 
		   ('Ordinateur Portable', '/Content/Image/Categorie/portable.jpeg','Les ordinateurs portables derniers prix'  ),
		   ('PC Fixe','/Content/Image/Categorie/pcfixe.jpeg', 'Trouvez le pc de vos rêves' ),
		   ('Appareil Photo', '/Content/Image/Categorie/reflex.jpeg', 'Vos objectifs au meilleur prix') ;

INSERT INTO [dbo].[Methode_de_livraison]
           ([nom]
           ,[description]
           ,[prix])
     VALUES
           ('La poste'
           ,'Délai de livraison 10 jours'
           ,'5 €'),
		   ('Chronopost'
           ,'Délai de livraison 5 jours'
           ,'10 €'),
		   ('Mondial relay'
           ,'Délai de livraison 2 jours'
           ,'8 €'),
		   ('UPS'
           ,'Délai de livraison 3 jours'
           ,'7 €');

INSERT INTO [dbo].[Produit]
           ([nom_produit]
           ,[id_categorie]
           ,[prix]
           ,[image]
           ,[description]
           ,[stock]
           ,[quantite_lots]
           ,[niveau_reapprovisionnement])
     VALUES
   	('HTC One M8 Android','1','245','/Content/Image/Produits/tel1.jpeg','Smartphone quadri-bande 4G, Android, HTC Sense avec HTC BlinkFeed, fonction GPS, processeur 2,3 GHz, écran Full HD 5", appareil photo Duo Camera, Bluetooth 4.0 avec aptX, Wi-Fi (IEEE 802.11a/b/g/n/ac), vidéo Full HD, lecteur MP3, radio FM - Mémoire interne 16 Go, emplacement pour carte MicroSD', '20', '20','20'),
   	('HTC One Mini Blue','1','100','/Content/Image/Produits/tel2.jpeg','HTC ONE MINI BLUE - HTC mini, One. Taille : 109.2 mm (4.3 ), Résolution : 1280 x 720 pixels, Technologie tactile: Multi-touch. Vitesse du processeur: 1.4 G', '20', '20','20'),
	('Nokia Lumia 1020','1','345','/Content/Image/Produits/tel3.jpeg','Suite à la belle démonstration de sa techno photo maison sur le 808 Pureview, Nokia ne pouvait en rester là. Le fabricant avait prévenu, sa technologie serait un jour portée sur un Windows Phone. C est désormais chose faite avec le Lumia 1020, qui prend la suite du très réussi Lumia 925. Commercialisé dès ce mois-ci, le Lumia 1020 est donc le premier Windows Phone à intégrer le capteur stabilisé Pureview de 41 Mpixels, censé délivrer d excellentes photos dans n importe quelle condition lumineuse.', '20', '20','20'),
	('HP Titan Gamer 1800EV','3','1389','/Content/Image/Produits/ordi1.jpeg','Ne vous contentez plus d être un simple gamer, devenez une légende du jeu en disposant de toute la puissance nécessaire pour battre la concurrence. Cet ordinateur portable OMEN allie un design de pointe à une technologie de pointe, c est un véritable monstre de performances, prêt à lancer les plus beaux AAA en toute simplicité et avec style.', '20', '20','20'),
	('Digital Storm VANQUISH','3','1259','/Content/Image/Produits/ordi2.jpeg','Si l’assemblage d’un PC relève du mysticisme pour beaucoup d’utilisateurs, il existe pourtant des outils pratiques permettant de sélectionner un ensemble de composants compatibles. Digital Storm propose désormais une solution intermédiaire spécifiquement dédiée aux joueurs. ', '20', '20','20'),
	('Lenovo IdeaCentre 600','3','500','/Content/Image/Produits/ordi3.jpeg','Le premier PC tout-en-un de Lenovo, l IdeaCentre A600, est architecturé autour d un écran Full HD de 21,5 pouces, d un lecteur Blu-ray et d un système audio 2.1.', '20', '20','20'),
	('Nikon D5500 DSLR ','4','670','/Content/Image/Produits/app1.jpeg','À mi-chemin entre les D3000 pour novices et les D7000 pour utilisateurs experts, les Nikon D5000 ont toujours été des APN très intéressants pour qui désirait se lancer dans l aventure photographique au reflex au meilleur rapport qualité/prix. ', '20', '20','20'),
	('Apple iCam ','4','1300','/Content/Image/Produits/app3.jpeg','Intéressant nouveau concept signé ADR Studio, voici l’Apple iCam. Véritable boitier photo avec objectif interchangeable, cet accessoire est conçu pour permettre à un iPhone 5 de venir s’y loger par l’arrière.', '20', '20','20'),
	('Leica T Mirrorless','4','530','/Content/Image/Produits/app2.jpeg','ors de l abandon du système R (reflex) en 2008, les aficionados de Leica et les observateurs ont redouté que la marque sombre dans la monoculture M (télémétrique). Heureusement, il n en a rien été et l Allemand a su se diversifier. Après les reflex numériques moyen format (système S) puis les hybrides à capteur APS-C et objectif fixe (système X), Leica investit enfin le marché des hybrides à objectifs interchangeables et capteurs APS-C à travers le système T.', '20', '20','20'),
	('Lenovo Thinkpad Carbon','2','245','/Content/Image/Produits/laptop1.jpeg','Ultrafin. Ultraléger. Ultrarobuste. Pour l Ultrabook™ moyen, ces attributs peuvent sembler contradictoires… Mais le nouveau X1 Carbon est loin d être dans la moyenne ! Il est équipé d un boîtier renforcé en fibre de carbone et franchit haut la main les tests de robustesse les plus rigoureux. De plus, il offre une autonomie supérieure à une journée, un stockage plus performant et des options de station d accueil innovantes (notamment sans fil).', '20', '20','20'),
	('Samsung Series NP900X4C','2','245','/Content/Image/Produits/laptop2.jpeg','Reprenant le magnifique châssis de la version 13,3 pouces (33,8 cm), le Samsung Série 9 au format 15 pouces (38,1 cm) est une bonne machine de travail. Un PC portable aussi beau qu un MacBook Air mais dont l écran souffre de taux de contrastes vraiment médiocres.', '20', '20','20'),
	('Asus N551JK-XO076H','2','245','/Content/Image/Produits/laptop3.jpeg','L’Asus N551JK-XO076H est un nouvel ordinateur portable polyvalent de 15.6 pouces qui s’affiche à moins de 800 euros sous plateforme Intel Shark Bay et Windows 8.1. Il embarque un processeur Haswell Core i7 Quad Core, une carte graphique dédiée NVIDIA Maxwell couplée à la technologie Optimus, un disque dur de capacité importante, une connectique USB 3.0 ou encore un caisson de basses externe.', '20', '20','20'),
	('HP Spectre Pro','2','245','/Content/Image/Produits/laptop4.jpeg','Le Spectre XT est le second ultrabook HP de 13,3 pouces. Avec sont look plus séduisant que celui du Folio 13, sera-t-il capable de rejoindre les UX31A d Asus et Samsung Serie 9 ?', '20', '20','20');
GO

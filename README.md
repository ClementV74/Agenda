

Projet Agenda 🗓️

Bienvenue dans notre projet d'agenda ultra pratique ! Ce projet est développé en C# avec WPF et .NET 7.0, utilisant Entity Framework pour la gestion de la base de données. Voici un aperçu rapide des fonctionnalités :

🗓️ Calendrier Google
Dans cet onglet, vous pouvez accéder à votre calendrier Google directement depuis notre application. Suivez vos événements, rendez-vous et anniversaires sans quitter notre interface.

🗓️ **Calendrier Google**

Dans cet onglet, vous pouvez accéder à votre calendrier Google directement depuis notre application. Suivez vos événements, rendez-vous et anniversaires sans quitter notre interface.





📇 Onglet Contacts
Gérez tous vos contacts à partir de cette section. Ajoutez, supprimez et modifiez facilement vos contacts à l'aide d'une data grid intuitive.


<img width="592" alt="calendar" src="https://github.com/ClementVABRE/calendrier2/assets/45317801/0664b95b-c6c7-461f-aed1-70752e54028a">


📋 Liste de Tâches
Organisez-vous avec notre liste de tâches ! Ne manquez plus jamais une échéance importante. Ajoutez des tâches, cochez-les lorsque vous les avez accomplies et restez sur la bonne voie.

🔗 Base de Données
Toutes vos données sont stockées en toute sécurité dans notre base de données. Notre système garantit la sauvegarde et la synchronisation efficaces de vos informations, vous permettant d'accéder à votre agenda de n'importe où, à tout moment.

🚀 Comment démarrer ?
Clonez ce dépôt sur votre machine locale.

Assurez-vous d'avoir les dépendances nécessaires installées.

Configurez les paramètres de connexion à Google Calendar dans l'onglet approprié.

Utilisez la commande suivante pour générer les classes de modèle à partir de la base de données :

Scaffold-DbContext "server=localhost;port=3306;user=root;password=;database=contact" Pomelo.EntityFrameworkCore.MySql -OutputDir contact_DB -f
Lancez l'application et profitez-en pour rester organisé !

🛠️ Technologies Utilisées
C# avec WPF et .NET 7.0 pour l'interface utilisateur
Entity Framework pour la gestion de la base de données
Google Calendar API pour l'intégration du calendrier
📝 Contribution
Les contributions sont les bienvenues ! N'hésitez pas à soumettre des problèmes, des demandes de fonctionnalités ou même des pull requests pour améliorer ce projet d'agenda.

